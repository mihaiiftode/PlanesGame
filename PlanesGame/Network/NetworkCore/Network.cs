using System.Net.Sockets;

namespace PlanesGame.Network.NetworkCore
{
    public abstract class Network 
    {
        public Socket Socket { get; set; }

        public NetworkStream Stream { get; set; }

        public abstract void SendData(DataType dataType, string data="");

        public abstract void StartService();
    }
}