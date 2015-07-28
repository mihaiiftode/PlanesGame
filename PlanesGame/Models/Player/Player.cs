using System.Drawing;

namespace PlanesGame.Models.Player
{
    public class Player : IPlayer
    {
        public string Name { get; set; }

        public int PlanesAlive { get; set; }

        public int PlanesDestroyed { get; set; }

        public int Hits { get; set; }

        public int Misses { get; set; }

        public int OwnPlaneMatrix { get; set; }

        public int OponentPlaneMatrix { get; set; }

        public void Attack(int row, int collumn)
        {
            Common.GameBoardController.ExecuteAttack(new Point(row, collumn));
        }
    }
}