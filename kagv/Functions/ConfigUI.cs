using System;
using System.Drawing;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm  {

        private void ConfigUi() {

            if (Globals.SemiTransparency)
                Globals.SemiTransparent = Color.FromArgb(Globals.Opacity, Color.WhiteSmoke);

            for (int i = 0; i < _startPos.Count; i++) {
                _AGVs[i] = new Vehicle(this) {
                    ID = i
                };
            }

            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.LeftBarOffset;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset + 7; //+7 for borders
            Size = new Size(Width, Height + Globals.BottomBarOffset);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //Transparent and SemiTransparent feature serves the agri/industrial branch recursively
            importImageLayoutToolStripMenuItem.Enabled = Globals.SemiTransparency;

            importImageLayoutToolStripMenuItem.Text = importImageLayoutToolStripMenuItem.Enabled ? "Import image layout" : "Semi Transparency feature is disabled";

            stepsToolStripMenuItem.Checked = false;
            linesToolStripMenuItem.Checked =
            dotsToolStripMenuItem.Checked =
            bordersToolStripMenuItem.Checked =
            aGVIndexToolStripMenuItem.Checked =
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;


            Text = "K-aGv2 Simulator (Industrial branch)";
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = Globals.TimerInterval;

            //Do not show the START menu because there is no valid path yet
            TriggerStartMenu(false);

            rb_start.Checked = true;
            BackColor = Color.DarkGray;

            CenterToScreen();

            alwaysCrossMenu.Checked = _alwaysCross;
            atLeastOneMenu.Checked = _atLeastOneObstacle;
            neverCrossMenu.Checked = _never;
            noObstaclesMenu.Checked = _ifNoObstacles;

            manhattanToolStripMenuItem.Checked = true;

            tree_stats.Location = new Point(0, 25);
            tree_stats.Height = statusStrip1.Location.Y - tree_stats.Location.Y;


            debugToolStripMenuItem.Visible = Globals.Debug;
            if (!Globals.Debug) {
                TreeNode[] tmpagvnodes = tree_stats.Nodes.Find("node_debug", false);
                tmpagvnodes[0].Text = "Debug is not available";
                for (int i = 0; i < tmpagvnodes[0].Nodes.Count; i++) {
                    tmpagvnodes[0].Nodes[i].Text = "Debug is not available";
                    tmpagvnodes[0].Nodes[i].ForeColor = Color.Red;
                }

            }

            tree_stats.Nodes[1].Nodes[0].Text = "CO: -";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: -";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: -";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: -";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: -";


            //dynamically add the location of menupanel.
            //We have to do it dynamically because the forms size is always depended on PCs actual screen size
            menuPanel.Location = new Point(tree_stats.Width, 24 + 1);//24=menu bar Y
            menuPanel.Width = Width;

            panel_resize.Location = new Point(Width / 2 - (panel_resize.Width / 2), Height / 2 - menuPanel.Height);
            panel_resize.Visible = false;
            nud_side.BackColor = panel_resize.BackColor;

            nud_weight.Value = Convert.ToDecimal(Globals.AStarWeight);
            statusStrip1.Location = new Point(tree_stats.Width, Height - statusStrip1.Height);

            statusStrip1.BringToFront();

            _tp = new ToolTip {

                AutomaticDelay = 0,
                ReshowDelay = 0,
                InitialDelay = 0,
                AutoPopDelay = 0,
                IsBalloon = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Grid Block Information",
            };



        }
    }
}
