namespace PlanesGame.GameCore
{
    public interface IGameArbiter
    {
        // Should manage AI Combat
        void ExecuteAttack(int firstPoint, int secondPoint);
    }
}