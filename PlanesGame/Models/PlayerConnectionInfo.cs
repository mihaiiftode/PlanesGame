using System.Net;

namespace PlanesGame.Models
{
    public class PlayerConnectionInfo
    {
        public IPAddress RemoteAddress { get; set; }
        public string Name { get; set; }
    }
}