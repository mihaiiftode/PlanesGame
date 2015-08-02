using System.Drawing;

namespace PlanesGame.GameGraphics
{
    interface IEngine
    {
        BufferedGraphicsContext GraphicsContext { get; set; }

        BufferedGraphics GraphicsBuffer { get; set; }
        
        Tile[,] Tiles { get; set; }
        int TilesNumberOfRows { get; set; }

        int TilesNumberOfCollumns { get; set; }

        void Draw();

        void SetTileMatrixSize(int rows, int collums);

        void UpdateTile(int x, int y, Color myColor);

        MatrixCoordinate GetTilePosition(Point location);
    }
}
