using PlanesGame.Models.Player;

namespace PlanesGame.Models.PlayerFactory
{
    public abstract class PlayerFactory
    {
        public abstract IPlayer CreatePlayer(string type);
    }
}