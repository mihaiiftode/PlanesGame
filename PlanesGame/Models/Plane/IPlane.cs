using System.Collections.Generic;

namespace PlanesGame.Models.Plane
{
    public interface IPlane
    {
        int[,] PlaneMatrix { get; set; }
        int NumberOfRows { get; set; }
        int NumberOfColumns { get; set; }
        MatrixCoordinate PlaneStartPosition { get; set; }
        List<MatrixCoordinate> KillPoints { get; set; }
    }
}