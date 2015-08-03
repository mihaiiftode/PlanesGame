using System;
using System.Net;
using System.Windows.Forms;
using PlanesGame.Controllers;

namespace PlanesGame.Views
{
    public partial class PlayerConnectionView : Form, IPlayerConnectionView
    {
        private PlayerConnectionController _controller;
        private bool _showConnectionData;
        public PlayerConnectionView()
        {
            InitializeComponent();
        }

        public IPAddress PlayerIp
        {
            get
            {
                try
                {
                    return IPAddress.Parse(IpTextBox.Text);
                }
                catch (Exception)
                {
                    if(_showConnectionData)
                        MessageBox.Show(@"Invalid Ip/Port format");
                    return null;
                }
            }
            set
            {
                IpTextBox.Text = value.ToString();
            }
        }

        public string PlayerName
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }

        public void SetConnectionDataView(bool flag)
        {
            IpTextBox.Visible = flag;
            IpLabel.Visible = flag;
            _showConnectionData = flag;
        }

        public void SetController(PlayerConnectionController controller)
        {
            _controller = controller;
        }

        private void PlayerConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_showConnectionData)
                {
                    IPAddress.Parse(IpTextBox.Text);
                }
                if (string.IsNullOrEmpty(PlayerName) || string.IsNullOrWhiteSpace(PlayerName))
                {
                    throw new Exception();
                }
                _controller.SetUpConnection();
                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Check IP format. Name should not be empty");
                e.Cancel = true;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IpTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                IPAddress.Parse(IpTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Invalid IP format!");
            }
        }

        private void NameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show(@"Name field can't be empty/whitespace");
            }
        }
    }
}
