using PlanesGame.Models;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class PlayerConnectionController
    {
        private IPlayerConnectionView _view;
        private PlayerConnectionInfo playerConnectionInfo;

        public PlayerConnectionController(IPlayerConnectionView view, bool showConnectionInfo)
        {
            _view = view;
            _view.SetConnectionDataView(showConnectionInfo);
        }

        public void SetUpConnection()
        {
            playerConnectionInfo = new PlayerConnectionInfo
            {
                Name = _view.PlayerName,
                RemoteAddress = _view.PlayerIp
            };
            Common.GameBoardController.ReadyConnection();
        }
    }
}
