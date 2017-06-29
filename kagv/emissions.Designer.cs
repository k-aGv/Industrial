namespace kagv {
    partial class emissions {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(emissions));
            this.CO2_label = new System.Windows.Forms.Label();
            this.CO_label = new System.Windows.Forms.Label();
            this.NOx_label = new System.Windows.Forms.Label();
            this.THC_label = new System.Windows.Forms.Label();
            this.Global_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CO2_label
            // 
            this.CO2_label.AutoSize = true;
            this.CO2_label.Location = new System.Drawing.Point(12, 9);
            this.CO2_label.Name = "CO2_label";
            this.CO2_label.Size = new System.Drawing.Size(35, 13);
            this.CO2_label.TabIndex = 0;
            this.CO2_label.Text = "label1";
            // 
            // CO_label
            // 
            this.CO_label.AutoSize = true;
            this.CO_label.Location = new System.Drawing.Point(12, 41);
            this.CO_label.Name = "CO_label";
            this.CO_label.Size = new System.Drawing.Size(35, 13);
            this.CO_label.TabIndex = 1;
            this.CO_label.Text = "label2";
            // 
            // NOx_label
            // 
            this.NOx_label.AutoSize = true;
            this.NOx_label.Location = new System.Drawing.Point(12, 73);
            this.NOx_label.Name = "NOx_label";
            this.NOx_label.Size = new System.Drawing.Size(35, 13);
            this.NOx_label.TabIndex = 2;
            this.NOx_label.Text = "label3";
            // 
            // THC_label
            // 
            this.THC_label.AutoSize = true;
            this.THC_label.Location = new System.Drawing.Point(12, 104);
            this.THC_label.Name = "THC_label";
            this.THC_label.Size = new System.Drawing.Size(35, 13);
            this.THC_label.TabIndex = 3;
            this.THC_label.Text = "label4";
            // 
            // Global_label
            // 
            this.Global_label.AutoSize = true;
            this.Global_label.Location = new System.Drawing.Point(12, 135);
            this.Global_label.Name = "Global_label";
            this.Global_label.Size = new System.Drawing.Size(35, 13);
            this.Global_label.TabIndex = 4;
            this.Global_label.Text = "label5";
            // 
            // emissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 173);
            this.Controls.Add(this.Global_label);
            this.Controls.Add(this.THC_label);
            this.Controls.Add(this.NOx_label);
            this.Controls.Add(this.CO_label);
            this.Controls.Add(this.CO2_label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "emissions";
            this.Text = "emissions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label CO2_label;
        public System.Windows.Forms.Label CO_label;
        public System.Windows.Forms.Label NOx_label;
        public System.Windows.Forms.Label THC_label;
        public System.Windows.Forms.Label Global_label;

    }
}