using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanesGame.Network.Interfaces;

namespace PlanesGame.Network.NetworkCore
{
    public class Client : INetwork
    {
        public Socket Socket { get; set; }
        public NetworkStream Stream { get; set; }

        private readonly TcpClient _tcpClient;

        public Client()
        {
            _tcpClient = new TcpClient("127.0.0.1", 2000);
            Task.Factory.StartNew(ClientService);
        }

        private void ClientService()
        {
            try
            {
                Stream = _tcpClient.GetStream();
                var buffer = new byte[1024]; 
                var commandInterpreter = new CommandInterpreter();
                while (true)
                {
                    if (!Stream.DataAvailable) continue;
                    var bytesRead = Stream.Read(buffer, 0, buffer.Length);
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    commandInterpreter.ExecuteCommand(receivedMessage);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong with the client connection:" + e.Message);
            }
            finally
            {
                _tcpClient.Close();
            }
        }
    }
}