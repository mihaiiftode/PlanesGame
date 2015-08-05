using System;
using System.Linq;
using System.Windows.Forms;
using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public partial class GameBoardView : Form, IGameBoardView
    {
        private GameBoardController _controller;

        public GameBoardView()
        {
            InitializeComponent();
            InitiateRadioButtons();
        }

        public string MessageBoxInputText
        {
            get { return MessageBoxInput.Text; }
            set { MessageBoxInput.Text = value; }
        }

        public string MessageBoxOutputText
        {
            get { return MessageBoxOutput.Text; }
            set { MessageBoxOutput.Text = value; }
        }

        public void SetController(GameBoardController controller)
        {
            _controller = controller;
        }

        public void SetConnectionStatus(string data)
        {
            ConnectionStatusLabel.Text = data;
        }

        public void SetOponentName(string data)
        {
            OponentNameLabel.Text = data;
        }

        public void SetGameStatus(string data)
        {
            GameStatusLabel.Text = data;
        }

        public void SetPlaneOrientationVisibile(bool flag)
        {
            PlaneOrietationBar.BeginInvoke((Action) (() =>
            {
                PlaneOrietationBar.Visible = flag;
                if (flag)
                {
                    PlaneOrietationBar.BringToFront();
                }
                else
                {
                    PlaneOrietationBar.SendToBack();
                }
            }));
        }

        public void AddMessage(string data)
        {
            MessageBoxOutput.BeginInvoke((Action) (() => { MessageBoxOutput.AppendText(data); }));
        }

        private void InitiateRadioButtons()
        {
            foreach (var radioButton in PlaneOrietationBar.Controls.Cast<RadioButton>())
            {
                radioButton.Click += (sender, args) =>
                {
                    ResetAllRadioButtons(sender);
                    _controller.SetPlaneOrientation(((RadioButton) sender).Text);
                };
            }
        }

        private void ResetAllRadioButtons(object sender)
        {
            ((RadioButton) sender).Checked = true;
            foreach (
                var radioButton in
                    PlaneOrietationBar.Controls.Cast<RadioButton>().Where(radioButton => !radioButton.Equals(sender)))
            {
                radioButton.Checked = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _controller.Redraw();
        }

        private void startNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallEngineStart();
            _controller.StartNewGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void hostAGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallEngineStart();
            _controller.StartServer();
        }

        private void connectToGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallEngineStart();
            _controller.ConnectToGame();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Disconnect();
        }

        private void networkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SetOponent("network");
        }

        private void aIModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SetOponent("computer");
        }

        private void setKillRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SetKillRules();
        }

        private void PlayerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            _controller.SetUp(e.Location);
        }

        private void OponentPanel_MouseClick(object sender, MouseEventArgs e)
        {
            _controller.ExecuteAttack(e.Location);
        }

        private void CallEngineStart()
        {
            _controller.SetEnginePlayer(PlayerPanel.CreateGraphics(), PlayerPanel.ClientRectangle);
            _controller.SetEngineOponent(OponentPanel.CreateGraphics(), OponentPanel.ClientRectangle);
        }

        private void MessageSendButton_Click(object sender, EventArgs e)
        {
            _controller.SendMessage(MessageBoxInputText);
            MessageBoxInput.Text = "";
        }

        private void MessageBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            _controller.SendMessage(MessageBoxInputText);
            MessageBoxInput.Text = "";
        }

        private void GameBoardView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.Disconnect();
            e.Cancel = false;
        }
    }
}