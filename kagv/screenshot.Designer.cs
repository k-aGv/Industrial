namespace kagv
{
    partial class Screenshot
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
            this.btn_save = new System.Windows.Forms.Button();
            this.pb_save = new System.Windows.Forms.ProgressBar();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_drawinfo = new System.Windows.Forms.CheckBox();
            this.cb_drawscale = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Location = new System.Drawing.Point(262, 84);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(84, 32);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // pb_save
            // 
            this.pb_save.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pb_save.Location = new System.Drawing.Point(6, 6);
            this.pb_save.Margin = new System.Windows.Forms.Padding(2);
            this.pb_save.Name = "pb_save";
            this.pb_save.Size = new System.Drawing.Size(340, 25);
            this.pb_save.TabIndex = 2;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(262, 42);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(84, 32);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hold alt while creating the rectangle";
            // 
            // cb_drawinfo
            // 
            this.cb_drawinfo.AutoSize = true;
            this.cb_drawinfo.Checked = true;
            this.cb_drawinfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_drawinfo.Location = new System.Drawing.Point(7, 70);
            this.cb_drawinfo.Name = "cb_drawinfo";
            this.cb_drawinfo.Size = new System.Drawing.Size(127, 17);
            this.cb_drawinfo.TabIndex = 6;
            this.cb_drawinfo.Text = "Also draw coords info";
            this.cb_drawinfo.UseVisualStyleBackColor = true;
            // 
            // cb_drawscale
            // 
            this.cb_drawscale.AutoSize = true;
            this.cb_drawscale.Checked = true;
            this.cb_drawscale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_drawscale.Location = new System.Drawing.Point(6, 94);
            this.cb_drawscale.Name = "cb_drawscale";
            this.cb_drawscale.Size = new System.Drawing.Size(120, 17);
            this.cb_drawscale.TabIndex = 7;
            this.cb_drawscale.Text = "Also draw scale info";
            this.cb_drawscale.UseVisualStyleBackColor = true;
            // 
            // Screenshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 122);
            this.Controls.Add(this.cb_drawscale);
            this.Controls.Add(this.cb_drawinfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.pb_save);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(20, 164);
            this.Name = "Screenshot";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screenshot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Screenshot_FormClosing);
            this.Load += new System.EventHandler(this.Screenshot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ProgressBar pb_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_drawinfo;
        private System.Windows.Forms.CheckBox cb_drawscale;
    }
}