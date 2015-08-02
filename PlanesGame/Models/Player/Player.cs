using System.Collections.Generic;
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

        public int[,] PlaneMatrix { get; set; }

        public int[,] OponentPlaneMatrix { get; set; }

        public bool CanAttack { get; set; }
        public bool CanSetup { get; set; }
        public List<Plane.Plane> PlanesList { get; set; }

        public Player()
        {
            PlanesList = new List<Plane.Plane>();
            PlaneMatrix = new int[10,10];
            OponentPlaneMatrix = new int[10,10];
        }

        public void Attack(int row, int collumn)
        {
            Common.GameBoardController.ExecuteAttack(new Point(row, collumn));
        }
    }
}