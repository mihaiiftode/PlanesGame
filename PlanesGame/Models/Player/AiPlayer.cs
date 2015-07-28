using System;

namespace PlanesGame.Models.Player
{
    public class AiPlayer : IPlayer
    {
        // ai logic should go here
        public string Name { get; set; }
        public int PlanesAlive { get; set; }
        public int PlanesDestroyed { get; set; }
        public int Hits { get; set; }
        public int Misses { get; set; }
        public int OwnPlaneMatrix { get; set; }
        public int OponentPlaneMatrix { get; set; }
        public void Attack(int row, int collumn)
        {
            throw new NotImplementedException();
        }
    }
}