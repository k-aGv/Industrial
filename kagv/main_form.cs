using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace kagv {

    public partial class main_form : Form {


        public main_form() {
            InitializeComponent();
            initialization();
        }
        private void main_form_Paint(object sender, PaintEventArgs e) {
            paper = e.Graphics;
            paper.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            try {
                //draws the grid
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Normal);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Start);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.End);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Wall);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Load);

                        if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load
                            && isLoad[widthTrav, heightTrav] == 3)
                            m_rectangles[widthTrav][heightTrav].SetAsTargetted(paper);
                    }
                }

                int c = 0;
                for (int i = 0; i < StartPos.Count; i++)
                    c += AGVs[i].JumpPoints.Count;

                for (int i = 0; i < nUD_AGVs.Value; i++) {
                    AGVs[i].StepsCounter = 0;
                    if (!NoJumpPointsFound) {
                        for (int resultTrav = 0; resultTrav < c; resultTrav++) {
                            try {
                                if (linesToolStripMenuItem.Checked)
                                    AGVs[i].Paths[resultTrav].drawLine(paper);
                                if (!isMouseDown) 
                                    DrawPoints(AGVs[i].Paths[resultTrav], i);
                            } catch { }
                        }
                    }
                }

                if (aGVIndexToolStripMenuItem.Checked) {
                    for (int i = 0; i < StartPos.Count; i++) {
                        paper.DrawString("AGV:" + AGVs[i].ID,
                                         new Font("Tahoma", 8, FontStyle.Bold),
                                         new SolidBrush(Color.Red),
                                         new Point((StartPos[i].x * 20) - 10, ((StartPos[i].y * 20) + Constants.__TopBarOffset) - 20));
                    }
                }

            } catch { }

        }

        private void main_form_Load(object sender, EventArgs e) {

#if !emissionsSymbol
            gb_type.Visible=false;
#endif

            var _proc = System.Diagnostics.Process.GetCurrentProcess();
            _proc.ProcessorAffinity = new IntPtr(0x0003);//use cores 1,2 
            //ptr flag has to be (bin) 0011 so its IntPtr 0x0003

            agv1steps_LB.Text =
            agv2steps_LB.Text =
            agv3steps_LB.Text =
            agv4steps_LB.Text =
            agv5steps_LB.Text = "";


            refresh_label.Text = "Delay :" + timer0.Interval + " ms";

            nUD_AGVs.Value = 0;
            stepsToolStripMenuItem.Checked =
            linesToolStripMenuItem.Checked =
            dotsToolStripMenuItem.Checked =
            bordersToolStripMenuItem.Checked =
            aGVIndexToolStripMenuItem.Checked =
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;

            triggerStartMenu(false);

            rb_start.Checked = true;
            this.BackColor = Color.DarkGray;

            this.StartPosition = FormStartPosition.CenterScreen;

            useRecursiveToolStripMenuItem.Checked = useRecursive;
            crossCornerToolStripMenuItem.Checked = crossCorners;
            crossAdjacentPointToolStripMenuItem.Checked = crossAdjacent;
            manhattanToolStripMenuItem.Checked = true;


            menuPanel.Location = new Point(0, 24 + 1);//24=menu bar Y
            menuPanel.Width = this.Width;

            tp = new ToolTip();

            tp.AutomaticDelay = 0;
            tp.ReshowDelay = 0;
            tp.InitialDelay = 0;
            tp.AutoPopDelay = 0;
            tp.IsBalloon = true;
            tp.ToolTipIcon = ToolTipIcon.Info;
            tp.ToolTipTitle = "Grid Block Information";

        }

        private void main_form_MouseDown(object sender, MouseEventArgs e) {
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return;

            isMouseDown = true;
            if ((e.Button == MouseButtons.Left) && (rb_wall.Checked)) {
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType) {
                                case BoxType.Normal:
                                case BoxType.Wall:
                                    m_rectangles[widthTrav][heightTrav].SwitchBox();
                                    this.Invalidate();
                                    break;
                                case BoxType.Start:
                                case BoxType.End:
                                    break;
                            }
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Right) {

                Point mycoords = new Point(e.X, e.Y);
                GridBox clickedBox = null;
                bool isBorder = true;
                string currentBoxCoords = "X: - Y: -\r\n";
                string currentBoxIndex = "Index: N/A\r\n";
                string currentBoxType = "Block type: Border\r\n";
                string isPath = "Is part of path: N/A\r\n";
                bool isPathBlock = false;
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(mycoords)) {
                            currentBoxType =
                                "Block type: " +
                                m_rectangles[widthTrav][heightTrav].boxType + "\r\n";
                            currentBoxCoords =
                                "X: " +
                                m_rectangles[widthTrav][heightTrav].boxRec.X + " " +
                                "Y: " +
                                m_rectangles[widthTrav][heightTrav].boxRec.Y + "\r\n";
                            currentBoxIndex =
                                "Index: " +
                                "iX: " + widthTrav + " " + "iY: " + heightTrav + "\r\n";

                            int agv_index = 0;

                            if (StartPos != null) {
                                for (int j = 0; j < StartPos.Count; j++)
                                    for (int i = 0; i < Constants.__MaximumSteps; i++) {
                                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains
                                            (
                                                   new Point(
                                                       Convert.ToInt32(AGVs[j].Steps[i].X),
                                                       Convert.ToInt32(AGVs[j].Steps[i].Y)
                                                       )
                                            )) {
                                            isPathBlock = true;
                                            agv_index = j;
                                            i = Constants.__MaximumSteps;
                                            j = StartPos.Count;
                                        }

                                    }

                                clickedBox = m_rectangles[widthTrav][heightTrav];
                            }
                            tp.ToolTipIcon = ToolTipIcon.Info;
                            if (isPathBlock && StartPos != null) {
                                isPath = "Is part of AGV"+(agv_index)+" path";
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X
                                    , clickedBox.boxRec.Y - Constants.__TopBarOffset + 17);
                                isBorder = false;
                            } else {
                                isPath = "Is part of path:No\r\n";
                                clickedBox = new GridBox(e.X, e.Y, BoxType.Normal);
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X - 10
                                    , clickedBox.boxRec.Y - Constants.__TopBarOffset + 12);
                                isBorder = false;
                            }
                        }
                    }
                }

                if (isBorder) {
                    tp.ToolTipIcon = ToolTipIcon.Error;
                    tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                               , this
                               , e.X - 8
                               , e.Y - Constants.__TopBarOffset + 14);
                }

            }

        }

        private void main_form_MouseMove(object sender, MouseEventArgs e) {

            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            if (c > 0)
                triggerStartMenu(true);

            if (isMouseDown && rb_wall.Checked) {
                if (e.Button == MouseButtons.Left) {
                    if (m_lastBoxSelect.boxType == BoxType.Start ||
                        m_lastBoxSelect.boxType == BoxType.End)
                        return;

                    if (m_lastBoxSelect == null) {
                        for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                            for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                                if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                                    m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                                    m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                    switch (m_lastBoxType) {
                                        case BoxType.Normal:
                                        case BoxType.Wall:
                                            m_rectangles[widthTrav][heightTrav].SwitchBox();
                                            this.Invalidate();
                                            break;
                                        case BoxType.Start:
                                        case BoxType.End:
                                            break;
                                    }
                                }

                            }
                        }

                        return;
                    } else {
                        for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                            for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                                if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                                    if (m_rectangles[widthTrav][heightTrav] == m_lastBoxSelect) {
                                        return;
                                    } else {

                                        switch (m_lastBoxType) {
                                            case BoxType.Normal:
                                            case BoxType.Wall:
                                                if (m_rectangles[widthTrav][heightTrav].boxType == m_lastBoxType) {
                                                    m_rectangles[widthTrav][heightTrav].SwitchBox();
                                                    m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                    this.Invalidate();
                                                }
                                                break;
                                            case BoxType.Start:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];

                                                this.Invalidate();
                                                break;
                                            case BoxType.End:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                m_lastBoxSelect.SetEndBox();
                                                this.Invalidate();
                                                break;

                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (
                 timer0.Enabled ||
                 timer1.Enabled ||
                 timer2.Enabled ||
                 timer3.Enabled ||
                 timer4.Enabled
               )
                return;


            if (allowHighlight) {
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(new Point(e.X, e.Y))
                            && m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            if (rb_load.Checked)
                                m_rectangles[widthTrav][heightTrav].onHover(Color.FromArgb(150, Color.FromArgb(138, 109, 86)));
                            else if (rb_start.Checked)
                                m_rectangles[widthTrav][heightTrav].onHover(Color.LightGreen);
                            else if (rb_stop.Checked)
                                m_rectangles[widthTrav][heightTrav].onHover(Color.FromArgb(80, Color.FromArgb(255, 26, 26)));
                            else //wall
                                m_rectangles[widthTrav][heightTrav].onHover(Color.FromArgb(20, Color.LightGray));

                            this.Invalidate();
                        } else if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            m_rectangles[widthTrav][heightTrav].onHover(boxDefaultColor);
                            this.Invalidate();
                        }

                    }
                }
            }
        }

        private void main_form_MouseUp(object sender, MouseEventArgs e) {
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled) return;

            isMouseDown = false;

            for (int i = 0; i < StartPos.Count; i++)
                AGVs[i].StepsCounter = 0;

            if (e.Button == MouseButtons.Right) {
                tp.Hide(this);
            }
            Redraw();
            this.Invalidate();

        }


        private void nUD_AGVs_ValueChanged(object sender, EventArgs e) {
            int starts_counter = 0;
            bool removed = false;
            int[,] starts_position = new int[2, Convert.ToInt32(nUD_AGVs.Value) + 1]; //keeps the size of the array +1 in relation with the nUD

            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start) {
                        starts_position[0, starts_counter] = widthTrav;
                        starts_position[1, starts_counter] = heightTrav;
                        starts_counter++;
                    }
                    if (starts_counter > nUD_AGVs.Value) {
                        m_rectangles[starts_position[0, starts_counter - 1]][starts_position[1, starts_counter - 1]].SwitchEnd_StartToNormal(); //removes the very last
                        
                        removed = true;
                        this.Invalidate();
                    }
                }
            if (removed)
                Redraw();

        }

        private void main_form_MouseClick(object sender, MouseEventArgs e) {

            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled) return;

            Point click_coords = new Point(e.X, e.Y);
            if (!isvalid(click_coords) || e.Button != MouseButtons.Left || nUD_AGVs.Value == 0)
                return;

            if (rb_load.Checked) {
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType) {
                                case BoxType.Normal:
                                    loads++;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 1;
                                    this.Invalidate();
                                    break;
                                case BoxType.Load:
                                    loads--;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 2;
                                    this.Invalidate();
                                    break;
                                case BoxType.Wall:
                                case BoxType.Start:
                                case BoxType.End:
                                    break;
                            }
                        }


                    }
                }
            }
            if (rb_start.Checked) {

                if (nUD_AGVs.Value == 1)//Saves only the last Click position to place the Start (1 start exists)
                {
                    for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                        for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++)
                            if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                                m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();
                } else if (nUD_AGVs.Value > 1)//Deletes the start with the smallest iX - iY coords and keeps the rest
                {
                    int starts_counter = 0;
                    int[,] starts_position = new int[2, Convert.ToInt32(nUD_AGVs.Value)];


                    for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                        for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                            if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start) {
                                starts_position[0, starts_counter] = widthTrav;
                                starts_position[1, starts_counter] = heightTrav;
                                starts_counter++;
                            }
                            if (starts_counter == nUD_AGVs.Value) {
                                m_rectangles[starts_position[0, 0]][starts_position[1, 0]].SwitchEnd_StartToNormal();
                            }
                        }

                }


                //Converts the clicked box to Start point
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                            &&
                            m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + Constants.__TopBarOffset, BoxType.Start);

                           
                        }


            }
            //same for Stop
            if (rb_stop.Checked) {
                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                            m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();//allow only one end point


                for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                             &&
                            m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + Constants.__TopBarOffset, BoxType.End);
                        }
            }

           

            this.Invalidate();
        }
        //parametres
        private void useRecursiveToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            useRecursive = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();

        }

        private void crossAdjacentPointToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            crossAdjacent = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();
        }

        private void crossCornerToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            crossCorners = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();
        }

        //heurestic mode
        private void manhattanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.MANHATTAN;
            euclideanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.EUCLIDEAN;
            manhattanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
        }

        private void chebyshevToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.CHEBYSHEV;
            manhattanToolStripMenuItem.Checked = false;
            euclideanToolStripMenuItem.Checked = false;
        }

        private void stepsToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

             if (sender as ToolStripMenuItem == bordersToolStripMenuItem)
                updateBorderVisibility(!bordersToolStripMenuItem.Checked);
            else if (sender as ToolStripMenuItem == highlightOverCurrentBoxToolStripMenuItem)
                allowHighlight = highlightOverCurrentBoxToolStripMenuItem.Checked;

            Redraw();
            this.Invalidate();

        }

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cd_grid.ShowDialog() == DialogResult.OK) {
                this.BackColor = cd_grid.Color;
                selectedColor = cd_grid.Color;
                borderColorToolStripMenuItem.Checked = true;
            }
        }

        private void wallsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (nUD_AGVs.Value != 0) {
                for (int agv = 0; agv < nUD_AGVs.Value; agv++) {
                    AGVs[agv].JumpPoints.Clear();
                }
            }

            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                    switch (m_rectangles[widthTrav][heightTrav].boxType) {
                        case BoxType.Normal:
                        case BoxType.Start:
                        case BoxType.End:
                            break;
                        case BoxType.Wall:
                            m_rectangles[widthTrav][heightTrav].SetNormalBox();
                            break;
                    }
                }
            }
            this.Invalidate();
            Redraw();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e) {

            FullyRestore();
        }

        private void exportMapToolStripMenuItem_Click(object sender, EventArgs e) {
            export();
        }

        private void importMapToolStripMenuItem_Click(object sender, EventArgs e) {

            import();
        }

        private void importPictureToolStripMenuItem_Click(object sender, EventArgs e) {

            ofd_importpic.Filter = "png picture (*.png)|*.png";
            ofd_importpic.FileName = "";

            if (ofd_importpic.ShowDialog() == DialogResult.OK) {
                this.BackgroundImage = Image.FromFile(ofd_importpic.FileName);
                for (int i = 0; i < Constants.__WidthBlocks; i++)
                    for (int j = 0; j < Constants.__HeightBlocks; j++) {
                        m_rectangles[i][j].BeTransparent();
                        boxDefaultColor = Color.Transparent;

                    }
                this.Invalidate();
                bordersToolStripMenuItem.Checked = false;
            } else
                return;
        }



        private void startToolStripMenuItem_Click(object sender, EventArgs e) {

            

            nUD_AGVs.Value = getNumberOfAGVs();

            if (nUD_AGVs.Value > 2)
                gb_monitor.Width += gb_monitor.Width - 100;

            for (int i = 0; i < fromstart.Length; i++)
                fromstart[i] = true;

            beforeStart = false;
            allowHighlight = false;

            for (int i = 0; i < StartPos.Count; i++)
                AGVs[i].MarkedLoad = new Point();

            Redraw();

            for (int i = 0; i < StartPos.Count; i++) {
                AGVs[i].StartX = m_rectangles[StartPos[i].x][StartPos[i].y].boxRec.X;
                AGVs[i].StartY = m_rectangles[StartPos[i].x][StartPos[i].y].boxRec.Y;
                AGVs[i].SizeX = 19;
                AGVs[i].SizeY = 19;
                AGVs[i].Init();
            }


            timer_counter = new int[StartPos.Count];
            timers(StartPos.Count);
            settings_menu.Enabled = false;
            gb_settings.Enabled = false;

#if emissionsSymbol
            show_emissions();
#endif

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
        }

        private void increaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = timer0.Interval + 100;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";
        }

        private void decreaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
            if (timer0.Interval == 100)
                return;

            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = timer0.Interval - 100;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";

        }

        private void implementGoogleMapsToolStripMenuItem_Click(object sender, EventArgs e) {

            gmaps maps = new gmaps();
            maps.ShowDialog();

        }

        private void borderColorToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.BackColor = Color.DarkGray;
            borderColorToolStripMenuItem.Checked = false;
        }
#if emissionsSymbol
        private void main_form_LocationChanged(object sender, EventArgs e) {

            emissions.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);

        }
#endif

    }

}
