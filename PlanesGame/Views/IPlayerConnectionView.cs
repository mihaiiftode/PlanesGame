using System.Net;
using PlanesGame.Controllers;
using PlanesGame.Models;

namespace PlanesGame.Views
{
    public interface IPlayerConnectionView
    {
        IPEndPoint PlayerIp { get; set; }

        string PlayerName { get; set; }

        void SetConnectionDataView(bool flag);

        void SetController(PlayerConnectionController controller);
    }
}