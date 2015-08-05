using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using PlanesGame.GameCore;
using PlanesGame.GameCore.PlayerFactory;
using PlanesGame.GameGraphics;
using PlanesGame.Models;
using PlanesGame.Models.Plane;
using PlanesGame.Models.Player;
using PlanesGame.Network;
using PlanesGame.Network.Factory;
using PlanesGame.Network.NetworkCore;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class GameBoardController
    {
        private readonly string _oponentType;
        private readonly IGameBoardView _view;
        private bool _connected;
        private MatrixCoordinate _currentAttackPoint;
        private bool _gameCanStart;
        private bool _gameInProgress;
        private IPlayer _localPlayer;
        private Network.NetworkCore.Network _network;
        private IPlayer _oponent;
        private IEngine _oponentPanelEngine;
        private string _planeOrientation;
        private Plane _planeTemplate;
        private PlayerConnectionInfo _playerConnectionInfo;
        private IEngine _playerPanelEngine;

        public GameBoardController(IGameBoardView view)
        {
            _planeTemplate = new Plane();
            _view = view;
            Common.GameBoardController = this;
            _oponentType = "network";
            _planeOrientation = "up";
        }

        public void StartNewGame()
        {
            ResetGameAssests();
            if (_connected && _network.ConnectionType == ConnectionType.Server)
            {
                SetKillRules();
                SendGameInfo();
                _network.SendData(DataType.RestartGame);
                _localPlayer.CanSetup = true;
            }
        }

        public void RestartGame()
        {
            StartNewGame();
            _localPlayer.CanSetup = true;
        }

        private void ResetGameAssests()
        {
            if (!_connected) return;

            var name = _localPlayer.Name;
            var playerFactory = new ConcretePlayerFactory();

            _localPlayer = new Player {Name = name};
            name = _oponent.Name;
            _oponent = playerFactory.CreatePlayer(_oponentType);
            _oponent.Name = name;


            _playerPanelEngine.ResetTiles();
            _oponentPanelEngine.ResetTiles();

            _view.SetPlaneOrientationVisibile(true);
            _view.SetGameStatus("Setting up Planes, You must set 4!");
        }

        public void Redraw()
        {
            if (_playerPanelEngine != null && _oponentPanelEngine != null)
            {
                _playerPanelEngine.Draw();
                _oponentPanelEngine.Draw();
            }
        }

        public void ConnectToGame()
        {
            InitiateNetwork("client");
        }

        public void StartServer()
        {
            InitiateNetwork("server");
        }

        private void InitiateNetwork(string type)
        {
            var networkFactory = new UserNetworkFactory();
            var playerFactory = new ConcretePlayerFactory();
            _view.SetGameStatus("Awaiting Game Start");
            _localPlayer = playerFactory.CreatePlayer("network");
            _oponent = playerFactory.CreatePlayer("network");
            _network = networkFactory.CreateNetwork(type);
            SetPlayerRelatedData();
            _network.StartService();
        }

        internal void ConnectionEstablished()
        {
            _network.SendData(DataType.SetUp, "name: " + _localPlayer.Name);
            _view.SetConnectionStatus("Status: Connected!");
            _gameInProgress = true;
            _connected = true;
            StartNewGame();
        }

        private void SendGameInfo()
        {
            if (_network != null && _network.ConnectionType == ConnectionType.Server)
            {
                var killpoints = "killpoints:";
                _planeTemplate.KillPoints.ForEach(killpoint => killpoints += killpoint.ToString() + " ");
                _network.SendData(DataType.SetUp, killpoints);
            }
        }

        public void SetUpData(string data)
        {
            if (data.Contains("killpoints:"))
            {
                _planeTemplate.KillPoints.Clear();
                data = data.Remove(0, "killpoints:".Count() + 1);
                var points = data.Split(' ');
                foreach (var matrixCoordinate in from point in points
                    where point != ""
                    select new MatrixCoordinate(int.Parse(point[0].ToString()),
                        int.Parse(point[1].ToString())))
                {
                    _planeTemplate.KillPoints.Add(matrixCoordinate);
                    _planeTemplate.PlaneMatrix[matrixCoordinate.Row, matrixCoordinate.Column] = 2;
                }
            }

            if (!data.Contains("name: ")) return;

            data = data.Remove(0, "name: ".Count());
            _oponent.Name = data;
            _view.SetOponentName("Oponent: " + data);
        }

        public void Disconnect()
        {
            try
            {
                if (_network == null) return;
                if (!_network.IsConnected()) return;
                _network.SendData(DataType.Disconnect);
                _gameInProgress = false;
                _network.StopService();
                _view.SetConnectionStatus("Status: Disconnected!");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void SetOponent(string type)
        {
            var playerFactory = new ConcretePlayerFactory();
            _oponent = playerFactory.CreatePlayer(type);
        }

        public void SetKillRules()
        {
            if (_network == null) return;
            if (_network.ConnectionType != ConnectionType.Server) return;

            var killRulesView = new KillRulesView();
            var killRulesController = new KillRulesController(killRulesView, _planeTemplate);
            if (killRulesView.ShowDialog() != DialogResult.OK) return;
            _planeTemplate = killRulesController.Plane;
        }

        public void SetPlayerRelatedData()
        {
            var playerConnectionView = new PlayerConnectionView();
            var playerConnectionController = _network.ConnectionType == ConnectionType.Server
                ? new PlayerConnectionController(playerConnectionView, false)
                : new PlayerConnectionController(playerConnectionView, true);
            playerConnectionView.SetController(playerConnectionController);
            if (playerConnectionView.ShowDialog() == DialogResult.OK)
            {
                _playerConnectionInfo = playerConnectionController.PlayerConnectionInfo;
                Common.IpEndPoint = _playerConnectionInfo.RemoteAddress != null
                    ? new IPEndPoint(_playerConnectionInfo.RemoteAddress, 2000)
                    : new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000);
            }
            _localPlayer.Name = playerConnectionController.PlayerConnectionInfo.Name;
        }

        public void StartGame()
        {
            if (_localPlayer.CanSetup == false)
            {
                _localPlayer.CanAttack = CanAttack();
            }
            else
            {
                _gameCanStart = true;
            }
        }

        public void SetUp(Point location)
        {
            if (_gameInProgress == false) return;
            if (_localPlayer.CanSetup == false) return;
            var tileLocation = _playerPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;

            var plane = GetPlaneRotation(tileLocation);

            plane.PlaneStartPosition = tileLocation;

            if (_localPlayer.PlanesAlive != 4) return;
            if (_gameCanStart)
            {
                _localPlayer.CanAttack = CanAttack();
            }
            _localPlayer.CanSetup = false;
            _view.SetPlaneOrientationVisibile(false);
            _network.SendData(DataType.StartGame);
            _view.SetGameStatus(_network.ConnectionType == ConnectionType.Server
                ? "You can attack!"
                : "You can not attack!");
        }

        private bool CanAttack()
        {
            return _network.ConnectionType == ConnectionType.Server;
        }

        private Plane GetPlaneRotation(MatrixCoordinate tileLocation)
        {
            var plane = new Plane
            {
                PlaneMatrix = _planeTemplate.PlaneMatrix,
                KillPoints = _planeTemplate.KillPoints.ToList()
            };
            var planeRotator = new PlaneRotator();
            switch (_planeOrientation)
            {
                case "up":
                    if (tileLocation.Row <= 6 && tileLocation.Column >= 1 && tileLocation.Column <= 8)
                    {
                        tileLocation.Column -= 1;
                        BuildPlane(tileLocation, plane);
                    }
                    break;
                case "down":
                    if (tileLocation.Row >= 3 && tileLocation.Column >= 1 && tileLocation.Column <= 8)
                    {
                        tileLocation.Column -= 1;
                        tileLocation.Row -= 3;
                        planeRotator.SetPlaneDown(plane);
                        BuildPlane(tileLocation, plane);
                    }
                    break;
                case "right":
                    if (tileLocation.Column >= 3 && tileLocation.Row >= 1 && tileLocation.Row <= 8)
                    {
                        tileLocation.Column -= 3;
                        tileLocation.Row -= 1;
                        planeRotator.SetPlaneRight(plane);
                        BuildPlane(tileLocation, plane);
                    }
                    break;
                case "left":
                    if (tileLocation.Column <= 6 && tileLocation.Row >= 1 && tileLocation.Row <= 8)
                    {
                        tileLocation.Row -= 1;
                        planeRotator.SetPlaneLeft(plane);
                        BuildPlane(tileLocation, plane);
                    }
                    break;
            }
            return plane;
        }

        private void BuildPlane(MatrixCoordinate matrixCoordinate, Plane plane)
        {
            if (CheckPlaneDuplicates(matrixCoordinate, plane)) return;
            _localPlayer.PlanesAlive++;
            _localPlayer.PlanesList.Add(plane);
            var row = 0;
            for (var i = matrixCoordinate.Row; i < matrixCoordinate.Row + plane.NumberOfRows; i++, row++)
            {
                var column = 0;
                for (var j = matrixCoordinate.Column;
                    j < matrixCoordinate.Column + plane.NumberOfColumns;
                    j++, column++)
                {
                    if (plane.PlaneMatrix[row, column] == 0) continue;
                    _playerPanelEngine.UpdateTile(i, j, Color.Blue);
                    _localPlayer.PlaneMatrix[i, j] = plane.PlaneMatrix[row, column];
                }
            }
        }

        private bool CheckPlaneDuplicates(MatrixCoordinate matrixCoordinate, Plane plane)
        {
            var row = 0;
            for (var i = matrixCoordinate.Row; i < matrixCoordinate.Row + plane.NumberOfRows; i++, row++)
            {
                var column = 0;
                for (var j = matrixCoordinate.Column;
                    j < matrixCoordinate.Column + plane.NumberOfColumns;
                    j++, column++)
                {
                    if (_localPlayer.PlaneMatrix[i, j] == 0 ||
                        plane.PlaneMatrix[row, column] != 1 && plane.PlaneMatrix[row, column] != 2) continue;
                    MessageBox.Show(@"Planes intersecting, retry!");
                    return true;
                }
            }
            return false;
        }

        public void ExecuteAttack(Point location)
        {
            if (_gameInProgress == false) return;
            if (_localPlayer.CanAttack == false) return;
            var tileLocation = _oponentPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;
            if (_localPlayer.OponentPlaneMatrix[tileLocation.Row, tileLocation.Column] != 0)
            {
                MessageBox.Show(@"Already attacked here!");
                return;
            }
            _localPlayer.CanAttack = false;
            _view.SetGameStatus("You can not attack!");
            _network.SendData(DataType.Attack, tileLocation.ToString());
        }

        public void SetEnginePlayer(Graphics graphicsObject, Rectangle clientRectangle)
        {
            _playerPanelEngine = new Engine(graphicsObject, clientRectangle);
            _playerPanelEngine.Draw();
        }

        public void SetEngineOponent(Graphics graphicsObject, Rectangle clientRectangle)
        {
            _oponentPanelEngine = new Engine(graphicsObject, clientRectangle);
            _oponentPanelEngine.Draw();
        }

        public void SetPlaneOrientation(string data)
        {
            _planeOrientation = data.ToLower();
        }

        public void AddMessage(string data)
        {
            _view.AddMessage(_oponent.Name + " >> " + data + Environment.NewLine);
        }

        public void SendMessage(string messageBoxInputText)
        {
            if (!messageBoxInputText.Equals("") && _gameInProgress)
            {
                _network.SendData(DataType.Message, messageBoxInputText);
                _view.AddMessage("You >> " + messageBoxInputText + Environment.NewLine);
            }
        }

        public void Attacked(string data)
        {
            _localPlayer.CanAttack = true;
            _view.SetGameStatus("You can attack!");
            _currentAttackPoint = new MatrixCoordinate(int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));
            if (_localPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] == 0)
            {
                _network.SendData(DataType.AttackResponse, "m" + _currentAttackPoint);
                _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Cyan);
                _localPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] = 4; // 4 - plane miss
                return;
            }
            _localPlayer.PlanesList.ForEach(plane =>
            {
                if (PlaneContains(_currentAttackPoint, plane))
                {
                    var killPointToFind = GetKillPoint(plane);
                    if (killPointToFind != null)
                    {
                        _network.SendData(DataType.AttackResponse, "h" + _currentAttackPoint);
                        _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Red);
                        plane.KillPoints.Remove(killPointToFind);
                        if (plane.KillPoints.Count == 0)
                        {
                            _localPlayer.PlanesAlive--;
                            _network.SendData(DataType.AttackResponse,
                                "d" + plane.Orientation[0] + plane.PlaneStartPosition);
                            BuildDestroyedPlane(plane, true);
                        }
                    }
                    else
                    {
                        _network.SendData(DataType.AttackResponse, "h" + _currentAttackPoint);
                        _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Red);
                    }
                    _localPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] = 3; // 3 - hit plane
                }
            });
            CheckGameEnd();
        }

        private MatrixCoordinate GetKillPoint(Plane plane)
        {
            return plane.KillPoints.Find(
                killpoint =>
                    killpoint.Row == _currentAttackPoint.Row - plane.PlaneStartPosition.Row &&
                    killpoint.Column == _currentAttackPoint.Column - plane.PlaneStartPosition.Column);
        }

        private static bool PlaneContains(MatrixCoordinate hitpoint, Plane plane)
        {
            return hitpoint.Row >= plane.PlaneStartPosition.Row &&
                   hitpoint.Row <= plane.PlaneStartPosition.Row + plane.NumberOfRows &&
                   hitpoint.Column >= plane.PlaneStartPosition.Column &&
                   hitpoint.Column <= plane.PlaneStartPosition.Column + plane.NumberOfColumns;
        }

        public void AttackResponse(string data)
        {
            if (data[1] == 'h')
            {
                var matrixCoordonate = new MatrixCoordinate(int.Parse(data[2].ToString()), int.Parse(data[3].ToString()));
                _oponentPanelEngine.UpdateTile(matrixCoordonate.Row, matrixCoordonate.Column, Color.Red);
                _localPlayer.OponentPlaneMatrix[matrixCoordonate.Row, matrixCoordonate.Column] = 3; // 3 - hit
            }
            if (data[1] == 'm')
            {
                var matrixCoordonate = new MatrixCoordinate(int.Parse(data[2].ToString()), int.Parse(data[3].ToString()));
                _oponentPanelEngine.UpdateTile(matrixCoordonate.Row, matrixCoordonate.Column, Color.Cyan);
                _localPlayer.OponentPlaneMatrix[matrixCoordonate.Row, matrixCoordonate.Column] = 4; // 3 - miss
            }
            if (data[1] == 'd')
            {
                _oponent.PlanesAlive--;
                var matrixCoordonate = new MatrixCoordinate(int.Parse(data[3].ToString()), int.Parse(data[4].ToString()));
                if (data[2] == 'u')
                    BuildOponentPlane(matrixCoordonate, "up");
                if (data[2] == 'd')
                    BuildOponentPlane(matrixCoordonate, "down");
                if (data[2] == 'l')
                    BuildOponentPlane(matrixCoordonate, "left");
                if (data[2] == 'r')
                    BuildOponentPlane(matrixCoordonate, "right");
            }
            CheckGameEnd();
        }

        private void CheckGameEnd()
        {
            if (_localPlayer.PlanesAlive == 0)
            {
                MessageBox.Show(@"You Lost!");
                _view.SetGameStatus("You Lost!");
                _network.SendData(DataType.Won);
                _localPlayer.CanAttack = false;
            }
        }

        public void GameWon()
        {
            _localPlayer.CanAttack = false;
            MessageBox.Show(@"You Won!");
            _view.SetGameStatus("You Won!");
        }

        private void BuildOponentPlane(MatrixCoordinate startCoordinate, string direction)
        {
            var plane = new Plane();
            var planeRotator = new PlaneRotator();
            switch (direction)
            {
                case "up":

                    break;
                case "down":
                    planeRotator.SetPlaneDown(plane);
                    break;
                case "right":
                    planeRotator.SetPlaneRight(plane);
                    break;
                case "left":
                    planeRotator.SetPlaneLeft(plane);
                    break;
            }
            plane.PlaneStartPosition = startCoordinate;
            BuildDestroyedPlane(plane, false);
        }

        private void BuildDestroyedPlane(Plane plane, bool friendlyPlane)
        {
            var row = 0;
            for (var i = plane.PlaneStartPosition.Row;
                i < plane.PlaneStartPosition.Row + plane.NumberOfRows;
                i++, row++)
            {
                var column = 0;
                for (var j = plane.PlaneStartPosition.Column;
                    j < plane.PlaneStartPosition.Column + plane.NumberOfColumns;
                    j++, column++)
                {
                    if (plane.PlaneMatrix[row, column] != 0)
                    {
                        if (friendlyPlane)
                        {
                            _playerPanelEngine.UpdateTile(i, j, Color.Red);
                        }
                        else
                        {
                            _oponentPanelEngine.UpdateTile(i, j, Color.Red);
                        }
                        _localPlayer.PlaneMatrix[i, j] = 3;
                    }
                }
            }
        }
    }
}