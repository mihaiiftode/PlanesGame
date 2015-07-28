using System.Collections.Generic;
using PlanesGame.GameGraphics;

namespace PlanesGame.Models.Plane
{
    public class PlaneUp : IPlane
    {
        public bool[,] PlaneMatrix { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCollumns { get; set; }
        public List<MatrixCoordinate> KillPoints { get; set; }
    }
}