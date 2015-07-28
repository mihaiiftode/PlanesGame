using System;

namespace PlanesGame.Network
{
    public class CommandInterpreter
    {
        public bool ExecuteCommand(string message)
        {
            var dataType = (DataType)message[0];
            switch (dataType)
            {
                case DataType.Connect:
                    Common.GameBoardController.ConnectionEstablished();
                    return false;
                case DataType.Disconnect:
                    break;
                case DataType.StartGame:
                    break;
                case DataType.RestartGame:
                    break;
                case DataType.Attack:
                    break;
                case DataType.EndGame:
                    break;
                case DataType.SetUp:
                    break;
                case DataType.SetUpData:
                    break;
                case DataType.Won:
                    break;
                case DataType.Lost:
                    break;
                case DataType.Message:
                    break;
                case DataType.Acknowledge:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}