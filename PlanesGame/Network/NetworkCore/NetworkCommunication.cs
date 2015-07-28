using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PlanesGame.Network.NetworkCore
{
    public class NetworkCommunication : Network
    {
        public new Socket Socket { get; set; }

        public new NetworkStream Stream { get; set; }
        public override void StartService()
        {
            
        }

        public NetworkCommunication(Network targetNetwork)
        {
            Socket = targetNetwork.Socket;
            Stream = targetNetwork.Stream;
        }

        public void SendData(DataType dataType, string data = "")
        {
            try
            {
                var messageBytes = Encoding.UTF8.GetBytes(dataType + data);
                Stream.Write(messageBytes, 0, messageBytes.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong:" + e.Message);
            }

        }
    }
}