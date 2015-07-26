using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using PlanesGame.Network.Interfaces;

namespace PlanesGame.Network.NetworkCore
{
    public class NetworkCommunication : INetwork
    {
        public Socket Socket { get; set; }

        public NetworkStream Stream { get; set; }

        public NetworkCommunication(INetwork targetNetwork)
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