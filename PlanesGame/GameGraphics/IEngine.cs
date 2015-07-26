using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesGame.GameGraphics
{
    interface IEngine
    {
        BufferedGraphicsContext GraphicsContext { get; set; }

        BufferedGraphics GraphicsBuffer { get; set; }

        Tile[,] Tiles { get; set; }

        void Draw();

        void UpdateTile(Point location);
        void UpdateTile(int x, int y, Color myColor);
    }
}
