using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IGameBoardView
    {
        string MessageBoxInputText { get; set; }
        string MessageBoxOutputText { get; set; }
        void SetController(GameBoardController controller);
        void SetConnectionStatus(string data);
        void SetOponentName(string data);
        void SetGameStatus(string data);
        void SetPlaneOrientationVisibile(bool flag);
        void AddMessage(string data);
    }
}