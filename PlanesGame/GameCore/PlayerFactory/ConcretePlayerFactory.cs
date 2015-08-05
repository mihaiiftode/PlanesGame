using System;
using PlanesGame.Models.Player;

namespace PlanesGame.GameCore.PlayerFactory
{
    public class ConcretePlayerFactory : PlayerFactory
    {
        public override IPlayer CreatePlayer(string type)
        {
            switch (type)
            {
                case "computer":
                    return new AiPlayer();
                case "network":
                    return new Models.Player.Player();
                default:
                    throw new ArgumentException("Wrong player type");
            }
        }
    }
}