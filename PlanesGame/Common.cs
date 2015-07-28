using System.Net;
using PlanesGame.Controllers;
using PlanesGame.GameCore;

namespace PlanesGame
{
    public static class Common
    {
        public static GameBoardController GameBoardController;

        public static IGameArbiter GameArbiter;

        public static IPEndPoint IpEndPoint;
    }
}