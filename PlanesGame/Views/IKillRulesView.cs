using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IKillRulesView
    {
        void SetController(KillRulesController controller);
        void GetGraphics();
        void StopTimer();
    }
}