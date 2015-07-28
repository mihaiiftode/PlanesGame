using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanesGame.Controllers;
using PlanesGame.GameGraphics;
using Timer = System.Timers.Timer;

namespace PlanesGame.Views
{
    public partial class KillRulesView : Form, IKillRulesView
    {
        private Timer _timer;
        private KillRuleController _controller;
        public KillRulesView()
        {
            InitializeComponent();
        }
        
        public void SetController(KillRuleController controller)
        {
            _controller = controller;
        }

        public void GetGraphics()
        {
            _controller.InitializeEngine(Panel.CreateGraphics(), Panel.ClientRectangle);
            // Graphics first draw workaround .
            _timer = new Timer(100);
            _timer.Elapsed += (sender, args) => { _controller.Redraw(); };
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            _controller.UpdateTile(e);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void KillRulesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.VerifyKillRules(e);
        }
    }
}
