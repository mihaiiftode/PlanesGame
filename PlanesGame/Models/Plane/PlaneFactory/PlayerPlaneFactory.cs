using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanesGame.GameCore;
namespace PlanesGame.Models.Plane.PlaneFactory
{
    class PlayerPlaneFactory : PlaneFactory
    {
        public override IPlane CreatePlane(string orientation)
        {
            switch (orientation)
            {
                case "up": return new PlaneUp();
                case "dow": return new PlaneDown();
                case "right": return new PlaneRight();
                case "left": return new PlaneLeft();
                default: throw new ArgumentOutOfRangeException(@"Wrong orientation, the plane shouldn't be 3D!");
            }
        }
    }
}
