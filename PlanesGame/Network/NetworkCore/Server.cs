using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanesGame.Network.Interfaces;

namespace PlanesGame.Network.NetworkCore
{
    public class Server : INetwork
    {
        public Socket Socket { get; set; }
        public NetworkStream Stream { get; set; }

        private readonly TcpListener _tcpListener;

        public Server()
        {
            _tcpListener = TcpListener.Create(2000);
            Task.Factory.StartNew(ServerService);
        }

        private void ServerService()
        {
            _tcpListener.Start();
            Socket = _tcpListener.AcceptSocket();
            try
            {
                Stream = new NetworkStream(Socket);
                var buffer = new byte[1024];
                var commandInterpreter = new CommandInterpreter();
                while (true) 
                {
                    if (!Stream.DataAvailable) continue;
                    var bytesRead = Stream.Read(buffer, 0, buffer.Length);
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (commandInterpreter.ExecuteCommand(receivedMessage))
                    {
                        MessageBox.Show("Remote Client Disconnected");
                        break;
                    }
                }
                Stream.Close();
                _tcpListener.Stop();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong with the connection " + e.Message);
            }
        }
    }
}