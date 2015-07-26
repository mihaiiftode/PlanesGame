using PlanesGame.Network.Interfaces;

namespace PlanesGame.Network.Factory
{
    public abstract class NetworkFactory
    {
        public abstract INetwork CreateNetwork(string connectionType);
    }
}