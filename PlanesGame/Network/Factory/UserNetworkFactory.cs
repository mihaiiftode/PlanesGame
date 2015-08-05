using System;
using PlanesGame.Network.NetworkCore;

namespace PlanesGame.Network.Factory
{
    public class UserNetworkFactory : NetworkFactory
    {
        public override NetworkCore.Network CreateNetwork(string connectionType)
        {
            switch (connectionType)
            {
                case "server":
                    return new Server();
                case "client":
                    return new Client();
                default:
                    throw new Exception("Invalid connection type:" + connectionType);
            }
        }
    }
}