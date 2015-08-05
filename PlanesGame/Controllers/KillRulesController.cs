using System.Drawing;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Models;
using PlanesGame.Models.Plane;
using PlanesGame.Views;

namespace PlanesGame.Controllers
{
    public class KillRulesController
    {
        private readonly IKillRulesView _view;
        private Engine _engine;

        public KillRulesController(IKillRulesView view, Plane planeTemplate)
        {
            Plane = planeTemplate;
            _view = view;
            _view.SetController(this);
            _view.GetGraphics();
        }

        public Plane Plane { get; set; }

        public void UpdateTile(MouseEventArgs args)
        {
            _view.StopTimer();
            for (var i = 0; i < _engine.TilesNumberOfRows; i++)
            {
                for (var j = 0; j < _engine.TilesNumberOfCollumns; j++)
                {
                    if (!_engine.Tiles[i, j].Rectangle.Contains(args.Location)) continue;
                    var matrixCoordinate = new MatrixCoordinate(i, j);
                    switch (args.Button)
                    {
                        case MouseButtons.Left:
                            if (Plane.PlaneMatrix[i, j] == 1)
                            {
                                _engine.UpdateTile(i, j, Color.Red);
                                Plane.PlaneMatrix[i, j] = 2;
                                Plane.KillPoints.Add(matrixCoordinate);
                                return;
                            }
                            break;
                        case MouseButtons.Right:
                            if (Plane.PlaneMatrix[i, j] == 2)
                            {
                                _engine.UpdateTile(i, j, Color.Blue);
                                Plane.PlaneMatrix[i, j] = 1;
                                Plane.KillPoints.Remove(
                                    Plane.KillPoints.Find(
                                        points =>
                                            matrixCoordinate.Column == points.Column &&
                                            matrixCoordinate.Row == points.Row));
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
            _engine.SetTileMatrixSize(4, 3);
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    switch (Plane.PlaneMatrix[i, j])
                    {
                        case 1:
                            _engine.UpdateTile(i, j, Color.Blue);
                            break;
                        case 2:
                            _engine.UpdateTile(i, j, Color.Red);
                            break;
                    }
                }
            }
        }

        public void VerifyKillRules(FormClosingEventArgs formClosingEventArgs)
        {
            if (Plane.KillPoints.Count == 0)
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