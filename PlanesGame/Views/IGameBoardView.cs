using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IGameBoardView
    {
        string MessageBoxInputText { get; set; }

        string MessageBoxOutputText { get; set; }

        void SetController(GameBoardController controller);
        void SetPlayerPlanesAlive(string data);
        void SetPlayerPlanesDestroyed(string data);
        void SetPlayerHits(string data);
        void SetPlayerMisses(string data);
        void SetOponentPlanesAlive(string data);
        void SetOponentPlanesDestroyed(string data);
        void SetOponentHits(string data);
        void SetOponentMisses(string data);
        void SetConnectionStatus(string data);
        void SetOponentName(string data);
        void SetGameStatus(string data);
        void SetPlaneOrientationVisibile(bool flag);
        void SetScoreBoardVisibile(bool flag);
        void AddMessage(string data);
    }
}