using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using PlanesGame.GameCore;
using PlanesGame.GameGraphics;
using PlanesGame.Models;
using PlanesGame.Models.Plane;
using PlanesGame.Models.Player;
using PlanesGame.Models.PlayerFactory;
using PlanesGame.Network;
using PlanesGame.Network.Factory;
using PlanesGame.Network.NetworkCore;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class GameBoardController
    {
        private IGameBoardView _view;
        private Network.NetworkCore.Network _network;
        private IEngine _playerPanelEngine;
        private IEngine _oponentPanelEngine;
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        private PlayerConnectionInfo _playerConnectionInfo;
        private bool _gameInProgress = false;
        private bool _connected = false;
        private string _planeOrientation;
        private readonly GameArbiter _gameArbiter;
        private Plane _planeTemplate;
        private MatrixCoordinate _currentAttackPoint;
        private string _oponentType;

        public GameBoardController(IGameBoardView view)
        {
            _view = view;
            Common.GameBoardController = this;
            _gameArbiter = new GameArbiter();
            Common.GameArbiter = _gameArbiter;
            _oponentType = "network";
        }

        public void StartNewGame()
        {
            if (_gameInProgress)
            {
                ResetGameAssests();
                SetKillRules();
                SendGameInfo();
                _view.SetPlaneOrientationVisibile(true);
            }
        }

        private void ResetGameAssests()
        {
            var name = _firstPlayer.Name;
            var playerFactory = new ConcretePlayerFactory();
            _firstPlayer = new Player{Name = name};
            name = _secondPlayer.Name;
            _secondPlayer = playerFactory.CreatePlayer(_oponentType);
            _secondPlayer.Name = name;
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
            _view.SetGameStatus("Awaiting Game Start");
            var networkFactory = new UserNetworkFactory();

            _firstPlayer = new Player();
            _secondPlayer = new Player();
            _network = networkFactory.CreateNetwork(type);
            SetPlayerRelatedData();

            _network.StartService();
        }

        public void Disconnect()
        {
            if (!_gameInProgress) return;
            _network.SendData(DataType.Disconnect);
            _gameInProgress = false;
        }

        public void SetOponent(string type)
        {
            var playerFactory = new ConcretePlayerFactory();
            _secondPlayer = playerFactory.CreatePlayer(type);
        }

        public void SetKillRules()
        {
            _planeTemplate = new Plane();
            if (_network.GetType() != typeof (Server)) return;
            var killRulesView = new KillRulesView();
            var killRulesController = new KillRulesController(killRulesView);
            if (killRulesView.ShowDialog() != DialogResult.OK) return;

            _firstPlayer.CanSetup = true;
            _view.SetGameStatus("Setting up Planes, You must set 4!");
            _planeTemplate = killRulesController.Plane;
        }

        public void SetPlayerRelatedData()
        {
            var playerConnectionView = new PlayerConnectionView();
            var playerConnectionController = _network.GetType() == typeof (Server)
                ? new PlayerConnectionController(playerConnectionView, false)
                : new PlayerConnectionController(playerConnectionView, true);
            playerConnectionView.SetController(playerConnectionController);
            if (playerConnectionView.ShowDialog() == DialogResult.OK)
            {
                _playerConnectionInfo = playerConnectionController.PlayerConnectionInfo;
                Common.IpEndPoint = _playerConnectionInfo.RemoteAddress != null ? 
                    new IPEndPoint(_playerConnectionInfo.RemoteAddress, 2000) : 
                    new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2000);
            }
            _firstPlayer.Name = playerConnectionController.PlayerConnectionInfo.Name;
        }

        public void SetUp(Point location)
        {
            if (_gameInProgress == false) return;
            if (_firstPlayer.CanSetup == false ) return;
            var tileLocation = _playerPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;

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
                default:
                    MessageBox.Show(@"Please select a plane orientation");
                    return;
            }
            plane.PlaneStartPosition = tileLocation;

            if (_firstPlayer.PlanesAlive == 4)
            {
                _view.SetPlaneOrientationVisibile(false);
                _view.SetScoreBoardVisibile(true);
                _firstPlayer.CanSetup = false;
                _network.SendData(DataType.StartGame);
                if(_network.GetType() == typeof(Server))
                    _view.SetGameStatus("You can attack!");
                else
                {
                    _view.SetGameStatus("You can not attack!");
                }
            }
        }

        private void BuildPlane(MatrixCoordinate matrixCoordinate, Plane plane)
        {
            if (CheckPlaneDuplicates(matrixCoordinate, plane)) return;
            _firstPlayer.PlanesAlive++;
            _firstPlayer.PlanesList.Add(plane);
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
                    _firstPlayer.PlaneMatrix[i, j] = plane.PlaneMatrix[row, column];
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
                    if (_firstPlayer.PlaneMatrix[i, j] ==0 || plane.PlaneMatrix[row, column] != 1) continue;
                    MessageBox.Show(@"Planes intersecting, retry!");
                    return true;
                }
            }
            return false;
        }

        public void ExecuteAttack(Point location)
        {
            if ( _gameInProgress == false ) return;
            if (_firstPlayer.CanAttack == false) return;
            var tileLocation = _oponentPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;
            if (_firstPlayer.OponentPlaneMatrix[tileLocation.Row, tileLocation.Column] != 0)
            {
                MessageBox.Show(@"Already attacked here!");
                return;
            }
            _firstPlayer.CanAttack = false;
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

        private void SendGameInfo()
        {
            if (_network.GetType() == typeof (Server))
            {
                var killpoints = "killpoints:";
                _planeTemplate.KillPoints.ForEach(killpoint => killpoints += killpoint.ToString() + " ");
                _network.SendData(DataType.SetUp, killpoints);
            }
        }

        internal void ConnectionEstablished()
        {
            _network.SendData(DataType.SetUp, "name: " + _firstPlayer.Name);
            _view.SetConnectionStatus("Status: Connected!");
            _gameInProgress = true;
            _connected = true;
        }

        public void AddMessage(string data)
        {
            _view.AddMessage(_secondPlayer.Name + " >> " + data + Environment.NewLine);
        }

        public void SendMessage(string messageBoxInputText)
        {
            if (!messageBoxInputText.Equals("") && _gameInProgress)
            {
                _network.SendData(DataType.Message, messageBoxInputText);
                _view.AddMessage("You >> " + messageBoxInputText + Environment.NewLine);
            }
        }

        public void SetUpData(string data)
        {
            if (data.Contains("killpoints:"))
            {
                data = data.Remove(0, "killpoints:".Count() + 1);
                var points = data.Split(' ');
                foreach (var point in points)
                {
                    if (point != "")
                    {
                        var matrixCoordinate = new MatrixCoordinate(int.Parse(point[0].ToString()),
                            int.Parse(point[1].ToString()));
                        _planeTemplate.KillPoints.Add(matrixCoordinate);
                        _planeTemplate.PlaneMatrix[matrixCoordinate.Row, matrixCoordinate.Column] = 2;
                    }
                }
                _view.SetGameStatus("Setting up Planes, You must set 4!");
                _firstPlayer.CanSetup = true;
            }
            if (data.Contains("name: "))
            {
                data = data.Remove(0, "name: ".Count());
                _secondPlayer.Name = data;
                _view.SetOponentName("Oponent: "+data);
            }
        }

        public void StartGame()
        {
            _firstPlayer.CanAttack = _network.GetType() == typeof (Server);
        }

        public void Attacked(string data)
        {
            _firstPlayer.CanAttack = true;
            _view.SetGameStatus("You can attack!");
            _currentAttackPoint = new MatrixCoordinate(int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));
            if (_firstPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] == 0)
            {
                _network.SendData(DataType.AttackResponse, "m" + _currentAttackPoint);
                _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Cyan);
                _firstPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] = 4; // 4 - plane miss
                _secondPlayer.Misses++;
                return;
               
            }
            _firstPlayer.PlanesList.ForEach(plane =>
            {
                if (PlaneContains(_currentAttackPoint, plane))
                {
                    var killPointToFind = plane.KillPoints.Find(
                        killpoint =>
                            killpoint.Row == _currentAttackPoint.Row - plane.PlaneStartPosition.Row &&
                            killpoint.Column == _currentAttackPoint.Column - plane.PlaneStartPosition.Column);
                    if (killPointToFind != null)
                    {
                        _secondPlayer.Hits++;
                        _network.SendData(DataType.AttackResponse, "h" + _currentAttackPoint);
                        _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Red);
                        plane.KillPoints.Remove(killPointToFind);
                        if (plane.KillPoints.Count == 0)
                        {
                            _firstPlayer.PlanesAlive--;
                            _network.SendData(DataType.AttackResponse,
                                "d" + plane.Orientation[0] + plane.PlaneStartPosition);
                            var row = 0;
                            for (var i = plane.PlaneStartPosition.Row; i < plane.PlaneStartPosition.Row + plane.NumberOfRows; i++, row++)
                            {
                                var column = 0;
                                for (var j = plane.PlaneStartPosition.Column;
                                    j < plane.PlaneStartPosition.Column + plane.NumberOfColumns;
                                    j++, column++)
                                {
                                    if (plane.PlaneMatrix[row, column] != 0)
                                    {
                                        _playerPanelEngine.UpdateTile(i, j, Color.Red);
                                        _firstPlayer.PlaneMatrix[i, j] = 3;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _secondPlayer.Hits++;
                        _network.SendData(DataType.AttackResponse, "h" + _currentAttackPoint);
                        _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Red);
                    }
                    _firstPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] = 3; // 3 - hit plane
                }
            });
            CheckGameEnd();
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
                _firstPlayer.Hits++;
                var matrixCoordonate = new MatrixCoordinate(int.Parse(data[2].ToString()), int.Parse(data[3].ToString()));
                _oponentPanelEngine.UpdateTile(matrixCoordonate.Row, matrixCoordonate.Column, Color.Red);
                _firstPlayer.OponentPlaneMatrix[matrixCoordonate.Row, matrixCoordonate.Column] = 3; // 3 - hit
            }
            if (data[1] == 'm')
            {
                _firstPlayer.Misses++;
                var matrixCoordonate = new MatrixCoordinate(int.Parse(data[2].ToString()), int.Parse(data[3].ToString()));
                _oponentPanelEngine.UpdateTile(matrixCoordonate.Row, matrixCoordonate.Column, Color.Cyan);
                _firstPlayer.OponentPlaneMatrix[matrixCoordonate.Row, matrixCoordonate.Column] = 4; // 3 - miss
            }
            if (data[1] == 'd')
            {
                _secondPlayer.PlanesAlive--;
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
            if (_firstPlayer.PlanesAlive == 0)
            {
                MessageBox.Show("You Won!");
                _view.SetGameStatus("You Won!");
                _network.SendData(DataType.Lost);
                _firstPlayer.CanAttack = false;
            }
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
            var row = 0;
            for (var i = startCoordinate.Row; i < startCoordinate.Row + plane.NumberOfRows; i++, row++)
            {
                var column = 0;
                for (var j = startCoordinate.Column;
                    j < startCoordinate.Column + plane.NumberOfColumns;
                    j++, column++)
                {
                    if (plane.PlaneMatrix[row,column] != 0)
                    {
                        _oponentPanelEngine.UpdateTile(i, j, Color.Red);
                        _firstPlayer.OponentPlaneMatrix[i,j] = plane.PlaneMatrix[row, column];
                    }
                }
            }
        }

        public void GameLost()
        {
            _firstPlayer.CanAttack = false;
            MessageBox.Show(@"You lost!");
            _view.SetGameStatus("You lost!");
        }
    }
}
