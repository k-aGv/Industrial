namespace kagv {
    partial class resolution {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tb_res = new System.Windows.Forms.TrackBar();
            this.gb_res = new System.Windows.Forms.GroupBox();
            this.lb_multiplier = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tb_res)).BeginInit();
            this.gb_res.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_res
            // 
            this.tb_res.BackColor = System.Drawing.SystemColors.Control;
            this.tb_res.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tb_res.Location = new System.Drawing.Point(6, 34);
            this.tb_res.Maximum = 2;
            this.tb_res.Minimum = 1;
            this.tb_res.Name = "tb_res";
            this.tb_res.Size = new System.Drawing.Size(273, 45);
            this.tb_res.TabIndex = 0;
            this.tb_res.Value = 1;
            this.tb_res.Scroll += new System.EventHandler(this.tb_res_Scroll);
            // 
            // gb_res
            // 
            this.gb_res.Controls.Add(this.lb_multiplier);
            this.gb_res.Controls.Add(this.tb_res);
            this.gb_res.Location = new System.Drawing.Point(12, 12);
            this.gb_res.Name = "gb_res";
            this.gb_res.Size = new System.Drawing.Size(286, 108);
            this.gb_res.TabIndex = 1;
            this.gb_res.TabStop = false;
            this.gb_res.Text = "Resolution";
            // 
            // lb_multiplier
            // 
            this.lb_multiplier.AutoSize = true;
            this.lb_multiplier.Location = new System.Drawing.Point(6, 82);
            this.lb_multiplier.Name = "lb_multiplier";
            this.lb_multiplier.Size = new System.Drawing.Size(51, 13);
            this.lb_multiplier.TabIndex = 1;
            this.lb_multiplier.Text = "Multiplier:";
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(204, 148);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(94, 31);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(12, 148);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(94, 31);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // resolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 198);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.gb_res);
            this.MaximizeBox = false;
            this.Name = "resolution";
            this.Text = "resolution";
            this.Load += new System.EventHandler(this.resolution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_res)).EndInit();
            this.gb_res.ResumeLayout(false);
            this.gb_res.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar tb_res;
        private System.Windows.Forms.GroupBox gb_res;
        private System.Windows.Forms.Label lb_multiplier;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
    }
}