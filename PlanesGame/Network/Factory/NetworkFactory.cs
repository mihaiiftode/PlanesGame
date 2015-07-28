namespace PlanesGame.Network.Factory
{
    public abstract class NetworkFactory
    {
        public abstract NetworkCore.Network CreateNetwork(string connectionType);
    }
}