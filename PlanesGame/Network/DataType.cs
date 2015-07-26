namespace PlanesGame.Network
{
    public enum DataType
    {
        Connect = 0,
        Disconnect,
        StartGame,
        RestartGame,
        Attack,
        EndGame,
        SetUp,
        SetUpData,
        Won,
        Lost,
        Message,
        acknowledge
     }
}