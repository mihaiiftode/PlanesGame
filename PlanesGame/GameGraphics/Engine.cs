using System.Drawing;
using PlanesGame.Models;

namespace PlanesGame.GameGraphics
{
    public class Engine : IEngine
    {
        private Rectangle _clientRectangle;

        public Engine(Graphics targetGraphics, Rectangle clientRectangle)
        {
            _clientRectangle = clientRectangle;
            GraphicsContext = BufferedGraphicsManager.Current;
            GraphicsBuffer = GraphicsContext.Allocate(targetGraphics, clientRectangle);

            TilesNumberOfRows = TilesNumberOfCollumns = 10;
            Tiles = new Tile[TilesNumberOfRows, TilesNumberOfCollumns];
            IntializeTiles();
        }

        public BufferedGraphicsContext GraphicsContext { get; set; }
        public BufferedGraphics GraphicsBuffer { get; set; }
        public Tile[,] Tiles { get; set; }
        public int TilesNumberOfRows { get; set; }
        public int TilesNumberOfCollumns { get; set; }

        public void Draw()
        {
            var tileSize = new Size(_clientRectangle.Width/TilesNumberOfCollumns - 2,
                _clientRectangle.Height/TilesNumberOfRows - 2);
            var offsetBetweenTiles = new
            {
                X = tileSize.Width + 2,
                Y = tileSize.Height + 2
            };

            GraphicsBuffer.Graphics.FillRectangle(Brushes.BurlyWood, _clientRectangle);

            for (var i = 0; i < TilesNumberOfRows; i++)
            {
                for (var j = 0; j < TilesNumberOfCollumns; j++)
                {
                    Tiles[i, j].Rectangle = new Rectangle(new Point(j*offsetBetweenTiles.X, i*offsetBetweenTiles.Y),
                        tileSize);
                    GraphicsBuffer.Graphics.FillRectangle(Tiles[i, j].Brush, Tiles[i, j].Rectangle);
                }
            }
            GraphicsBuffer.Render();
        }

        public void SetTileMatrixSize(int rows, int collumns)
        {
            Tiles = new Tile[rows, collumns];
            TilesNumberOfRows = rows;
            TilesNumberOfCollumns = collumns;
            IntializeTiles();
        }

        public void UpdateTile(int x, int y, Color myColor)
        {
            ((SolidBrush) Tiles[x, y].Brush).Color = myColor;
            Draw();
        }

        public void ResetTiles()
        {
            IntializeTiles();
        }

        public MatrixCoordinate GetTilePosition(Point location)
        {
            for (var i = 0; i < TilesNumberOfRows; i++)
            {
                for (var j = 0; j < TilesNumberOfCollumns; j++)
                {
                    if (Tiles[i, j].Rectangle.Contains(location))
                    {
                        return new MatrixCoordinate(i, j);
                    }
                }
            }
            return new MatrixCoordinate(-1, -1);
        }

        private void IntializeTiles()
        {
            for (var i = 0; i < TilesNumberOfRows; i++)
            {
                for (var j = 0; j < TilesNumberOfCollumns; j++)
                {
                    Tiles[i, j] = new Tile
                    {
                        Rectangle = new Rectangle(),
                        Brush = new SolidBrush(Color.Black)
                    };
                }
            }
        }
    }
}