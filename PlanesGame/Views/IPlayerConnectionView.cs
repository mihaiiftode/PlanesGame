using System.Net;
using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IPlayerConnectionView
    {
        IPAddress PlayerIp { get; set; }

        string PlayerName { get; set; }

        void SetConnectionDataView(bool flag);

        void SetController(PlayerConnectionController controller);
    }
}