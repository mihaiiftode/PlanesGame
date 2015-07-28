using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Models;
using PlanesGame.Models.Plane;
using PlanesGame.Models.Player;
using PlanesGame.Models.PlayerFactory;
using PlanesGame.Network.Factory;
using PlanesGame.Network.NetworkCore;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class GameBoardController
    {
        private IGameBoardView _view;
        private Network.NetworkCore.Network _network;
        private Network.NetworkCore.Network _networkCommunication;
        private IEngine _playerPanelEngine;
        private IEngine _oponentPanelEngine;
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        private PlayerConnectionInfo _playerConnectionInfo;

        public GameBoardController(IGameBoardView view)
        {
            _view = view;
            Common.GameBoardController = this;
        }

        public void StartNewGame()
        {
            _firstPlayer = new Player();
            _secondPlayer = new Player();
            SetPlayerName();
            SetKillRules();
        }

        public void StartServer()
        {
            var networkFactory = new UserNetworkFactory();
            _network = networkFactory.CreateNetwork("server");
            StartNewGame();
            _network.StartService();
        }

        public void ConnectToGame()
        {
            var networkFactory = new UserNetworkFactory();
            _network = networkFactory.CreateNetwork("client");
            StartNewGame();
            _network.StartService();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SetOponent(string type)
        {
            var playerFactory = new ConcretePlayerFactory();
            _secondPlayer = playerFactory.CreatePlayer(type);
        }

        public void SetKillRules()
        {
            if (_network.GetType() != typeof (Server)) return;
            var killRulesView = new KillRulesView();
            var killRulesController = new KillRuleController(killRulesView);
            if (killRulesView.ShowDialog() != DialogResult.OK) return;

            _firstPlayer.Plane.PlaneMatrix = killRulesController.Plane.PlaneMatrix;
        }

        public void SetPlayerName()
        {
            var playerConnectionView = new PlayerConnectionView();
            var playerConnectionController =  _network.GetType() == typeof(Server) ? new PlayerConnectionController(playerConnectionView,false):  new PlayerConnectionController(playerConnectionView,true);
            playerConnectionView.SetController(playerConnectionController);
            if (playerConnectionView.ShowDialog() == DialogResult.OK)
            {
                _playerConnectionInfo = playerConnectionController.PlayerConnectionInfo;
                if (_playerConnectionInfo.RemoteAddress != null)
                {
                    Common.IpEndPoint = new IPEndPoint(_playerConnectionInfo.RemoteAddress, 2000);
                }
                else
                {
                    Common.IpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),2000);
                }
            }
            _firstPlayer.Name = playerConnectionController.PlayerConnectionInfo.Name;
        }

        public void SetUp(Point location)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAttack(Point location)
        {
            throw new NotImplementedException();
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

        public void SetPlaneOrientation(string text)
        {
            throw new NotImplementedException();
        }

        internal void ConnectionEstablished()
        {
            throw new NotImplementedException();
        }
    }
}
