namespace Tram.Simulation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rightPanel = new System.Windows.Forms.Panel();
            this.renderPanel = new System.Windows.Forms.Panel();
            this.zoomInButton = new System.Windows.Forms.Panel();
            this.zoomOutButton = new System.Windows.Forms.Panel();
            this.zoomOriginalButton = new System.Windows.Forms.Panel();
            this.centerScreenButton = new System.Windows.Forms.Panel();
            this.settingsButton = new System.Windows.Forms.Panel();
            this.aboutUsButton = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Panel();
            this.pauseButton = new System.Windows.Forms.Panel();
            this.speedTrackBar = new System.Windows.Forms.TrackBar();
            this.stopButton = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPanel.Location = new System.Drawing.Point(806, 12);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(298, 715);
            this.rightPanel.TabIndex = 0;
            // 
            // renderPanel
            // 
            this.renderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.renderPanel.Location = new System.Drawing.Point(12, 12);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(734, 715);
            this.renderPanel.TabIndex = 1;
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseMove);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInButton.BackColor = System.Drawing.Color.White;
            this.zoomInButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomInButton.BackgroundImage")));
            this.zoomInButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomInButton.Location = new System.Drawing.Point(752, 12);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(48, 48);
            this.zoomInButton.TabIndex = 4;
            this.zoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutButton.BackColor = System.Drawing.Color.White;
            this.zoomOutButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.BackgroundImage")));
            this.zoomOutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomOutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomOutButton.Location = new System.Drawing.Point(752, 66);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(48, 48);
            this.zoomOutButton.TabIndex = 5;
            this.zoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // zoomOriginalButton
            // 
            this.zoomOriginalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOriginalButton.BackColor = System.Drawing.Color.White;
            this.zoomOriginalButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("zoomOriginalButton.BackgroundImage")));
            this.zoomOriginalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zoomOriginalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zoomOriginalButton.Location = new System.Drawing.Point(752, 129);
            this.zoomOriginalButton.Name = "zoomOriginalButton";
            this.zoomOriginalButton.Size = new System.Drawing.Size(48, 48);
            this.zoomOriginalButton.TabIndex = 5;
            this.zoomOriginalButton.Click += new System.EventHandler(this.zoomOriginalButton_Click);
            // 
            // centerScreenButton
            // 
            this.centerScreenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.centerScreenButton.BackColor = System.Drawing.Color.White;
            this.centerScreenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("centerScreenButton.BackgroundImage")));
            this.centerScreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.centerScreenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.centerScreenButton.Location = new System.Drawing.Point(752, 183);
            this.centerScreenButton.Name = "centerScreenButton";
            this.centerScreenButton.Size = new System.Drawing.Size(48, 48);
            this.centerScreenButton.TabIndex = 5;
            this.centerScreenButton.Click += new System.EventHandler(this.centerScreenButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.BackColor = System.Drawing.Color.White;
            this.settingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settingsButton.BackgroundImage")));
            this.settingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsButton.Location = new System.Drawing.Point(752, 679);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(48, 48);
            this.settingsButton.TabIndex = 6;
            // 
            // aboutUsButton
            // 
            this.aboutUsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutUsButton.BackColor = System.Drawing.Color.White;
            this.aboutUsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aboutUsButton.BackgroundImage")));
            this.aboutUsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutUsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutUsButton.Location = new System.Drawing.Point(752, 625);
            this.aboutUsButton.Name = "aboutUsButton";
            this.aboutUsButton.Size = new System.Drawing.Size(48, 48);
            this.aboutUsButton.TabIndex = 7;
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playButton.BackColor = System.Drawing.Color.White;
            this.playButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("playButton.BackgroundImage")));
            this.playButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playButton.Location = new System.Drawing.Point(12, 733);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(48, 48);
            this.playButton.TabIndex = 7;
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseButton.BackColor = System.Drawing.Color.White;
            this.pauseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pauseButton.BackgroundImage")));
            this.pauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pauseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pauseButton.Location = new System.Drawing.Point(66, 733);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(48, 48);
            this.pauseButton.TabIndex = 8;
            // 
            // speedTrackBar
            // 
            this.speedTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.speedTrackBar.BackColor = System.Drawing.Color.White;
            this.speedTrackBar.LargeChange = 10;
            this.speedTrackBar.Location = new System.Drawing.Point(174, 736);
            this.speedTrackBar.Maximum = 100;
            this.speedTrackBar.Minimum = 1;
            this.speedTrackBar.Name = "speedTrackBar";
            this.speedTrackBar.Size = new System.Drawing.Size(584, 45);
            this.speedTrackBar.TabIndex = 9;
            this.speedTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.speedTrackBar.Value = 1;
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopButton.BackColor = System.Drawing.Color.White;
            this.stopButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stopButton.BackgroundImage")));
            this.stopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.Location = new System.Drawing.Point(120, 733);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(48, 48);
            this.stopButton.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1116, 793);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.speedTrackBar);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.aboutUsButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.zoomOriginalButton);
            this.Controls.Add(this.renderPanel);
            this.Controls.Add(this.centerScreenButton);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.zoomInButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel renderPanel;
        private System.Windows.Forms.Panel zoomInButton;
        private System.Windows.Forms.Panel zoomOutButton;
        private System.Windows.Forms.Panel zoomOriginalButton;
        private System.Windows.Forms.Panel centerScreenButton;
        private System.Windows.Forms.Panel settingsButton;
        private System.Windows.Forms.Panel aboutUsButton;
        private System.Windows.Forms.Panel playButton;
        private System.Windows.Forms.Panel pauseButton;
        private System.Windows.Forms.TrackBar speedTrackBar;
        private System.Windows.Forms.Panel stopButton;
    }
}