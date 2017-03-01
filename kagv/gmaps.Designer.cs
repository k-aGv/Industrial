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
            this.url_label = new System.Windows.Forms.Label();
            this.refreshURL = new System.Windows.Forms.Timer(this.components);
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
            // url_label
            // 
            this.url_label.AutoSize = true;
            this.url_label.Location = new System.Drawing.Point(80, 11);
            this.url_label.Name = "url_label";
            this.url_label.Size = new System.Drawing.Size(35, 13);
            this.url_label.TabIndex = 2;
            this.url_label.Text = "label1";
            // 
            // refreshURL
            // 
            this.refreshURL.Tick += new System.EventHandler(this.refreshURL_Tick);
            // 
            // gmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 566);
            this.Controls.Add(this.url_label);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "gmaps";
            this.Text = "gmaps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.gmaps_FormClosing);
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
        private System.Windows.Forms.Label url_label;
        private System.Windows.Forms.Timer refreshURL;
    }
}