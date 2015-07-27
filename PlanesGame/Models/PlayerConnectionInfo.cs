using System.Net;

namespace PlanesGame.Models
{
    public class PlayerConnectionInfo
    {
        public IPEndPoint RemoteAddress { get; set; }

        public string Name { get; set; }
    }
}