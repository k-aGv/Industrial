﻿/*!
The MIT License (MIT)

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
using System.Collections.Generic;
using System.IO;


namespace kagv {

    public partial class main_form : Form {


        //custom constructor of this form.
        public main_form() {
            DoubleBuffered = true;
            InitializeComponent();//Create the form layout
            Application.AddMessageFilter(this);
            MeasureScreen();
            Initialization();//initialize our stuff
        }

        //paint event on form.
        //This event is triggered when a paint event or mouse event is happening over the form.
        //mouse clicks ,hovers and clicks are also considered as triggers
        private void main_form_Paint(object sender, PaintEventArgs e) {
            paper = e.Graphics;
            paper.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            paper.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            paper.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            paper.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            paper.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            SetStyle(
            ControlStyles.DoubleBuffer, true);

            try {
                if (importedLayout != null) {

                    Rectangle r = new Rectangle(new Point(m_rectangles[0][0].x, m_rectangles[0][0].y)
                        , new Size((m_rectangles[Globals._WidthBlocks - 1][Globals._HeightBlocks - 1].x) - Globals._LeftBarOffset + Globals._BlockSide
                            , (m_rectangles[Globals._WidthBlocks - 1][Globals._HeightBlocks - 1].y) - Globals._TopBarOffset + Globals._BlockSide));
                    paper.DrawImage(importedLayout, r);

                }
                //draws the grid
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++) {
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
                        //show the relative box color regarding the box type we have chose
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
                for (int i = 0; i < startPos.Count; i++) //count how much agvs we have added to the grid
                    c += AGVs[i].JumpPoints.Count; //...and add them in a variable

                for (int i = 0; i < startPos.Count; i++) {
                    AGVs[i].StepsCounter = 0;
                    for (int resultTrav = 0; resultTrav < c; resultTrav++)
                        try {
                            if (linesToolStripMenuItem.Checked)
                                AGVs[i].Paths[resultTrav].drawLine(paper);//draw the lines 
                            if (!isMouseDown)
                                DrawPoints(AGVs[i].Paths[resultTrav], i);//show points
                        } catch { }
                }

                //handle the red message above every agv
                int AGVs_list_index = 0;
                if (aGVIndexToolStripMenuItem.Checked)
                    for (int i = 0; i < nUD_AGVs.Value; i++)
                        if (!trappedStatus[i]) {
                            paper.DrawString("AGV:" + AGVs[AGVs_list_index].ID,
                                             new Font("Tahoma", 8, FontStyle.Bold),
                                             new SolidBrush(Color.Red),
                                             new Point((startPos[AGVs_list_index].x * Globals._BlockSide) - 10 + Globals._LeftBarOffset, ((startPos[AGVs_list_index].y * Globals._BlockSide) + Globals._TopBarOffset) - Globals._BlockSide));
                            AGVs_list_index++;
                        }

            } catch { }
        }
     
        private void main_form_Load(object sender, EventArgs e) {
            
            
            ReflectVariables();

            //Automatically enable the CPUs for this app.
            var _proc = System.Diagnostics.Process.GetCurrentProcess();
            int coreFlag;
            if (Environment.ProcessorCount == 1) coreFlag = 0x0001;
            else if (Environment.ProcessorCount == 2) coreFlag = 0x0003;
            else if (Environment.ProcessorCount == 3) coreFlag = 0x0007;
            else coreFlag = 0x000F; //use only 4 cores.We dont care for pcs with more than 4 cores.

            _proc.ProcessorAffinity = new IntPtr(coreFlag);
            //More infos here:https://msdn.microsoft.com/en-us/library/system.diagnostics.processthread.processoraffinity(v=vs.110).aspx
           
        }

        private void main_form_MouseDown(object sender, MouseEventArgs e) {
            //If the simulation is running, do not do anything.leave the function explicitly
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return;

            //Supposing that timers are not enabled(that means that the simulation is not running)
            //we have a clicked point.Check if that point is valid.if not explicitly leave
            Point _validationPoint = new Point(e.X, e.Y);
            if (!Isvalid(_validationPoint))
                return;
            //if the clicked point is inside a rectangle...
            isMouseDown = true;
            if ((e.Button == MouseButtons.Left) && (rb_wall.Checked))
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType) { //...measure the reaction
                                case BoxType.Normal: //if its wall or normal ,switch it to the opposite.
                                case BoxType.Wall:
                                    m_rectangles[widthTrav][heightTrav].SwitchBox();
                                    Invalidate();
                                    break;
                                case BoxType.Start: //if its start or end,do nothing.
                                case BoxType.End:
                                    break;
                            }
                        }
            //if the user press the right button of the mouse
            if (e.Button == MouseButtons.Right) {

                //prepare the tooltip
                Point mycoords = new Point(e.X, e.Y);
                GridBox clickedBox = null;
                bool isBorder = true;
                string currentBoxCoords = "X: - Y: -\r\n";
                string currentBoxIndex = "Index: N/A\r\n";
                string currentBoxType = "Block type: Border\r\n";
                string isPath = "Is part of path: N/A\r\n";
                bool isPathBlock = false;
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
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

                            if (startPos != null) {
                                for (int j = 0; j < startPos.Count; j++)
                                    for (int i = 0; i < Globals._MaximumSteps; i++)
                                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains
                                            (
                                                   new Point(
                                                       Convert.ToInt32(AGVs[j].Steps[i].X),
                                                       Convert.ToInt32(AGVs[j].Steps[i].Y)
                                                       )
                                            )) {
                                            isPathBlock = true;
                                            agv_index = j;
                                            i = Globals._MaximumSteps;
                                            j = startPos.Count;
                                        }
                                clickedBox = m_rectangles[widthTrav][heightTrav];
                            }
                            tp.ToolTipIcon = ToolTipIcon.Info;
                            if (isPathBlock && startPos != null) {
                                isPath = "Is part of AGV" + (agv_index) + " path";
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X
                                    , clickedBox.boxRec.Y - Globals._TopBarOffset + 17);
                                isBorder = false;
                            } else {
                                isPath = "Is part of path:No\r\n";
                                clickedBox = new GridBox(e.X, e.Y, BoxType.Normal);
                                //show the tooltip
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X - 10
                                    , clickedBox.boxRec.Y - Globals._TopBarOffset + 12);
                                isBorder = false;
                            }
                        }



                if (isBorder) {
                    tp.ToolTipIcon = ToolTipIcon.Error;
                    //show the tooltip
                    tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                               , this
                               , e.X - 8
                               , e.Y - Globals._TopBarOffset + 14);
                }

            }

        }

        private void main_form_MouseMove(object sender, MouseEventArgs e) {
            //this event is triggered when the mouse is moving above the form

            //if we hold the left click and the Walls setting is selected....
            if (isMouseDown && rb_wall.Checked) {
                if (e.Button == MouseButtons.Left) {
                    if (m_lastBoxSelect.boxType == BoxType.Start ||
                        m_lastBoxSelect.boxType == BoxType.End)
                        return;

                    //that IF() means: if my click is over an already drawn box...
                    if (m_lastBoxSelect == null) {
                        for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++) {
                            for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
                                if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                                    m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                                    m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                    switch (m_lastBoxType) {
                                        case BoxType.Normal:
                                        case BoxType.Wall:
                                            m_rectangles[widthTrav][heightTrav].SwitchBox(); //switch it if needed...
                                            Invalidate();
                                            break;
                                        case BoxType.Start:
                                        case BoxType.End:
                                            break;
                                    }
                                }

                            }
                        }

                        return;
                        //else...its a new/fresh box
                    } else {
                        for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++) {
                            for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
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
                                                    Invalidate();
                                                }
                                                break;
                                            case BoxType.Start:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                Invalidate();
                                                break;
                                            case BoxType.End:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                m_lastBoxSelect.SetEndBox();
                                                Invalidate();
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

            //if user enable the highlighting over a box while mouse hovering
            if (allowHighlight)
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
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

                            Invalidate();
                        } else if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            m_rectangles[widthTrav][heightTrav].onHover(boxDefaultColor);
                            Invalidate();
                        }
        }

        //The most important event.When we let our mouse click up,all our changes are
        //shown in the screen
        private void main_form_MouseUp(object sender, MouseEventArgs e) {


            if (timer0.Enabled
                || timer1.Enabled
                || timer2.Enabled
                || timer3.Enabled
                || timer4.Enabled) return;

            isMouseDown = false;

            for (int i = 0; i < startPos.Count; i++)
                AGVs[i].StepsCounter = 0;

            if (e.Button == MouseButtons.Right)
                tp.Hide(this);

            Redraw();//The main function of this executable.Contains almost every drawing and calculating stuff
            Invalidate();
        }

        
        private void nUD_AGVs_ValueChanged(object sender, EventArgs e) {

            //if we change the AGVs value from numeric updown,do the following
            bool removed = false;
            List<GridPos> start_position = new List<GridPos>();

            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                        start_position.Add(new GridPos(widthTrav, heightTrav));
                    //if we reduce the numeric value and become less than the already-drawn agvs,remove the rest agvs
                    if (start_position.Count > nUD_AGVs.Value) {
                        m_rectangles[start_position[0].x][start_position[0].y].SwitchEnd_StartToNormal(); //removes the very last
                        removed = true;
                        gb_monitor.Controls.Find(
                     "agv" + (start_position.Count) + "steps_LB",
                 true)
                 [0].Text = "";
                        Invalidate();
                    }
                }
            if (removed)
                Redraw();
            

            int node_list = 0;
            do {
                
                removed = false;
                if (tree_stats.Nodes[node_list].Text.Contains("AGV")) {
                    tree_stats.Nodes[node_list].Remove();
                    removed = true;
                }

                if (!removed)
                    node_list++;
                
            } while (node_list < tree_stats.Nodes.Count);

            for (int p = 0; p < nUD_AGVs.Value; p++) {
                TreeNode n = new TreeNode("AGV:" + (p));
                n.Name = ("AGV:" + (p));
                n.Nodes.Add("Loads Delivered");
                n.Nodes.Add("Will add moar");
                tree_stats.Nodes.Add(n);
            }
            tree_stats.Refresh();
        }

        private void main_form_MouseClick(object sender, MouseEventArgs e) {

            if (timer0.Enabled
                || timer1.Enabled
                || timer2.Enabled
                || timer3.Enabled
                || timer4.Enabled) return;


            Point click_coords = new Point(e.X, e.Y);
            if (!Isvalid(click_coords) || e.Button != MouseButtons.Left || nUD_AGVs.Value == 0)
                return;

            if (rb_load.Checked)
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType) {
                                case BoxType.Normal:
                                    //loads++;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 1;
                                    Invalidate();
                                    break;
                                case BoxType.Load:
                                    loads--;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 2;
                                    Invalidate();
                                    break;
                                case BoxType.Wall:
                                case BoxType.Start:
                                case BoxType.End:
                                    break;
                            }
                        }

            if (rb_start.Checked) {

                if (nUD_AGVs.Value == 1)//Saves only the last Click position to place the Start (1 start exists)
                {
                    for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                        for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                            if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                                m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();
                } else if (nUD_AGVs.Value > 1)//Deletes the start with the smallest iX - iY coords and keeps the rest
                {
                    int starts_counter = 0;
                    int[,] starts_position = new int[2, Convert.ToInt32(nUD_AGVs.Value)];


                    for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                        for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
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
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                         && m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal)
                            m_rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals._BlockSide) + Globals._LeftBarOffset, heightTrav * Globals._BlockSide + Globals._TopBarOffset, BoxType.Start);



            }
            //same for Stop
            if (rb_stop.Checked) {
                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                            m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();//allow only one end point


                for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                             &&
                            m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal) {
                            m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * Globals._BlockSide + Globals._LeftBarOffset, heightTrav * Globals._BlockSide + Globals._TopBarOffset, BoxType.End);
                        }
            }

            Invalidate();
        }
        //parametres
        private void useRecursiveToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            neverCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            jumpParam.DiagonalMovement = DiagonalMovement.Always;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                jumpParam.DiagonalMovement = DiagonalMovement.Always;
            }
            Redraw();


        }

        private void crossAdjacentPointToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            jumpParam.DiagonalMovement = DiagonalMovement.Never;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                jumpParam.DiagonalMovement = DiagonalMovement.Never;
            }
            Redraw();
        }

        private void crossCornerToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            neverCrossMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            jumpParam.DiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                jumpParam.DiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable;
            }
            Redraw();
        }
        private void crossCornerOnlyWhenNoObstaclesToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            neverCrossMenu.Checked = false;
            jumpParam.DiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                jumpParam.DiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
            }
            Redraw();
        }

        //heurestic mode
        private void manhattanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

            jumpParam.SetHeuristic(HeuristicMode.MANHATTAN);
            euclideanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
            Redraw();

        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            jumpParam.SetHeuristic(HeuristicMode.EUCLIDEAN);
            manhattanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
            Redraw();
        }

        private void chebyshevToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            jumpParam.SetHeuristic(HeuristicMode.CHEBYSHEV);
            manhattanToolStripMenuItem.Checked = false;
            euclideanToolStripMenuItem.Checked = false;
            Redraw();
        }

        private void stepsToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

            if (sender as ToolStripMenuItem == bordersToolStripMenuItem)
                UpdateBorderVisibility(!bordersToolStripMenuItem.Checked);
            else if (sender as ToolStripMenuItem == highlightOverCurrentBoxToolStripMenuItem)
                allowHighlight = highlightOverCurrentBoxToolStripMenuItem.Checked;

            Redraw();
            Invalidate();

        }

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cd_grid.ShowDialog() == DialogResult.OK) {
                BackColor = cd_grid.Color;
                selectedColor = cd_grid.Color;
                borderColorToolStripMenuItem.Checked = true;
            }
        }

        private void wallsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (nUD_AGVs.Value != 0)
                for (int agv = 0; agv < nUD_AGVs.Value; agv++)
                    AGVs[agv].JumpPoints.Clear();

            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                    switch (m_rectangles[widthTrav][heightTrav].boxType) {
                        case BoxType.Normal:
                        case BoxType.Start:
                        case BoxType.End:
                            break;
                        case BoxType.Wall:
                            m_rectangles[widthTrav][heightTrav].SetNormalBox();
                            break;
                    }
            Invalidate();
            Redraw();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e) {

            FullyRestore();
        }

        private void exportMapToolStripMenuItem_Click(object sender, EventArgs e) {
            Export();
        }

        private void importMapToolStripMenuItem_Click(object sender, EventArgs e) {
            Import();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e) {
            //start the animations


            //refresh the numeric value regarding the drawn agvs
            nUD_AGVs.Value = GetNumberOfAGVs();

            //if we add more than 2 agvs,we have to resize the monitor.
            if (nUD_AGVs.Value > 2)
                gb_monitor.Size = new Size(Globals._gb_monitor_width * 2, Globals._gb_monitor_height);
            else
                gb_monitor.Size = new Size(Globals._gb_monitor_width, Globals._gb_monitor_height);

            for (int i = 0; i < fromstart.Length; i++)
                fromstart[i] = true;

            beforeStart = false;
            allowHighlight = false;//do not allow highlight while emulation is active

            for (int i = 0; i < startPos.Count; i++)
                AGVs[i].MarkedLoad = new Point();

            Redraw();

            labeled_loads = loads;
            for (int i = 0; i < startPos.Count; i++) {
                AGVs[i].StartX = m_rectangles[startPos[i].x][startPos[i].y].boxRec.X;
                AGVs[i].StartY = m_rectangles[startPos[i].x][startPos[i].y].boxRec.Y;
                AGVs[i].SizeX = Globals._BlockSide - 1;
                AGVs[i].SizeY = Globals._BlockSide - 1;
                AGVs[i].Init();
            }

            on_which_step = new int[startPos.Count];
            Timers();
            settings_menu.Enabled = false;
            gb_settings.Enabled = false;
            nud_weight.Enabled = false;
            cb_type.Enabled = false;
            toolStripStatusLabel1.Text = "Simulation is running...";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
        }

        private void increaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
            int d = timer0.Interval;
            d += 50;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = d;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";
        }

        private void decreaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
            if (timer0.Interval == 50)
                return;

            int d = timer0.Interval;
            d -= 50;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = d;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";

        }



        private void borderColorToolStripMenuItem1_Click(object sender, EventArgs e) {
            BackColor = Color.DarkGray;
            borderColorToolStripMenuItem.Checked = false;
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

            int c = 0;
            for (int i = 0; i < startPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            if (c > 0)
                TriggerStartMenu(true);
            else
                TriggerStartMenu(false);
        }


        //one timer for each agv.
        private void timer0_Tick(object sender, EventArgs e) {
            int mysteps = 0;//init the steps
            for (int i = 0; i < Globals._MaximumSteps; i++)
                if (AGVs[0].Steps[i].X == 0 || AGVs[0].Steps[i].Y == 0)
                    i = Globals._MaximumSteps;
                else
                    mysteps++;//really count the steps

            AGVs[0].StepsCounter = mysteps;//add them inside the class

            Animator(on_which_step[0], 0); //animate that class/agv

            on_which_step[0]++;
        }
        private void timer1_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Globals._MaximumSteps; i++)
                if (AGVs[1].Steps[i].X == 0 || AGVs[1].Steps[i].Y == 0)
                    i = Globals._MaximumSteps;
                else
                    mysteps++;

            AGVs[1].StepsCounter = mysteps;

            Animator(on_which_step[1], 1);

            on_which_step[1]++;
        }

        private void timer2_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Globals._MaximumSteps; i++)
                if (AGVs[2].Steps[i].X == 0 || AGVs[2].Steps[i].Y == 0)
                    i = Globals._MaximumSteps;
                else
                    mysteps++;

            AGVs[2].StepsCounter = mysteps;

            Animator(on_which_step[2], 2);

            on_which_step[2]++;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Globals._MaximumSteps; i++)
                if (AGVs[3].Steps[i].X == 0 || AGVs[3].Steps[i].Y == 0)
                    i = Globals._MaximumSteps;
                else
                    mysteps++;

            AGVs[3].StepsCounter = mysteps;

            Animator(on_which_step[3], 3);

            on_which_step[3]++;
        }

        private void timer4_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Globals._MaximumSteps; i++)
                if (AGVs[4].Steps[i].X == 0 || AGVs[4].Steps[i].Y == 0)
                    i = Globals._MaximumSteps;
                else
                    mysteps++;

            AGVs[4].StepsCounter = mysteps;

            Animator(on_which_step[4], 4);

            on_which_step[4]++;
        }

        private void importImageLayoutToolStripMenuItem_Click(object sender, EventArgs e) {
            ImportImage();
        }

        private void priorityRulesbetaToolStripMenuItem_Click(object sender, EventArgs e) {
            use_Halt = !use_Halt;
            if (use_Halt)
                priorityRulesbetaToolStripMenuItem.Checked = true;
            else
                priorityRulesbetaToolStripMenuItem.Checked = false;
        }

        private void nud_weight_ValueChanged(object sender, EventArgs e) {
            Redraw();
        }


        private void btn_up_Click(object sender, EventArgs e) {
            Globals._HeightBlocks--;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_right_Click(object sender, EventArgs e) {
            Globals._WidthBlocks++;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_down_Click(object sender, EventArgs e) {
            Globals._HeightBlocks++;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_left_Click(object sender, EventArgs e) {
            Globals._WidthBlocks--;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_leftup_Click(object sender, EventArgs e) {
            Globals._WidthBlocks--;
            Globals._HeightBlocks--;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_rightup_Click(object sender, EventArgs e) {
            Globals._WidthBlocks++;
            Globals._HeightBlocks--;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_rightdown_Click(object sender, EventArgs e) {
            Globals._WidthBlocks++;
            Globals._HeightBlocks++;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void btn_leftdown_Click(object sender, EventArgs e) {
            Globals._WidthBlocks--;
            Globals._HeightBlocks++;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Width = (Globals._WidthBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset;
            Size = new Size(Width, Height + Globals._BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        private void nud_side_ValueChanged(object sender, EventArgs e) {
            Globals._BlockSide = Convert.ToInt32(nud_side.Value);
            UpdateGridStats();
            FullyRestore();
            holdCTRL = !holdCTRL;
        }

        //Debug methods

        private void showGridBlockLocationsToolStripMenuItem_MouseEnter(object sender, EventArgs e) {

            Graphics g = CreateGraphics();

            //Supposed to make graphics faster but i see no difference.nevermind...who cares -->
            Paint -= main_form_Paint;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            SetStyle(
            ControlStyles.DoubleBuffer, true);
            
            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++) {
                for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {
                    g.DrawString("x" + widthTrav + "\n" + "y" + heightTrav,
                                    new Font("Tahoma", 5, FontStyle.Bold),
                                    new SolidBrush(Color.DarkSlateBlue),
                                    new Point(m_rectangles[widthTrav][heightTrav].x, m_rectangles[widthTrav][heightTrav].y)
                                    );
                    
                }
            }

            // <---
            Paint += main_form_Paint;
        }

        private void showGridBlockLocationsToolStripMenuItem_MouseLeave(object sender, EventArgs e) {
            Redraw();
            Refresh();
        }
        
        private void main_form_FormClosing(object sender, FormClosingEventArgs e) {
            if (File.Exists("info.txt"))
                File.Delete("info.txt");

            StreamWriter writer = new StreamWriter("info.txt");
            writer.WriteLine(
                Globals._WidthBlocks + "\n" +
                Globals._HeightBlocks + "\n" +
                Globals._BlockSide);
            writer.Dispose();
        }

        private void defaultGridSizeToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals._BlockSide = 15;
            MeasureScreen();
            Initialization();
        }
    }
    
}
