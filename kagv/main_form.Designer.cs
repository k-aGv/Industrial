﻿namespace kagv {
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("We will add vars here");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("more var");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("even moar");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("even even moar");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("EVEN MOAR");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("me think itz enuf");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("--------------");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Debug", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Emissions", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.timer0 = new System.Windows.Forms.Timer(this.components);
            this.menuPanel = new System.Windows.Forms.Panel();
            this.gb_type = new System.Windows.Forms.GroupBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.gb_monitor = new System.Windows.Forms.GroupBox();
            this.loads_label = new System.Windows.Forms.Label();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nud_weight = new System.Windows.Forms.NumericUpDown();
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
            this.importImageLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysCrossMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.neverCrossMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.noObstaclesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.atLeastOneMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.priorityRulesbetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridBlockLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cd_grid = new System.Windows.Forms.ColorDialog();
            this.panel_resize = new System.Windows.Forms.Panel();
            this.lb_blockside = new System.Windows.Forms.Label();
            this.nud_side = new System.Windows.Forms.NumericUpDown();
            this.lb_height = new System.Windows.Forms.Label();
            this.lb_width = new System.Windows.Forms.Label();
            this.btn_rightup = new System.Windows.Forms.Button();
            this.btn_rightdown = new System.Windows.Forms.Button();
            this.btn_leftdown = new System.Windows.Forms.Button();
            this.btn_leftup = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tree_stats = new BufferedTreeView();
            this.menuPanel.SuspendLayout();
            this.gb_type.SuspendLayout();
            this.gb_monitor.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.gb_agvs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_AGVs)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).BeginInit();
            this.settings_menu.SuspendLayout();
            this.panel_resize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_side)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer0
            // 
            this.timer0.Tick += new System.EventHandler(this.timer0_Tick);
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuPanel.Controls.Add(this.gb_type);
            this.menuPanel.Controls.Add(this.gb_monitor);
            this.menuPanel.Controls.Add(this.gb_settings);
            this.menuPanel.Controls.Add(this.groupBox1);
            this.menuPanel.Location = new System.Drawing.Point(191, 27);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(656, 75);
            this.menuPanel.TabIndex = 7;
            // 
            // gb_type
            // 
            this.gb_type.Controls.Add(this.cb_type);
            this.gb_type.Location = new System.Drawing.Point(226, 5);
            this.gb_type.Name = "gb_type";
            this.gb_type.Size = new System.Drawing.Size(68, 45);
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
            this.gb_monitor.Controls.Add(this.loads_label);
            this.gb_monitor.Controls.Add(this.agv3steps_LB);
            this.gb_monitor.Controls.Add(this.refresh_label);
            this.gb_monitor.Controls.Add(this.agv4steps_LB);
            this.gb_monitor.Controls.Add(this.agv1steps_LB);
            this.gb_monitor.Controls.Add(this.agv5steps_LB);
            this.gb_monitor.Controls.Add(this.agv2steps_LB);
            this.gb_monitor.Location = new System.Drawing.Point(376, 5);
            this.gb_monitor.Name = "gb_monitor";
            this.gb_monitor.Size = new System.Drawing.Size(262, 65);
            this.gb_monitor.TabIndex = 26;
            this.gb_monitor.TabStop = false;
            this.gb_monitor.Text = "Monitor";
            // 
            // loads_label
            // 
            this.loads_label.AutoSize = true;
            this.loads_label.Location = new System.Drawing.Point(77, 52);
            this.loads_label.Name = "loads_label";
            this.loads_label.Size = new System.Drawing.Size(0, 13);
            this.loads_label.TabIndex = 30;
            // 
            // agv3steps_LB
            // 
            this.agv3steps_LB.AutoSize = true;
            this.agv3steps_LB.Location = new System.Drawing.Point(272, 15);
            this.agv3steps_LB.Name = "agv3steps_LB";
            this.agv3steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv3steps_LB.TabIndex = 22;
            this.agv3steps_LB.Text = "AGV3";
            // 
            // refresh_label
            // 
            this.refresh_label.AutoSize = true;
            this.refresh_label.Location = new System.Drawing.Point(6, 49);
            this.refresh_label.Name = "refresh_label";
            this.refresh_label.Size = new System.Drawing.Size(65, 13);
            this.refresh_label.TabIndex = 19;
            this.refresh_label.Text = "Refresh rate";
            // 
            // agv4steps_LB
            // 
            this.agv4steps_LB.AutoSize = true;
            this.agv4steps_LB.Location = new System.Drawing.Point(272, 32);
            this.agv4steps_LB.Name = "agv4steps_LB";
            this.agv4steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv4steps_LB.TabIndex = 23;
            this.agv4steps_LB.Text = "AGV4";
            // 
            // agv1steps_LB
            // 
            this.agv1steps_LB.AutoSize = true;
            this.agv1steps_LB.Location = new System.Drawing.Point(6, 16);
            this.agv1steps_LB.Name = "agv1steps_LB";
            this.agv1steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv1steps_LB.TabIndex = 20;
            this.agv1steps_LB.Text = "AGV1";
            // 
            // agv5steps_LB
            // 
            this.agv5steps_LB.AutoSize = true;
            this.agv5steps_LB.Location = new System.Drawing.Point(272, 48);
            this.agv5steps_LB.Name = "agv5steps_LB";
            this.agv5steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv5steps_LB.TabIndex = 24;
            this.agv5steps_LB.Text = "AGV5";
            // 
            // agv2steps_LB
            // 
            this.agv2steps_LB.AutoSize = true;
            this.agv2steps_LB.Location = new System.Drawing.Point(6, 32);
            this.agv2steps_LB.Name = "agv2steps_LB";
            this.agv2steps_LB.Size = new System.Drawing.Size(35, 13);
            this.agv2steps_LB.TabIndex = 21;
            this.agv2steps_LB.Text = "AGV2";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nud_weight);
            this.groupBox1.Location = new System.Drawing.Point(301, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(69, 45);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "A* Weight";
            // 
            // nud_weight
            // 
            this.nud_weight.DecimalPlaces = 2;
            this.nud_weight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nud_weight.Location = new System.Drawing.Point(3, 20);
            this.nud_weight.Name = "nud_weight";
            this.nud_weight.Size = new System.Drawing.Size(64, 20);
            this.nud_weight.TabIndex = 28;
            this.nud_weight.ValueChanged += new System.EventHandler(this.nud_weight_ValueChanged);
            // 
            // ofd_importmap
            // 
            this.ofd_importmap.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
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
            this.aboutToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.settings_menu.Location = new System.Drawing.Point(0, 0);
            this.settings_menu.Name = "settings_menu";
            this.settings_menu.Size = new System.Drawing.Size(804, 24);
            this.settings_menu.TabIndex = 8;
            this.settings_menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportMapToolStripMenuItem,
            this.importMapToolStripMenuItem,
            this.toolStripMenuItem1,
            this.importImageLayoutToolStripMenuItem,
            this.toolStripSeparator1,
            this.startToolStripMenuItem,
            this.increaseSpeedToolStripMenuItem,
            this.decreaseSpeedToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.fileToolStripMenuItem.Text = "Simulation";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
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
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(293, 6);
            // 
            // importImageLayoutToolStripMenuItem
            // 
            this.importImageLayoutToolStripMenuItem.Name = "importImageLayoutToolStripMenuItem";
            this.importImageLayoutToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.importImageLayoutToolStripMenuItem.Text = "Import image layout";
            this.importImageLayoutToolStripMenuItem.Click += new System.EventHandler(this.importImageLayoutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(293, 6);
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
            this.alwaysCrossMenu,
            this.neverCrossMenu,
            this.noObstaclesMenu,
            this.atLeastOneMenu,
            this.toolStripSeparator2,
            this.priorityRulesbetaToolStripMenuItem});
            this.parametresToolStripMenuItem.Name = "parametresToolStripMenuItem";
            this.parametresToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.parametresToolStripMenuItem.Text = "Parametres";
            // 
            // alwaysCrossMenu
            // 
            this.alwaysCrossMenu.Name = "alwaysCrossMenu";
            this.alwaysCrossMenu.Size = new System.Drawing.Size(299, 22);
            this.alwaysCrossMenu.Text = "Always cross corners";
            this.alwaysCrossMenu.Click += new System.EventHandler(this.useRecursiveToolStripMenuItem_Click);
            // 
            // neverCrossMenu
            // 
            this.neverCrossMenu.Name = "neverCrossMenu";
            this.neverCrossMenu.Size = new System.Drawing.Size(299, 22);
            this.neverCrossMenu.Text = "Never cross corners";
            this.neverCrossMenu.Click += new System.EventHandler(this.crossAdjacentPointToolStripMenuItem_Click);
            // 
            // noObstaclesMenu
            // 
            this.noObstaclesMenu.Name = "noObstaclesMenu";
            this.noObstaclesMenu.Size = new System.Drawing.Size(299, 22);
            this.noObstaclesMenu.Text = "Cross corner only when no obstacles";
            this.noObstaclesMenu.Click += new System.EventHandler(this.crossCornerOnlyWhenNoObstaclesToolStripMenuItem_Click);
            // 
            // atLeastOneMenu
            // 
            this.atLeastOneMenu.Name = "atLeastOneMenu";
            this.atLeastOneMenu.Size = new System.Drawing.Size(299, 22);
            this.atLeastOneMenu.Text = "Cross corner only if at least one is walkable";
            this.atLeastOneMenu.Click += new System.EventHandler(this.crossCornerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(296, 6);
            // 
            // priorityRulesbetaToolStripMenuItem
            // 
            this.priorityRulesbetaToolStripMenuItem.Name = "priorityRulesbetaToolStripMenuItem";
            this.priorityRulesbetaToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.priorityRulesbetaToolStripMenuItem.Text = "Priority rules (beta)";
            this.priorityRulesbetaToolStripMenuItem.Click += new System.EventHandler(this.priorityRulesbetaToolStripMenuItem_Click);
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
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGridBlockLocationsToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // showGridBlockLocationsToolStripMenuItem
            // 
            this.showGridBlockLocationsToolStripMenuItem.Name = "showGridBlockLocationsToolStripMenuItem";
            this.showGridBlockLocationsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.showGridBlockLocationsToolStripMenuItem.Text = "Show grid block locations";
            this.showGridBlockLocationsToolStripMenuItem.MouseEnter += new System.EventHandler(this.showGridBlockLocationsToolStripMenuItem_MouseEnter);
            this.showGridBlockLocationsToolStripMenuItem.MouseLeave += new System.EventHandler(this.showGridBlockLocationsToolStripMenuItem_MouseLeave);
            // 
            // panel_resize
            // 
            this.panel_resize.Controls.Add(this.lb_blockside);
            this.panel_resize.Controls.Add(this.nud_side);
            this.panel_resize.Controls.Add(this.lb_height);
            this.panel_resize.Controls.Add(this.lb_width);
            this.panel_resize.Controls.Add(this.btn_rightup);
            this.panel_resize.Controls.Add(this.btn_rightdown);
            this.panel_resize.Controls.Add(this.btn_leftdown);
            this.panel_resize.Controls.Add(this.btn_leftup);
            this.panel_resize.Controls.Add(this.btn_down);
            this.panel_resize.Controls.Add(this.btn_up);
            this.panel_resize.Controls.Add(this.btn_left);
            this.panel_resize.Controls.Add(this.btn_right);
            this.panel_resize.Location = new System.Drawing.Point(226, 176);
            this.panel_resize.Name = "panel_resize";
            this.panel_resize.Size = new System.Drawing.Size(259, 202);
            this.panel_resize.TabIndex = 9;
            // 
            // lb_blockside
            // 
            this.lb_blockside.AutoSize = true;
            this.lb_blockside.Location = new System.Drawing.Point(4, 39);
            this.lb_blockside.Name = "lb_blockside";
            this.lb_blockside.Size = new System.Drawing.Size(61, 13);
            this.lb_blockside.TabIndex = 9;
            this.lb_blockside.Text = "Block Side:";
            // 
            // nud_side
            // 
            this.nud_side.Location = new System.Drawing.Point(71, 37);
            this.nud_side.Name = "nud_side";
            this.nud_side.Size = new System.Drawing.Size(41, 20);
            this.nud_side.TabIndex = 8;
            this.nud_side.ValueChanged += new System.EventHandler(this.nud_side_ValueChanged);
            // 
            // lb_height
            // 
            this.lb_height.AutoSize = true;
            this.lb_height.Location = new System.Drawing.Point(4, 21);
            this.lb_height.Name = "lb_height";
            this.lb_height.Size = new System.Drawing.Size(75, 13);
            this.lb_height.TabIndex = 7;
            this.lb_height.Text = "Height blocks:";
            // 
            // lb_width
            // 
            this.lb_width.AutoSize = true;
            this.lb_width.Location = new System.Drawing.Point(4, 4);
            this.lb_width.Name = "lb_width";
            this.lb_width.Size = new System.Drawing.Size(72, 13);
            this.lb_width.TabIndex = 6;
            this.lb_width.Text = "Width blocks:";
            // 
            // btn_rightup
            // 
            this.btn_rightup.Location = new System.Drawing.Point(165, 77);
            this.btn_rightup.Name = "btn_rightup";
            this.btn_rightup.Size = new System.Drawing.Size(26, 23);
            this.btn_rightup.TabIndex = 5;
            this.btn_rightup.Text = "/";
            this.btn_rightup.UseVisualStyleBackColor = true;
            this.btn_rightup.Click += new System.EventHandler(this.btn_rightup_Click);
            // 
            // btn_rightdown
            // 
            this.btn_rightdown.Location = new System.Drawing.Point(165, 157);
            this.btn_rightdown.Name = "btn_rightdown";
            this.btn_rightdown.Size = new System.Drawing.Size(26, 23);
            this.btn_rightdown.TabIndex = 5;
            this.btn_rightdown.Text = "\\";
            this.btn_rightdown.UseVisualStyleBackColor = true;
            this.btn_rightdown.Click += new System.EventHandler(this.btn_rightdown_Click);
            // 
            // btn_leftdown
            // 
            this.btn_leftdown.Location = new System.Drawing.Point(66, 157);
            this.btn_leftdown.Name = "btn_leftdown";
            this.btn_leftdown.Size = new System.Drawing.Size(26, 23);
            this.btn_leftdown.TabIndex = 5;
            this.btn_leftdown.Text = "/";
            this.btn_leftdown.UseVisualStyleBackColor = true;
            this.btn_leftdown.Click += new System.EventHandler(this.btn_leftdown_Click);
            // 
            // btn_leftup
            // 
            this.btn_leftup.Location = new System.Drawing.Point(66, 77);
            this.btn_leftup.Name = "btn_leftup";
            this.btn_leftup.Size = new System.Drawing.Size(26, 23);
            this.btn_leftup.TabIndex = 5;
            this.btn_leftup.Text = "\\";
            this.btn_leftup.UseVisualStyleBackColor = true;
            this.btn_leftup.Click += new System.EventHandler(this.btn_leftup_Click);
            // 
            // btn_down
            // 
            this.btn_down.Location = new System.Drawing.Point(98, 153);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(64, 45);
            this.btn_down.TabIndex = 1;
            this.btn_down.Text = "Increase height";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(98, 60);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(64, 45);
            this.btn_up.TabIndex = 2;
            this.btn_up.Text = "Decrease height";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_left
            // 
            this.btn_left.Location = new System.Drawing.Point(28, 106);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(64, 45);
            this.btn_left.TabIndex = 3;
            this.btn_left.Text = "Decrease width";
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_right
            // 
            this.btn_right.Location = new System.Drawing.Point(168, 106);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(64, 45);
            this.btn_right.TabIndex = 4;
            this.btn_right.Text = "Increase width";
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(190, 17);
            this.toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
            // 
            // tree_stats
            // 
            this.tree_stats.Location = new System.Drawing.Point(0, 27);
            this.tree_stats.Name = "tree_stats";
            treeNode1.Name = "Node0";
            treeNode1.Text = "We will add vars here";
            treeNode2.Name = "Node1";
            treeNode2.Text = "more var";
            treeNode3.Name = "Node2";
            treeNode3.Text = "even moar";
            treeNode4.Name = "Node3";
            treeNode4.Text = "even even moar";
            treeNode5.Name = "Node4";
            treeNode5.Text = "EVEN MOAR";
            treeNode6.Name = "Node5";
            treeNode6.Text = "me think itz enuf";
            treeNode7.Name = "Node0";
            treeNode7.Text = "--------------";
            treeNode8.Checked = true;
            treeNode8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            treeNode8.Name = "node_debug";
            treeNode8.Text = "Debug";
            treeNode9.Name = "Node2";
            treeNode9.Text = "Node2";
            treeNode10.Name = "Node3";
            treeNode10.Text = "Node3";
            treeNode11.Name = "Node4";
            treeNode11.Text = "Node4";
            treeNode12.Name = "Node5";
            treeNode12.Text = "Node5";
            treeNode13.Name = "Node6";
            treeNode13.Text = "Node6";
            treeNode14.Name = "node_emissions";
            treeNode14.Text = "Emissions";
            this.tree_stats.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode14});
            this.tree_stats.Size = new System.Drawing.Size(150, 280);
            this.tree_stats.TabIndex = 12;
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(804, 466);
            this.Controls.Add(this.tree_stats);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel_resize);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.settings_menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.settings_menu;
            this.MaximizeBox = false;
            this.Name = "main_form";
            this.Text = "kagv Simulation-Industrial";
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
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_weight)).EndInit();
            this.settings_menu.ResumeLayout(false);
            this.settings_menu.PerformLayout();
            this.panel_resize.ResumeLayout(false);
            this.panel_resize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_side)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem alwaysCrossMenu;
        private System.Windows.Forms.ToolStripMenuItem neverCrossMenu;
        private System.Windows.Forms.ToolStripMenuItem atLeastOneMenu;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label refresh_label;
        private System.Windows.Forms.ToolStripMenuItem increaseSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
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
        private System.Windows.Forms.ToolStripMenuItem importImageLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem priorityRulesbetaToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown nud_weight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem noObstaclesMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label loads_label;
        private System.Windows.Forms.Panel panel_resize;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btn_rightup;
        private System.Windows.Forms.Button btn_rightdown;
        private System.Windows.Forms.Button btn_leftdown;
        private System.Windows.Forms.Button btn_leftup;
        private System.Windows.Forms.Label lb_height;
        private System.Windows.Forms.Label lb_width;
        private System.Windows.Forms.Label lb_blockside;
        private System.Windows.Forms.NumericUpDown nud_side;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridBlockLocationsToolStripMenuItem;
        private BufferedTreeView tree_stats;
    }
}

