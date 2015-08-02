namespace PlanesGame.Network
{
    public enum DataType
    {
        Connect = 1,
        Disconnect,
        StartGame,
        RestartGame,
        Attack,
        AttackResponse,
        SetUp,
        Won,
        Message,
        Lost,
     }
}