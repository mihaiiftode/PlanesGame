using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            var thread = new Thread(ClientService);

            thread.IsBackground = true;
            thread.Start();
        }

        private void ClientService()
        {

                Stream = _tcpClient.GetStream();
                var buffer = new byte[1024]; 
                var commandInterpreter = new CommandInterpreter();
                var connectValidation = true;
                while (true)
                {
                    if (connectValidation)
                    {
                        Common.GameBoardController.ConnectionEstablished();
                        connectValidation = false;
                    }

                    var streamReader = new StreamReader(Stream);
                    var receivedMessage =  streamReader.ReadLine();

                    if (commandInterpreter.ExecuteCommand(receivedMessage))
                    {
                        break;
                    }
                }


        }

        public override void SendData(DataType dataType, string data = "")
        {
            try
            {
                var streamWriter = new StreamWriter(Stream);
                streamWriter.WriteLine((int)dataType + " " + data);
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong:" + e.Message);
            }

        }
    }
}