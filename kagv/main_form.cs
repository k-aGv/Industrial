/*!
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
using kagv.DLL_source;

namespace kagv {

    public partial class MainForm : Form {


        //custom constructor of this form.
        public MainForm() {
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
            _paper = e.Graphics;
            _paper.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            _paper.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            _paper.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            _paper.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            _paper.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            SetStyle(
            ControlStyles.DoubleBuffer, true);

            try {
                if (_importedLayout != null) {

                    Rectangle r = new Rectangle(new Point(_rectangles[0][0].X, _rectangles[0][0].Y)
                        , new Size((_rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].X) - Globals.LeftBarOffset + Globals.BlockSide
                            , (_rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].Y) - Globals.TopBarOffset + Globals.BlockSide));
                    _paper.DrawImage(_importedLayout, r);

                }
                //draws the grid
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                        //show the relative box color regarding the box type we have chose
                        _rectangles[widthTrav][heightTrav].DrawBox(_paper, BoxType.Normal);
                        _rectangles[widthTrav][heightTrav].DrawBox(_paper, BoxType.Start);
                        _rectangles[widthTrav][heightTrav].DrawBox(_paper, BoxType.End);
                        _rectangles[widthTrav][heightTrav].DrawBox(_paper, BoxType.Wall);
                        _rectangles[widthTrav][heightTrav].DrawBox(_paper, BoxType.Load);

                        if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.Load
                            && _isLoad[widthTrav, heightTrav] == 3)
                            _rectangles[widthTrav][heightTrav].SetAsTargetted(_paper);

                    }
                }

                for (short i = 0; i < _startPos.Count; i++) {
                    _AGVs[i].StepsCounter = 0;
                    for (var resultTrav = 0; resultTrav < _AGVs[i].JumpPoints.Count; resultTrav++)
                        try {
                            if (linesToolStripMenuItem.Checked)
                                _AGVs[i].Paths[resultTrav].DrawLine(_paper);//draw the lines 
                            if (!_isMouseDown)
                                DrawPoints(_AGVs[i].Paths[resultTrav], i);//show points
                        } catch { }
                }

                //handle the red message above every agv
                var AGVsListIndex = 0;
                if (aGVIndexToolStripMenuItem.Checked)
                    for (short i = 0; i < nUD_AGVs.Value; i++)
                        if (!_trappedStatus[i]) {
                            _paper.DrawString("AGV:" + _AGVs[AGVsListIndex].ID,
                                             new Font("Tahoma", 8, FontStyle.Bold),
                                             new SolidBrush(Color.Red),
                                             new Point((_startPos[AGVsListIndex].X * Globals.BlockSide) - 10 + Globals.LeftBarOffset, ((_startPos[AGVsListIndex].Y * Globals.BlockSide) + Globals.TopBarOffset) - Globals.BlockSide));
                            AGVsListIndex++;
                        }

            } catch { }
        }
     
        private void main_form_Load(object sender, EventArgs e) {
            
            
            ReflectVariables();

            //Automatically enable the CPUs for this app.
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            int coreFlag;
            if (Environment.ProcessorCount == 1) coreFlag = 0x0001;
            else if (Environment.ProcessorCount == 2) coreFlag = 0x0003;
            else if (Environment.ProcessorCount == 3) coreFlag = 0x0007;
            else coreFlag = 0x000F; //use only 4 cores.We dont care for pcs with more than 4 cores.

            proc.ProcessorAffinity = new IntPtr(coreFlag);
            //More infos here:https://msdn.microsoft.com/en-us/library/system.diagnostics.processthread.processoraffinity(v=vs.110).aspx
        }

        private void main_form_MouseDown(object sender, MouseEventArgs e) {
            //If the simulation is running, do not do anything.leave the function explicitly
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return;

            //Supposing that timers are not enabled(that means that the simulation is not running)
            //we have a clicked point.Check if that point is valid.if not explicitly leave
            if (!Isvalid(new Point(e.X, e.Y)))
                return;
            //if the clicked point is inside a rectangle...
            _isMouseDown = true;
            if ((e.Button == MouseButtons.Left) && (rb_wall.Checked))
                for (int widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (int heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            _lastBoxType = _rectangles[widthTrav][heightTrav].BoxType;
                            _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                            switch (_lastBoxType) { //...measure the reaction
                                case BoxType.Normal: //if its wall or normal ,switch it to the opposite.
                                case BoxType.Wall:
                                    _rectangles[widthTrav][heightTrav].SwitchBox();
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
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.Contains(mycoords)) {
                            currentBoxType =
                                "Block type: " +
                                _rectangles[widthTrav][heightTrav].BoxType + "\r\n";
                            currentBoxCoords =
                                "X: " +
                                _rectangles[widthTrav][heightTrav].BoxRec.X + " " +
                                "Y: " +
                                _rectangles[widthTrav][heightTrav].BoxRec.Y + "\r\n";
                            currentBoxIndex =
                                "Index: " +
                                "iX: " + widthTrav + " " + "iY: " + heightTrav + "\r\n";

                            int agvIndex = 0;

                            if (_startPos != null) {
                                for (int j = 0; j < _startPos.Count; j++)
                                    for (int i = 0; i < Globals.MaximumSteps; i++)
                                        if (_rectangles[widthTrav][heightTrav].BoxRec.Contains
                                            (
                                                   new Point(
                                                       Convert.ToInt32(_AGVs[j].Steps[i].X),
                                                       Convert.ToInt32(_AGVs[j].Steps[i].Y)
                                                       )
                                            )) {
                                            isPathBlock = true;
                                            agvIndex = j;
                                            i = Globals.MaximumSteps;
                                            j = _startPos.Count;
                                        }
                                clickedBox = _rectangles[widthTrav][heightTrav];
                            }
                            _tp.ToolTipIcon = ToolTipIcon.Info;
                            if (isPathBlock && _startPos != null) {
                                isPath = "Is part of AGV" + (agvIndex) + " path";
                                _tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.BoxRec.X
                                    , clickedBox.BoxRec.Y - Globals.TopBarOffset + 17);
                                isBorder = false;
                            } else {
                                isPath = "Is part of path:No\r\n";
                                clickedBox = new GridBox(e.X, e.Y, BoxType.Normal);
                                //show the tooltip
                                _tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.BoxRec.X - 10
                                    , clickedBox.BoxRec.Y - Globals.TopBarOffset + 12);
                                isBorder = false;
                            }
                        }



                if (isBorder) {
                    _tp.ToolTipIcon = ToolTipIcon.Error;
                    //show the tooltip
                    _tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                               , this
                               , e.X - 8
                               , e.Y - Globals.TopBarOffset + 14);
                }

            }

        }

        private void main_form_MouseMove(object sender, MouseEventArgs e) {
            //this event is triggered when the mouse is moving above the form

            //if we hold the left click and the Walls setting is selected....
            if (_isMouseDown && rb_wall.Checked) {
                if (e.Button == MouseButtons.Left) {
                    if (_lastBoxSelect.BoxType == BoxType.Start ||
                        _lastBoxSelect.BoxType == BoxType.End)
                        return;

                    //that IF() means: if my click is over an already drawn box...
                    if (_lastBoxSelect == null) {
                        for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                            for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                                if (_rectangles[widthTrav][heightTrav].BoxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                                    _lastBoxType = _rectangles[widthTrav][heightTrav].BoxType;
                                    _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                                    switch (_lastBoxType) {
                                        case BoxType.Normal:
                                        case BoxType.Wall:
                                            _rectangles[widthTrav][heightTrav].SwitchBox(); //switch it if needed...
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
                    }
                    for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                        for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                            if (_rectangles[widthTrav][heightTrav].BoxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                                if (_rectangles[widthTrav][heightTrav] == _lastBoxSelect) {
                                    return;
                                }
                                switch (_lastBoxType) {
                                    case BoxType.Normal:
                                    case BoxType.Wall:
                                        if (_rectangles[widthTrav][heightTrav].BoxType == _lastBoxType) {
                                            _rectangles[widthTrav][heightTrav].SwitchBox();
                                            _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                                            Invalidate();
                                        }
                                        break;
                                    case BoxType.Start:
                                        _lastBoxSelect.SetNormalBox();
                                        _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                                        Invalidate();
                                        break;
                                    case BoxType.End:
                                        _lastBoxSelect.SetNormalBox();
                                        _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                                        _lastBoxSelect.SetEndBox();
                                        Invalidate();
                                        break;
                                }
                                return;
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
            if (_allowHighlight)
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.Contains(new Point(e.X, e.Y))
                            && _rectangles[widthTrav][heightTrav].BoxType == BoxType.Normal) {
                            if (rb_load.Checked)
                                _rectangles[widthTrav][heightTrav].OnHover(Color.FromArgb(150, Color.FromArgb(138, 109, 86)));
                            else if (rb_start.Checked)
                                _rectangles[widthTrav][heightTrav].OnHover(Color.LightGreen);
                            else if (rb_stop.Checked)
                                _rectangles[widthTrav][heightTrav].OnHover(Color.FromArgb(80, Color.FromArgb(255, 26, 26)));
                            else //wall
                                _rectangles[widthTrav][heightTrav].OnHover(Color.FromArgb(20, Color.LightGray));

                            Invalidate();
                        } else if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.Normal) {
                            _rectangles[widthTrav][heightTrav].OnHover(_boxDefaultColor);
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

            if (e.Button == MouseButtons.Right) {
                _tp.Hide(this);
                return;
            }

            Point clickCoords = new Point(e.X, e.Y);
            if (!Isvalid(clickCoords) || nUD_AGVs.Value == 0)
                return;

            _isMouseDown = false;

            if (rb_load.Checked)
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1)))) {
                            _lastBoxType = _rectangles[widthTrav][heightTrav].BoxType;
                            _lastBoxSelect = _rectangles[widthTrav][heightTrav];
                            switch (_lastBoxType) {
                                case BoxType.Normal:
                                    _rectangles[widthTrav][heightTrav].SwitchLoad();
                                    _isLoad[widthTrav, heightTrav] = 1;
                                    break;
                                case BoxType.Load:
                                    _loads--;
                                    _rectangles[widthTrav][heightTrav].SwitchLoad();
                                    _isLoad[widthTrav, heightTrav] = 2;
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
                    for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                        for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                            if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.Start)
                                _rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();
                } else if (nUD_AGVs.Value > 1) {//Deletes the start with the smallest iX - iY coords and keeps the rest

                    int startsCounter = 0;
                    int[,] startsPosition = new int[2, Convert.ToInt32(nUD_AGVs.Value)];


                    for (int widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                        for (int heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                            if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.Start) {
                                startsPosition[0, startsCounter] = widthTrav;
                                startsPosition[1, startsCounter] = heightTrav;
                                startsCounter++;
                            }
                            if (startsCounter == nUD_AGVs.Value) {
                                _rectangles[startsPosition[0, 0]][startsPosition[1, 0]].SwitchEnd_StartToNormal();
                            }
                        }
                }

              
                //Converts the clicked box to Start point
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.Contains(clickCoords)
                         && _rectangles[widthTrav][heightTrav].BoxType == BoxType.Normal)
                            _rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals.BlockSide) + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, BoxType.Start);



            }
            //same for Stop
          
            if (rb_stop.Checked) {
                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.End)
                            _rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();//allow only one end point


                for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                    for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                        if (_rectangles[widthTrav][heightTrav].BoxRec.Contains(clickCoords)
                             &&
                            _rectangles[widthTrav][heightTrav].BoxType == BoxType.Normal) {
                            _rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * Globals.BlockSide + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, BoxType.End);
                        }
            }

            Redraw();//The main function of this executable.Contains almost every drawing and calculating stuff
            Invalidate();
        }

        
        private void nUD_AGVs_ValueChanged(object sender, EventArgs e) {

            //if we change the AGVs value from numeric updown,do the following
            bool removed = false;
            List<GridPos> startPosition = new List<GridPos>();

            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                    if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.Start)
                        startPosition.Add(new GridPos(widthTrav, heightTrav));
                    //if we reduce the numeric value and become less than the already-drawn agvs,remove the rest agvs
                    if (startPosition.Count > nUD_AGVs.Value) {
                        _rectangles[startPosition[0].X][startPosition[0].Y].SwitchEnd_StartToNormal(); //removes the very last
                        removed = true;
                        
                        Invalidate();
                    }
                }
            if (removed)
                Redraw();
            

            int nodeList = 0;
            do {
                
                removed = false;
                if (tree_stats.Nodes[nodeList].Text.Contains("AGV")) {
                    tree_stats.Nodes[nodeList].Remove();
                    removed = true;
                }

                if (!removed)
                    nodeList++;
                
            } while (nodeList < tree_stats.Nodes.Count);

            for (short p = 0; p < nUD_AGVs.Value; p++) {
                TreeNode n = new TreeNode("AGV:" + (p))
                {
                    Name = "AGV:" + p
                };
                n.Nodes.Add("Loads Delivered");
                n.Nodes.Add("Load at: ");
                n.Nodes.Add("Status: ");
                n.Nodes.Add("Delay: "+Globals.TimerInterval+" ms");
                tree_stats.Nodes.Add(n);
            }
            
            tree_stats.Refresh();
        }
        //parametres
        private void useRecursiveToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            neverCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            _jumpParam.DiagonalMovement = DiagonalMovement.Always;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                _jumpParam.DiagonalMovement = DiagonalMovement.Always;
            }
            Redraw();


        }

        private void crossAdjacentPointToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            _jumpParam.DiagonalMovement = DiagonalMovement.Never;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                _jumpParam.DiagonalMovement = DiagonalMovement.Never;
            }
            Redraw();
        }

        private void crossCornerToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            neverCrossMenu.Checked = false;
            noObstaclesMenu.Checked = false;
            _jumpParam.DiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                _jumpParam.DiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable;
            }
            Redraw();
        }
        private void crossCornerOnlyWhenNoObstaclesToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            alwaysCrossMenu.Checked = false;
            atLeastOneMenu.Checked = false;
            neverCrossMenu.Checked = false;
            _jumpParam.DiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;

            //do not allow to have an unselected item
            if (neverCrossMenu.Checked == false &&
            atLeastOneMenu.Checked == false &&
            noObstaclesMenu.Checked == false &&
            alwaysCrossMenu.Checked == false) {
                (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
                _jumpParam.DiagonalMovement = DiagonalMovement.OnlyWhenNoObstacles;
            }
            Redraw();
        }

        //heurestic mode
        private void manhattanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

            _jumpParam.SetHeuristic(HeuristicMode.Manhattan);
            euclideanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
            Redraw();

        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            _jumpParam.SetHeuristic(HeuristicMode.Euclidean);
            manhattanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
            Redraw();
        }

        private void chebyshevToolStripMenuItem_Click(object sender, EventArgs e) {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            _jumpParam.SetHeuristic(HeuristicMode.Chebyshev);
            manhattanToolStripMenuItem.Checked = false;
            euclideanToolStripMenuItem.Checked = false;
            Redraw();
        }

        private void stepsToolStripMenuItem_Click(object sender, EventArgs e) {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

            if (sender as ToolStripMenuItem == bordersToolStripMenuItem)
                UpdateBorderVisibility(!bordersToolStripMenuItem.Checked);
            else if (sender as ToolStripMenuItem == highlightOverCurrentBoxToolStripMenuItem)
                _allowHighlight = highlightOverCurrentBoxToolStripMenuItem.Checked;

            Redraw();
            Invalidate();

        }

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cd_grid.ShowDialog() == DialogResult.OK) {
                BackColor = cd_grid.Color;
                _selectedColor = cd_grid.Color;
                borderColorToolStripMenuItem.Checked = true;
            }
        }

        private void wallsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (nUD_AGVs.Value != 0)
                for (int agv = 0; agv < nUD_AGVs.Value; agv++)
                    _AGVs[agv].JumpPoints.Clear();

            for (int widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                    switch (_rectangles[widthTrav][heightTrav].BoxType) {
                        case BoxType.Normal:
                        case BoxType.Start:
                        case BoxType.End:
                            break;
                        case BoxType.Wall:
                            _rectangles[widthTrav][heightTrav].SetNormalBox();
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

            for (short i = 0; i < _fromstart.Length; i++)
                _fromstart[i] = true;

            _beforeStart = false;
            _allowHighlight = false;//do not allow highlight while emulation is active

            for (short i = 0; i < _startPos.Count; i++)
                _AGVs[i].MarkedLoad = new Point();

            Redraw();

            _labeled_loads = _loads;
            for (short i = 0; i < _startPos.Count; i++) {
                _AGVs[i].StartX = _rectangles[_startPos[i].X][_startPos[i].Y].BoxRec.X;
                _AGVs[i].StartY = _rectangles[_startPos[i].X][_startPos[i].Y].BoxRec.Y;
                _AGVs[i].SizeX = Globals.BlockSide - 1;
                _AGVs[i].SizeY = Globals.BlockSide - 1;
                _AGVs[i].Init();
            }

            _onWhichStep = new int[_startPos.Count];
            Timers();
            settings_menu.Enabled = false;
            gb_settings.Enabled = false;
            nud_weight.Enabled = false;
            cb_type.Enabled = false;
            toolStripStatusLabel1.Text = "Simulation is running...";

            foreach (TreeNode s in tree_stats.Nodes) {
                if (s.Name.Contains("AGV")) {
                    if (!s.IsExpanded) {
                        s.Expand();
                        s.Nodes[2].Text = "Status: Empty";
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.ShowDialog();
        }

        private void increaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
           
            Globals.TimerInterval += Globals.TimerStep;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = Globals.TimerInterval;

            for (short i = 0; i < nUD_AGVs.Value; i++) {
                tree_stats.Nodes.Find("AGV:" + (i), false)[0].Expand();
                tree_stats.Nodes.Find("AGV:" + (i), false)[0].Nodes[3].Text = ("Delay: " + Globals.TimerInterval + " ms");
                
            }
        }

        private void decreaseSpeedToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Globals.TimerInterval - Globals.TimerStep == 0)
                return;

            Globals.TimerInterval -= Globals.TimerStep;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = Globals.TimerInterval;

            for (short i = 0; i < nUD_AGVs.Value; i++) {
                tree_stats.Nodes.Find("AGV:" + (i), false)[0].Nodes[3].Text = ("Delay: " + Globals.TimerInterval + " ms");
                tree_stats.Nodes.Find("AGV:" + (i), false)[0].Expand();
            }
        }



        private void borderColorToolStripMenuItem1_Click(object sender, EventArgs e) {
            BackColor = Color.DarkGray;
            borderColorToolStripMenuItem.Checked = false;
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

            int c = 0;
            for (short i = 0; i < _startPos.Count; i++)
                c += _AGVs[i].JumpPoints.Count;

            TriggerStartMenu(c > 0);
        }


        //one timer for each agv.
        private void timer0_Tick(object sender, EventArgs e) {
            var mysteps = 0;//init the steps
            for (var i = 0; i < Globals.MaximumSteps; i++)
                if (_AGVs[0].Steps[i].X == 0 || _AGVs[0].Steps[i].Y == 0)
                    i = Globals.MaximumSteps;
                else
                    mysteps++;//really count the steps

            _AGVs[0].StepsCounter = mysteps;//add them inside the class

            Animator(_onWhichStep[0], 0); //animate that class/agv

            _onWhichStep[0]++;
        }
        private void timer1_Tick(object sender, EventArgs e) {
            var mysteps = 0;
            for (var i = 0; i < Globals.MaximumSteps; i++)
                if (_AGVs[1].Steps[i].X == 0 || _AGVs[1].Steps[i].Y == 0)
                    i = Globals.MaximumSteps;
                else
                    mysteps++;

            _AGVs[1].StepsCounter = mysteps;

            Animator(_onWhichStep[1], 1);

            _onWhichStep[1]++;
        }

        private void timer2_Tick(object sender, EventArgs e) {
            var mysteps = 0;
            for (var i = 0; i < Globals.MaximumSteps; i++)
                if (_AGVs[2].Steps[i].X == 0 || _AGVs[2].Steps[i].Y == 0)
                    i = Globals.MaximumSteps;
                else
                    mysteps++;

            _AGVs[2].StepsCounter = mysteps;

            Animator(_onWhichStep[2], 2);

            _onWhichStep[2]++;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            var mysteps = 0;
            for (var i = 0; i < Globals.MaximumSteps; i++)
                if (_AGVs[3].Steps[i].X == 0 || _AGVs[3].Steps[i].Y == 0)
                    i = Globals.MaximumSteps;
                else
                    mysteps++;

            _AGVs[3].StepsCounter = mysteps;

            Animator(_onWhichStep[3], 3);

            _onWhichStep[3]++;
        }

        private void timer4_Tick(object sender, EventArgs e) {
            var mysteps = 0;
            for (var i = 0; i < Globals.MaximumSteps; i++)
                if (_AGVs[4].Steps[i].X == 0 || _AGVs[4].Steps[i].Y == 0)
                    i = Globals.MaximumSteps;
                else
                    mysteps++;

            _AGVs[4].StepsCounter = mysteps;

            Animator(_onWhichStep[4], 4);

            _onWhichStep[4]++;
        }

        private void importImageLayoutToolStripMenuItem_Click(object sender, EventArgs e) {
            ImportImage();
        }

        private void priorityRulesbetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _useHalt = !_useHalt;
            priorityRulesbetaToolStripMenuItem.Checked = _useHalt;
        }

        private void nud_weight_ValueChanged(object sender, EventArgs e) {
            Redraw();
        }


        private void btn_up_Click(object sender, EventArgs e) {
            Globals.HeightBlocks--;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_right_Click(object sender, EventArgs e) {
            Globals.WidthBlocks++;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_down_Click(object sender, EventArgs e) {
            Globals.HeightBlocks++;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_left_Click(object sender, EventArgs e) {
            Globals.WidthBlocks--;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_leftup_Click(object sender, EventArgs e) {
            Globals.WidthBlocks--;
            Globals.HeightBlocks--;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_rightup_Click(object sender, EventArgs e) {
            Globals.WidthBlocks++;
            Globals.HeightBlocks--;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_rightdown_Click(object sender, EventArgs e) {
            Globals.WidthBlocks++;
            Globals.HeightBlocks++;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void btn_leftdown_Click(object sender, EventArgs e) {
            Globals.WidthBlocks--;
            Globals.HeightBlocks++;
            Height = (Globals.HeightBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Width = (Globals.WidthBlocks + 1) * Globals.BlockSide + Globals.BottomBarOffset;
            Size = new Size(Width, Height + Globals.BottomBarOffset);

            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
        }

        private void nud_side_ValueChanged(object sender, EventArgs e) {
            Globals.BlockSide = Convert.ToInt32(nud_side.Value);
            UpdateGridStats();
            FullyRestore();
            _holdCtrl = !_holdCtrl;
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
            
            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {
                    g.DrawString("x" + widthTrav + "\n" + "y" + heightTrav,
                                    new Font("Tahoma", 5, FontStyle.Bold),
                                    new SolidBrush(Color.DarkSlateBlue),
                                    new Point(_rectangles[widthTrav][heightTrav].X, _rectangles[widthTrav][heightTrav].Y)
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
                "WidthBlocks:"+Globals.WidthBlocks + "\n" +
                "HeightBlocks:"+Globals.HeightBlocks + "\n" +
                "BlockSide:"+Globals.BlockSide + "\n" +
                "DiagonalMovement:"+_jumpParam.DiagonalMovement+"\n"+
                "Heuristic:"+_jumpParam.HeuristicFunc.Method+"\n"+
                "ShowSteps:"+stepsToolStripMenuItem.Checked+"\n"+
                "ShowLines:"+linesToolStripMenuItem.Checked+"\n"+
                "ShowDots:"+dotsToolStripMenuItem.Checked+"\n"+
                "ShowBorders:"+bordersToolStripMenuItem.Checked+"\n"+
                "Highlight:"+highlightOverCurrentBoxToolStripMenuItem.Checked+"\n"+
                "ShowAGVindex:"+aGVIndexToolStripMenuItem.Checked
                );
            writer.Dispose();
        }

        private void defaultGridSizeToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.BlockSide = 15;
            MeasureScreen();
            Initialization();
        }
    }
    
}
