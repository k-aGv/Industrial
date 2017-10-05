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

namespace kagv {

    public partial class main_form : IMessageFilter {

        //Message callback of key
        public bool PreFilterMessage(ref Message _msg) {
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return false;
            if (_msg.Msg == 0x101) //0x101 means key is up
            {

                holdCTRL = false;
                panel_resize.Visible = false;
                toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
                Refresh();
                return true;
            }
            return false;
        }

        //function for handling keystrokes and assigning specific actions to them
        protected override bool ProcessCmdKey(ref Message _msg, Keys _keyData) {
            bool emptymap = true;
            if (ModifierKeys.HasFlag(Keys.Control) && !holdCTRL) {

                if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                    return false;

                for (int k = 0; k < Globals._WidthBlocks; k++) {
                    for (int l = 0; l < Globals._HeightBlocks; l++) {
                        if (m_rectangles[k][l].boxType != BoxType.Normal) {
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
                        holdCTRL = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        FullyRestore();
                        return true;
                    } else return false;
                }

                if (overImage) {
                    DialogResult s = MessageBox.Show("Grid resize is only possible in an empty grid\nThe grid will be deleted.\nProceed?"
                                  , "Grid Resize triggered", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (s == DialogResult.Yes) {
                        holdCTRL = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        overImage = false;
                        FullyRestore();
                        return true;
                    } else return false;
                } else {
                    holdCTRL = true;
                    UpdateGridStats();
                    toolStripStatusLabel1.Text = "Release CTRL to return...";
                    panel_resize.Visible = true;
                    return true;
                }

            }

            switch (_keyData) {
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
                    for (int i = 0; i < startPos.Count; i++)
                        c += AGVs[i].JumpPoints.Count;

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
        private void Update_emissions(int which_agv) {

            if (cb_type.SelectedItem.ToString() == "LPG") {
                if (AGVs[which_agv].Status.Busy) {
                    CO2 += 2959.57;
                    CO += 27.04;
                    NOx += 19.63;
                    THC += 3.06;
                    GlobalWarming += 3.58;
                } else {
                    CO2 += 1935.16;
                    CO += 13.36;
                    NOx += 13.90;
                    THC += 1.51;
                    GlobalWarming += 2.33;
                }
            } else if (cb_type.SelectedItem.ToString() == "DSL") {
                if (AGVs[which_agv].Status.Busy) {
                    CO2 += 2130.11;
                    CO += 7.28;
                    NOx += 20.16;
                    THC += 1.77;
                    GlobalWarming += 2.49;
                } else {
                    CO2 += 1510.83;
                    CO += 3.84;
                    NOx += 14.33;
                    THC += 1.08;
                    GlobalWarming += 1.2;
                }
            } else // ELE
                {
                CO2 = 0;
                CO = 0;
                NOx = 0;
                THC = 0;
                if (AGVs[which_agv].Status.Busy)
                    GlobalWarming += 0.67;
                else
                    GlobalWarming += 0.64;
            }

            
            if (!tree_stats.Nodes[1].IsExpanded)
                tree_stats.Nodes[1].Expand();

            tree_stats.Nodes[1].Nodes[0].Text = "CO: " + Math.Round(CO, 2) + " gr";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: " + Math.Round(CO2, 2) + " gr";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: " + Math.Round(NOx, 2) + " gr";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: " + Math.Round(THC, 2) + " gr";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";

        }

        private void StopTimers(int agv_index) {
            switch (agv_index) {
                case 0:
                    timer0.Stop();
                    agv1steps_LB.Text = "AGV 0: Finished";
                    break;
                case 1:
                    timer1.Stop();
                    agv2steps_LB.Text = "AGV 1: Finished";
                    break;
                case 2:
                    timer2.Stop();
                    agv3steps_LB.Text = "AGV 2: Finished";
                    break;
                case 3:
                    timer3.Stop();
                    agv4steps_LB.Text = "AGV 3: Finished";
                    break;
                case 4:
                    timer4.Stop();
                    agv5steps_LB.Text = "AGV 4: Finished";
                    break;
            }
        }

        //function that executes the whole animation. Will be explaining thoroughly below
        private void Animator(int which_step, int which_agv) {
            
            

            //we use the incoming parameters, given from the corresponding Timer that calls Animator at a given time
            //steps_counter index tells us on which Step the timer is AT THE TIME this function is called
            //agv_index index tells us which timer is calling this function so as to know which AGV will be handled
            int stepx = Convert.ToInt32(AGVs[which_agv].Steps[which_step].X);
            int stepy = Convert.ToInt32(AGVs[which_agv].Steps[which_step].Y);

            //if, for any reason, the above steps are set to "0", obviously something is wrong so function returns
            if (stepx == 0 || stepx == 0)
                return;


            bool isfreeload = false;
            bool halted = false;

            DisplayStepsToLoad(which_step, which_agv); //Call of function that shows information regarding the status of AGVs in the Monitoring Panel
            Update_emissions(which_agv); //Call of function that updates the values of emissions

            //RULES OF WHICH AGV WILL STOP WILL BE ADDED
            if (use_Halt) {
                for (int i = 0; i < startPos.Count; i++)
                    if (which_agv != i
                        && AGVs[i].GetLocation() != new Point(0, 0)
                        && AGVs[which_agv].GetLocation() == AGVs[i].GetLocation()
                        && AGVs[which_agv].GetLocation() != endPointCoords
                        ) {
                        Halt(which_agv, which_step); //function for manipulating the movement of AGVs - must be perfected (still under dev)
                        halted = true;
                    } else
                        if (!halted)
                        AGVs[which_agv].SetLocation(stepx - ((Globals._BlockSide / 2) - 1) + 1, stepy - ((Globals._BlockSide / 2) - 1) + 1);
            } else
                AGVs[which_agv].SetLocation(stepx - ((Globals._BlockSide / 2) - 1) + 1, stepy - ((Globals._BlockSide / 2) - 1) + 1); //this is how we move the AGV on the grid (Setlocation function)

            /////////////////////////////////////////////////////////////////
            //Here is the part where an AGV arrives at the Load it has marked.
            if (AGVs[which_agv].GetMarkedLoad() == AGVs[which_agv].GetLocation() &&
                !AGVs[which_agv].Status.Busy) {

                m_rectangles[AGVs[which_agv].MarkedLoad.X][AGVs[which_agv].MarkedLoad.Y].SwitchLoad(); //converts a specific GridBox, from Load, to Normal box (SwitchLoad function)
                searchGrid.SetWalkableAt(AGVs[which_agv].MarkedLoad.X, AGVs[which_agv].MarkedLoad.Y, true);//marks the picked-up load as walkable AGAIN (since it is now a normal gridbox)
                labeled_loads--;
                if (labeled_loads <= 0)
                    loads_label.Text = "All loads have been picked up";
                else
                    loads_label.Text = "Loads remaining: " + labeled_loads;

                AGVs[which_agv].Status.Busy = true; //Sets the status of the AGV to Busy (because it has just picked-up the marked Load
                AGVs[which_agv].SetLoaded(); //changes the icon of the AGV and it now appears as Loaded
                Refresh();

                //We needed to find a way to know if the animation is scheduled by Redraw or by GetNextLoad
                //fromstart means that an AGV is starting from its VERY FIRST position, heading to a Load and then to exit
                //When fromstart becomes false, it means that the AGV has completed its first task and now it is handled by GetNextLoad
                if (fromstart[which_agv]) {
                    loads--;
                    isLoad[AGVs[which_agv].MarkedLoad.X, AGVs[which_agv].MarkedLoad.Y] = 2;

                    fromstart[which_agv] = false;
                }
            }

            if (!fromstart[which_agv]) {
                //this is how we check if the AGV has arrived at the exit (red block - end point)
                if (AGVs[which_agv].GetLocation().X == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].x &&
                    AGVs[which_agv].GetLocation().Y == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].y) {

                    AGVs[which_agv].LoadsDelivered++;
                    tree_stats.Nodes.Find("AGV:" + (which_agv), false)[0].Nodes[0].Text = "Loads Delivered: " + AGVs[which_agv].LoadsDelivered;
                    AGVs[which_agv].Status.Busy = false; //change the AGV's status back to available again (not busy obviously)

                    //here we scan the Grid and search for Loads that either ARE available or WILL BE available
                    //if there's at least 1 available Load, set isfreeload = true and stop the double For-loops
                    for (int k = 0; k < Globals._WidthBlocks; k++) {
                        for (int b = 0; b < Globals._HeightBlocks; b++) {
                            if (isLoad[k, b] == 1 || isLoad[k, b] == 4) //isLoad[ , ] == 1 means the corresponding Load is available at the moment
                                                                        //isLoad[ ,] == 4 means that the corresponding Load is surrounded by other 
                            {                                        //loads and TEMPORARILY unavailable - will be freed later
                                isfreeload = true;
                                k = Globals._WidthBlocks;
                                b = Globals._HeightBlocks;
                            }
                        }
                    }


                    if (loads > 0 && isfreeload) { //means that the are still Loads left in the Grid, that can be picked up

                        Reset(which_agv);
                        AGVs[which_agv].Status.Busy = true;
                        AGVs[which_agv].MarkedLoad = new Point();
                        GetNextLoad(which_agv); //function that is responsible for Aaaaaall the future path planning


                        AGVs[which_agv].Status.Busy = false;
                        AGVs[which_agv].SetEmpty();

                    } else { //if no other AVAILABLE Loads are found in the grid
                        AGVs[which_agv].SetEmpty();
                        isfreeload = false;
                        StopTimers(which_agv);
                    }

                    on_which_step[which_agv] = -1;
                    which_step = 0;

                }
            } else {
                if (!AGVs[which_agv].HasLoadToPick) {
                    if (AGVs[which_agv].GetLocation().X == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].x &&
                        AGVs[which_agv].GetLocation().Y == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].y)
                        StopTimers(which_agv);
                }
                if (isLoad[AGVs[which_agv].MarkedLoad.X, AGVs[which_agv].MarkedLoad.Y] == 2) //if the AGV has picked up the Load it has marked...
                    if (AGVs[which_agv].GetLocation().X == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].x &&
                        AGVs[which_agv].GetLocation().Y == m_rectangles[endPointCoords.X / Globals._BlockSide][(endPointCoords.Y - Globals._TopBarOffset) / Globals._BlockSide].y) {
                        StopTimers(which_agv);
                    }
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
                for (int i = 0; i < startPos.Count(); i++)
                    AGVs[i].JumpPoints = new List<GridPos>();

                toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
                allowHighlight = false;
                highlightOverCurrentBoxToolStripMenuItem.Checked = allowHighlight;
                TriggerStartMenu(false);
                Refresh();
                Invalidate(); //invalidates the form, causing it to "refresh" the graphics
            }

        }

