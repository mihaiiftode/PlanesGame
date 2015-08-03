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
        private readonly IGameBoardView _view;
        private Network.NetworkCore.Network _network;
        private IEngine _playerPanelEngine;
        private IEngine _oponentPanelEngine;
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        private PlayerConnectionInfo _playerConnectionInfo;
        private bool _connected = false;
        private string _planeOrientation;
        private Plane _planeTemplate;
        private MatrixCoordinate _currentAttackPoint;

        public GameBoardController(IGameBoardView view)
        {
            _view = view;
            Common.GameBoardController = this;
            var gameArbiter = new GameArbiter();
            Common.GameArbiter = gameArbiter;
        }

        public void StartNewGame()
        {
            if (_connected)
            {
                SetKillRules();
                SendGameInfo();
            }
            _view.SetPlaneOrientationVisibile(true);
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

            _firstPlayer = new Player();
            _secondPlayer = new Player();
            _network = networkFactory.CreateNetwork(type);
            SetPlayerRelatedData();
            _network.StartService();
        }

        public void Disconnect()
        {
            _network.SendData(DataType.Disconnect);
        }

        public void SetOponent(string type)
        {
            var playerFactory = new ConcretePlayerFactory();
            _secondPlayer = playerFactory.CreatePlayer(type);
        }

        public void SetKillRules()
        {
            _planeTemplate = new Plane();
            if (_network.ConnectionType != ConnType.Server) return;
            var killRulesView = new KillRulesView();
            var killRulesController = new KillRulesController(killRulesView);
            if (killRulesView.ShowDialog() != DialogResult.OK) return;

            _planeTemplate = killRulesController.Plane;
        }

        public void SetPlayerRelatedData()
        {
            var playerConnectionView = new PlayerConnectionView();
            var showConnInfo = _network.ConnectionType != ConnType.Server;
            var playerConnectionController = new PlayerConnectionController(playerConnectionView, showConnInfo);
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
            if (_firstPlayer.CanSetup == false) return;
            var tileLocation = _playerPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;

            var plane = new Plane
            {
                PlaneMatrix = _planeTemplate.PlaneMatrix,
                KillPoints = _planeTemplate.KillPoints.ToList()
            };
            var planeRotator = new PlaneRotater();
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
                    MessageBox.Show(@"Please select a plane orientation"); return;
            }
            plane.PlaneStartPosition = tileLocation;
            _firstPlayer.PlanesList.Add(plane);
            if (_firstPlayer.PlanesAlive == 4)
            {
                _firstPlayer.CanSetup = false;
                _network.SendData(DataType.StartGame);
            }
        }

        private delegate bool MatrixDel(int rowPlayer, int colPlayer, int rowPlane, int colPlane);
        private void ProcessMatrix(MatrixCoordinate matrixCoordinate, Plane plane, MatrixDel del)
        {
            var row = 0;
            for (var i = matrixCoordinate.Row; i < matrixCoordinate.Row + plane.NumberOfRows; i++, row++)
            {
                var column = 0;
                for (var j = matrixCoordinate.Column; j < matrixCoordinate.Column + plane.NumberOfCollumns; j++, column++)
                {
                    if (del(i, j, row, column)) return; // delegate wants to finish
                }
            }
        }

        private void BuildPlane(MatrixCoordinate matrixCoordinate, Plane plane)
        {
            _firstPlayer.PlanesAlive++;
            if (CheckPlaneDuplicates(matrixCoordinate, plane)) return;
            ProcessMatrix(matrixCoordinate, plane, (i, j, row, col) =>
            {
                if (plane.PlaneMatrix[row, col] == 0) return false;
                _playerPanelEngine.UpdateTile(i, j, Color.Blue);
                _firstPlayer.PlaneMatrix[i, j] = plane.PlaneMatrix[row, col];
                return false; // continue iterating matrix
            });
        }
        private bool CheckPlaneDuplicates(MatrixCoordinate matrixCoordinate, Plane plane)
        {
            bool result = false;
            ProcessMatrix(matrixCoordinate, plane, (i, j, row, col) =>
            {
                if (_firstPlayer.PlaneMatrix[i, j] != 1 || plane.PlaneMatrix[row, col] != 1) return false;
                MessageBox.Show(@"Planes intersecting, retry!");
                _firstPlayer.PlanesAlive--;
                return (result = true); // matrix is done iterating after this
            });
            return result;
        }

        public void ExecuteAttack(Point location)
        {
            if (!_firstPlayer.CanAttack) return;
            var tileLocation = _oponentPanelEngine.GetTilePosition(location);
            if (tileLocation.Row == -1 || tileLocation.Column == -1) return;
            _network.SendData(DataType.Attack, tileLocation.ToString());
            _firstPlayer.CanAttack = false;
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
            if (_network.ConnectionType == ConnType.Server)
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
            _connected = true;
            _firstPlayer.CanSetup = true;
            StartNewGame();
        }

        public void AddMessage(string data)
        {
            _view.AddMessage(_secondPlayer.Name + " >> " + data + Environment.NewLine);
        }

        public void SendMessage(string messageBoxInputText)
        {
            if (!messageBoxInputText.Equals(""))
            {
                _network.SendData(DataType.Message, messageBoxInputText);
                _view.AddMessage("You >> " + messageBoxInputText + Environment.NewLine);
            }
        }

        public void SetUpData(string data)
        {
            if (data.Contains("killpoints:"))
            {
                foreach (var point in data.Replace("killpoints:", "").Split(' '))
                {
                    if (point != "")
                        _planeTemplate.KillPoints.Add(new MatrixCoordinate(
                                int.Parse(point[0].ToString()), int.Parse(point[1].ToString())));
                }
            }
            if (data.Contains("name: "))
            {
                _secondPlayer.Name = data.Replace("name: ", "");
                _view.SetOponentName(_secondPlayer.Name);
            }
        }

        public void StartGame()
        {
            _firstPlayer.CanAttack = _network.ConnectionType == ConnType.Server;
        }

        public void Attacked(string data)
        {
            _firstPlayer.CanAttack = true;
            _currentAttackPoint = new MatrixCoordinate(int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));
            if (_firstPlayer.PlaneMatrix[_currentAttackPoint.Row, _currentAttackPoint.Column] == 0)
            {
                _network.SendData(DataType.AttackResponse, "m" + _currentAttackPoint);
                _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Cyan);
                return;
            }
            _firstPlayer.PlanesList.ForEach(plane =>
            {
                if (PlaneContains(_currentAttackPoint, plane))
                {
                    var killPointToFind = plane.KillPoints.Find(
                        killpoint =>
                            killpoint.Row == _currentAttackPoint.Row - plane.PlaneStartPosition.Row &&
                            killpoint.Collumn == _currentAttackPoint.Column - plane.PlaneStartPosition.Collumn);
                    if (killPointToFind != null)
                    {
                        plane.KillPoints.Remove(killPointToFind);
                        if (plane.KillPoints.Count == 0)
                        {
                            _network.SendData(DataType.AttackResponse,
                                "d" + plane.Orientation[0] + plane.PlaneStartPosition);
                        }
                    }
                    else
                    {
                        _network.SendData(DataType.AttackResponse, "h" + _currentAttackPoint);
                        _playerPanelEngine.UpdateTile(_currentAttackPoint.Row, _currentAttackPoint.Column, Color.Red);
                    }
                }
            });
        }

        private static bool PlaneContains(MatrixCoordinate hitpoint, Plane plane)
        {
            return hitpoint.Row >= plane.PlaneStartPosition.Row &&
                   hitpoint.Row <= plane.PlaneStartPosition.Row + plane.NumberOfRows &&
                   hitpoint.Column >= plane.PlaneStartPosition.Column &&
                   hitpoint.Column <= plane.PlaneStartPosition.Column + plane.NumberOfCollumns;
        }

        public void AttackResponse(string data)
        {
            if (data[1] == 'h' || data[1] == 'm')
            {
                Color tile = (data[1] == 'h') ? Color.Brown : Color.Cyan;
                _oponentPanelEngine.UpdateTile(int.Parse(data[2].ToString()), int.Parse(data[3].ToString()), tile);
            }
            if (data[1] =='d')
            {
                    if (data[2] == 'u') ;
                    if (data[2] == 'd') ;
                    if (data[2] == 'l') ;
                    if (data[2] == 'r') ;
            }
        }
    }
}
