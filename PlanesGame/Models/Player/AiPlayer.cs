using System;
using System.Collections.Generic;

namespace PlanesGame.Models.Player
{
    public class AiPlayer : IPlayer
    {
        // ai logic should go here
        public string Name { get; set; }
        public int PlanesAlive { get; set; }
        public int[,] PlaneMatrix { get; set; }
        public int[,] OponentPlaneMatrix { get; set; }
        public bool CanAttack { get; set; }
        public bool CanSetup { get; set; }
        public List<Plane.Plane> PlanesList { get; set; }

        public void Attack(int row, int collumn)
        {
            throw new NotImplementedException();
        }
    }
}