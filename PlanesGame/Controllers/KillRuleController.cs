using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Models.Plane;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class KillRuleController
    {
        public int NumberKillPoints { get; set; } 

        private Engine _engine;

        public int[,] PlaneMatrix { get; set; } 

        private readonly IKillRulesView _view;

        public KillRuleController(IKillRulesView view)
        {
            PlaneMatrix = new Plane().PlaneMatrix;
            _view = view;
            _view.SetController(this);
            _view.GetGraphics();
            NumberKillPoints = 0;
        }

        public void UpdateTile(MouseEventArgs args)
        {
            _view.StopTimer();
            for (var i = 0; i < _engine.TilesNumberOfRows; i++)
            {
                for (var j = 0; j < _engine.TilesNumberOfCollumns; j++)
                {
                    if (!_engine.Tiles[i, j].Rectangle.Contains(args.Location)) continue;
                    switch (args.Button)
                    {
                        
                        case MouseButtons.Left:
                            if (PlaneMatrix[i, j] == 1)
                            {
                                NumberKillPoints++;
                                _engine.UpdateTile(i, j, Color.Red);
                                PlaneMatrix[i, j] = 2;
                                return;
                            }
                            break;
                        case MouseButtons.Right:
                            if (PlaneMatrix[i, j] == 1)
                            {
                                NumberKillPoints--;
                                PlaneMatrix[i, j] = 2;
                                _engine.UpdateTile(i, j, Color.Blue);
                                return;
                            }
                            break;
                    }
                }
            }
        }

        public void InitializeEngine(Graphics targerGraphics, Rectangle clientRectangle)
        {
            _engine = new Engine(targerGraphics, clientRectangle);
            _engine.SetTileMatrixSize(4,3);
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (PlaneMatrix[i, j] == 1)
                    {
                        _engine.UpdateTile(i, j, Color.Blue);
                    }
                }
            }
        }

        public void VerifyKillRules(FormClosingEventArgs formClosingEventArgs)
        {
            if (NumberKillPoints == 0)
            {
                MessageBox.Show(@"At least one kill point must be selected");
                formClosingEventArgs.Cancel = true;
            }
        }

        public void Redraw()
        {
            _engine.Draw();
        }
    }
}
