using System;

namespace PlanesGame.Network
{
    public class CommandInterpreter
    {
        public bool ExecuteCommand(string message)
        {
            Common.GameBoardController.AddMessage(message);
            if (message == null) return false;
            var dataType = (DataType)int.Parse(message[0].ToString());
            var data = message.Substring(1);
            switch (dataType)
            {
                case DataType.Connect:
                    Common.GameBoardController.ConnectionEstablished();
                    return false;
                case DataType.Disconnect:
                    return true;
                case DataType.StartGame:
                    Common.GameBoardController.StartGame();
                    return false;
                case DataType.RestartGame:
                    return false;
                case DataType.Attack:
                    Common.GameBoardController.Attacked(data);
                    return false;
                case DataType.AttackResponse:
                    Common.GameBoardController.AttackResponse(data);
                    return false;
                case DataType.SetUp:
                    Common.GameBoardController.SetUpData(data);
                    return false;
                case DataType.Won:
                    return false;
                case DataType.Lost:
                    return false;
                case DataType.Message:
                    Common.GameBoardController.AddMessage(data);
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}