using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlanesGame.GameGraphics;
using PlanesGame.Network;
using PlanesGame.Network.NetworkCore;
using PlanesGame.Views;

namespace PlanesGame
{
    public partial class GameBoard : Form , IUserView
    {
        public GameBoard()
        {
            InitializeComponent();
        }

        private void startNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hostAGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void connectToGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void networkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aIModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setKillRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setPlayerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PlayerPanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void OponentPanel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void OrientationUp_MouseClick(object sender, MouseEventArgs e)
        {
            ResetAllRadioButtons(sender);
        }

        private void OrientationDown_MouseClick(object sender, MouseEventArgs e)
        {
            ResetAllRadioButtons(sender);
        }

        private void OrientationLeft_MouseClick(object sender, MouseEventArgs e)
        {
            ResetAllRadioButtons(sender);
        }

        private void OrientationRight_MouseClick(object sender, MouseEventArgs e)
        {
            ResetAllRadioButtons(sender);
        }
        private void ResetAllRadioButtons(object sender)
        {
            ((RadioButton)sender).Checked = true;
            foreach (var radioButton in PlaneOrietationBar.Controls.Cast<RadioButton>().Where(radioButton => !radioButton.Equals(sender)))
            {
                radioButton.Checked = false;
            }
        }

        public string ChatBoxText { get; set; }
        public string ChatBoxInputText { get; set; }
        public void SetController()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerPlanesAlive(string data)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerPlanesDestroyed(string data)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerHits(string data)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerMisses(string data)
        {
            throw new NotImplementedException();
        }

        public void SetOponentPlanesAlive(string data)
        {
            throw new NotImplementedException();
        }

        public void SetOponentPlanesDestroyed(string data)
        {
            throw new NotImplementedException();
        }

        public void SetOponentHits(string data)
        {
            throw new NotImplementedException();
        }

        public void SetOponentMisses(string data)
        {
            throw new NotImplementedException();
        }

        public void SetConnectionStatus(string data)
        {
            throw new NotImplementedException();
        }

        public void SetOponentsName(string data)
        {
            throw new NotImplementedException();
        }

        public void SetGameStatus(string data)
        {
            throw new NotImplementedException();
        }

        public void SetPlaneOrientationVisibile(bool flag)
        {
            throw new NotImplementedException();
        }

        public void SetScoreBoardVisibile(bool flag)
        {
            throw new NotImplementedException();
        }
    }
}
