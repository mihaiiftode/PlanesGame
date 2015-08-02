using PlanesGame.GameCore;
using PlanesGame.GameGraphics;

namespace PlanesGame.Models.Plane
{
    public class PlaneRotater
    {

        public void SetPlaneDown(Plane plane)
        {
            plane.Orientation = "down";
            var matrixOperation =new MatrixOperations();
            plane.PlaneMatrix = matrixOperation.Invert(plane.PlaneMatrix, plane.NumberOfRows,
                           plane.NumberOfCollumns);
            UpdateKillPoints(plane);
        }


        public void SetPlaneRight(Plane plane)
        {
            plane.Orientation = "right";
            var matrixOperation = new MatrixOperations();
            plane.PlaneMatrix = matrixOperation.Rotate(plane.PlaneMatrix, plane.NumberOfRows,
            plane.NumberOfCollumns);
            plane.NumberOfCollumns = 4;
            plane.NumberOfRows = 3;
            UpdateKillPoints(plane);
        }
        public void SetPlaneLeft(Plane plane)
        {
            plane.Orientation = "left";
            var matrixOperation = new MatrixOperations();
            plane.PlaneMatrix = matrixOperation.Rotate(plane.PlaneMatrix, plane.NumberOfRows,
              plane.NumberOfCollumns);
            plane.NumberOfCollumns = 4;
            plane.NumberOfRows = 3;
            plane.PlaneMatrix = matrixOperation.Invert(plane.PlaneMatrix, plane.NumberOfRows,
                plane.NumberOfCollumns);
            UpdateKillPoints(plane);
        }
        private static void UpdateKillPoints(Plane plane)
        {
            plane.KillPoints.Clear();
            for (var i = 0; i < plane.NumberOfRows; i++)
            {
                for (var j = 0; j < plane.NumberOfCollumns; j++)
                {
                    if (plane.PlaneMatrix[i, j] == 2)
                        plane.KillPoints.Add(new MatrixCoordinate(i, j));
                }
            }
        }
    }
}