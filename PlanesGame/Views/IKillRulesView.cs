using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public interface IKillRulesView
    {
        void SetController(KillRuleController controller);

        void GetGraphics();

        void StopTimer();
    }
}