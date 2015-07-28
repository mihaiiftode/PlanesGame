using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanesGame.Network.NetworkCore
{
    public class Client : Network
    {
        public new Socket Socket { get; set; }
        public new NetworkStream Stream { get; set; }

        private TcpClient _tcpClient;

        public override void StartService()
        {
            _tcpClient = new TcpClient(Common.IpEndPoint.Address.ToString(), Common.IpEndPoint.Port);
            Task.Factory.StartNew(ClientService);
        }

        private void ClientService()
        {
            try
            {
                Stream = _tcpClient.GetStream();
                var buffer = new byte[1024]; 
                var commandInterpreter = new CommandInterpreter();
                var connectValidation = true;
                while (true)
                {
                    if (connectValidation)
                    {
                        var dataType = (int)DataType.Connect;;
                        var messageBytes = Encoding.UTF8.GetBytes(dataType.ToString()); 
                        Stream.Write(messageBytes, 0, messageBytes.Length);
                        connectValidation = false;
                    }
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