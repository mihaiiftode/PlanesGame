using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanesGame.Network.NetworkCore
{
    public class Server : Network
    {
        public new Socket Socket { get; set; }
        public new NetworkStream Stream { get; set; }
        private TcpListener _tcpListener;

        public override void StartService()
        {
            _tcpListener = TcpListener.Create(Common.IpEndPoint.Port);
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
                var connectValidation = true;
                while (true) 
                {
                    if (connectValidation)
                    {
                        var dataType = (int)DataType.Connect; 
                        var messageBytes = Encoding.UTF8.GetBytes(dataType.ToString());
                        Stream.Write(messageBytes, 0, messageBytes.Length);
                        connectValidation = false;
                    }

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