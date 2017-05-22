namespace kagv {
    partial class main_form {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.timer0 = new System.Windows.Forms.Timer(this.components);
            this.menuPanel = new System.Windows.Forms.Panel();
            this.gb_type = new System.Windows.Forms.GroupBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.gb_monitor = new System.Windows.Forms.GroupBox();
            this.agv3steps_LB = new System.Windows.Forms.Label();
            this.refresh_label = new System.Windows.Forms.Label();
            this.agv4steps_LB = new System.Windows.Forms.Label();
            this.agv1steps_LB = new System.Windows.Forms.Label();
            this.agv5steps_LB = new System.Windows.Forms.Label();
            this.agv2steps_LB = new System.Windows.Forms.Label();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.gb_agvs = new System.Windows.Forms.GroupBox();
            this.nUD_AGVs = new System.Windows.Forms.NumericUpDown();
            this.rb_wall = new System.Windows.Forms.RadioButton();
            this.rb_start = new System.Windows.Forms.RadioButton();
            this.rb_stop = new System.Windows.Forms.RadioButton();
            this.rb_load = new System.Windows.Forms.RadioButton();
            this.tp_info = new System.Windows.Forms.ToolTip(this.components);
            this.sfd_exportmap = new System.Windows.Forms.SaveFileDialog();
            this.ofd_importmap = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.ofd_importpic = new System.Windows.Forms.OpenFileDialog();
            this.settings_menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useRecursiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossAdjacentPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossCornerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heuristicModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manhattanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.euclideanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chebyshevToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightOverCurrentBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aGVIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cd_grid = new System.Windows.Forms.ColorDialog();
            this.menuPanel.SuspendLayout();
            this.gb_type.SuspendLayout();
            this.gb_monitor.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.gb_agvs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_AGVs)).BeginInit();
            this.settings_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer0
            // 
            this.timer0.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuPanel.Controls.Add(this.gb_type);
            this.menuPanel.Controls.Add(this.gb_monitor);
            this.menuPanel.Controls.Add(this.gb_settings);
            this.menuPanel.Location = new System.Drawing.Point(0, 27);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(656, 75);
            this.menuPanel.TabIndex = 7;
            // 
            // gb_type
            // 
            this.gb_type.Controls.Add(this.cb_type);
            this.gb_type.Location = new System.Drawing.Point(226, 5);
            this.gb_type.Name = "gb_type";
            this.gb_type.Size = new System.Drawing.Size(68, 65);
            this.gb_type.TabIndex = 27;
            this.gb_type.TabStop = false;
            this.gb_type.Text = "Type";
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "LPG",
            "DSL",
            "ELE"});
            this.cb_type.Location = new System.Drawing.Point(7, 20);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(55, 21);
            this.cb_type.TabIndex = 0;
            this.cb_type.Text = "LPG";
            // 
            // gb_monitor
            // 
            this.gb_monitor.Controls.Add(this.agv3steps_LB);
            this.gb_monitor.Controls.Add(this.refresh_label);
            this.gb_monitor.Controls.Add(this.agv4steps_LB);
            this.gb_monitor.Controls.Add(this.agv1steps_LB);
            this.gb_monitor.Controls.Add(this.agv5steps_LB);
            this.gb_monitor.Controls.Add(this.agv2steps_LB);
            this.gb_monitor.Location = new System.Drawing.Point(300, 5);
            this.gb_monitor.Name = "gb_monitor";
            this.gb_monitor.Size = new System.Drawing.Size(338, 65);
            this.gb_monitor.TabIndex = 26;
            this.gb_monitor.TabStop = false;
            this.gb_monitor.Text = "Monitor";
            // 
            // agv3steps_LB
            // 
            this.agv3steps_LB.AutoSize = true;
            this.agv3steps_LB.Location = new System.Drawing.Point(297, 13);
            this.agv3steps_LB.Name = "agv3steps_LB";
            this.agv3steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv3steps_LB.TabIndex = 22;
            this.agv3steps_LB.Text = "label3";
            // 
            // refresh_label
            // 
            this.refresh_label.AutoSize = true;
            this.refresh_label.Location = new System.Drawing.Point(6, 49);
            this.refresh_label.Name = "refresh_label";
            this.refresh_label.Size = new System.Drawing.Size(35, 13);
            this.refresh_label.TabIndex = 19;
            this.refresh_label.Text = "label1";
            // 
            // agv4steps_LB
            // 
            this.agv4steps_LB.AutoSize = true;
            this.agv4steps_LB.Location = new System.Drawing.Point(297, 30);
            this.agv4steps_LB.Name = "agv4steps_LB";
            this.agv4steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv4steps_LB.TabIndex = 23;
            this.agv4steps_LB.Text = "label4";
            // 
            // agv1steps_LB
            // 
            this.agv1steps_LB.AutoSize = true;
            this.agv1steps_LB.Location = new System.Drawing.Point(6, 16);
            this.agv1steps_LB.Name = "agv1steps_LB";
            this.agv1steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv1steps_LB.TabIndex = 20;
            this.agv1steps_LB.Text = "label1";
            // 
            // agv5steps_LB
            // 
            this.agv5steps_LB.AutoSize = true;
            this.agv5steps_LB.Location = new System.Drawing.Point(297, 46);
            this.agv5steps_LB.Name = "agv5steps_LB";
            this.agv5steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv5steps_LB.TabIndex = 24;
            this.agv5steps_LB.Text = "label5";
            // 
            // agv2steps_LB
            // 
            this.agv2steps_LB.AutoSize = true;
            this.agv2steps_LB.Location = new System.Drawing.Point(6, 32);
            this.agv2steps_LB.Name = "agv2steps_LB";
            this.agv2steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv2steps_LB.TabIndex = 21;
            this.agv2steps_LB.Text = "label2";
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.gb_agvs);
            this.gb_settings.Controls.Add(this.rb_wall);
            this.gb_settings.Controls.Add(this.rb_start);
            this.gb_settings.Controls.Add(this.rb_stop);
            this.gb_settings.Controls.Add(this.rb_load);
            this.gb_settings.Location = new System.Drawing.Point(3, 5);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Size = new System.Drawing.Size(216, 65);
            this.gb_settings.TabIndex = 25;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Settings";
            // 
            // gb_agvs
            // 
            this.gb_agvs.Controls.Add(this.nUD_AGVs);
            this.gb_agvs.Location = new System.Drawing.Point(145, 13);
            this.gb_agvs.Name = "gb_agvs";
            this.gb_agvs.Size = new System.Drawing.Size(65, 43);
            this.gb_agvs.TabIndex = 18;
            this.gb_agvs.TabStop = false;
            this.gb_agvs.Text = "AGVs";
            // 
            // nUD_AGVs
            // 
            this.nUD_AGVs.Location = new System.Drawing.Point(16, 17);
            this.nUD_AGVs.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUD_AGVs.Name = "nUD_AGVs";
            this.nUD_AGVs.Size = new System.Drawing.Size(30, 20);
            this.nUD_AGVs.TabIndex = 10;
            this.nUD_AGVs.ValueChanged += new System.EventHandler(this.nUD_AGVs_ValueChanged);
            // 
            // rb_wall
            // 
            this.rb_wall.AutoSize = true;
            this.rb_wall.Location = new System.Drawing.Point(9, 19);
            this.rb_wall.Name = "rb_wall";
            this.rb_wall.Size = new System.Drawing.Size(46, 17);
            this.rb_wall.TabIndex = 4;
            this.rb_wall.TabStop = true;
            this.rb_wall.Text = "Wall";
            this.rb_wall.UseVisualStyleBackColor = true;
            // 
            // rb_start
            // 
            this.rb_start.AutoSize = true;
            this.rb_start.Location = new System.Drawing.Point(91, 19);
            this.rb_start.Name = "rb_start";
            this.rb_start.Size = new System.Drawing.Size(47, 17);
            this.rb_start.TabIndex = 4;
            this.rb_start.TabStop = true;
            this.rb_start.Text = "Start";
            this.rb_start.UseVisualStyleBackColor = true;
            // 
            // rb_stop
            // 
            this.rb_stop.AutoSize = true;
            this.rb_stop.Location = new System.Drawing.Point(91, 42);
            this.rb_stop.Name = "rb_stop";
            this.rb_stop.Size = new System.Drawing.Size(47, 17);
            this.rb_stop.TabIndex = 4;
            this.rb_stop.TabStop = true;
            this.rb_stop.Text = "Stop";
            this.rb_stop.UseVisualStyleBackColor = true;
            // 
            // rb_load
            // 
            this.rb_load.AutoSize = true;
            this.rb_load.Location = new System.Drawing.Point(9, 42);
            this.rb_load.Name = "rb_load";
            this.rb_load.Size = new System.Drawing.Size(49, 17);
            this.rb_load.TabIndex = 17;
            this.rb_load.TabStop = true;
            this.rb_load.Text = "Load";
            this.rb_load.UseVisualStyleBackColor = true;
            // 
            // ofd_importmap
            // 
            this.ofd_importmap.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // ofd_importpic
            // 
            this.ofd_importpic.FileName = "openFileDialog1";
            // 
            // settings_menu
            // 
            this.settings_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aToolStripMenuItem,
            this.gridToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.settings_menu.Location = new System.Drawing.Point(0, 0);
            this.settings_menu.Name = "settings_menu";
            this.settings_menu.Size = new System.Drawing.Size(650, 24);
            this.settings_menu.TabIndex = 8;
            this.settings_menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportMapToolStripMenuItem,
            this.importMapToolStripMenuItem,
