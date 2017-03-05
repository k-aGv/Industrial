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
            this.refreshURL = new System.Windows.Forms.Timer(this.components);
            this.mymap = new GMap.NET.WindowsForms.GMapControl();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.gb_provider = new System.Windows.Forms.GroupBox();
            this.cb_provider = new System.Windows.Forms.ComboBox();
            this.btn_rec = new System.Windows.Forms.Button();
            this.cb_cross = new System.Windows.Forms.CheckBox();
            this.btn_visit = new System.Windows.Forms.Button();
            this.tb_location = new System.Windows.Forms.TextBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_coords = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gb_settings.SuspendLayout();
            this.gb_provider.SuspendLayout();
            this.gb_coords.SuspendLayout();
            this.SuspendLayout();
            // 
            // mymap
            // 
            this.mymap.BackColor = System.Drawing.SystemColors.ControlDark;
            this.mymap.Bearing = 0F;
            this.mymap.CanDragMap = true;
            this.mymap.EmptyTileColor = System.Drawing.Color.LightSkyBlue;
            this.mymap.GrayScaleMode = false;
            this.mymap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mymap.LevelsKeepInMemmory = 5;
            this.mymap.Location = new System.Drawing.Point(12, 12);
            this.mymap.MarkersEnabled = true;
            this.mymap.MaxZoom = 2;
            this.mymap.MinZoom = 2;
            this.mymap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mymap.Name = "mymap";
            this.mymap.NegativeMode = false;
            this.mymap.PolygonsEnabled = true;
            this.mymap.RetryLoadTile = 0;
            this.mymap.RoutesEnabled = true;
            this.mymap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mymap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mymap.ShowTileGridLines = false;
            this.mymap.Size = new System.Drawing.Size(766, 516);
            this.mymap.TabIndex = 3;
            this.mymap.Zoom = 0D;
            this.mymap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mymap_MouseClick);
            this.mymap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mymap_MouseMove);
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.gb_coords);
            this.gb_settings.Controls.Add(this.gb_provider);
            this.gb_settings.Controls.Add(this.btn_rec);
            this.gb_settings.Controls.Add(this.cb_cross);
            this.gb_settings.Controls.Add(this.btn_visit);
            this.gb_settings.Controls.Add(this.tb_location);
            this.gb_settings.Location = new System.Drawing.Point(798, 12);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Size = new System.Drawing.Size(167, 516);
            this.gb_settings.TabIndex = 4;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Settings";
            // 
            // gb_provider
            // 
            this.gb_provider.Controls.Add(this.cb_provider);
            this.gb_provider.Location = new System.Drawing.Point(7, 171);
            this.gb_provider.Name = "gb_provider";
            this.gb_provider.Size = new System.Drawing.Size(154, 57);
            this.gb_provider.TabIndex = 5;
            this.gb_provider.TabStop = false;
            this.gb_provider.Text = "Map provider";
            // 
            // cb_provider
            // 
            this.cb_provider.FormattingEnabled = true;
            this.cb_provider.Location = new System.Drawing.Point(6, 19);
            this.cb_provider.Name = "cb_provider";
            this.cb_provider.Size = new System.Drawing.Size(142, 21);
            this.cb_provider.TabIndex = 4;
            this.cb_provider.SelectedIndexChanged += new System.EventHandler(this.cb_provider_SelectedIndexChanged);
            // 
            // btn_rec
            // 
            this.btn_rec.Location = new System.Drawing.Point(6, 125);
            this.btn_rec.Name = "btn_rec";
            this.btn_rec.Size = new System.Drawing.Size(154, 23);
            this.btn_rec.TabIndex = 3;
            this.btn_rec.Text = "rectangle";
            this.btn_rec.UseVisualStyleBackColor = true;
            this.btn_rec.Click += new System.EventHandler(this.btn_marker_Click);
            // 
            // cb_cross
            // 
            this.cb_cross.AutoSize = true;
            this.cb_cross.Checked = true;
            this.cb_cross.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_cross.Location = new System.Drawing.Point(7, 102);
            this.cb_cross.Name = "cb_cross";
            this.cb_cross.Size = new System.Drawing.Size(82, 17);
            this.cb_cross.TabIndex = 2;
            this.cb_cross.Text = "Show Cross";
            this.cb_cross.UseVisualStyleBackColor = true;
            this.cb_cross.CheckedChanged += new System.EventHandler(this.cb_cross_CheckedChanged);
            // 
            // btn_visit
            // 
            this.btn_visit.Location = new System.Drawing.Point(7, 73);
            this.btn_visit.Name = "btn_visit";
            this.btn_visit.Size = new System.Drawing.Size(154, 23);
            this.btn_visit.TabIndex = 1;
            this.btn_visit.Text = "Visit";
            this.btn_visit.UseVisualStyleBackColor = true;
            this.btn_visit.Click += new System.EventHandler(this.btn_visit_Click);
            // 
            // tb_location
            // 
            this.tb_location.Location = new System.Drawing.Point(6, 47);
            this.tb_location.Name = "tb_location";
            this.tb_location.Size = new System.Drawing.Size(154, 20);
            this.tb_location.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // gb_coords
            // 
            this.gb_coords.Controls.Add(this.label6);
            this.gb_coords.Controls.Add(this.label5);
            this.gb_coords.Controls.Add(this.label4);
            this.gb_coords.Controls.Add(this.label3);
            this.gb_coords.Controls.Add(this.label2);
            this.gb_coords.Location = new System.Drawing.Point(7, 234);
            this.gb_coords.Name = "gb_coords";
            this.gb_coords.Size = new System.Drawing.Size(148, 276);
            this.gb_coords.TabIndex = 6;
            this.gb_coords.TabStop = false;
            this.gb_coords.Text = "Coordinates";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "label2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "label6";
            // 
            // gmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 566);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gb_settings);
            this.Controls.Add(this.mymap);
            this.Name = "gmaps";
            this.Text = "gmaps";
            this.Load += new System.EventHandler(this.gmaps_Load);
            this.gb_settings.ResumeLayout(false);
            this.gb_settings.PerformLayout();
            this.gb_provider.ResumeLayout(false);
            this.gb_coords.ResumeLayout(false);
            this.gb_coords.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer refreshURL;
        public GMap.NET.WindowsForms.GMapControl mymap;//BE CAREFUL WITH THIS ONE
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.Button btn_visit;
        private System.Windows.Forms.TextBox tb_location;
        private System.Windows.Forms.CheckBox cb_cross;
        private System.Windows.Forms.Button btn_rec;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.GroupBox gb_provider;
        private System.Windows.Forms.ComboBox cb_provider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gb_coords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}