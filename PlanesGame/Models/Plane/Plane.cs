using System.Collections.Generic;

namespace PlanesGame.Models.Plane
{
    public class Plane : IPlane
    {
        public Plane()
        {
            Orientation = "up";
            KillPoints = new List<MatrixCoordinate>();
            NumberOfRows = 4;
            NumberOfColumns = 3;
            PlaneMatrix = new[,]
            {
                {0, 1, 0},
                {1, 1, 1},
                {0, 1, 0},
                {1, 1, 1}
            };
        }

        public string Orientation { get; set; }
        public int[,] PlaneMatrix { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public MatrixCoordinate PlaneStartPosition { get; set; }
        public List<MatrixCoordinate> KillPoints { get; set; }
    }
}