#if !industrial
            this.importPictureToolStripMenuItem,
#endif
            this.toolStripMenuItem1,
            this.startToolStripMenuItem,
            this.increaseSpeedToolStripMenuItem,
            this.decreaseSpeedToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.fileToolStripMenuItem.Text = "Simulation";
            // 
            // exportMapToolStripMenuItem
            // 
            this.exportMapToolStripMenuItem.Name = "exportMapToolStripMenuItem";
            this.exportMapToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.exportMapToolStripMenuItem.Text = "Export map";
            this.exportMapToolStripMenuItem.Click += new System.EventHandler(this.exportMapToolStripMenuItem_Click);
            // 
            // importMapToolStripMenuItem
            // 
            this.importMapToolStripMenuItem.Name = "importMapToolStripMenuItem";
            this.importMapToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.importMapToolStripMenuItem.Text = "Import map";
            this.importMapToolStripMenuItem.Click += new System.EventHandler(this.importMapToolStripMenuItem_Click);
#if !industrial
            // 
            // importPictureToolStripMenuItem
            // 
            this.importPictureToolStripMenuItem.Name = "importPictureToolStripMenuItem";
            this.importPictureToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.importPictureToolStripMenuItem.Text = "Import picture";
            this.importPictureToolStripMenuItem.Click += new System.EventHandler(this.importPictureToolStripMenuItem_Click);