        //function that calculates all the intermediate points between each turning point (or jumpPoint if you may)
        private void DrawPoints(GridLine x, int agv_index) {
            //think of the incoming GridLine as follows:
            //If you want to move from A to B, there might be an obstacle in the way, which must be bypassed
            //For this purpose, there must be found a point to break the final route into 2 smaller (let's say A->b + b->B (AB in total)
            //The incoming GridLine contains the pair of Coordinates for each one of the smaller routes
            //So, for our example, GridLine x containts the starting A(x,y) & b(x,y)
            //In a nutshell, this functions calculates all the child-steps of the parent-Line, determined by x.fromX,x.fromY and x.toX,x.toY


            //the parent-Line will finaly consist of many pairs of (x,y): e.g [X1,Y1 / X2,Y2 / X3,Y3 ... Xn,Yn]
            Point[] currentLinePoints;//1d array of points.used to track all the points of current line

            int x1 = x.fromX;
            int y1 = x.fromY;
            int x2 = x.toX;
            int y2 = x.toY;
            double distance = __f.GetLength(x1, y1, x2, y2); //function that returns the Euclidean distance between 2 points

            double side = __f.getSide(m_rectangles[0][0].height
                            , m_rectangles[0][0].height); //function that returns the hypotenuse of a GridBox

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
                distanceBlocks = Convert.ToInt32(distance / m_rectangles[0][0].width);
            else
                MessageBox.Show(this, "Unexpected error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);


            currentLinePoints = new Point[distanceBlocks];
            double t;
            //here we calculate the X,Y coordinates of all the intermediate points
            for (int i = 0; i < distanceBlocks; i++) {
                calibrated = false;

                if (distance != 0) //obviously, distance cannot be zero
                    t = ((side) / distance);
                else
                    return;

                //these are the x,y coord that are calculated in every for-loop
                a = Convert.ToInt32(((1 - t) * x1) + (t * x2));
                b = Convert.ToInt32(((1 - t) * y1) + (t * y2));
                Point _p = new Point(a, b); //merges the calculated x,y into 1 Point variable

                for (int k = 0; k < Globals._WidthBlocks; k++)
                    for (int l = 0; l < Globals._HeightBlocks; l++)
                        if (m_rectangles[k][l].boxRec.Contains(_p)) { //this is how we assign the previously calculated pair of X,Y to a GridBox

                            //a smart way to handle GridBoxes from their center
                            int sideX = m_rectangles[k][l].boxRec.X + ((Globals._BlockSide / 2) - 1);
                            int sideY = m_rectangles[k][l].boxRec.Y + ((Globals._BlockSide / 2) - 1);
                            currentLinePoints[i].X = sideX;
                            currentLinePoints[i].Y = sideY;

                            if (dotsToolStripMenuItem.Checked) {
                                using (SolidBrush br = new SolidBrush(Color.BlueViolet))
                                    paper.FillEllipse(br, currentLinePoints[i].X - 3,
                                        currentLinePoints[i].Y - 3,
                                        5, 5);
                            }

                            using (Font stepFont = new Font("Tahoma", 8, FontStyle.Bold))//Font used for numbering the steps/current block)
                            {
                                using (SolidBrush fontBR = new SolidBrush(Color.FromArgb(53, 153, 153)))
                                    if (stepsToolStripMenuItem.Checked)
                                        paper.DrawString(AGVs[agv_index].StepsCounter + ""
                                        , stepFont
                                        , fontBR
                                        , currentLinePoints[i]);

                            }
                            calibrated = true;

                        }

                if (calibrated) { //for each one of the above calculations, we check if the calibration has been done correctly and, if so, each pair is inserted to the corresponding AGV's steps List 
                    AGVs[agv_index].Steps[AGVs[agv_index].StepsCounter].X = currentLinePoints[i].X;
                    AGVs[agv_index].Steps[AGVs[agv_index].StepsCounter].Y = currentLinePoints[i].Y;
                    AGVs[agv_index].StepsCounter++;
                }
                //initialize next steps.
                x1 = currentLinePoints[i].X;
                y1 = currentLinePoints[i].Y;
                distance = __f.GetLength(x1, y1, x2, y2);

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

            loads_label.Text = "";
            labeled_loads = 0;

            if (on_which_step != null)
                Array.Clear(on_which_step, 0, on_which_step.GetLength(0));

            if (trappedStatus != null)
                Array.Clear(trappedStatus, 0, trappedStatus.GetLength(0));


            for (int i = 0; i < AGVs.Count; i++)
                AGVs[i].KillIcon();


            if (importmap != null) {
                Array.Clear(importmap, 0, importmap.GetLength(0));
                Array.Clear(importmap, 0, importmap.GetLength(1));
            }

            if (BackgroundImage != null)
                BackgroundImage = null;

            fromstart = new bool[Globals._MaximumAGVs];


            startPos = new List<GridPos>();
            endPointCoords = new Point(-1, -1);
            selectedColor = Color.DarkGray;

            for (int i = 0; i < startPos.Count(); i++)
                AGVs[i].JumpPoints = new List<GridPos>();

            
            searchGrid = new StaticGrid(Globals._WidthBlocks, Globals._HeightBlocks);

            alwaysCross =
            aGVIndexToolStripMenuItem.Checked =
            beforeStart =
            allowHighlight = true;

            atLeastOneObstacle =
            ifNoObstacles =
            never =
            imported =
            importedImage =
            calibrated =
            isMouseDown =
            mapHasLoads = false;

            use_Halt = false;
            priorityRulesbetaToolStripMenuItem.Checked = false;

            importedLayout = null;
            jumpParam = null;
            paper = null;
            loads = pos_index = 0;

            a
            = b
            = new int();


            AGVs = new List<Vehicle>();
            CO2 = CO = NOx = THC = GlobalWarming = 0;

            allowHighlight = true;
            highlightOverCurrentBoxToolStripMenuItem.Enabled = true;
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;



            isLoad = new int[Globals._WidthBlocks, Globals._HeightBlocks];
            m_rectangles = new GridBox[Globals._WidthBlocks][];
            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                m_rectangles[widthTrav] = new GridBox[Globals._HeightBlocks];

            //jagged array has to be resetted like this
            for (int i = 0; i < Globals._WidthBlocks; i++)
                for (int j = 0; j < Globals._HeightBlocks; j++)
                    m_rectangles[i][j] = new GridBox(i * Globals._BlockSide, j * Globals._BlockSide + Globals._TopBarOffset, BoxType.Normal);

           
            Initialization();

            main_form_Load(new object(), new EventArgs());

            for (int i = 0; i < AGVs.Count; i++)
                AGVs[i].Status.Busy = false;

            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = 50;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";

            nUD_AGVs.Value = AGVs.Count;

        }

        //has to do with optical features in the Grid option from the menu
        private void UpdateBorderVisibility(bool hide) {
            if (hide) {
                for (int i = 0; i < Globals._WidthBlocks; i++)
                    for (int j = 0; j < Globals._HeightBlocks; j++)
                        m_rectangles[i][j].BeTransparent();
                BackColor = Color.DarkGray;
            } else {
                for (int i = 0; i < Globals._WidthBlocks; i++)
                    for (int j = 0; j < Globals._HeightBlocks; j++)
                        if (m_rectangles[i][j].boxType == BoxType.Normal) {
                            m_rectangles[i][j].BeVisible();

                            boxDefaultColor = Globals._SemiTransparency ? Color.FromArgb(128, 255, 0, 255) : Color.WhiteSmoke;
                        }
                BackColor = selectedColor;
            }

            //no need of invalidation since its done after the call of this function
        }

        //returns the number of AGVs
        private int GetNumberOfAGVs() {
            int agvs = 0;
            for (int i = 0; i < Globals._WidthBlocks; i++)
                for (int j = 0; j < Globals._HeightBlocks; j++)
                    if (m_rectangles[i][j].boxType == BoxType.Start)
                        agvs++;

            return agvs;
        }

        //function that returns a list that contains the Available for action AGVs
        private List<GridPos> NotTrappedVehicles(List<GridPos> Vehicles, GridPos End) {
            //Vehicles is a list with all the AGVs that are inserted in the Grid by the user
            int list_index = 0;
            int trapped_index = 0;
            bool removed;

            //First, we must assume that ALL the AGVs are trapped and cannot move (trapped means they are prevented from reaching the END block)
            for (int i = 0; i < trappedStatus.Length; i++)
                trappedStatus[i] = true;

            do {
                removed = false;
                jumpParam.Reset(Vehicles[list_index], End); //we use the A* setting function and pass the 
                                                            //initial start point of every AGV and the final destination (end block)
                if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0) //if the number of JumpPoints that is calculated is 0 (zero)
                {                                                          //it means that there was no path found
                    Vehicles.Remove(Vehicles[list_index]); //we removed, from the returning list, the AGV for which there was no path found
                    AGVs.Remove(AGVs[list_index]); //we remove the corresponding AGV from the public list that contains all the AGVs which will participate in the simulation
                    removed = true;
                } else
                    trappedStatus[trapped_index] = false; //since it's not trapped, we switch its state to false 

                if (!removed) {
                    AGVs[list_index].ID = list_index;
                    list_index++;
                }
                trapped_index++;
            }
            while (list_index < Vehicles.Count); //the above process will be repeated until all elements of the incoming List are parsed.
            return Vehicles; //list with NOT TRAPPED AGVs' starting points (trapped AGVs have been removed)


            //the point of this function is to consider every AGV as trap and then find out which AGVs
            //eventually, are not trapped and keep ONLY those ones.
        }

        //Basic path planner function
        private void Redraw() {

            bool start_found = false;
            bool end_found = false;
            mapHasLoads = false;

            GridPos endPos = new GridPos();

            pos_index = 0;
            startPos = new List<GridPos>(); //list that will be filled with the starting points of every AGV
            AGVs = new List<Vehicle>();  //list that will be filled with objects of the class Vehicle
            loadPos = new List<GridPos>(); //list that will be filled with the points of every Load
            loads = 0;
            //Double FOR-loops to scan the whole Grid and perform the needed actions
            for (int i = 0; i < Globals._WidthBlocks; i++)
                for (int j = 0; j < Globals._HeightBlocks; j++) {

                    if (m_rectangles[i][j].boxType == BoxType.Wall)
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);//Walls are marked as non-walkable
                    else
                        searchGrid.SetWalkableAt(new GridPos(i, j), true);//every other block is marked as walkable (for now)

                    if (m_rectangles[i][j].boxType == BoxType.Load) {
                        mapHasLoads = true;
                        searchGrid.SetWalkableAt(new GridPos(i, j), false); //marks every Load as non-walkable
                        isLoad[i, j] = 1; //considers every Load as available
                        loads++; //counts the number of available Loads in the grid
                        loadPos.Add(new GridPos(i, j)); //inserts the coordinates of the Load inside a list
                    }
                    if (m_rectangles[i][j].boxType == BoxType.Normal)
                        m_rectangles[i][j].onHover(boxDefaultColor);

                    if (m_rectangles[i][j].boxType == BoxType.Start) {

                        if (beforeStart) {
                            searchGrid.SetWalkableAt(new GridPos(i, j), false); //initial starting points of AGV are non walkable until 1st run is completed
                        } else
                            searchGrid.SetWalkableAt(new GridPos(i, j), true);

                        start_found = true;

                        AGVs.Add(new Vehicle(this));
                        AGVs[pos_index].ID = pos_index;

                        startPos.Add(new GridPos(i, j)); //adds the starting coordinates of an AGV to the StartPos list

                        //a & b are used by DrawPoints() as the starting x,y for calculation purposes
                        a = startPos[pos_index].x;
                        b = startPos[pos_index].y;

                        if (pos_index < startPos.Count) {
                            startPos[pos_index] = new GridPos(startPos[pos_index].x, startPos[pos_index].y);
                            pos_index++;
                        }
                    }

                    if (m_rectangles[i][j].boxType == BoxType.End) {
                        end_found = true;
                        endPos.x = i;
                        endPos.y = j;
                        endPointCoords = new Point(i * Globals._BlockSide, j * Globals._BlockSide + Globals._TopBarOffset);
                    }
                }

    

            if (!start_found || !end_found)
                return; //will return if there are no starting or end points in the Grid


            pos_index = 0;

            if (AGVs != null)
                for (int i = 0; i < AGVs.Count(); i++)
                    if (AGVs[i] != null) {
                        AGVs[i].UpdateAGV();
                        AGVs[i].Status.Busy = false; //initialize the status of AGVs, as 'available'
                    }

            startPos = NotTrappedVehicles(startPos, endPos); //replaces the List with all the inserted AGVs
                                                             //with a new one containing the right ones
            if (mapHasLoads)
                KeepValidLoads(endPos); //calls a function that checks which Loads are available
                                        //to be picked up by AGVs and removed the trapped ones.


            //For-loop to repeat the path-finding process for ALL the AGVs that participate in the simulation
            for (int i = 0; i < startPos.Count; i++) {
                if (loadPos.Count != 0)
                    loadPos = CheckForTrappedLoads(loadPos);

                if (loadPos.Count == 0) {
                    mapHasLoads = false;
                    AGVs[i].HasLoadToPick = false;
                } else {
                    mapHasLoads = true;
                    AGVs[i].HasLoadToPick = true;
                }


                if (AGVs[i].Status.Busy == false) {
                    List<GridPos> JumpPointsList;
                    switch (mapHasLoads) {
                        case true:
                            //====create the path FROM START TO LOAD, if load exists=====
                            for (int m = 0; m < loadPos.Count; m++)
                                searchGrid.SetWalkableAt(loadPos[m], false); //Do not allow walk over any other load except the targeted one
                            searchGrid.SetWalkableAt(loadPos[0], true);

                            //use of the A* alorithms to find the path between AGV and its marked Load
                            jumpParam.Reset(startPos[pos_index], loadPos[0]);
                            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
                            AGVs[i].JumpPoints = JumpPointsList;
                            AGVs[i].Status.Busy = true;
                            //====create the path FROM START TO LOAD, if load exists=====

                            //======FROM LOAD TO END======
                            for (int m = 0; m < loadPos.Count; m++)
                                searchGrid.SetWalkableAt(loadPos[m], false);
                            jumpParam.Reset(loadPos[0], endPos);
                            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
                            AGVs[i].JumpPoints.AddRange(JumpPointsList);

                            //marks the load that each AGV picks up on the 1st route, as 3, so each agv knows where to go after delivering the 1st load
                            isLoad[loadPos[0].x, loadPos[0].y] = 3;
                            AGVs[i].MarkedLoad = new Point(loadPos[0].x, loadPos[0].y);

                            loadPos.Remove(loadPos[0]);
                            //======FROM LOAD TO END======
                            break;
                        case false:
                            jumpParam.Reset(startPos[pos_index], endPos);
                            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);

                            AGVs[i].JumpPoints = JumpPointsList;
                            break;
                    }
                }
                pos_index++;
            }

