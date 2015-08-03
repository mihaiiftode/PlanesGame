using System.Net.Sockets;

namespace PlanesGame.Network.NetworkCore
{
    public enum ConnType { Server, Client };

    public abstract class Network
    {
        public Socket Socket { get; set; }

        public NetworkStream Stream { get; set; }

        public abstract ConnType ConnectionType { get; }

        public abstract void SendData(DataType dataType, string data = "");

        public abstract void StartService();
    }
}