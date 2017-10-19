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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using kagv.DLL_source;
namespace kagv {

    public partial class MainForm : IMessageFilter {

        //Message callback of key
        public bool PreFilterMessage(ref Message msg) {
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return false;
            if (msg.Msg == 0x101) //0x101 means key is up
            {

                _holdCtrl = false;
                panel_resize.Visible = false;
                toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
                Refresh();
                return true;
            }
            return false;
        }

        //function for handling keystrokes and assigning specific actions to them
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool emptymap = true;
            if (ModifierKeys.HasFlag(Keys.Control) && !_holdCtrl) {

                if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                    return false;

                for (int k = 0; k < Globals.WidthBlocks; k++) {
                    for (int l = 0; l < Globals.HeightBlocks; l++) {
                        if (_rectangles[k][l].BoxType != BoxType.Normal) {
                            emptymap = false;
                            break;
                        }
                    }
                    if (!emptymap) {
                        break;
                    }
                }

                if (!emptymap) {
                    DialogResult s = MessageBox.Show("Grid resize is only possible in an empty grid\nThe grid will be deleted.\nProceed?"
                                   , "Grid Resize triggered", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (s == DialogResult.Yes) {
                        _holdCtrl = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        FullyRestore();
                        return true;
                    }
                    return false;
                }

                if (_overImage) {
                    DialogResult s = MessageBox.Show("Grid resize is only possible in an empty grid\nThe grid will be deleted.\nProceed?"
                                  , "Grid Resize triggered", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (s == DialogResult.Yes) {
                        _holdCtrl = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        _overImage = false;
                        FullyRestore();
                        return true;
                    }
                    return false;
                } 
                _holdCtrl = true;
                UpdateGridStats();
                toolStripStatusLabel1.Text = "Release CTRL to return...";
                panel_resize.Visible = true;
                return true;
            }

            switch (keyData) {
                case Keys.F5:
                    allToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Up:
                    increaseSpeedToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Down:
                    decreaseSpeedToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Space:
                    if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                        return false;
                    int c = 0;
                    for (int i = 0; i < _startPos.Count; i++)
                        c += _AGVs[i].JumpPoints.Count;

                    if (c > 0)
                        TriggerStartMenu(true);

                    if (startToolStripMenuItem.Enabled)
                        startToolStripMenuItem_Click(new object(), new EventArgs());
                    else
                        MessageBox.Show(this, "Create a path please", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                default:
                    return false;
            }

        }
        /*-----------------------------------------------------*/
        //function for updating the values that are shown in the emissions Form
        private void Update_emissions(int whichAgv) {

            switch (cb_type.SelectedItem.ToString())
            {
                case "LPG":
                    if (_AGVs[whichAgv].Status.Busy) {
                        _CO2 += 2959.57;
                        _CO += 27.04;
                        _NOx += 19.63;
                        _THC += 3.06;
                        _globalWarming += 3.58;
                    } else {
                        _CO2 += 1935.16;
                        _CO += 13.36;
                        _NOx += 13.90;
                        _THC += 1.51;
                        _globalWarming += 2.33;
                    }
                    break;
                case "DSL":
                    if (_AGVs[whichAgv].Status.Busy) {
                        _CO2 += 2130.11;
                        _CO += 7.28;
                        _NOx += 20.16;
                        _THC += 1.77;
                        _globalWarming += 2.49;
                    } else {
                        _CO2 += 1510.83;
                        _CO += 3.84;
                        _NOx += 14.33;
                        _THC += 1.08;
                        _globalWarming += 1.2;
                    }
                    break;
                default:
                    _CO2 = 0;
                    _CO = 0;
                    _NOx = 0;
                    _THC = 0;
                    if (_AGVs[whichAgv].Status.Busy)
                        _globalWarming += 0.67;
                    else
                        _globalWarming += 0.64;
                    break;
            }

            
            if (!tree_stats.Nodes[1].IsExpanded)
                tree_stats.Nodes[1].Expand();

            tree_stats.Nodes[1].Nodes[0].Text = "CO: " + Math.Round(_CO, 2) + " gr";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: " + Math.Round(_CO2, 2) + " gr";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: " + Math.Round(_NOx, 2) + " gr";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: " + Math.Round(_THC, 2) + " gr";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: " + Math.Round(_globalWarming, 2) + " kgr";

        }

        private void StopTimers(int agvIndex) {
            switch (agvIndex) {
                case 0:
                    timer0.Stop();
                    break;
                case 1:
                    timer1.Stop();
                    break;
                case 2:
                    timer2.Stop();
                    break;
                case 3:
                    timer3.Stop();
                    break;
                case 4:
                    timer4.Stop();
                    break;
            }
        }

        //function that executes the whole animation. Will be explaining thoroughly below
        private void Animator(int whichStep, int whichAgv) {
            
            //we use the incoming parameters, given from the corresponding Timer that calls Animator at a given time
            //steps_counter index tells us on which Step the timer is AT THE TIME this function is called
            //agv_index index tells us which timer is calling this function so as to know which AGV will be handled
            var stepx = Convert.ToInt32(_AGVs[whichAgv].Steps[whichStep].X);
            var stepy = Convert.ToInt32(_AGVs[whichAgv].Steps[whichStep].Y);
            if (_AGVs[whichAgv].HasLoadToPick)
                tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "Load at: " + GetStepsToLoad(whichAgv);
            else
                tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "No load to pick";
            //if, for any reason, the above steps are set to "0", obviously something is wrong so function returns
            if (stepx == 0 || stepx == 0)
                return;


            bool isfreeload = false;
            bool halted = false;

            Update_emissions(whichAgv); //Call of function that updates the values of emissions

            //RULES OF WHICH AGV WILL STOP WILL BE ADDED
            if (_useHalt) {
                for (short i = 0; i < _startPos.Count; i++)
                    if (whichAgv != i
                        && _AGVs[i].GetLocation() != new Point(0, 0)
                        && _AGVs[whichAgv].GetLocation() == _AGVs[i].GetLocation()
                        && _AGVs[whichAgv].GetLocation() != _endPointCoords
                        ) {
                        Halt(whichAgv, whichStep); //function for manipulating the movement of _AGVs - must be perfected (still under dev)
                        halted = true;
                    } else
                        if (!halted)
                        _AGVs[whichAgv].SetLocation(stepx - ((Globals.BlockSide / 2) - 1) + 1, stepy - ((Globals.BlockSide / 2) - 1) + 1);
            } else
                _AGVs[whichAgv].SetLocation(stepx - ((Globals.BlockSide / 2) - 1) + 1, stepy - ((Globals.BlockSide / 2) - 1) + 1); //this is how we move the AGV on the grid (Setlocation function)

            /////////////////////////////////////////////////////////////////
            //Here is the part where an AGV arrives at the Load it has marked.
            if (_AGVs[whichAgv].GetMarkedLoad() == _AGVs[whichAgv].GetLocation() &&
                !_AGVs[whichAgv].Status.Busy) {

                _rectangles[_AGVs[whichAgv].MarkedLoad.X][_AGVs[whichAgv].MarkedLoad.Y].SwitchLoad(); //converts a specific GridBox, from Load, to Normal box (SwitchLoad function)
                _searchGrid.SetWalkableAt(_AGVs[whichAgv].MarkedLoad.X, _AGVs[whichAgv].MarkedLoad.Y, true);//marks the picked-up load as walkable AGAIN (since it is now a normal gridbox)
                _labeled_loads--;
                if (_labeled_loads <= 0)
                tree_stats.Nodes[2].Text = "All loads were picked up";
                else
                tree_stats.Nodes[2].Text = "Remaining loads: " + _labeled_loads;

                _AGVs[whichAgv].Status.Busy = true; //Sets the status of the AGV to Busy (because it has just picked-up the marked Load
                _AGVs[whichAgv].SetLoaded(); //changes the icon of the AGV and it now appears as Loaded
                tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Loaded";
                Refresh();

                //We needed to find a way to know if the animation is scheduled by Redraw or by GetNextLoad
                //fromstart means that an AGV is starting from its VERY FIRST position, heading to a Load and then to exit
                //When fromstart becomes false, it means that the AGV has completed its first task and now it is handled by GetNextLoad
                if (_fromstart[whichAgv]) {
                    _loads--;
                    _isLoad[_AGVs[whichAgv].MarkedLoad.X, _AGVs[whichAgv].MarkedLoad.Y] = 2;

                    _fromstart[whichAgv] = false;
                }
            }

            if (!_fromstart[whichAgv]) {
                //this is how we check if the AGV has arrived at the exit (red block - end point)
                if (_AGVs[whichAgv].GetLocation().X == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].X &&
                    _AGVs[whichAgv].GetLocation().Y == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].Y) {

                    _AGVs[whichAgv].LoadsDelivered++;
                    tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[0].Text = "Loads Delivered: " + _AGVs[whichAgv].LoadsDelivered;

                    _AGVs[whichAgv].Status.Busy = false; //change the AGV's status back to available again (not busy obviously)
                    tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Empty";
                    //here we scan the Grid and search for Loads that either ARE available or WILL BE available
                    //if there's at least 1 available Load, set isfreeload = true and stop the double For-loops
                    for (var k = 0; k < Globals.WidthBlocks; k++) {
                        for (var b = 0; b < Globals.HeightBlocks; b++) {
                            if (_isLoad[k, b] == 1 || _isLoad[k, b] == 4) //isLoad[ , ] == 1 means the corresponding Load is available at the moment
                                                                        //isLoad[ ,] == 4 means that the corresponding Load is surrounded by other 
                            {                                        //loads and TEMPORARILY unavailable - will be freed later
                                isfreeload = true;
                                k = Globals.WidthBlocks;
                                b = Globals.HeightBlocks;
                            }
                        }
                    }


                    if (_loads > 0 && isfreeload) { //means that the are still Loads left in the Grid, that can be picked up

                        Reset(whichAgv);
                        _AGVs[whichAgv].Status.Busy = true;
                        _AGVs[whichAgv].MarkedLoad = new Point();
                        GetNextLoad(whichAgv); //function that is responsible for Aaaaaall the future path planning


                        _AGVs[whichAgv].Status.Busy = false;
                        _AGVs[whichAgv].SetEmpty();

                    } else { //if no other AVAILABLE Loads are found in the grid
                        _AGVs[whichAgv].SetEmpty();
                        isfreeload = false;
                        
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Finished";
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "No load to pick";
                        StopTimers(whichAgv);
                    }

                    _onWhichStep[whichAgv] = -1;
                    whichStep = 0;

                }
            } else {
                if (!_AGVs[whichAgv].HasLoadToPick) {
                    if (_AGVs[whichAgv].GetLocation().X == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].X &&
                        _AGVs[whichAgv].GetLocation().Y == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].Y) {
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "No load to pick";
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Finished";
                        StopTimers(whichAgv);
                    }
                }
                if (_isLoad[_AGVs[whichAgv].MarkedLoad.X, _AGVs[whichAgv].MarkedLoad.Y] == 2) //if the AGV has picked up the Load it has marked...
                    if (_AGVs[whichAgv].GetLocation().X == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].X &&
                        _AGVs[whichAgv].GetLocation().Y == _rectangles[_endPointCoords.X / Globals.BlockSide][(_endPointCoords.Y - Globals.TopBarOffset) / Globals.BlockSide].Y) {
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "No load to pick";
                        tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Finished";
                        StopTimers(whichAgv);
                    }
            }
            if (!_AGVs[whichAgv].HasLoadToPick && !_fromstart[whichAgv]) {
                tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[1].Text = "No load to pick";
                tree_stats.Nodes.Find("AGV:" + (whichAgv), false)[0].Nodes[2].Text = "Status: Finished";
                StopTimers(whichAgv);
            }

            //if at least 1 timer is active, do not let the user access the Checkboxes etc. etc
            if (!(timer0.Enabled
                || timer1.Enabled
                || timer2.Enabled
                || timer3.Enabled
                || timer4.Enabled)) {
                gb_settings.Enabled = true;
                settings_menu.Enabled = true;
                nud_weight.Enabled = true;
                cb_type.Enabled = true;
            }

            //when all agvs have finished their tasks
            if (!timer0.Enabled
                && !timer1.Enabled
                && !timer2.Enabled
                && !timer3.Enabled
                && !timer4.Enabled) {
                //clear all the paths
                for (short i = 0; i < _startPos.Count; i++)
                    _AGVs[i].JumpPoints = new List<GridPos>();

                toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
                _allowHighlight = false;
                highlightOverCurrentBoxToolStripMenuItem.Checked = _allowHighlight;
                TriggerStartMenu(false);
                Refresh();
                Invalidate(); //invalidates the form, causing it to "refresh" the graphics
            }

        }

        //function that calculates all the intermediate points between each turning point (or jumpPoint if you may)
        private void DrawPoints(GridLine x, int agvIndex) {
            //think of the incoming GridLine as follows:
            //If you want to move from A to B, there might be an obstacle in the way, which must be bypassed
            //For this purpose, there must be found a point to break the final route into 2 smaller (let's say A->b + b->B (AB in total)
            //The incoming GridLine contains the pair of Coordinates for each one of the smaller routes
            //So, for our example, GridLine x containts the starting A(x,y) & b(x,y)
            //In a nutshell, this functions calculates all the child-steps of the parent-Line, determined by x.fromX,x.fromY and x.toX,x.toY


            //the parent-Line will finaly consist of many pairs of (x,y): e.g [X1,Y1 / X2,Y2 / X3,Y3 ... Xn,Yn]
           
            var x1 = x.FromX;
            var y1 = x.FromY;
            var x2 = x.ToX;
            var y2 = x.ToY;
            double distance = _f.GetLength(x1, y1, x2, y2); //function that returns the Euclidean distance between 2 points

            double side = _f.getSide(_rectangles[0][0].Height
                            , _rectangles[0][0].Height); //function that returns the hypotenuse of a GridBox

            int distanceBlocks = -1; //the quantity of blocks,matching the current line's length

            //Calculates the number of GridBoxes that the Line consists of. Calculation depends on 2 scenarios:
            //Scenario 1: Line is Diagonal
            //Scenario 2: Line is Straight
            if ((x1 < x2) && (y1 < y2)) //diagonal-right bottom direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 < x2) && (y1 > y2)) //diagonal-right top direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 > x2) && (y1 < y2)) //diagonal-left bottom direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 > x2) && (y1 > y2)) //diagonal-left top direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((y1 == y2) || (x1 == x2)) //horizontal or vertical
                distanceBlocks = Convert.ToInt32(distance / _rectangles[0][0].Width);
            else
                MessageBox.Show(this, "Unexpected error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //1d array of points.used to track all the points of current line
            Point[] currentLinePoints = new Point[distanceBlocks];
           
            //here we calculate the X,Y coordinates of all the intermediate points
            for (var i = 0; i < distanceBlocks; i++) {
                _calibrated = false;
                double t;
                if (distance != 0) //obviously, distance cannot be zero
                    t = ((side) / distance);
                else
                    return;

                //these are the x,y coord that are calculated in every for-loop
                _a = Convert.ToInt32(((1 - t) * x1) + (t * x2));
                _b = Convert.ToInt32(((1 - t) * y1) + (t * y2));
                Point p = new Point(_a, _b); //merges the calculated x,y into 1 Point variable

                for (var k = 0; k < Globals.WidthBlocks; k++)
                    for (var l = 0; l < Globals.HeightBlocks; l++)
                        if (_rectangles[k][l].BoxRec.Contains(p)) { //this is how we assign the previously calculated pair of X,Y to a GridBox

                            //a smart way to handle GridBoxes from their center
                            int sideX = _rectangles[k][l].BoxRec.X + ((Globals.BlockSide / 2) - 1);
                            int sideY = _rectangles[k][l].BoxRec.Y + ((Globals.BlockSide / 2) - 1);
                            currentLinePoints[i].X = sideX;
                            currentLinePoints[i].Y = sideY;

                            if (dotsToolStripMenuItem.Checked) {
                                using (SolidBrush br = new SolidBrush(Color.BlueViolet))
                                    _paper.FillEllipse(br, currentLinePoints[i].X - 3,
                                        currentLinePoints[i].Y - 3,
                                        5, 5);
                            }

                            using (Font stepFont = new Font("Tahoma", 8, FontStyle.Bold))//Font used for numbering the steps/current block)
                            {
                                using (SolidBrush fontBr = new SolidBrush(Color.FromArgb(53, 153, 153)))
                                    if (stepsToolStripMenuItem.Checked)
                                        _paper.DrawString(_AGVs[agvIndex].StepsCounter + ""
                                        , stepFont
                                        , fontBr
                                        , currentLinePoints[i]);

                            }
                            _calibrated = true;

                        }

                if (_calibrated) { //for each one of the above calculations, we check if the calibration has been done correctly and, if so, each pair is inserted to the corresponding AGV's steps List 
                    _AGVs[agvIndex].Steps[_AGVs[agvIndex].StepsCounter].X = currentLinePoints[i].X;
                    _AGVs[agvIndex].Steps[_AGVs[agvIndex].StepsCounter].Y = currentLinePoints[i].Y;
                    _AGVs[agvIndex].StepsCounter++;
                }
                //initialize next steps.
                x1 = currentLinePoints[i].X;
                y1 = currentLinePoints[i].Y;
                distance = _f.GetLength(x1, y1, x2, y2);

            }


        }

        //function that resets all of the used objects so they are ready for reuse, preventing memory leaks
        private void FullyRestore() {

          
            if (tree_stats.Nodes[1].IsExpanded)
                tree_stats.Nodes[1].Collapse();

            tree_stats.Nodes[1].Nodes[0].Text = "CO: -";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: -";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: -";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: -";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: -";

            _labeled_loads = 0;

            if (_onWhichStep != null)
                Array.Clear(_onWhichStep, 0, _onWhichStep.GetLength(0));

            if (_trappedStatus != null)
                Array.Clear(_trappedStatus, 0, _trappedStatus.GetLength(0));


            for (short i = 0; i < _AGVs.Count; i++)
                _AGVs[i].KillIcon();


            if (_importmap != null) {
                Array.Clear(_importmap, 0, _importmap.GetLength(0));
                Array.Clear(_importmap, 0, _importmap.GetLength(1));
            }

            if (BackgroundImage != null)
                BackgroundImage = null;

            _fromstart = new bool[Globals.MaximumAGVs];


            _startPos = new List<GridPos>();
            _endPointCoords = new Point(-1, -1);
            _selectedColor = Color.DarkGray;

            for (short i = 0; i < _startPos.Count(); i++)
                _AGVs[i].JumpPoints = new List<GridPos>();


            _searchGrid = new StaticGrid(Globals.WidthBlocks, Globals.HeightBlocks);

            _alwaysCross =
            aGVIndexToolStripMenuItem.Checked =
            _beforeStart =
            _allowHighlight = true;

            _atLeastOneObstacle =
            _ifNoObstacles =
            _never =
            _imported =
            _calibrated =
            _isMouseDown =
            _mapHasLoads = false;

            _useHalt = false;
            priorityRulesbetaToolStripMenuItem.Checked = false;

            _importedLayout = null;
            _jumpParam = null;
            _paper = null;
            _loads = _posIndex = 0;

            _a
            = _b
            = new int();


            _AGVs = new List<Vehicle>();
            _CO2 = _CO = _NOx = _THC = _globalWarming = 0;

            _allowHighlight = true;
            highlightOverCurrentBoxToolStripMenuItem.Enabled = true;
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;



            _isLoad = new int[Globals.WidthBlocks, Globals.HeightBlocks];
            _rectangles = new GridBox[Globals.WidthBlocks][];
            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                _rectangles[widthTrav] = new GridBox[Globals.HeightBlocks];

            //jagged array has to be resetted like this
            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++)
                    _rectangles[i][j] = new GridBox(i * Globals.BlockSide, j * Globals.BlockSide + Globals.TopBarOffset, BoxType.Normal);

           
            Initialization();

            main_form_Load(new object(), new EventArgs());

            for (short i = 0; i < _AGVs.Count; i++)
                _AGVs[i].Status.Busy = false;

            Globals.TimerStep = 0;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = Globals.TimerInterval;
            

            nUD_AGVs.Value = _AGVs.Count;
            tree_stats.Nodes[2].Text = "Loads remaining: ";


        }

        //has to do with optical features in the Grid option from the menu
        private void UpdateBorderVisibility(bool hide) {
            if (hide) {
                for (var i = 0; i < Globals.WidthBlocks; i++)
                    for (var j = 0; j < Globals.HeightBlocks; j++)
                        _rectangles[i][j].BeTransparent();
                BackColor = Color.DarkGray;
            } else {
                for (var i = 0; i < Globals.WidthBlocks; i++)
                    for (var j = 0; j < Globals.HeightBlocks; j++)
                        if (_rectangles[i][j].BoxType == BoxType.Normal) {
                            _rectangles[i][j].BeVisible();

                            _boxDefaultColor = Globals.SemiTransparency ? Color.FromArgb(128, 255, 0, 255) : Color.WhiteSmoke;
                        }
                BackColor = _selectedColor;
            }

            //no need of invalidation since its done after the call of this function
        }

        //returns the number of _AGVs
        private int GetNumberOfAGVs() {
            short agvs = 0;
            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++)
                    if (_rectangles[i][j].BoxType == BoxType.Start)
                        agvs++;

            return agvs;
        }

        //function that returns a list that contains the Available for action _AGVs
        private List<GridPos> NotTrappedVehicles(List<GridPos> vehicles, GridPos end) {
            //Vehicles is a list with all the _AGVs that are inserted in the Grid by the user
            short listIndex = 0;
            short trappedIndex = 0;
          
            //First, we must assume that ALL the _AGVs are trapped and cannot move (trapped means they are prevented from reaching the END block)
            for (int i = 0; i < _trappedStatus.Length; i++)
                _trappedStatus[i] = true;

            do {
                bool removed = false;
                _jumpParam.Reset(vehicles[listIndex], end); //we use the A* setting function and pass the 
                                                            //initial start point of every AGV and the final destination (end block)
                if (AStarFinder.FindPath(_jumpParam, nud_weight.Value).Count == 0) //if the number of JumpPoints that is calculated is 0 (zero)
                {                                                          //it means that there was no path found
                    vehicles.Remove(vehicles[listIndex]); //we removed, from the returning list, the AGV for which there was no path found
                    _AGVs.Remove(_AGVs[listIndex]); //we remove the corresponding AGV from the public list that contains all the _AGVs which will participate in the simulation
                    removed = true;
                } else
                    _trappedStatus[trappedIndex] = false; //since it's not trapped, we switch its state to false 

                if (!removed) {
                    _AGVs[listIndex].ID = listIndex;
                    listIndex++;
                }
                trappedIndex++;
            }
            while (listIndex < vehicles.Count); //the above process will be repeated until all elements of the incoming List are parsed.
            return vehicles; //list with NOT TRAPPED _AGVs' starting points (trapped _AGVs have been removed)


            //the point of this function is to consider every AGV as trap and then find out which _AGVs
            //eventually, are not trapped and keep ONLY those ones.
        }

        //Basic path planner function
        private void Redraw() {

            bool startFound = false;
            bool endFound = false;
            _mapHasLoads = false;

            GridPos endPos = new GridPos();

            _posIndex = 0;
            _startPos = new List<GridPos>(); //list that will be filled with the starting points of every AGV
            _AGVs = new List<Vehicle>();  //list that will be filled with objects of the class Vehicle
            _loadPos = new List<GridPos>(); //list that will be filled with the points of every Load
            _loads = 0;
            //Double FOR-loops to scan the whole Grid and perform the needed actions
            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++) {

                    if (_rectangles[i][j].BoxType == BoxType.Wall)
                        _searchGrid.SetWalkableAt(new GridPos(i, j), false);//Walls are marked as non-walkable
                    else
                        _searchGrid.SetWalkableAt(new GridPos(i, j), true);//every other block is marked as walkable (for now)

                    if (_rectangles[i][j].BoxType == BoxType.Load) {
                        _mapHasLoads = true;
                        _searchGrid.SetWalkableAt(new GridPos(i, j), false); //marks every Load as non-walkable
                        _isLoad[i, j] = 1; //considers every Load as available
                        _loads++; //counts the number of available Loads in the grid
                        _loadPos.Add(new GridPos(i, j)); //inserts the coordinates of the Load inside a list
                    }
                    if (_rectangles[i][j].BoxType == BoxType.Normal)
                        _rectangles[i][j].OnHover(_boxDefaultColor);

                    if (_rectangles[i][j].BoxType == BoxType.Start) {

                        if (_beforeStart) {
                            _searchGrid.SetWalkableAt(new GridPos(i, j), false); //initial starting points of AGV are non walkable until 1st run is completed
                        } else
                            _searchGrid.SetWalkableAt(new GridPos(i, j), true);

                        startFound = true;

                        _AGVs.Add(new Vehicle(this));
                        _AGVs[_posIndex].ID = _posIndex;

                        _startPos.Add(new GridPos(i, j)); //adds the starting coordinates of an AGV to the StartPos list

                        //a & b are used by DrawPoints() as the starting x,y for calculation purposes
                        _a = _startPos[_posIndex].X;
                        _b = _startPos[_posIndex].Y;

                        if (_posIndex < _startPos.Count) {
                            _startPos[_posIndex] = new GridPos(_startPos[_posIndex].X, _startPos[_posIndex].Y);
                            _posIndex++;
                        }
                    }

                    if (_rectangles[i][j].BoxType == BoxType.End) {
                        endFound = true;
                        endPos.X = i;
                        endPos.Y = j;
                        _endPointCoords = new Point(i * Globals.BlockSide, j * Globals.BlockSide + Globals.TopBarOffset);
                    }
                }

    

            if (!startFound || !endFound)
                return; //will return if there are no starting or end points in the Grid


            _posIndex = 0;

            if (_AGVs != null)
                for (short i = 0; i < _AGVs.Count(); i++)
                    if (_AGVs[i] != null) {
                        _AGVs[i].UpdateAGV();
                        _AGVs[i].Status.Busy = false; //initialize the status of _AGVs, as 'available'
                    }

            _startPos = NotTrappedVehicles(_startPos, endPos); //replaces the List with all the inserted _AGVs
                                                             //with a new one containing the right ones
            if (_mapHasLoads)
                KeepValidLoads(endPos); //calls a function that checks which Loads are available
                                        //to be picked up by _AGVs and removed the trapped ones.


            //For-loop to repeat the path-finding process for ALL the _AGVs that participate in the simulation
            for (short i = 0; i < _startPos.Count; i++) {
                if (_loadPos.Count != 0)
                    _loadPos = CheckForTrappedLoads(_loadPos, endPos);

                if (_loadPos.Count == 0) {
                    _mapHasLoads = false;
                    _AGVs[i].HasLoadToPick = false;
                } else {
                    _mapHasLoads = true;
                    _AGVs[i].HasLoadToPick = true;
                }


                if (_AGVs[i].Status.Busy == false) {
                    List<GridPos> jumpPointsList;
                    switch (_mapHasLoads) {
                        case true:
                            //====create the path FROM START TO LOAD, if load exists=====
                            for (int m = 0; m < _loadPos.Count; m++)
                                _searchGrid.SetWalkableAt(_loadPos[m], false); //Do not allow walk over any other load except the targeted one
                            _searchGrid.SetWalkableAt(_loadPos[0], true);

                            //use of the A* alorithms to find the path between AGV and its marked Load
                            _jumpParam.Reset(_startPos[_posIndex], _loadPos[0]);
                            jumpPointsList = AStarFinder.FindPath(_jumpParam, nud_weight.Value);
                            _AGVs[i].JumpPoints = jumpPointsList;
                            _AGVs[i].Status.Busy = true;
                            //====create the path FROM START TO LOAD, if load exists=====

                            //======FROM LOAD TO END======
                            for (int m = 0; m < _loadPos.Count; m++)
                                _searchGrid.SetWalkableAt(_loadPos[m], false);
                            _jumpParam.Reset(_loadPos[0], endPos);
                            jumpPointsList = AStarFinder.FindPath(_jumpParam, nud_weight.Value);
                            _AGVs[i].JumpPoints.AddRange(jumpPointsList);

                            //marks the load that each AGV picks up on the 1st route, as 3, so each agv knows where to go after delivering the 1st load
                            _isLoad[_loadPos[0].X, _loadPos[0].Y] = 3;
                            _AGVs[i].MarkedLoad = new Point(_loadPos[0].X, _loadPos[0].Y);

                            _loadPos.Remove(_loadPos[0]);
                            //======FROM LOAD TO END======
                            break;
                        case false:
                            _jumpParam.Reset(_startPos[_posIndex], endPos);
                            jumpPointsList = AStarFinder.FindPath(_jumpParam, nud_weight.Value);

                            _AGVs[i].JumpPoints = jumpPointsList;
                            break;
                    }
                }
                _posIndex++;
            }

            int c = 0;
            for (short i = 0; i < _startPos.Count; i++)
                c += _AGVs[i].JumpPoints.Count;


            for (short i = 0; i < _startPos.Count; i++)
                for (int j = 0; j < _AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine
                        (
                        _rectangles[_AGVs[i].JumpPoints[j].X][_AGVs[i].JumpPoints[j].Y],
                        _rectangles[_AGVs[i].JumpPoints[j + 1].X][_AGVs[i].JumpPoints[j + 1].Y]
                        );

                    _AGVs[i].Paths[j] = line;
                }

            for (int i = 0; i < _startPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref _AGVs[i].Paths, c - 1); //resize of the _AGVs steps Table
            if (_loads != 0)
                tree_stats.Nodes[2].Text = "Remaining loads: " + _loads;
            else
            tree_stats.Nodes[2].Text = "Remaining loads: ";
            Invalidate();
        }

        //function that determines which loads are valid to keep and which are not
        private void KeepValidLoads(GridPos endPoint) {
            int listIndex = 0;
            for (int i = 0; i < _loadPos.Count; i++)
                _searchGrid.SetWalkableAt(_loadPos[i], true); //assumes that all loads are walkable
                                                            //and only walls are in fact the only obstacles in the grid

            do {
                bool removed = false;
                _jumpParam.Reset(_loadPos[listIndex], endPoint); //tries to find path between each Load and the exit
                if (AStarFinder.FindPath(_jumpParam, nud_weight.Value).Count == 0) //if no path is found
                {
                    _isLoad[_loadPos[listIndex].X, _loadPos[listIndex].Y] = 2; //mark the corresponding load as NOT available
                    _loads--; //decrease the counter of total loads in the grid
                    _loadPos.RemoveAt(listIndex); //remove that load from the list
                    removed = true;
                }
                if (!removed) {
                    listIndex++;
                }

            } while (listIndex < _loadPos.Count); //loop repeats untill all loads are checked

            if (_loadPos.Count == 0)
                _mapHasLoads = false;
        }

        //function that scans and finds which loads are surrounded by other loads
        private List<GridPos> CheckForTrappedLoads(List<GridPos> pos, GridPos endPos) {
            int listIndex = 0;

            for (int i = 0; i < pos.Count; i++) {
                _searchGrid.SetWalkableAt(pos[i], false);
                _isLoad[pos[i].X, pos[i].Y] = 4;
            }

            //if the 1st AGV  cannot reach a Load, then that Load is  
            //removed from the loadPos and not considered as available - marked as "4"  (temporarily trapped)
            do {
                _searchGrid.SetWalkableAt(new GridPos(pos[0].X, pos[0].Y), true);
                _jumpParam.Reset(pos[0], endPos);
                if (AStarFinder.FindPath(_jumpParam, nud_weight.Value).Count == 0) {
                    _searchGrid.SetWalkableAt(new GridPos(pos[0].X, pos[0].Y), false);
                    pos.Remove(pos[0]); //load is removed from the List with available Loads

                } else {
                    _isLoad[pos[0].X, pos[0].Y] = 1; //otherwise, Load is marked as available
                    listIndex = pos.Count;
                }
            } while (listIndex < pos.Count);

            return pos;
        }

        //returns the number of steps until AGV reaches the marked Load
        private int GetStepsToLoad(int whichAgv) {
            Point iCords = new Point(_AGVs[whichAgv].GetMarkedLoad().X, _AGVs[whichAgv].GetMarkedLoad().Y);
            int step = -1;

            for (int i = 0; i < _AGVs[whichAgv].Steps.GetLength(0); i++)
                if (_AGVs[whichAgv].Steps[i].X - ((Globals.BlockSide / 2) - 1) == iCords.X &&
                    _AGVs[whichAgv].Steps[i].Y - ((Globals.BlockSide / 2) - 1) == iCords.Y) {
                    step = i;
                    i = _AGVs[whichAgv].Steps.GetLength(0);
                }

            if (step >= 0) return step;
            return -1;
        }

        //Reset function with overload for specific AGV 
        private void Reset(int whichAgv) //overloaded Reset
        {
            int c = _AGVs[0].Paths.Length;

            _AGVs[whichAgv].JumpPoints = new List<GridPos>(); //empties the AGV's JumpPoints List for the new JumpPoints to be added

            _startPos[whichAgv] = new GridPos(); //empties the correct start Pos for each AGV

            for (int i = 0; i < c; i++)
                _AGVs[whichAgv].Paths[i] = null;

            for (int j = 0; j < Globals.MaximumSteps; j++) {
                _AGVs[whichAgv].Steps[j].X = 0;
                _AGVs[whichAgv].Steps[j].Y = 0;
            }

            _AGVs[whichAgv].StepsCounter = 0;
        }


        //function for holding the AGV back so another can pass without colliding
        private void Halt(int index, int c) {
            _onWhichStep[index]--;
            if (!(c - 1 < 0)) //in case the intersection is in the 1st step of the route, then the index of that step will be 0. 
            {                  //this means that trying to get to the "_c -1" step, will have the index decreased to -1 causing the "index out of bound" crash
                int stepx = Convert.ToInt32(_AGVs[index].Steps[c - 1].X);
                int stepy = Convert.ToInt32(_AGVs[index].Steps[c - 1].Y);
                _AGVs[index].SetLocation(stepx - ((Globals.BlockSide / 2) - 1), stepy - ((Globals.BlockSide / 2) - 1));
            }
        }

        //manipulates the text of the Start button 
        private void TriggerStartMenu(bool t) {
            startToolStripMenuItem.Enabled = t;
            if (!t) {
                startToolStripMenuItem.Text = "Start            Clear and redraw the components please.";
                if (_endPointCoords.X == -1 && _endPointCoords.Y == -1)
                    startToolStripMenuItem.Text = "Start            Create a complete path please.";
                startToolStripMenuItem.ShortcutKeyDisplayString = "";
            } else {
                startToolStripMenuItem.Text = "Start";
                startToolStripMenuItem.ShortcutKeyDisplayString = "(Space)";
            }
        }

        //Path-planner for collecting all the remaining Loads in the Grid
        private void GetNextLoad(int whichAgv) {

            aGVIndexToolStripMenuItem.Checked = false;
            GridPos endPos = new GridPos();


            //finds the End point and uses it's coordinates as the starting coords for every AGV
            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++)
                    if (_rectangles[widthTrav][heightTrav].BoxType == BoxType.End)
                        try {
                            _startPos[whichAgv] = new GridPos(widthTrav, heightTrav);
                            _a = _startPos[whichAgv].X;
                            _b = _startPos[whichAgv].Y;
                        } catch { }

            List<GridPos> loadPos = new List<GridPos>();

            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++) {
                    if (_rectangles[i][j].BoxType == BoxType.Load)
                        _searchGrid.SetWalkableAt(new GridPos(i, j), false);

                    //places the available AND the temporarily trapped loads in a list
                    if (_isLoad[i, j] == 1 || _isLoad[i, j] == 4)
                        loadPos.Add(new GridPos(i, j));
                }
            loadPos = CheckForTrappedLoads(loadPos,new GridPos(_a,_b)); //scans the loadPos list to check which loads are available
            if (loadPos.Count == 0) {
                _AGVs[whichAgv].HasLoadToPick = false;
                return;
            }
            _isLoad[loadPos[0].X, loadPos[0].Y] = 3;
            _AGVs[whichAgv].MarkedLoad = new Point(loadPos[0].X, loadPos[0].Y);
            _loads--;
            endPos = loadPos[0];

            //Mark all loads as unwalkable,except the targetted ones
            for (var m = 0; m < loadPos.Count; m++)
                _searchGrid.SetWalkableAt(loadPos[m], false);
            _searchGrid.SetWalkableAt(loadPos[0], true);

            //creates the path between the AGV (which at the moment is at the exit) and the Load
            _jumpParam.Reset(_startPos[whichAgv], endPos);
            List<GridPos> jumpPointsList = AStarFinder.FindPath(_jumpParam, nud_weight.Value);
            _AGVs[whichAgv].JumpPoints = jumpPointsList;//adds the result from A* to the AGV's
                                                       //embedded List

            //Mark all loads as unwalkable
            for (var m = 0; m < loadPos.Count; m++)
                _searchGrid.SetWalkableAt(loadPos[m], false);

            int c = 0;
            for (short i = 0; i < _startPos.Count; i++) {
                c += _AGVs[i].JumpPoints.Count;
                if ((c - 1) > 0)
                    Array.Resize(ref _AGVs[i].Paths, c - 1);
            }


            for (int j = 0; j < _AGVs[whichAgv].JumpPoints.Count - 1; j++) {
                GridLine line = new GridLine(
                    _rectangles
                        [_AGVs[whichAgv].JumpPoints[j].X]
                        [_AGVs[whichAgv].JumpPoints[j].Y],
                    _rectangles
                        [_AGVs[whichAgv].JumpPoints[j + 1].X]
                        [_AGVs[whichAgv].JumpPoints[j + 1].Y]
                                           );

                _AGVs[whichAgv].Paths[j] = line;
            }


            //2nd part of route: Go to exit
            int oldC = c - 1;

            _jumpParam.Reset(endPos, _startPos[whichAgv]);
            jumpPointsList = AStarFinder.FindPath(_jumpParam, nud_weight.Value);
            _AGVs[whichAgv].JumpPoints.AddRange(jumpPointsList);

            c = 0;
            for (int i = 0; i < _startPos.Count; i++)
            {
                c += _AGVs[i].JumpPoints.Count;
                if ((c - 1) > 0)
                    Array.Resize(ref _AGVs[i].Paths, oldC + (c - 1));
            }


            for (short i = 0; i < _startPos.Count; i++)
                for (int j = 0; j < _AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine(
                        _rectangles
                            [_AGVs[i].JumpPoints[j].X]
                            [_AGVs[i].JumpPoints[j].Y],
                        _rectangles
                            [_AGVs[i].JumpPoints[j + 1].X]
                            [_AGVs[i].JumpPoints[j + 1].Y]
                                           );

                    _AGVs[i].Paths[j] = line;
                }

            Invalidate();
        }

        //function that starts the needed timers
        private void Timers() {
            //every timer is responsible for every agv for up to 5 _AGVs

            int c = 0;
            for (int i = 0; i < _trappedStatus.Length; i++)
                if (!_trappedStatus[i]) //array containing the status of AGV
                    c++; //counts the number of free-to-move _AGVs

            switch (c) //depending on the _c, the required timers will be started
            {
                case 1:
                    timer0.Start();
                    break;
                case 2:
                    timer0.Start();
                    timer1.Start();
                    break;
                case 3:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    break;
                case 4:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    break;
                case 5:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    timer4.Start();
                    break;
            }
        }
      
        private void ConfigUi() {

            if (Globals.SemiTransparency)
                Globals.SemiTransparent = Color.FromArgb(Globals.Opacity, Color.WhiteSmoke);

            for (int i = 0; i < _startPos.Count; i++) {
                _AGVs[i] = new Vehicle(this)
                {
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

            _tp = new ToolTip
            {

                AutomaticDelay = 0,
                ReshowDelay = 0,
                InitialDelay = 0,
                AutoPopDelay = 0,
                IsBalloon = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Grid Block Information",
            };

           

        }

        private void MeasureScreen() {
            Location = Screen.PrimaryScreen.Bounds.Location;

            int usableSize = Screen.PrimaryScreen.Bounds.Height - menuPanel.Height - Globals.BottomBarOffset - Globals.TopBarOffset;
            Globals.HeightBlocks = usableSize / Globals.BlockSide;

            usableSize = Screen.PrimaryScreen.Bounds.Width - tree_stats.Width - Globals.LeftBarOffset;
            Globals.WidthBlocks = usableSize / Globals.BlockSide;
            
        }

        //Initializes all the objects in main_form
        private void Initialization() {
            if (Globals.FirstFormLoad) {
                if (File.Exists("info.txt")) {
                    StreamReader reader = new StreamReader("info.txt");
                    try {
                        Globals.WidthBlocks = Convert.ToInt32(reader.ReadLine());
                        Globals.HeightBlocks = Convert.ToInt32(reader.ReadLine());
                        Globals.BlockSide = Convert.ToInt32(reader.ReadLine());
                    } catch {
                        MessageBox.Show("An error has occured while parsing the file to initialize form.\nPlease delete the file.");
                    }
                    reader.Close();
                }
                Globals.FirstFormLoad = false;
            }



            _isLoad = new int[Globals.WidthBlocks, Globals.HeightBlocks];
            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            _rectangles = new GridBox[Globals.WidthBlocks][];


            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                //declares the length of the seconds 1d array
                _rectangles[widthTrav] = new GridBox[Globals.HeightBlocks];
                for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {

                    //dynamically add the gridboxes into the _rectangles.
                    //size of the m_rectangels is constantly increasing (while adding
                    //the gridbox values) until size=height or size = width.
                    if (_imported) { //this IF is executed as long as the user has imported a map of his choice
                        _rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals.BlockSide) + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, _importmap[widthTrav, heightTrav]);
                        if (_importmap[widthTrav, heightTrav] == BoxType.Load) {
                            _isLoad[widthTrav, heightTrav] = 1;
                            _loads++;
                        }
                    } else {
                        _rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals.BlockSide) + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, BoxType.Normal);
                        _isLoad[widthTrav, heightTrav] = 2;
                    }


                }
            }
            if (_imported)
                _imported = false;


            _searchGrid = new StaticGrid(Globals.WidthBlocks, Globals.HeightBlocks);
            _jumpParam = new AStarParam(_searchGrid, Convert.ToSingle(Globals.AStarWeight));//Default value until user edit it
            _jumpParam.SetHeuristic(HeuristicMode.Manhattan); //default value until user edit it

            ConfigUi();
        }

        //Function for exporting the map
        private void Export() {
            sfd_exportmap.FileName = "";
            sfd_exportmap.Filter = "kagv Map (*.kmap)|*.kmap";

            if (sfd_exportmap.ShowDialog() == DialogResult.OK) {
                StreamWriter writer = new StreamWriter(sfd_exportmap.FileName);
                writer.WriteLine(
                    "Map info:\r\n"+
                    "Width blocks: " + Globals.WidthBlocks + 
                    "  Height blocks: " + Globals.HeightBlocks + 
                    "  BlockSide: " + Globals.BlockSide + 
                    "\r\n"
                    );
                for (var i = 0; i < Globals.WidthBlocks; i++) {
                    for (var j = 0; j < Globals.HeightBlocks; j++) {
                        writer.Write(_rectangles[i][j].BoxType + " ");
                    }
                    writer.Write("\r\n");
                }
                writer.Close();
            }

        }

        //Function for importing a map 
        private void Import() {

            ofd_importmap.Filter = "kagv Map (*.kmap)|*.kmap";
            ofd_importmap.FileName = "";


            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                bool proceed = false;
                string line = "";
                char[] sep = { ':', ' ' };

                StreamReader reader = new StreamReader(ofd_importmap.FileName);
                do {
                    line = reader.ReadLine();
                    if (line.Contains("Width blocks:") && line.Contains("Height blocks:") && line.Contains("BlockSide:"))
                        proceed = true;
                } while (!(line.Contains("Width blocks:") && line.Contains("Height blocks:") && line.Contains("BlockSide:")) &&
                         !reader.EndOfStream);
                string[] lineArray = line.Split(sep);


                if (proceed) {

                    Globals.WidthBlocks = Convert.ToInt32(lineArray[3]);
                    Globals.HeightBlocks = Convert.ToInt32(lineArray[8]);
                    Globals.BlockSide = Convert.ToInt32(lineArray[12]);

                    FullyRestore();

                    
                    char[] delim = { ' ' };
                    reader.ReadLine();
                    _importmap = new BoxType[Globals.WidthBlocks, Globals.HeightBlocks];
                    string[] words = reader.ReadLine().Split(delim);

                    int startsCounter = 0;
                    for (int z = 0; z < _importmap.GetLength(0); z++) {
                        int i = 0;
                        foreach (string s in words)
                            if (i < _importmap.GetLength(1)) {
                                if (s == "Start") {
                                    _importmap[z, i] = BoxType.Start;
                                    startsCounter++;
                                } else if (s == "End")
                                    _importmap[z, i] = BoxType.End;
                                else if (s == "Normal")
                                    _importmap[z, i] = BoxType.Normal;
                                else if (s == "Wall")
                                    _importmap[z, i] = BoxType.Wall;
                                else if (s == "Load")
                                    _importmap[z, i] = BoxType.Load;
                                i++;
                            }
                        if (z == _importmap.GetLength(0) - 1) { } else
                            words = reader.ReadLine().Split(delim);
                    }
                    reader.Close();

                    nUD_AGVs.Value = startsCounter;
                    _imported = true;
                    Initialization();
                    Redraw();
                    if (_overImage) {
                        _importedLayout = _importedImageFile;
                        _overImage = false;
                    }
                } else
                    MessageBox.Show(this, "You have chosen an incompatible file import.\r\nPlease try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //function for importing an image as background
        private void ImportImage() {
            ofd_importmap.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|(*.jpg)";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                _importedLayout = Image.FromFile(ofd_importmap.FileName);
                _importedImageFile = Image.FromFile(ofd_importmap.FileName);
                _overImage = true;
            }

        }

        //Function that validates the user's click 
        private bool Isvalid(Point temp) {

            //The function received the coordinates of the user's click.
            //Clicking anywhere but on the Grid itself, will cause a "false" return, preventing
            //the click from giving any results

            if (temp.Y < menuPanel.Location.Y)
                return false;

            if (temp.X > _rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].BoxRec.X + (Globals.BlockSide - 1) + Globals.LeftBarOffset
            || temp.Y > _rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].BoxRec.Y + (Globals.BlockSide - 1)) // 18 because its 20-boarder size
                return false;

            if (!_rectangles[(temp.X - Globals.LeftBarOffset) / Globals.BlockSide][(temp.Y - Globals.TopBarOffset) / Globals.BlockSide].BoxRec.Contains(temp))
                return false;

            return true;
        }

        private void UpdateGridStats() {
            var pixelsWidth = Globals.WidthBlocks * Globals.BlockSide;
            var pixelsHeight = Globals.HeightBlocks * Globals.BlockSide;
            lb_width.Text = "Width blocks: " + Globals.WidthBlocks + ".  " + pixelsWidth + " pixels";
            lb_height.Text = "Height blocks: " + Globals.HeightBlocks + ". " + pixelsHeight + " pixels";
            nud_side.Value = Convert.ToDecimal(Globals.BlockSide);
        }

        private void ToDebugPanel(object var, string varname) {
            TreeNode node = new TreeNode(varname.ToString())
            {
                Name = varname,
                Text = varname + ":" + var,
            };
            tree_stats.Nodes[0].Nodes.Add(node);
            tree_stats.Nodes[0].Expand();
        }

        private void ReflectVariables() {
            if (Globals.Debug) {
                //ToDebugPanel(Globals.AStarWeight, nameof(Globals.AStarWeight));
                //add more reflections here
            }
        }

    }
}