            int c = 0;
            for (int i = 0; i < startPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;


            for (int i = 0; i < startPos.Count; i++)
                for (int j = 0; j < AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine
                        (
                        m_rectangles[AGVs[i].JumpPoints[j].x][AGVs[i].JumpPoints[j].y],
                        m_rectangles[AGVs[i].JumpPoints[j + 1].x][AGVs[i].JumpPoints[j + 1].y]
                        );

                    AGVs[i].Paths[j] = line;
                }

            for (int i = 0; i < startPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, c - 1); //resize of the AGVs steps Table
            if (loads != 0)
                loads_label.Text = "Loads: " + loads;
            Invalidate();
        }

        //function that determines which loads are valid to keep and which are not
        private void KeepValidLoads(GridPos EndPoint) {
            int list_index = 0;
            bool removed;

            for (int i = 0; i < loadPos.Count; i++)
                searchGrid.SetWalkableAt(loadPos[i], true); //assumes that all loads are walkable
                                                            //and only walls are in fact the only obstacles in the grid
            do {
                removed = false;
                jumpParam.Reset(loadPos[list_index], EndPoint); //tries to find path between each Load and the exit
                if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0) //if no path is found
                {
                    isLoad[loadPos[list_index].x, loadPos[list_index].y] = 2; //mark the corresponding load as NOT available
                    loads--; //decrease the counter of total loads in the grid
                    loadPos.RemoveAt(list_index); //remove that load from the list
                    removed = true;
                }
                if (!removed) { 
                    searchGrid.SetWalkableAt(loadPos[list_index], false);
                    list_index++;
                }
                
            } while (list_index < loadPos.Count); //loop repeats untill all loads are checked

