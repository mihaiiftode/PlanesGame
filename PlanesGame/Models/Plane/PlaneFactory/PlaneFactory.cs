using PlanesGame.GameCore;

namespace PlanesGame.Models.Plane.PlaneFactory
{
    public abstract class PlaneFactory
    {
        public abstract IPlane CreatePlane(string orientation);
    }
}