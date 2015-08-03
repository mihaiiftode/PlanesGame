
using System.Collections.Generic;
using PlanesGame.Models.Plane;
namespace PlanesGame.Models.Player
{
    public interface IPlayer
    {
        string Name { get; set; }

        int PlanesAlive { get; set; }

        int PlanesDestroyed { get; set; }

        int Hits { get; set; }

        int Misses { get; set; }

        int[,] PlaneMatrix { get; set; }

        int[,] OponentPlaneMatrix { get; set; }

        bool CanAttack { get; set; }

        bool CanSetup { get; set; }

        List<Plane.Plane> PlanesList { get; set; }

        void Attack(int row, int collumn);
    }
}