#endif
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(293, 6);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeyDisplayString = "(Space)";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // increaseSpeedToolStripMenuItem
            // 
            this.increaseSpeedToolStripMenuItem.Name = "increaseSpeedToolStripMenuItem";
            this.increaseSpeedToolStripMenuItem.ShortcutKeyDisplayString = "(UP Arrow)";
            this.increaseSpeedToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.increaseSpeedToolStripMenuItem.Text = "Increase animation delay";
            this.increaseSpeedToolStripMenuItem.Click += new System.EventHandler(this.increaseSpeedToolStripMenuItem_Click);
            // 
            // decreaseSpeedToolStripMenuItem
            // 
            this.decreaseSpeedToolStripMenuItem.Name = "decreaseSpeedToolStripMenuItem";
            this.decreaseSpeedToolStripMenuItem.ShortcutKeyDisplayString = "(DOWN Arrow)";
            this.decreaseSpeedToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.decreaseSpeedToolStripMenuItem.Text = "Decrease animation delay";
            this.decreaseSpeedToolStripMenuItem.Click += new System.EventHandler(this.decreaseSpeedToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametresToolStripMenuItem,
            this.heuristicModeToolStripMenuItem});
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.aToolStripMenuItem.Text = "Algorithm";
            // 
            // parametresToolStripMenuItem
            // 
            this.parametresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useRecursiveToolStripMenuItem,
            this.crossAdjacentPointToolStripMenuItem,
            this.crossCornerToolStripMenuItem});
            this.parametresToolStripMenuItem.Name = "parametresToolStripMenuItem";
            this.parametresToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.parametresToolStripMenuItem.Text = "Parametres";
            // 
            // useRecursiveToolStripMenuItem
            // 
            this.useRecursiveToolStripMenuItem.Name = "useRecursiveToolStripMenuItem";
            this.useRecursiveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.useRecursiveToolStripMenuItem.Text = "Use Recursive";
            this.useRecursiveToolStripMenuItem.Click += new System.EventHandler(this.useRecursiveToolStripMenuItem_Click);
            // 
            // crossAdjacentPointToolStripMenuItem
            // 
            this.crossAdjacentPointToolStripMenuItem.Name = "crossAdjacentPointToolStripMenuItem";
            this.crossAdjacentPointToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.crossAdjacentPointToolStripMenuItem.Text = "Cross Adjacent Point";
            this.crossAdjacentPointToolStripMenuItem.Click += new System.EventHandler(this.crossAdjacentPointToolStripMenuItem_Click);
            // 
            // crossCornerToolStripMenuItem
            // 
            this.crossCornerToolStripMenuItem.Name = "crossCornerToolStripMenuItem";
            this.crossCornerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.crossCornerToolStripMenuItem.Text = "Cross Corner";
            this.crossCornerToolStripMenuItem.Click += new System.EventHandler(this.crossCornerToolStripMenuItem_Click);
            // 
            // heuristicModeToolStripMenuItem
            // 
            this.heuristicModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manhattanToolStripMenuItem,
            this.euclideanToolStripMenuItem,
            this.chebyshevToolStripMenuItem});
            this.heuristicModeToolStripMenuItem.Name = "heuristicModeToolStripMenuItem";
            this.heuristicModeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.heuristicModeToolStripMenuItem.Text = "Heuristic Mode";
            // 
            // manhattanToolStripMenuItem
            // 
            this.manhattanToolStripMenuItem.Name = "manhattanToolStripMenuItem";
            this.manhattanToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.manhattanToolStripMenuItem.Text = "Manhattan";
            this.manhattanToolStripMenuItem.Click += new System.EventHandler(this.manhattanToolStripMenuItem_Click);
            // 
            // euclideanToolStripMenuItem
            // 
            this.euclideanToolStripMenuItem.Name = "euclideanToolStripMenuItem";
            this.euclideanToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.euclideanToolStripMenuItem.Text = "Euclidean";
            this.euclideanToolStripMenuItem.Click += new System.EventHandler(this.euclideanToolStripMenuItem_Click);
            // 
            // chebyshevToolStripMenuItem
            // 
            this.chebyshevToolStripMenuItem.Name = "chebyshevToolStripMenuItem";
            this.chebyshevToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.chebyshevToolStripMenuItem.Text = "Chebyshev";
            this.chebyshevToolStripMenuItem.Click += new System.EventHandler(this.chebyshevToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.borderColorToolStripMenuItem,
            this.resolutionToolStripMenuItem,
            #if !industrial
            this.implementGoogleMapsToolStripMenuItem,
#endif
            this.toolStripMenuItem2,
            this.clearToolStripMenuItem});
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.gridToolStripMenuItem.Text = "Grid";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepsToolStripMenuItem,
            this.linesToolStripMenuItem,
            this.dotsToolStripMenuItem,
            this.bordersToolStripMenuItem,
            this.highlightOverCurrentBoxToolStripMenuItem,
            this.aGVIndexToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.showToolStripMenuItem.Text = "Show...";
            // 
            // stepsToolStripMenuItem
            // 
            this.stepsToolStripMenuItem.Name = "stepsToolStripMenuItem";
            this.stepsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.stepsToolStripMenuItem.Text = "...Steps";
            this.stepsToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // linesToolStripMenuItem
            // 
            this.linesToolStripMenuItem.Name = "linesToolStripMenuItem";
            this.linesToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.linesToolStripMenuItem.Text = "...Lines";
            this.linesToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // dotsToolStripMenuItem
            // 
            this.dotsToolStripMenuItem.Name = "dotsToolStripMenuItem";
            this.dotsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.dotsToolStripMenuItem.Text = "...Dots";
            this.dotsToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // bordersToolStripMenuItem
            // 
            this.bordersToolStripMenuItem.Name = "bordersToolStripMenuItem";
            this.bordersToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.bordersToolStripMenuItem.Text = "...Borders";
            this.bordersToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // highlightOverCurrentBoxToolStripMenuItem
            // 
            this.highlightOverCurrentBoxToolStripMenuItem.Name = "highlightOverCurrentBoxToolStripMenuItem";
            this.highlightOverCurrentBoxToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.highlightOverCurrentBoxToolStripMenuItem.Text = "...Highlight over current box";
            this.highlightOverCurrentBoxToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // aGVIndexToolStripMenuItem
            // 
            this.aGVIndexToolStripMenuItem.Name = "aGVIndexToolStripMenuItem";
            this.aGVIndexToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.aGVIndexToolStripMenuItem.Text = "...AGV index";
            this.aGVIndexToolStripMenuItem.Click += new System.EventHandler(this.stepsToolStripMenuItem_Click);
            // 
            // borderColorToolStripMenuItem
            // 
            this.borderColorToolStripMenuItem.Name = "borderColorToolStripMenuItem";
            this.borderColorToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.borderColorToolStripMenuItem.Text = "Border Color";
            this.borderColorToolStripMenuItem.Click += new System.EventHandler(this.borderColorToolStripMenuItem_Click);
            // 
            // resolutionToolStripMenuItem
            // 
            this.resolutionToolStripMenuItem.Name = "resolutionToolStripMenuItem";
            this.resolutionToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.resolutionToolStripMenuItem.Text = "Resolution";
            this.resolutionToolStripMenuItem.Click += new System.EventHandler(this.resolutionToolStripMenuItem_Click);
