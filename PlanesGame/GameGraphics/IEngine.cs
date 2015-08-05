using System.Drawing;
using PlanesGame.Models;

namespace PlanesGame.GameGraphics
{
    internal interface IEngine
    {
        BufferedGraphicsContext GraphicsContext { get; set; }
        BufferedGraphics GraphicsBuffer { get; set; }
        Tile[,] Tiles { get; set; }
        int TilesNumberOfRows { get; set; }
        int TilesNumberOfCollumns { get; set; }
        void Draw();
        void SetTileMatrixSize(int rows, int collums);
        void UpdateTile(int x, int y, Color myColor);
        void ResetTiles();
        MatrixCoordinate GetTilePosition(Point location);
    }
}