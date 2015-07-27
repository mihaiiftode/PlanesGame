using System;
using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IUserView
    {
        string ChatBoxText { get; set; }

        string ChatBoxInputText { get; set; }

        void SetController(UserController controller);
        void SetPlayerPlanesAlive(string data);
        void SetPlayerPlanesDestroyed(string data);
        void SetPlayerHits(string data);
        void SetPlayerMisses(string data);
        void SetOponentPlanesAlive(string data);
        void SetOponentPlanesDestroyed(string data);
        void SetOponentHits(string data);
        void SetOponentMisses(string data);
        void SetConnectionStatus(string data);
        void SetOponentsName(string data);
        void SetGameStatus(string data);
        void SetPlaneOrientationVisibile(bool flag);
        void SetScoreBoardVisibile(bool flag);
    }
}