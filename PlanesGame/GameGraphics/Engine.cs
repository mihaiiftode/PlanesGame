using System;
using System.Drawing;

namespace PlanesGame.GameGraphics
{
    public class Engine : IEngine
    {
        public BufferedGraphicsContext GraphicsContext { get; set; }
        public BufferedGraphics GraphicsBuffer { get; set; }
        public Tile[,] Tiles { get; set; }

        private Rectangle _clientRectangle ;

        public Engine(Graphics targetGraphics, Rectangle clientRectangle)
        {
            _clientRectangle = clientRectangle;
            GraphicsContext = BufferedGraphicsManager.Current;
            GraphicsBuffer = GraphicsContext.Allocate(targetGraphics, clientRectangle);
            Tiles = new Tile[10,10];
            IntializeTiles();
        }

        public void Draw()
        {
            var tileSize = new Size(_clientRectangle.Width/10 - 2, _clientRectangle.Height/10 - 2);
            var offsetBetweenTiles = new
            {
                X = tileSize.Width + 2,
                Y = tileSize.Height + 2
            };

            GraphicsBuffer.Graphics.FillRectangle(Brushes.WhiteSmoke, _clientRectangle);

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Tiles[i, j].Rectangle = new Rectangle(new Point(j*offsetBetweenTiles.X, i*offsetBetweenTiles.Y),
                        tileSize);
                    GraphicsBuffer.Graphics.FillRectangle(Tiles[i, j].Brush, Tiles[i,j].Rectangle);
                }
            }
            GraphicsBuffer.Render();
        }

        public void UpdateTile(Point location)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTile(int x, int y, Color myColor)
        {
            ((SolidBrush)Tiles[x, y].Brush).Color = myColor;
            Draw();
        }
        private void IntializeTiles()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Tiles[i, j] = new Tile()
                    {
                        Rectangle = new Rectangle(),
                        Brush = new SolidBrush(Color.Black)
                    };
                }
            }
        } 
    }
}