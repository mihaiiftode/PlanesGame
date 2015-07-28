using System;
using System.Windows.Forms;
using PlanesGame.Controllers;
using PlanesGame.Views;

namespace PlanesGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var gameBoardView = new GameBoardView();
            var gameBoardController = new GameBoardController(gameBoardView);
            gameBoardView.SetController(gameBoardController);
            Application.Run(gameBoardView);
            
        }
    }
}