#if !industrial
            // 
            // implementGoogleMapsToolStripMenuItem
            // 
            this.implementGoogleMapsToolStripMenuItem.Name = "implementGoogleMapsToolStripMenuItem";
            this.implementGoogleMapsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.implementGoogleMapsToolStripMenuItem.Text = "Implement Google Maps";
            this.implementGoogleMapsToolStripMenuItem.Click += new System.EventHandler(this.implementGoogleMapsToolStripMenuItem_Click);
#endif
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(138, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wallsToolStripMenuItem,
            this.allToolStripMenuItem,
            this.borderColorToolStripMenuItem1});
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // wallsToolStripMenuItem
            // 
            this.wallsToolStripMenuItem.Name = "wallsToolStripMenuItem";
            this.wallsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.wallsToolStripMenuItem.Text = "Walls";
            this.wallsToolStripMenuItem.Click += new System.EventHandler(this.wallsToolStripMenuItem_Click);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.ShortcutKeyDisplayString = "(F5)";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // borderColorToolStripMenuItem1
            // 
            this.borderColorToolStripMenuItem1.Name = "borderColorToolStripMenuItem1";
            this.borderColorToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.borderColorToolStripMenuItem1.Text = "Border Color";
            this.borderColorToolStripMenuItem1.Click += new System.EventHandler(this.borderColorToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(650, 335);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.settings_menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.settings_menu;
            this.MaximizeBox = false;
            this.Name = "main_form";
            this.Text = "kagv Simulation-Agri";
            this.Load += new System.EventHandler(this.main_form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.main_form_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.main_form_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.main_form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.main_form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.main_form_MouseUp);
            this.menuPanel.ResumeLayout(false);
            this.gb_type.ResumeLayout(false);
            this.gb_monitor.ResumeLayout(false);
            this.gb_monitor.PerformLayout();
            this.gb_settings.ResumeLayout(false);
            this.gb_settings.PerformLayout();
            this.gb_agvs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_AGVs)).EndInit();
            this.settings_menu.ResumeLayout(false);
            this.settings_menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer0;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.RadioButton rb_stop;
        private System.Windows.Forms.RadioButton rb_start;
        private System.Windows.Forms.RadioButton rb_wall;
        private System.Windows.Forms.ToolTip tp_info;
        private System.Windows.Forms.SaveFileDialog sfd_exportmap;
        private System.Windows.Forms.OpenFileDialog ofd_importmap;
        private System.Windows.Forms.NumericUpDown nUD_AGVs;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.RadioButton rb_load;
        private System.Windows.Forms.OpenFileDialog ofd_importpic;
        private System.Windows.Forms.MenuStrip settings_menu;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useRecursiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossAdjacentPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossCornerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heuristicModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manhattanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem euclideanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chebyshevToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderColorToolStripMenuItem;
        private System.Windows.Forms.ColorDialog cd_grid;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMapToolStripMenuItem;
#if !industrial
        private System.Windows.Forms.ToolStripMenuItem importPictureToolStripMenuItem;
#endif
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label refresh_label;
        private System.Windows.Forms.ToolStripMenuItem increaseSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
#if !industrial
        private System.Windows.Forms.ToolStripMenuItem implementGoogleMapsToolStripMenuItem;
#endif
        private System.Windows.Forms.Label agv5steps_LB;
        private System.Windows.Forms.Label agv4steps_LB;
        private System.Windows.Forms.Label agv3steps_LB;
        private System.Windows.Forms.Label agv2steps_LB;
        private System.Windows.Forms.Label agv1steps_LB;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.GroupBox gb_agvs;
        private System.Windows.Forms.GroupBox gb_monitor;
        private System.Windows.Forms.ToolStripMenuItem bordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderColorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem highlightOverCurrentBoxToolStripMenuItem;
        private System.Windows.Forms.GroupBox gb_type;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.ToolStripMenuItem aGVIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resolutionToolStripMenuItem;

    }
}

