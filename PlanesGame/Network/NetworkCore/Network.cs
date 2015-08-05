using System.Net.Sockets;

namespace PlanesGame.Network.NetworkCore
{
    public enum ConnectionType
    {
        Server,
        Client
    };

    public abstract class Network
    {
        public NetworkStream Stream { get; set; }
        public abstract ConnectionType ConnectionType { get; }
        public abstract void SendData(DataType dataType, string data = "");
        public abstract void StartService();
        public abstract void StopService();
        public abstract bool IsConnected();
    }
}