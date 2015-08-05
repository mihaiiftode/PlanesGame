using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace PlanesGame.Network.NetworkCore
{
    public class Client : Network
    {
        private bool _isConnected;
        private TcpClient _tcpClient;
        public new NetworkStream Stream { get; set; }

        public override ConnectionType ConnectionType
        {
            get { return ConnectionType.Client; }
        }

        public override void StartService()
        {
            try
            {
                _tcpClient = new TcpClient(Common.IpEndPoint.Address.ToString(), Common.IpEndPoint.Port);
                var thread = new Thread(ClientService) {IsBackground = true};
                thread.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Something went Wrong: " + exception.Message);
            }
        }

        public override void StopService()
        {
            _tcpClient.Close();
        }

        public override bool IsConnected()
        {
            return _isConnected;
        }

        private void ClientService()
        {
            try
            {
                Stream = _tcpClient.GetStream();
                var commandInterpreter = new CommandInterpreter();
                var connectValidation = true;
                while (true)
                {
                    if (connectValidation)
                    {
                        Common.GameBoardController.ConnectionEstablished();
                        connectValidation = false;
                        _isConnected = true;
                    }

                    var streamReader = new StreamReader(Stream);
                    var receivedMessage = streamReader.ReadLine();

                    if (commandInterpreter.ExecuteCommand(receivedMessage))
                    {
                        _isConnected = false;
                        MessageBox.Show(@"Oponent bailed!");
                        break;
                    }
                }
                Stream.Close();
                _tcpClient.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Connection Error: " + exception.Message);
            }
        }

        public override void SendData(DataType dataType, string data = "")
        {
            try
            {
                var streamWriter = new StreamWriter(Stream);
                streamWriter.WriteLine((int) dataType + " " + data);
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Something went wrong:" + e.Message);
            }
        }
    }
}