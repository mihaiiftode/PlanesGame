using System.Drawing;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class UserController
    {
        private IUserView _view;

        public UserController(IUserView view)
        {
            _view = view;
        }


        public void StartNewGame()
        {
            SetPlayerName();
            SetKillRules();
        }

        public void StartServer()
        {
            StartNewGame();
        }

        public void ConnectToGame()
        {
            StartNewGame();
        }

        public void Disconnect()
        {
            throw new System.NotImplementedException();
        }

        public void SetOponent(string type)
        {
            throw new System.NotImplementedException();
        }

        public void SetKillRules()
        {
            throw new System.NotImplementedException();
        }

        public void SetPlayerName()
        {
            throw new System.NotImplementedException();
        }

        public void SetUp(Point location)
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteAttack(Point location)
        {
            throw new System.NotImplementedException();
        }

        public void SetEnginePlayer(Graphics graphicsObject, Rectangle clientRectangle)
        {
            throw new System.NotImplementedException();
        }

        public void SetEngineOponent(Graphics graphicsObject, Rectangle clientRectangle)
        {
            throw new System.NotImplementedException();
        }

        public void SetPlaneOrientation(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}