            //for (int i = 0; i < loadPos.Count; i++)
            //    searchGrid.SetWalkableAt(loadPos[i], false); //re-sets every Load to non-walkable

            if (loadPos.Count == 0)
                mapHasLoads = false;
        }

        //function that scans and finds which loads are surrounded by other loads
        private List<GridPos> CheckForTrappedLoads(List<GridPos> pos) {
            int list_index = 0;
            bool removed;

            //if the 1st AGV  cannot reach a Load, then that Load is  
            //removed from the loadPos and not considered as available - marked as "4"  (temporarily trapped)
            do {
                removed = false;
                searchGrid.SetWalkableAt(new GridPos(pos[list_index].x, pos[list_index].y), true);
                jumpParam.Reset(startPos[0], pos[list_index]);
                if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0) {
                    searchGrid.SetWalkableAt(new GridPos(pos[list_index].x, pos[list_index].y), false);
                    isLoad[pos[list_index].x, pos[list_index].y] = 4; //load is marked as trapped
                    pos.Remove(pos[list_index]); //load is removed from the List with available Loads
                    removed = true;
                } else
                    isLoad[pos[list_index].x, pos[list_index].y] = 1; //otherwise, Load is marked as available

                if (!removed)
                    list_index++;
            } while (list_index < pos.Count);

            return pos;
        }

        private void CheckForTrappedLoads() {
            int list_index = 0;
            bool removed;
            do {
                removed = false;
               // if (isLoad[loadPos[list_index].x, loadPos[list_index].y] == 4) {
                    searchGrid.SetWalkableAt(new GridPos(loadPos[list_index].x, loadPos[list_index].y), true);
                    jumpParam.Reset(startPos[0], loadPos[list_index]);
                    if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0) {

                        searchGrid.SetWalkableAt(new GridPos(loadPos[list_index].x, loadPos[list_index].y), false);
                        loadPos.Remove(loadPos[list_index]);
                        removed = true;
                    } else
                        isLoad[loadPos[list_index].x, loadPos[list_index].y] = 1;
               // }

                if (!removed)
                    list_index++;



            } while (list_index < loadPos.Count);
        }

        //returns the number of steps until AGV reaches the marked Load
        private int GetStepsToLoad(int which_agv) {
            Point iCords = new Point(AGVs[which_agv].GetMarkedLoad().X, AGVs[which_agv].GetMarkedLoad().Y);
            int step = -1;

            for (int i = 0; i < AGVs[which_agv].Steps.GetLength(0); i++)
                if (AGVs[which_agv].Steps[i].X - ((Globals._BlockSide / 2) - 1) == iCords.X &&
                    AGVs[which_agv].Steps[i].Y - ((Globals._BlockSide / 2) - 1) == iCords.Y) {
                    step = i;
                    i = AGVs[which_agv].Steps.GetLength(0);
                }

            if (step >= 0) return step;
            else return -1;
        }

        //Reset function with overload for specific AGV 
        private void Reset(int which_agv) //overloaded Reset
        {
            int c = AGVs[0].Paths.Length;

            AGVs[which_agv].JumpPoints = new List<GridPos>(); //empties the AGV's JumpPoints List for the new JumpPoints to be added

            startPos[which_agv] = new GridPos(); //empties the correct start Pos for each AGV

            for (int i = 0; i < c; i++)
                AGVs[which_agv].Paths[i] = null;

            for (int j = 0; j < Globals._MaximumSteps; j++) {
                AGVs[which_agv].Steps[j].X = 0;
                AGVs[which_agv].Steps[j].Y = 0;
            }

            AGVs[which_agv].StepsCounter = 0;
        }

        //Shows information on the Monitor Panel
        private void DisplayStepsToLoad(int counter, int agv_index) {
            int stepstoload;
            string agvinfo;

            if (GetStepsToLoad(agv_index) == -1)
                agvinfo = "AGV " + (agv_index) + ": Moving straight to the end point";
            else {
                stepstoload = (GetStepsToLoad(agv_index) - counter);
                agvinfo = "AGV " + (agv_index) + ": Marked load @" + GetStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                if (stepstoload < 0)
                    agvinfo = "AGV " + (agv_index) + " is Loaded.";
            }

            gb_monitor.Controls.Find(
                "agv" + (agv_index +1) + "steps_LB",
                 true)
                 [0].Text = agvinfo;

        }

        //function for holding the AGV back so another can pass without colliding
        private void Halt(int index, int _c) {
            on_which_step[index]--;
            if (!(_c - 1 < 0)) //in case the intersection is in the 1st step of the route, then the index of that step will be 0. 
            {                  //this means that trying to get to the "_c -1" step, will have the index decreased to -1 causing the "index out of bound" crash
                int stepx = Convert.ToInt32(AGVs[index].Steps[_c - 1].X);
                int stepy = Convert.ToInt32(AGVs[index].Steps[_c - 1].Y);
                AGVs[index].SetLocation(stepx - ((Globals._BlockSide / 2) - 1), stepy - ((Globals._BlockSide / 2) - 1));
            }
        }

        //manipulates the text of the Start button 
        private void TriggerStartMenu(bool t) {
            startToolStripMenuItem.Enabled = t;
            if (!t) {
                startToolStripMenuItem.Text = "Start            Clear and redraw the components please.";
                if (endPointCoords.X == -1 && endPointCoords.Y == -1)
                    startToolStripMenuItem.Text = "Start            Create a complete path please.";
                startToolStripMenuItem.ShortcutKeyDisplayString = "";
            } else {
                startToolStripMenuItem.Text = "Start";
                startToolStripMenuItem.ShortcutKeyDisplayString = "(Space)";
            }
        }

        //Path-planner for collecting all the remaining Loads in the Grid
        private void GetNextLoad(int which_agv) {

            aGVIndexToolStripMenuItem.Checked = false;
            GridPos endPos = new GridPos();


            //finds the End point and uses it's coordinates as the starting coords for every AGV
            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++)
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                        try {
                            startPos[which_agv] = new GridPos(widthTrav, heightTrav);
                            a = startPos[which_agv].x;
                            b = startPos[which_agv].y;
                        } catch { }

            List<GridPos> loadPos = new List<GridPos>();

            for (int i = 0; i < Globals._WidthBlocks; i++)
                for (int j = 0; j < Globals._HeightBlocks; j++) {
                    if (m_rectangles[i][j].boxType == BoxType.Load)
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);

                    //places the available AND the temporarily trapped loads in a list
                    if (isLoad[i, j] == 1 || isLoad[i, j] == 4)
                        loadPos.Add(new GridPos(i, j));
                }
            loadPos = CheckForTrappedLoads(loadPos); //scans the loadPos list to check which loads are available
            //CheckForTrappedLoads();
            isLoad[loadPos[0].x, loadPos[0].y] = 3;
            AGVs[which_agv].MarkedLoad = new Point(loadPos[0].x, loadPos[0].y);
            loads--;
            endPos = loadPos[0];

            //Mark all loads as unwalkable,except the targetted ones
            for (int m = 0; m < loadPos.Count; m++)
                searchGrid.SetWalkableAt(loadPos[m], false);
            searchGrid.SetWalkableAt(loadPos[0], true);

            //creates the path between the AGV (which at the moment is at the exit) and the Load
            jumpParam.Reset(startPos[which_agv], endPos);
            List<GridPos> JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
            AGVs[which_agv].JumpPoints = JumpPointsList;//adds the result from A* to the AGV's
                                                       //embedded List

            //Mark all loads as unwalkable
            for (int m = 0; m < loadPos.Count; m++)
                searchGrid.SetWalkableAt(loadPos[m], false);

            int c = 0;
            for (int i = 0; i < startPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < startPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, c - 1);


            for (int j = 0; j < AGVs[which_agv].JumpPoints.Count - 1; j++) {
                GridLine line = new GridLine(
                    m_rectangles
                        [AGVs[which_agv].JumpPoints[j].x]
                        [AGVs[which_agv].JumpPoints[j].y],
                   m_rectangles
                        [AGVs[which_agv].JumpPoints[j + 1].x]
                        [AGVs[which_agv].JumpPoints[j + 1].y]
                                           );

                AGVs[which_agv].Paths[j] = line;
            }


            //2nd part of route: Go to exit
            int old_c = c - 1;

            jumpParam.Reset(endPos, startPos[which_agv]);
            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
            AGVs[which_agv].JumpPoints.AddRange(JumpPointsList);

            c = 0;
            for (int i = 0; i < startPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < startPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, old_c + (c - 1));


            for (int i = 0; i < startPos.Count; i++)
                for (int j = 0; j < AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine(
                        m_rectangles
                            [AGVs[i].JumpPoints[j].x]
                            [AGVs[i].JumpPoints[j].y],
                        m_rectangles
                            [AGVs[i].JumpPoints[j + 1].x]
                            [AGVs[i].JumpPoints[j + 1].y]
                                           );

                    AGVs[i].Paths[j] = line;
                }

            Invalidate();
        }

        //function that starts the needed timers
        private void Timers() {
            //every timer is responsible for every agv for up to 5 AGVs

            int _c = 0;
            for (int i = 0; i < trappedStatus.Length; i++)
                if (!trappedStatus[i]) //array containing the status of AGV
                    _c++; //counts the number of free-to-move AGVs

            switch (_c) //depending on the _c, the required timers will be started
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
      
        private void ConfigUI() {

            if (Globals._SemiTransparency)
                Globals._SemiTransparent = Color.FromArgb(Globals._Opacity, Color.WhiteSmoke);

            for (int i = 0; i < startPos.Count; i++) {
                AGVs[i] = new Vehicle(this);
                AGVs[i].ID = i;
            }

            Width = ((Globals._WidthBlocks + 1) * Globals._BlockSide) + Globals._LeftBarOffset;
            Height = (Globals._HeightBlocks + 1) * Globals._BlockSide + Globals._BottomBarOffset + 7; //+7 for borders
            Size = new Size(Width, Height + Globals._BottomBarOffset);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //Transparent and SemiTransparent feature serves the agri/industrial branch recursively
            importImageLayoutToolStripMenuItem.Enabled = Globals._SemiTransparency;

            if (importImageLayoutToolStripMenuItem.Enabled)
                importImageLayoutToolStripMenuItem.Text = "Import image layout";
            else
                importImageLayoutToolStripMenuItem.Text = "Semi Transparency feature is disabled";

            stepsToolStripMenuItem.Checked = false;
            linesToolStripMenuItem.Checked =
            dotsToolStripMenuItem.Checked =
            bordersToolStripMenuItem.Checked =
            aGVIndexToolStripMenuItem.Checked =
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;


            Text = "K-aGv2 Simulator (Industrial branch)";
            gb_monitor.Size = new Size(Globals._gb_monitor_width, Globals._gb_monitor_height);
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = 50;
            refresh_label.Text = "Delay :" + timer0.Interval + " ms";

            loads_label.Location = new Point(refresh_label.Location.X + refresh_label.Width, refresh_label.Location.Y);

            agv1steps_LB.Text =
            agv2steps_LB.Text =
            agv3steps_LB.Text =
            agv4steps_LB.Text =
            agv5steps_LB.Text = "";

            //Do not show the START menu because there is no valid path yet
            TriggerStartMenu(false);

            rb_start.Checked = true;
            BackColor = Color.DarkGray;

            CenterToScreen();

            alwaysCrossMenu.Checked = alwaysCross;
            atLeastOneMenu.Checked = atLeastOneObstacle;
            neverCrossMenu.Checked = never;
            noObstaclesMenu.Checked = ifNoObstacles;

            manhattanToolStripMenuItem.Checked = true;

            tree_stats.Location = new Point(0, 25);
            tree_stats.Height = statusStrip1.Location.Y - tree_stats.Location.Y;


            debugToolStripMenuItem.Visible = Globals._Debug;
            if (!Globals._Debug) {
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

            nud_weight.Value = Convert.ToDecimal(Globals._AStarWeight);
            statusStrip1.Location = new Point(tree_stats.Width, Height - statusStrip1.Height);

            statusStrip1.BringToFront();

            tp = new ToolTip
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

            int usableSize = Screen.PrimaryScreen.Bounds.Height - menuPanel.Height - Globals._BottomBarOffset - Globals._TopBarOffset;
            Globals._HeightBlocks = usableSize / Globals._BlockSide;

            usableSize = Screen.PrimaryScreen.Bounds.Width - tree_stats.Width - Globals._LeftBarOffset;
            Globals._WidthBlocks = usableSize / Globals._BlockSide;
            
        }

        //Initializes all the objects in main_form
        private void Initialization() {
            if (Globals._FirstFormLoad) {
                if (File.Exists("info.txt")) {
                    StreamReader reader = new StreamReader("info.txt");
                    try {
                        Globals._WidthBlocks = Convert.ToInt32(reader.ReadLine());
                        Globals._HeightBlocks = Convert.ToInt32(reader.ReadLine());
                        Globals._BlockSide = Convert.ToInt32(reader.ReadLine());
                    } catch { MessageBox.Show("An error has occured while parsing the file to initialize form.\nPlease delete the file."); }
                    reader.Dispose();
                }
                Globals._FirstFormLoad = false;
            }



            isLoad = new int[Globals._WidthBlocks, Globals._HeightBlocks];
            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            m_rectangles = new GridBox[Globals._WidthBlocks][];


            for (int widthTrav = 0; widthTrav < Globals._WidthBlocks; widthTrav++) {
                //declares the length of the seconds 1d array
                m_rectangles[widthTrav] = new GridBox[Globals._HeightBlocks];
                for (int heightTrav = 0; heightTrav < Globals._HeightBlocks; heightTrav++) {

                    //dynamically add the gridboxes into the m_rectangles.
                    //size of the m_rectangels is constantly increasing (while adding
                    //the gridbox values) until size=height or size = width.
                    if (imported) { //this IF is executed as long as the user has imported a map of his choice
                        m_rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals._BlockSide) + Globals._LeftBarOffset, heightTrav * Globals._BlockSide + Globals._TopBarOffset, importmap[widthTrav, heightTrav]);
                        if (importmap[widthTrav, heightTrav] == BoxType.Load) {
                            isLoad[widthTrav, heightTrav] = 1;
                            loads++;
                        }
                    } else {
                        m_rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals._BlockSide) + Globals._LeftBarOffset, heightTrav * Globals._BlockSide + Globals._TopBarOffset, BoxType.Normal);
                        isLoad[widthTrav, heightTrav] = 2;
                    }


                }
            }
            if (imported)
                imported = false;


            searchGrid = new StaticGrid(Globals._WidthBlocks, Globals._HeightBlocks);
            jumpParam = new AStarParam(searchGrid, Convert.ToSingle(Globals._AStarWeight));//Default value until user edit it
            jumpParam.SetHeuristic(HeuristicMode.MANHATTAN); //default value until user edit it

            ConfigUI();
        }

        //Function for exporting the map
        private void Export() {
            sfd_exportmap.FileName = "";
            sfd_exportmap.Filter = "kagv Map (*.kmap)|*.kmap";

            if (sfd_exportmap.ShowDialog() == DialogResult.OK) {
                StreamWriter writer = new StreamWriter(sfd_exportmap.FileName);
                writer.WriteLine(
                    "Map info:\r\n"+
                    "Width blocks: " + Globals._WidthBlocks + 
                    "  Height blocks: " + Globals._HeightBlocks + 
                    "  BlockSide: " + Globals._BlockSide + 
                    "\r\n"
                    );
                for (int i = 0; i < Globals._WidthBlocks; i++) {
                    for (int j = 0; j < Globals._HeightBlocks; j++) {
                        writer.Write(m_rectangles[i][j].boxType + " ");
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
                string _line = "";
                char[] sep = { ':', ' ' };

                StreamReader reader = new StreamReader(ofd_importmap.FileName);
                do {
                    _line = reader.ReadLine();
                    if (_line.Contains("Width blocks:") && _line.Contains("Height blocks:") && _line.Contains("BlockSide:"))
                        proceed = true;
                } while (!(_line.Contains("Width blocks:") && _line.Contains("Height blocks:") && _line.Contains("BlockSide:")) &&
                         !reader.EndOfStream);
                string[] _lineArray = _line.Split(sep);


                if (proceed) {

                    Globals._WidthBlocks = Convert.ToInt32(_lineArray[3]);
                    Globals._HeightBlocks = Convert.ToInt32(_lineArray[8]);
                    Globals._BlockSide = Convert.ToInt32(_lineArray[12]);

                    FullyRestore();

                    string[] words;
                    char[] delim = { ' ' };
                    reader.ReadLine();
                    importmap = new BoxType[Globals._WidthBlocks, Globals._HeightBlocks];
                    words = reader.ReadLine().Split(delim);

                    int starts_counter = 0;
                    for (int z = 0; z < importmap.GetLength(0); z++) {
                        int i = 0;
                        foreach (string _s in words)
                            if (i < importmap.GetLength(1)) {
                                if (_s == "Start") {
                                    importmap[z, i] = BoxType.Start;
                                    starts_counter++;
                                } else if (_s == "End")
                                    importmap[z, i] = BoxType.End;
                                else if (_s == "Normal")
                                    importmap[z, i] = BoxType.Normal;
                                else if (_s == "Wall")
                                    importmap[z, i] = BoxType.Wall;
                                else if (_s == "Load")
                                    importmap[z, i] = BoxType.Load;
                                i++;
                            }
                        if (z == importmap.GetLength(0) - 1) { } else
                            words = reader.ReadLine().Split(delim);
                    }
                    reader.Close();

                    nUD_AGVs.Value = starts_counter;
                    imported = true;
                    Initialization();
                    Redraw();
                    if (overImage) {
                        importedLayout = importedImageFile;
                        overImage = false;
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
                importedLayout = Image.FromFile(ofd_importmap.FileName);
                importedImageFile = Image.FromFile(ofd_importmap.FileName);
                overImage = true;
            }

        }

        //Function that validates the user's click 
        private bool Isvalid(Point _temp) {

            //The function received the coordinates of the user's click.
            //Clicking anywhere but on the Grid itself, will cause a "false" return, preventing
            //the click from giving any results

            if (_temp.Y < menuPanel.Location.Y)
                return false;

            if (_temp.X > m_rectangles[Globals._WidthBlocks - 1][Globals._HeightBlocks - 1].boxRec.X + (Globals._BlockSide - 1) + Globals._LeftBarOffset
            || _temp.Y > m_rectangles[Globals._WidthBlocks - 1][Globals._HeightBlocks - 1].boxRec.Y + (Globals._BlockSide - 1)) // 18 because its 20-boarder size
                return false;

            if (!m_rectangles[(_temp.X - Globals._LeftBarOffset) / Globals._BlockSide][(_temp.Y - Globals._TopBarOffset) / Globals._BlockSide].boxRec.Contains(_temp))
                return false;

            return true;
        }

        private void UpdateGridStats() {
            int pixelsWidth = Globals._WidthBlocks * Globals._BlockSide;
            int pixelsHeight = Globals._HeightBlocks * Globals._BlockSide;
            lb_width.Text = "Width blocks: " + Globals._WidthBlocks + ".  " + pixelsWidth + " pixels";
            lb_height.Text = "Height blocks: " + Globals._HeightBlocks + ". " + pixelsHeight + " pixels";
            nud_side.Value = Convert.ToDecimal(Globals._BlockSide);
        }

        private void ToDebugPanel(object var, string varname) {
            TreeNode node = new TreeNode(varname.ToString())
            {
                Name = varname,
                Text = varname + ":" + var,
            };
            tree_stats.Nodes[0].Nodes.Add(node);
        }

        private void ReflectVariables() {
            if (Globals._Debug) {
                //ToDebugPanel(Globals._AStarWeight, nameof(Globals._AStarWeight));
                //add more reflections here
            }
        }

    }
}
