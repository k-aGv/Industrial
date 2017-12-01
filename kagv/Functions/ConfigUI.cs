/*!
The Apache License 2.0 License

Copyright (c) 2017 Dimitris Katikaridis <dkatikaridis@gmail.com>,Giannis Menekses <johnmenex@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace kagv {

    public partial class MainForm  {

        private void ConfigUi() {

            if (Globals.SemiTransparency)
                Globals.SemiTransparent = Color.FromArgb(Globals.Opacity, Color.WhiteSmoke);

            for (int i = 0; i < _startPos.Count; i++) {
                _AGVs[i] = new Vehicle(this,wms) {
                    ID = i
                };
            }

            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.LeftBarOffset;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset + 7; //+7 for borders
            Size = new Size(Width, Height + Globals.BottomBarOffset);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            StackTrace trace = new StackTrace();
            if (trace.GetFrame(2).GetMethod().Name.Contains("MenuItem_Click") || Globals.FirstFormLoad)
            {
                stepsToolStripMenuItem.Checked = false;
                linesToolStripMenuItem.Checked =
                dotsToolStripMenuItem.Checked =
                bordersToolStripMenuItem.Checked =
                aGVIndexToolStripMenuItem.Checked =
                highlightOverCurrentBoxToolStripMenuItem.Checked = true;

                timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = 50;
                Globals.AStarWeight = 0.5;
            }
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
