using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Models;
using PlanesGame.Models.Player;
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
        private List<MatrixCoordinate> _killPoints; 


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
        }

        public void ConnectToGame()
        {
            StartNewGame();
            var networkFactory = new UserNetworkFactory();
            _network = networkFactory.CreateNetwork("Client");
            StartNewGame();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SetOponent(string type)
        {
            throw new NotImplementedException();
        }

        public void SetKillRules()
        {
            if (_network.GetType() == typeof (Server))
            {
                var killRulesView = new KillRulesView();
                var killRulesController = new KillRuleController(killRulesView);
                if (killRulesView.ShowDialog() != DialogResult.OK) return;
                if (killRulesController.KillPoints.Count > 0)
                {
                    _killPoints = killRulesController.KillPoints;
                }
                else
                {

                }
            }
        }

        public void SetPlayerName()
        {
            var playerConnectionView = new PlayerConnectionView();
            var playerConnectionController =  _network.GetType() == typeof(Server) ? new PlayerConnectionController(playerConnectionView,false):  new PlayerConnectionController(playerConnectionView,true);
            playerConnectionView.SetController(playerConnectionController);
            if (playerConnectionView.ShowDialog() == DialogResult.OK)
            {
                _playerConnectionInfo = playerConnectionController.PlayerConnectionInfo;
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
        }

        public void SetEngineOponent(Graphics graphicsObject, Rectangle clientRectangle)
        {
            _oponentPanelEngine = new Engine(graphicsObject, clientRectangle);
        }

        public void SetPlaneOrientation(string text)
        {
            throw new NotImplementedException();
        }

        public void ReadyConnection(PlayerConnectionInfo playerConnectionInfo)
        {
            throw new NotImplementedException();
        }

        public void KillRulesSet(List<MatrixCoordinate> killPoints)
        {
            throw new NotImplementedException();
        }
    }
}
