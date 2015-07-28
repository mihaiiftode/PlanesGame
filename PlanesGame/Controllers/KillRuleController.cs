using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class KillRuleController
    {
        public List<MatrixCoordinate> KillPoints { get; set; } 

        private Engine _engine;

        private readonly bool[,] _planeMatrix;

        private readonly IKillRulesView _view;

        public KillRuleController(IKillRulesView view, bool[,] planeMatrix)
        {
            _planeMatrix = planeMatrix;
            _view = view;
            KillPoints = new List<MatrixCoordinate>();
            _view.SetController(this);
            _view.GetGraphics();
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
                            if (_planeMatrix[i, j])
                            {
                                KillPoints.Add(new MatrixCoordinate(i, j));
                                _engine.UpdateTile(i, j, Color.Red);
                                return;
                            }
                            break;
                        case MouseButtons.Right:
                            if (_planeMatrix[i, j])
                            {
                                KillPoints.Remove(KillPoints.Find(killPoint => killPoint.Row == i && killPoint.Collumn == j));
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
                    if (_planeMatrix[i, j])
                    {
                        _engine.UpdateTile(i, j, Color.Blue);
                    }
                }
            }
        }

        public void VerifyKillRules(FormClosingEventArgs formClosingEventArgs)
        {
            if (KillPoints.Count == 0)
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
