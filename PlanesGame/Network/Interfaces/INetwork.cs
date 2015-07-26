using System.Net.Sockets;

namespace PlanesGame.Network.Interfaces
{
    public interface INetwork
    {
        Socket Socket { get; set; }

        NetworkStream Stream { get; set; }
    }
}
