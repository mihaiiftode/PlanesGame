using PlanesGame.Models.Player;

namespace PlanesGame.GameCore.PlayerFactory
{
    public abstract class PlayerFactory
    {
        public abstract IPlayer CreatePlayer(string type);
    }
}