namespace kagv
{
    partial class gmaps
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapControl = new Awesomium.Windows.Forms.WebControl(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(977, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getScreenshotToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // getScreenshotToolStripMenuItem
            // 
            this.getScreenshotToolStripMenuItem.Name = "getScreenshotToolStripMenuItem";
            this.getScreenshotToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.getScreenshotToolStripMenuItem.Text = "Take screenshot";
            this.getScreenshotToolStripMenuItem.Click += new System.EventHandler(this.getScreenshotToolStripMenuItem_Click);
            // 
            // mapControl
            // 
            this.mapControl.Location = new System.Drawing.Point(12, 27);
            this.mapControl.Size = new System.Drawing.Size(953, 470);
            this.mapControl.TabIndex = 0;
            // 
            // gmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 566);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "gmaps";
            this.Text = "gmaps";
            this.Load += new System.EventHandler(this.gmaps_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Awesomium.Windows.Forms.WebControl mapControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getScreenshotToolStripMenuItem;
    }
}