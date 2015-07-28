using System.Collections.Generic;
using PlanesGame.GameGraphics;

namespace PlanesGame.Models.Plane
{
    public interface IPlane
    {
        int[,] PlaneMatrix { get; set; }

        int NumberOfRows { get; set; }

        int NumberOfCollumns { get; set; }

        List<MatrixCoordinate> KillPoints { get; set; }
    }
}
