namespace PlanesGame
{
    partial class GameBoardView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.setPlayerNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MessageBoxInput = new System.Windows.Forms.TextBox();
            this.MessageSendButton = new System.Windows.Forms.Button();
            this.MessageBoxOutput = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OponentNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScoreBar = new System.Windows.Forms.GroupBox();
            this.OponentsMisses = new System.Windows.Forms.Label();
            this.OponentsHits = new System.Windows.Forms.Label();
            this.OponentsPlanesDestroyed = new System.Windows.Forms.Label();
            this.OponentPlanesAlive = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.MissesLabel = new System.Windows.Forms.Label();
            this.HitsLabel = new System.Windows.Forms.Label();
            this.PlanesDestroyedLabel = new System.Windows.Forms.Label();
            this.PlanesAliveLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PlaneOrietationBar = new System.Windows.Forms.GroupBox();
            this.OrientationLeft = new System.Windows.Forms.RadioButton();
            this.OrientationRight = new System.Windows.Forms.RadioButton();
            this.OrientationDown = new System.Windows.Forms.RadioButton();
            this.OrientationUp = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OponentPanel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ScoreBar.SuspendLayout();
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
            this.setKillRulesToolStripMenuItem,
            this.setPlayerNameToolStripMenuItem});
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
            // setPlayerNameToolStripMenuItem
            // 
            this.setPlayerNameToolStripMenuItem.Name = "setPlayerNameToolStripMenuItem";
            resources.ApplyResources(this.setPlayerNameToolStripMenuItem, "setPlayerNameToolStripMenuItem");
            this.setPlayerNameToolStripMenuItem.Click += new System.EventHandler(this.setPlayerNameToolStripMenuItem_Click);
            // 
            // MessageBoxInput
            // 
            resources.ApplyResources(this.MessageBoxInput, "MessageBoxInput");
            this.MessageBoxInput.Name = "MessageBoxInput";
            // 
            // MessageSendButton
            // 
            resources.ApplyResources(this.MessageSendButton, "MessageSendButton");
            this.MessageSendButton.Name = "MessageSendButton";
            this.MessageSendButton.UseVisualStyleBackColor = true;
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
            // ScoreBar
            // 
            this.ScoreBar.Controls.Add(this.OponentsMisses);
            this.ScoreBar.Controls.Add(this.OponentsHits);
            this.ScoreBar.Controls.Add(this.OponentsPlanesDestroyed);
            this.ScoreBar.Controls.Add(this.OponentPlanesAlive);
            this.ScoreBar.Controls.Add(this.label12);
            this.ScoreBar.Controls.Add(this.MissesLabel);
            this.ScoreBar.Controls.Add(this.HitsLabel);
            this.ScoreBar.Controls.Add(this.PlanesDestroyedLabel);
            this.ScoreBar.Controls.Add(this.PlanesAliveLabel);
            this.ScoreBar.Controls.Add(this.label1);
            resources.ApplyResources(this.ScoreBar, "ScoreBar");
            this.ScoreBar.Name = "ScoreBar";
            this.ScoreBar.TabStop = false;
            // 
            // OponentsMisses
            // 
            resources.ApplyResources(this.OponentsMisses, "OponentsMisses");
            this.OponentsMisses.Name = "OponentsMisses";
            // 
            // OponentsHits
            // 
            resources.ApplyResources(this.OponentsHits, "OponentsHits");
            this.OponentsHits.Name = "OponentsHits";
            // 
            // OponentsPlanesDestroyed
            // 
            resources.ApplyResources(this.OponentsPlanesDestroyed, "OponentsPlanesDestroyed");
            this.OponentsPlanesDestroyed.Name = "OponentsPlanesDestroyed";
            // 
            // OponentPlanesAlive
            // 
            resources.ApplyResources(this.OponentPlanesAlive, "OponentPlanesAlive");
            this.OponentPlanesAlive.Name = "OponentPlanesAlive";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // MissesLabel
            // 
            resources.ApplyResources(this.MissesLabel, "MissesLabel");
            this.MissesLabel.Name = "MissesLabel";
            // 
            // HitsLabel
            // 
            resources.ApplyResources(this.HitsLabel, "HitsLabel");
            this.HitsLabel.Name = "HitsLabel";
            // 
            // PlanesDestroyedLabel
            // 
            resources.ApplyResources(this.PlanesDestroyedLabel, "PlanesDestroyedLabel");
            this.PlanesDestroyedLabel.Name = "PlanesDestroyedLabel";
            // 
            // PlanesAliveLabel
            // 
            resources.ApplyResources(this.PlanesAliveLabel, "PlanesAliveLabel");
            this.PlanesAliveLabel.Name = "PlanesAliveLabel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            this.OrientationLeft.TabStop = true;
            this.OrientationLeft.UseVisualStyleBackColor = true;
            // 
            // OrientationRight
            // 
            resources.ApplyResources(this.OrientationRight, "OrientationRight");
            this.OrientationRight.Name = "OrientationRight";
            this.OrientationRight.TabStop = true;
            this.OrientationRight.UseVisualStyleBackColor = true;
            // 
            // OrientationDown
            // 
            resources.ApplyResources(this.OrientationDown, "OrientationDown");
            this.OrientationDown.Name = "OrientationDown";
            this.OrientationDown.TabStop = true;
            this.OrientationDown.UseVisualStyleBackColor = true;
            // 
            // OrientationUp
            // 
            resources.ApplyResources(this.OrientationUp, "OrientationUp");
            this.OrientationUp.Name = "OrientationUp";
            this.OrientationUp.TabStop = true;
            this.OrientationUp.UseVisualStyleBackColor = true;
            // 
            // GameBoard
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
            this.Controls.Add(this.ScoreBar);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameBoard";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OponentPanel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ScoreBar.ResumeLayout(false);
            this.ScoreBar.PerformLayout();
            this.PlaneOrietationBar.ResumeLayout(false);
            this.PlaneOrietationBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PlayerPanel;
        private System.Windows.Forms.PictureBox OponentPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostAGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aIModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setKillRulesToolStripMenuItem;
        private System.Windows.Forms.TextBox MessageBoxInput;
        private System.Windows.Forms.Button MessageSendButton;
        private System.Windows.Forms.TextBox MessageBoxOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ConnectionStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel OponentNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel GameStatusLabel;
        private System.Windows.Forms.GroupBox ScoreBar;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.Label OponentsMisses;
        private System.Windows.Forms.Label OponentsHits;
        private System.Windows.Forms.Label OponentsPlanesDestroyed;
        private System.Windows.Forms.Label OponentPlanesAlive;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label MissesLabel;
        private System.Windows.Forms.Label HitsLabel;
        private System.Windows.Forms.Label PlanesDestroyedLabel;
        private System.Windows.Forms.Label PlanesAliveLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox PlaneOrietationBar;
        private System.Windows.Forms.RadioButton OrientationLeft;
        private System.Windows.Forms.RadioButton OrientationRight;
        private System.Windows.Forms.RadioButton OrientationDown;
        private System.Windows.Forms.RadioButton OrientationUp;
        private System.Windows.Forms.ToolStripMenuItem setPlayerNameToolStripMenuItem;
    }
}

