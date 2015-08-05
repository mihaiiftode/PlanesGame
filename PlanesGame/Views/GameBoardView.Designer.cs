using System.ComponentModel;
using System.Windows.Forms;

namespace PlanesGame.Views
{
    partial class GameBoardView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameBoardView));
            this.PlayerPanel = new System.Windows.Forms.PictureBox();
            this.OponentPanel = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostAGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aIModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setKillRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MessageBoxInput = new System.Windows.Forms.TextBox();
            this.MessageSendButton = new System.Windows.Forms.Button();
            this.MessageBoxOutput = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OponentNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlaneOrietationBar = new System.Windows.Forms.GroupBox();
            this.OrientationLeft = new System.Windows.Forms.RadioButton();
            this.OrientationRight = new System.Windows.Forms.RadioButton();
            this.OrientationDown = new System.Windows.Forms.RadioButton();
            this.OrientationUp = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OponentPanel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.PlaneOrietationBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerPanel
            // 
            this.PlayerPanel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.PlayerPanel, "PlayerPanel");
            this.PlayerPanel.Name = "PlayerPanel";
            this.PlayerPanel.TabStop = false;
            this.PlayerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayerPanel_MouseClick);
            // 
            // OponentPanel
            // 
            resources.ApplyResources(this.OponentPanel, "OponentPanel");
            this.OponentPanel.Name = "OponentPanel";
            this.OponentPanel.TabStop = false;
            this.OponentPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OponentPanel_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.playersToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNewToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // startNewToolStripMenuItem
            // 
            this.startNewToolStripMenuItem.Name = "startNewToolStripMenuItem";
            resources.ApplyResources(this.startNewToolStripMenuItem, "startNewToolStripMenuItem");
            this.startNewToolStripMenuItem.Click += new System.EventHandler(this.startNewToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostAGameToolStripMenuItem,
            this.connectToGameToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            resources.ApplyResources(this.networkToolStripMenuItem, "networkToolStripMenuItem");
            // 
            // hostAGameToolStripMenuItem
            // 
            this.hostAGameToolStripMenuItem.Name = "hostAGameToolStripMenuItem";
            resources.ApplyResources(this.hostAGameToolStripMenuItem, "hostAGameToolStripMenuItem");
            this.hostAGameToolStripMenuItem.Click += new System.EventHandler(this.hostAGameToolStripMenuItem_Click);
            // 
            // connectToGameToolStripMenuItem
            // 
            this.connectToGameToolStripMenuItem.Name = "connectToGameToolStripMenuItem";
            resources.ApplyResources(this.connectToGameToolStripMenuItem, "connectToGameToolStripMenuItem");
            this.connectToGameToolStripMenuItem.Click += new System.EventHandler(this.connectToGameToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            resources.ApplyResources(this.disconnectToolStripMenuItem, "disconnectToolStripMenuItem");
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.networkModeToolStripMenuItem,
            this.aIModeToolStripMenuItem,
            this.setKillRulesToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            resources.ApplyResources(this.playersToolStripMenuItem, "playersToolStripMenuItem");
            // 
            // networkModeToolStripMenuItem
            // 
            this.networkModeToolStripMenuItem.Checked = true;
            this.networkModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.networkModeToolStripMenuItem.Name = "networkModeToolStripMenuItem";
            resources.ApplyResources(this.networkModeToolStripMenuItem, "networkModeToolStripMenuItem");
            this.networkModeToolStripMenuItem.Click += new System.EventHandler(this.networkModeToolStripMenuItem_Click);
            // 
            // aIModeToolStripMenuItem
            // 
            resources.ApplyResources(this.aIModeToolStripMenuItem, "aIModeToolStripMenuItem");
            this.aIModeToolStripMenuItem.Name = "aIModeToolStripMenuItem";
            this.aIModeToolStripMenuItem.Click += new System.EventHandler(this.aIModeToolStripMenuItem_Click);
            // 
            // setKillRulesToolStripMenuItem
            // 
            this.setKillRulesToolStripMenuItem.Name = "setKillRulesToolStripMenuItem";
            resources.ApplyResources(this.setKillRulesToolStripMenuItem, "setKillRulesToolStripMenuItem");
            this.setKillRulesToolStripMenuItem.Click += new System.EventHandler(this.setKillRulesToolStripMenuItem_Click);
            // 
            // MessageBoxInput
            // 
            resources.ApplyResources(this.MessageBoxInput, "MessageBoxInput");
            this.MessageBoxInput.Name = "MessageBoxInput";
            this.MessageBoxInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageBoxInput_KeyDown);
            // 
            // MessageSendButton
            // 
            resources.ApplyResources(this.MessageSendButton, "MessageSendButton");
            this.MessageSendButton.Name = "MessageSendButton";
            this.MessageSendButton.UseVisualStyleBackColor = true;
            this.MessageSendButton.Click += new System.EventHandler(this.MessageSendButton_Click);
            // 
            // MessageBoxOutput
            // 
            resources.ApplyResources(this.MessageBoxOutput, "MessageBoxOutput");
            this.MessageBoxOutput.Name = "MessageBoxOutput";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionStatusLabel,
            this.OponentNameLabel,
            this.GameStatusLabel});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // ConnectionStatusLabel
            // 
            this.ConnectionStatusLabel.Name = "ConnectionStatusLabel";
            resources.ApplyResources(this.ConnectionStatusLabel, "ConnectionStatusLabel");
            // 
            // OponentNameLabel
            // 
            this.OponentNameLabel.Name = "OponentNameLabel";
            resources.ApplyResources(this.OponentNameLabel, "OponentNameLabel");
            // 
            // GameStatusLabel
            // 
            this.GameStatusLabel.Name = "GameStatusLabel";
            resources.ApplyResources(this.GameStatusLabel, "GameStatusLabel");
            // 
            // PlaneOrietationBar
            // 
            this.PlaneOrietationBar.Controls.Add(this.OrientationLeft);
            this.PlaneOrietationBar.Controls.Add(this.OrientationRight);
            this.PlaneOrietationBar.Controls.Add(this.OrientationDown);
            this.PlaneOrietationBar.Controls.Add(this.OrientationUp);
            resources.ApplyResources(this.PlaneOrietationBar, "PlaneOrietationBar");
            this.PlaneOrietationBar.Name = "PlaneOrietationBar";
            this.PlaneOrietationBar.TabStop = false;
            // 
            // OrientationLeft
            // 
            resources.ApplyResources(this.OrientationLeft, "OrientationLeft");
            this.OrientationLeft.Name = "OrientationLeft";
            this.OrientationLeft.UseVisualStyleBackColor = true;
            // 
            // OrientationRight
            // 
            resources.ApplyResources(this.OrientationRight, "OrientationRight");
            this.OrientationRight.Name = "OrientationRight";
            this.OrientationRight.UseVisualStyleBackColor = true;
            // 
            // OrientationDown
            // 
            resources.ApplyResources(this.OrientationDown, "OrientationDown");
            this.OrientationDown.Name = "OrientationDown";
            this.OrientationDown.UseVisualStyleBackColor = true;
            // 
            // OrientationUp
            // 
            resources.ApplyResources(this.OrientationUp, "OrientationUp");
            this.OrientationUp.Checked = true;
            this.OrientationUp.Name = "OrientationUp";
            this.OrientationUp.TabStop = true;
            this.OrientationUp.UseVisualStyleBackColor = true;
            // 
            // GameBoardView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MessageBoxOutput);
            this.Controls.Add(this.MessageSendButton);
            this.Controls.Add(this.MessageBoxInput);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.OponentPanel);
            this.Controls.Add(this.PlayerPanel);
            this.Controls.Add(this.PlaneOrietationBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "GameBoardView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameBoardView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OponentPanel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.PlaneOrietationBar.ResumeLayout(false);
            this.PlaneOrietationBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox PlayerPanel;
        private PictureBox OponentPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem startNewToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem networkToolStripMenuItem;
        private ToolStripMenuItem hostAGameToolStripMenuItem;
        private ToolStripMenuItem connectToGameToolStripMenuItem;
        private ToolStripMenuItem playersToolStripMenuItem;
        private ToolStripMenuItem networkModeToolStripMenuItem;
        private ToolStripMenuItem aIModeToolStripMenuItem;
        private ToolStripMenuItem setKillRulesToolStripMenuItem;
        private TextBox MessageBoxInput;
        private Button MessageSendButton;
        private TextBox MessageBoxOutput;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel ConnectionStatusLabel;
        private ToolStripStatusLabel OponentNameLabel;
        private ToolStripStatusLabel GameStatusLabel;
        private ToolStripMenuItem disconnectToolStripMenuItem;
        private GroupBox PlaneOrietationBar;
        private RadioButton OrientationLeft;
        private RadioButton OrientationRight;
        private RadioButton OrientationDown;
        private RadioButton OrientationUp;
    }
}

