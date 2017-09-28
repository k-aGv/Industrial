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

    public partial class main_form {

        //function for handling keystrokes and assigning specific actions to them
        protected override bool ProcessCmdKey(ref Message _msg, Keys _keyData) {
            switch (_keyData)
            {
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
                    for (int i = 0; i < StartPos.Count; i++)
                        c += AGVs[i].JumpPoints.Count;

                    if (c > 0)
                        triggerStartMenu(true);

                    if (startToolStripMenuItem.Enabled)
                        startToolStripMenuItem_Click(new object(), new EventArgs());
                    else
                        MessageBox.Show(this, "Create a path please", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                default:
                    return false;
            }

        }

        //function for emissions Form
        private void show_emissions() {
            //creates a set of coordinates (Point) that determine the Location of the emissions Form
            Point emissions_loc = new Point(this.Location.X + this.Size.Width - emissions.Size.Width, this.Location.Y+30);
            emissions.Show();
            emissions.Location = emissions_loc;
            emissions.BringToFront();
        }

        //function for updating the values that are shown in the emissions Form
        private void update_emissions(int whichAGV)
        {

            if (cb_type.SelectedItem.ToString() == "LPG")
            {
                if (AGVs[whichAGV].Status.Busy)
                {
                    CO2 += 2959.57;
                    CO += 27.04;
                    NOx += 19.63;
                    THC += 3.06;
                    GlobalWarming += 3.58;
                }
                else
                {
                    CO2 += 1935.16;
                    CO += 13.36;
                    NOx += 13.90;
                    THC += 1.51;
                    GlobalWarming += 2.33;
                }
            }
            else if (cb_type.SelectedItem.ToString() == "DSL")
            {
                if (AGVs[whichAGV].Status.Busy)
                {
                    CO2 += 2130.11;
                    CO += 7.28;
                    NOx += 20.16;
                    THC += 1.77;
                    GlobalWarming += 2.49;
                }
                else
                {
                    CO2 += 1510.83;
                    CO += 3.84;
                    NOx += 14.33;
                    THC += 1.08;
                    GlobalWarming += 1.2;
                }
            } 
            else // ELE
              { 
                CO2 = 0;
                CO = 0;
                NOx = 0;
                THC = 0;
                if (AGVs[whichAGV].Status.Busy)
                    GlobalWarming += 0.67;
                else
                    GlobalWarming += 0.64;
            }
            //Math.Round(, 2) is used to keep the 2 decimals of the emissions shown
            emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
            emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
            emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
            emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
            emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
        }

        private void stopTimers(int agv_index) {
            switch (agv_index) {
                case 0:
                    timer0.Stop();
                    agv1steps_LB.Text = "AGV 1: Finished";
                    break;
                case 1:
                    timer1.Stop();
                    agv2steps_LB.Text = "AGV 2: Finished";
                    break;
                case 2:
                    timer2.Stop();
                    agv3steps_LB.Text = "AGV 3: Finished";
                    break;
                case 3:
                    timer3.Stop();
                    agv4steps_LB.Text = "AGV 4: Finished";
                    break;
                case 4:
                    timer4.Stop();
                    agv5steps_LB.Text = "AGV 5: Finished";
                    break;
            }
        }

        //function that executes the whole animation. Will be explaining thoroughly below
        private void animator(int steps_counter, int agv_index) {

            //we use the incoming parameters, given from the corresponding Timer that calls Animator at a given time
            //steps_counter index tells us on which Step the timer is AT THE TIME this function is called
            //agv_index index tells us which timer is calling this function so as to know which AGV will be handled
            int stepx = Convert.ToInt32(AGVs[agv_index].Steps[steps_counter].X);
            int stepy = Convert.ToInt32(AGVs[agv_index].Steps[steps_counter].Y);

            //if, for any reason, the above steps are set to "0", obviously something is wrong so function returns
            if (stepx == 0 || stepx == 0)
                return;

            
            bool isfreeload = false;
            bool halted = false;

            displayStepsToLoad(steps_counter, agv_index); //Call of function that shows information regarding the status of AGVs in the Monitoring Panel
            update_emissions(agv_index); //Call of function that updates the values of emissions

            //RULES OF WHICH AGV WILL STOP WILL BE ADDED
            if (use_Halt) {
                for (int i = 0; i < StartPos.Count; i++)
                    if (agv_index != i
                        && AGVs[i].GetLocation() != new Point(0, 0)
                        && AGVs[agv_index].GetLocation() == AGVs[i].GetLocation()
                        && AGVs[agv_index].GetLocation() != endPointCoords
                        ) {
                        halt(agv_index, steps_counter); //function for manipulating the movement of AGVs - must be perfected (still under dev)
                        halted = true;
                    } else
                        if (!halted)
                        AGVs[agv_index].SetLocation(stepx - ((Constants.__BlockSide / 2) - 1), stepy - ((Constants.__BlockSide / 2) - 1));
            }
            else
                AGVs[agv_index].SetLocation(stepx - ((Constants.__BlockSide / 2) - 1), stepy - ((Constants.__BlockSide / 2) - 1)); //this is how we move the AGV on the grid (Setlocation function)

            /////////////////////////////////////////////////////////////////
            //Here is the part where an AGV arrives at the Load it has marked.
            if (AGVs[agv_index].MarkedLoad.X * Constants.__BlockSide == AGVs[agv_index].GetLocation().X &&
                (AGVs[agv_index].MarkedLoad.Y * Constants.__BlockSide) + Constants.__TopBarOffset == AGVs[agv_index].GetLocation().Y &&
                !AGVs[agv_index].Status.Busy) {
                
                m_rectangles[AGVs[agv_index].MarkedLoad.X][AGVs[agv_index].MarkedLoad.Y].SwitchLoad(); //converts a specific GridBox, from Load, to Normal box (SwitchLoad function)
                searchGrid.SetWalkableAt(AGVs[agv_index].MarkedLoad.X, AGVs[agv_index].MarkedLoad.Y, true);//marks the picked-up load as walkable AGAIN (since it is now a normal gridbox)
                labeled_loads--;
                if (labeled_loads <= 0)
                    loads_label.Text = "All loads have been picked up";
                else
                    loads_label.Text = "Loads remaining: " + labeled_loads;

                AGVs[agv_index].Status.Busy = true; //Sets the status of the AGV to Busy (because it has just picked-up the marked Load
                AGVs[agv_index].setLoaded(); //changes the icon of the AGV and it now appears as Loaded
                this.Refresh();

                //We needed to find a way to know if the animation is scheduled by Redraw or by GetNextLoad
                //fromstart means that an AGV is starting from its VERY FIRST position, heading to a Load and then to exit
                //When fromstart becomes false, it means that the AGV has completed its first task and now it is handled by GetNextLoad
                if (fromstart[agv_index]) {
                    loads--;
                    isLoad[AGVs[agv_index].MarkedLoad.X, AGVs[agv_index].MarkedLoad.Y] = 2;

                    fromstart[agv_index] = false;
                }
            }

            if (!fromstart[agv_index]) {
                //this is how we check if the AGV has arrived at the exit (red block - end point)
                if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].x &&
                    AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].y) {


                    AGVs[agv_index].Status.Busy = false; //change the AGV's status back to available again (not busy obviously)

                    //here we scan the Grid and search for Loads that either ARE available or WILL BE available
                    //if there's at least 1 available Load, set isfreeload = true and stop the double For-loops
                    for (int k = 0; k < Constants.__WidthBlocks; k++) {
                        for (int b = 0; b < Constants.__HeightBlocks; b++) {
                            if (isLoad[k, b] == 1 || isLoad[k,b]==4) //isLoad[ , ] == 1 means the corresponding Load is available at the moment
                                                                     //isLoad[ ,] == 4 means that the corresponding Load is surrounded by other 
                            {                                        //loads and TEMPORARILY unavailable - will be freed later
                                isfreeload = true;
                                k = Constants.__WidthBlocks;
                                b = Constants.__HeightBlocks;
                            }
                        }
                    }


                    if (loads > 0 && isfreeload) { //means that the are still Loads left in the Grid, that can be picked up

                        Reset(agv_index);
                        AGVs[agv_index].Status.Busy = true;
                        AGVs[agv_index].MarkedLoad = new Point();
                        getNextLoad(agv_index); //function that is responsible for Aaaaaall the future path planning


                        AGVs[agv_index].Status.Busy = false;
                        AGVs[agv_index].setEmpty();

                    } else { //if no other AVAILABLE Loads are found in the grid
                        AGVs[agv_index].setEmpty();
                        isfreeload = false;
                        stopTimers(agv_index);
                    }
                    
                    timer_counter[agv_index] = -1;
                    steps_counter = 0;

                }
            } else {
                if (!AGVs[agv_index].HasLoadToPick) {
                    if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].x &&
                        AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].y)
                        stopTimers(agv_index);
                }
                if (isLoad[AGVs[agv_index].MarkedLoad.X, AGVs[agv_index].MarkedLoad.Y] == 2) //if the AGV has picked up the Load it has marked...
                    if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].x &&
                        AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].y)
                        stopTimers(agv_index);
            }
            

            //if at least 1 timer is active, do not let the user access the Checkboxes etc. etc
            if (!(timer0.Enabled 
                || timer1.Enabled 
                || timer2.Enabled 
                || timer3.Enabled
                || timer4.Enabled)) 
            {
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
                && !timer4.Enabled)
            {
                //clear all the paths
                for (int i = 0; i < StartPos.Count(); i++)
                    AGVs[i].JumpPoints = new List<GridPos>();
                
                allowHighlight = false;
                highlightOverCurrentBoxToolStripMenuItem.Checked = allowHighlight;
                triggerStartMenu(false);
                this.Refresh();
                this.Invalidate(); //invalidates the form, causing it to "refresh" the graphics
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

                for (int k = 0; k < Constants.__WidthBlocks; k++)
                    for (int l = 0; l < Constants.__HeightBlocks; l++)
                        if (m_rectangles[k][l].boxRec.Contains(_p)) { //this is how we assign the previously calculated pair of X,Y to a GridBox
                            
                            //a smart way to handle GridBoxes from their center
                            int sideX = m_rectangles[k][l].boxRec.X + ((Constants.__BlockSide / 2) - 1);
                            int sideY = m_rectangles[k][l].boxRec.Y + ((Constants.__BlockSide / 2) - 1);
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
        private void FullyRestore()
        {
            loads_label.Text = "";
            labeled_loads = 0;

            if (timer_counter != null)
                Array.Clear(timer_counter, 0, timer_counter.GetLength(0));

            if (TrappedStatus != null)
                Array.Clear(TrappedStatus, 0, TrappedStatus.GetLength(0));


            for (int i = 0; i < AGVs.Count; i++)
                AGVs[i].killIcon();


            if (importmap != null)
            {
                Array.Clear(importmap, 0, importmap.GetLength(0));
                Array.Clear(importmap, 0, importmap.GetLength(1));
            }

            if (this.BackgroundImage != null)
                this.BackgroundImage = null;

            fromstart = new bool[Constants.__MaximumAGVs];


            StartPos = new List<GridPos>();
            endPointCoords = new Point(-1, -1);
            selectedColor = Color.DarkGray;

            for (int i = 0; i < StartPos.Count(); i++)
                AGVs[i].JumpPoints = new List<GridPos>();

            searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);

            alwaysCross =
            aGVIndexToolStripMenuItem.Checked =
            beforeStart =
            allowHighlight =
            NoJumpPointsFound = true;

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
            if (emissions != null)
            {
                emissions.Dispose();
                CO2 = CO = NOx = THC = GlobalWarming = 0;
                emissions = new emissions();
            }

            allowHighlight = true;
            highlightOverCurrentBoxToolStripMenuItem.Enabled = true;
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;



            isLoad = new int[Constants.__WidthBlocks, Constants.__HeightBlocks];
            m_rectangles = new GridBox[Constants.__WidthBlocks][];
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                m_rectangles[widthTrav] = new GridBox[Constants.__HeightBlocks];

            //jagged array has to be resetted like this
            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++)
                    m_rectangles[i][j] = new GridBox(i * Constants.__BlockSide, j * Constants.__BlockSide + Constants.__TopBarOffset, BoxType.Normal);

            initialization();
            main_form_Load(new object(), new EventArgs());

            for (int i = 0; i < AGVs.Count; i++)
                AGVs[i].Status.Busy = false;

            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = 50;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";

        }

        //has to do with optical features in the Grid option from the menu
        private void updateBorderVisibility(bool hide) {
            if (hide) {
                for (int i = 0; i < Constants.__WidthBlocks; i++)
                    for (int j = 0; j < Constants.__HeightBlocks; j++)
                        m_rectangles[i][j].BeTransparent();
                this.BackColor = Color.DarkGray;
            } 
            else {
                for (int i = 0; i < Constants.__WidthBlocks; i++)
                    for (int j = 0; j < Constants.__HeightBlocks; j++)
                        if (m_rectangles[i][j].boxType == BoxType.Normal) {
                            m_rectangles[i][j].BeVisible();

                            if (!Constants.__SemiTransparency)
                                boxDefaultColor = Color.WhiteSmoke;
                            else
                                boxDefaultColor = Color.FromArgb(128, 255, 0, 255);
                        }
                this.BackColor = selectedColor;
            }

            //no need of invalidation since its done after the call of this function
        }

        //returns the number of AGVs
        private int getNumberOfAGVs() {
            int agvs = 0;
            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++)
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
            for (int i = 0; i < TrappedStatus.Length; i++)
                TrappedStatus[i] = true;

            do {
                removed = false;
                jumpParam.Reset(Vehicles[list_index], End); //we use the A* setting function and pass the 
                                                            //initial start point of every AGV and the final destination (end block)
                if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0) //if the number of JumpPoints that is calculated is 0 (zero)
                {                                                          //it means that there was no path found
                    Vehicles.Remove(Vehicles[list_index]); //we removed, from the returning list, the AGV for which there was no path found
                    AGVs.Remove(AGVs[list_index]); //we remove the corresponding AGV from the public list that contains all the AGVs which will participate in the simulation
                    removed = true;
                }
                else
                    TrappedStatus[trapped_index] = false; //since it's not trapped, we switch its state to false 

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
            StartPos = new List<GridPos>(); //list that will be filled with the starting points of every AGV
            AGVs = new List<Vehicle>();  //list that will be filled with objects of the class Vehicle
            loadPos = new List<GridPos>(); //list that will be filled with the points of every Load
            loads = 0;
            //Double FOR-loops to scan the whole Grid and perform the needed actions
            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++) {

                    if (m_rectangles[i][j].boxType == BoxType.Wall)
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);//Walls are marked as non-walkable
                    else
                        searchGrid.SetWalkableAt(new GridPos(i, j), true);//every other block is marked as walkable (for now)
                    
                    if (m_rectangles[i][j].boxType == BoxType.Load) 
                    {
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

                        StartPos.Add(new GridPos(i, j)); //adds the starting coordinates of an AGV to the StartPos list

                        //a & b are used by DrawPoints() as the starting x,y for calculation purposes
                        a = StartPos[pos_index].x;
                        b = StartPos[pos_index].y;

                        if (pos_index < StartPos.Count) {
                            StartPos[pos_index] = new GridPos(StartPos[pos_index].x, StartPos[pos_index].y);
                            pos_index++;
                        }
                    }

                    if (m_rectangles[i][j].boxType == BoxType.End) {
                        end_found = true;
                        endPos.x = i;
                        endPos.y = j;
                        endPointCoords = new Point(i * Constants.__BlockSide, j * Constants.__BlockSide + Constants.__TopBarOffset);
                    }
                }
           
            Reset();
           
            if (!start_found || !end_found)
                return; //will return if there are no starting or end points in the Grid
            
            NoJumpPointsFound = true;
            
            pos_index = 0;
            
            if (AGVs != null)
                for (int i = 0; i < AGVs.Count(); i++)
                    if (AGVs[i] != null) {
                        AGVs[i].updateAGV();
                        AGVs[i].Status.Busy = false; //initialize the status of AGVs, as 'available'
                    }
            
            StartPos = NotTrappedVehicles(StartPos, endPos); //replaces the List with all the inserted AGVs
                                                             //with a new one containing the right ones
            if(mapHasLoads)
                KeepValidLoads(endPos); //calls a function that checks which Loads are available
                                        //to be picked up by AGVs and removed the trapped ones.

            
            //For-loop to repeat the path-finding process for ALL the AGVs that participate in the simulation
            for (int i = 0; i < StartPos.Count; i++) {
                if (loadPos.Count != 0)
                    loadPos = checkForTrappedLoads(loadPos);

                if (loadPos.Count ==0) {
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
                            jumpParam.Reset(StartPos[pos_index], loadPos[0]);
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
                            NoJumpPointsFound = false;

                            //marks the load that each AGV picks up on the 1st route, as 3, so each agv knows where to go after delivering the 1st load
                            isLoad[loadPos[0].x, loadPos[0].y] = 3;
                            AGVs[i].MarkedLoad = new Point(loadPos[0].x, loadPos[0].y);
                            
                            loadPos.Remove(loadPos[0]);
                            //======FROM LOAD TO END======
                            break;
                        case false:
                            jumpParam.Reset(StartPos[pos_index], endPos);
                            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);

                            AGVs[i].JumpPoints = JumpPointsList;
                            NoJumpPointsFound = false;
                            break;
                    }
                }
                pos_index++;
            }
           
            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;


            for (int i = 0; i < StartPos.Count; i++)
                for (int j = 0; j < AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine
                        (
                        m_rectangles[AGVs[i].JumpPoints[j].x][AGVs[i].JumpPoints[j].y],
                        m_rectangles[AGVs[i].JumpPoints[j + 1].x][AGVs[i].JumpPoints[j + 1].y]
                        );
                    
                    AGVs[i].Paths[j] = line;
                }

            for (int i = 0; i < StartPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, c - 1); //resize of the AGVs steps Table
            if (loads != 0)
                loads_label.Text = "Loads: " + loads;
            Invalidate();
        }

        //function that determines which loads are valid to keep and which are not
        private void KeepValidLoads(GridPos EndPoint)
        {
            int list_index = 0;
            bool removed;

            for (int i = 0; i < loadPos.Count; i++)
                searchGrid.SetWalkableAt(loadPos[i], true); //assumes that all loads are walkable
                                                            //and only walls are in fact the only obstacles in the grid
            do
            {
                removed = false;
                jumpParam.Reset(loadPos[list_index], EndPoint); //tries to find path between each Load and the exit
                if(AStarFinder.FindPath(jumpParam,nud_weight.Value).Count==0) //if no path is found
                {
                    isLoad[loadPos[list_index].x, loadPos[list_index].y] = 2; //mark the corresponding load as NOT available
                    loads--; //decrease the counter of total loads in the grid
                    loadPos.RemoveAt(list_index); //remove that load from the list
                    removed = true;
                }
                if (!removed)
                    list_index++;
            }while(list_index< loadPos.Count); //loop repeats untill all loads are checked

            for (int i = 0; i < loadPos.Count; i++)
                searchGrid.SetWalkableAt(loadPos[i], false); //re-sets every Load to non-walkable

            if (loadPos.Count == 0)
                mapHasLoads = false;
        }

        //funcion that scans and finds which loads are surrounded by other loads
        private List<GridPos> checkForTrappedLoads(List<GridPos> pos) {
            int list_index = 0;
            bool removed;    

            //if the 1st AGV  cannot reach a Load, then that Load is  
            //removed from the loadPos and not considered as available - marked as "4"  (temporarily trapped)
            do
            {
                removed = false;
                searchGrid.SetWalkableAt(new GridPos(pos[list_index].x, pos[list_index].y), true);
                jumpParam.Reset(StartPos[0], pos[list_index]);
                if (AStarFinder.FindPath(jumpParam, nud_weight.Value).Count == 0)
                {
                    searchGrid.SetWalkableAt(new GridPos(pos[list_index].x, pos[list_index].y), false);
                    isLoad[pos[list_index].x, pos[list_index].y] = 4; //load is marked as trapped
                    pos.Remove(pos[list_index]); //load is removed from the List with available Loads
                    removed = true;
                }
                else
                    isLoad[pos[list_index].x, pos[list_index].y] = 1; //otherwise, Load is marked as available

                if (!removed)
                    list_index++;
            } while (list_index < pos.Count);

            return pos;
        }

        //returns the number of steps until AGV reaches the marked Load
        private int getStepsToLoad(int whichAGV) {
            int ix = AGVs[whichAGV].MarkedLoad.X * Constants.__BlockSide;
            int iy = (AGVs[whichAGV].MarkedLoad.Y * Constants.__BlockSide) + Constants.__TopBarOffset;

            int step = -1;

            for (int i = 0; i < AGVs[whichAGV].Steps.GetLength(0); i++)
                if (AGVs[whichAGV].Steps[i].X - ((Constants.__BlockSide / 2) - 1) == ix &&
                    AGVs[whichAGV].Steps[i].Y - ((Constants.__BlockSide / 2) - 1) == iy) {
                    step = i;
                    i = AGVs[whichAGV].Steps.GetLength(0);
                }
            
            if (step >= 0) return step;
            else return -1;
        }

        //resets the all AGVs' embedded arrays - memory leaks prevention
        private void Reset() {
            
            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            
            for (int i = 0; i < AGVs.Count; i++)
                for (int j = 0; j < Constants.__MaximumSteps; j++) {
                    AGVs[i].Steps[j].X = 0;
                    AGVs[i].Steps[j].Y = 0;
                    AGVs[i].StepsCounter = new int();
                    AGVs[i].Paths = new GridLine[Constants.__MaximumSteps];
                }
            
        }

        //Reset function with overload for specific AGV 
        private void Reset(int whichAGV) //overloaded Reset
        {
            int c = AGVs[0].Paths.Length;

            AGVs[whichAGV].JumpPoints = new List<GridPos>(); //empties the AGV's JumpPoints List for the new JumpPoints to be added

            StartPos[whichAGV] = new GridPos(); //empties the correct start Pos for each AGV

            for (int i = 0; i < c; i++)
                AGVs[whichAGV].Paths[i] = null;

            for (int j = 0; j < Constants.__MaximumSteps; j++) {
                AGVs[whichAGV].Steps[j].X = 0;
                AGVs[whichAGV].Steps[j].Y = 0;
            }

            AGVs[whichAGV].StepsCounter = 0;
        }

        //Shows information on the Monitor Panel
        private void displayStepsToLoad(int counter, int agv_index) {
            int stepstoload;
            string agvinfo;

            if (getStepsToLoad(agv_index) == -1)
                agvinfo = "AGV " + (agv_index) + ": Moving straight to the end point";
            else {
                stepstoload = (getStepsToLoad(agv_index) - counter);
                agvinfo = "AGV " + (agv_index ) + ": Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                if (stepstoload < 0)
                    agvinfo = "AGV " + (agv_index ) + " is Loaded.";
            }

            gb_monitor.Controls.Find(
                "agv" + (agv_index + 1) + "steps_LB",
                 true)
                 [0].Text = agvinfo;

        }

        //function for holding the AGV back so another can pass without colliding
        private void halt(int index, int _c) {
            timer_counter[index]--;
            if (!(_c - 1 < 0)) //in case the intersection is in the 1st step of the route, then the index of that step will be 0. 
            {                  //this means that trying to get to the "_c -1" step, will have the index decreased to -1 causing the "index out of bound" crash
                int stepx = Convert.ToInt32(AGVs[index].Steps[_c - 1].X);
                int stepy = Convert.ToInt32(AGVs[index].Steps[_c - 1].Y);
                AGVs[index].SetLocation(stepx - ((Constants.__BlockSide / 2) - 1), stepy - ((Constants.__BlockSide / 2) - 1));
            }
        }

        //manipulates the text of the Start button 
        private void triggerStartMenu(bool t) {
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
        private void getNextLoad(int whichAGV) {

            aGVIndexToolStripMenuItem.Checked = false;
            GridPos endPos = new GridPos();


            //finds the End point and uses it's coordinates as the starting coords for every AGV
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++)
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                        try {
                            StartPos[whichAGV] = new GridPos(widthTrav, heightTrav);
                            a = StartPos[whichAGV].x;
                            b = StartPos[whichAGV].y;
                        }
                        catch {  }

            List<GridPos> loadPos = new List<GridPos>();

            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++)
                {
                    if (m_rectangles[i][j].boxType == BoxType.Load)
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);

                    //places the available AND the temporarily trapped loads in a list
                    if (isLoad[i, j] == 1 || isLoad[i, j] == 4)
                        loadPos.Add(new GridPos(i, j));
                }
            loadPos = checkForTrappedLoads(loadPos); //scans the loadPos list to check which loads are available
            
            isLoad[loadPos[0].x, loadPos[0].y] = 3;
            AGVs[whichAGV].MarkedLoad = new Point(loadPos[0].x, loadPos[0].y);
            loads--;
            endPos = loadPos[0];

            //Mark all loads as unwalkable,except the targetted ones
            for (int m = 0; m < loadPos.Count; m++)
                searchGrid.SetWalkableAt(loadPos[m], false);
            searchGrid.SetWalkableAt(loadPos[0], true);
            
            //creates the path between the AGV (which at the moment is at the exit) and the Load
            jumpParam.Reset(StartPos[whichAGV], endPos); 
            List <GridPos> JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
            AGVs[whichAGV].JumpPoints = JumpPointsList;//adds the result from A* to the AGV's
                                                       //embedded List
                                                       
            //Mark all loads as unwalkable
            for (int m = 0; m < loadPos.Count; m++)
                searchGrid.SetWalkableAt(loadPos[m], false);
            
            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < StartPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, c - 1);


            for (int j = 0; j < AGVs[whichAGV].JumpPoints.Count - 1; j++) {
                GridLine line = new GridLine(
                    m_rectangles
                        [AGVs[whichAGV].JumpPoints[j].x]
                        [AGVs[whichAGV].JumpPoints[j].y],
                   m_rectangles
                        [AGVs[whichAGV].JumpPoints[j + 1].x]
                        [AGVs[whichAGV].JumpPoints[j + 1].y]
                                           );

                AGVs[whichAGV].Paths[j] = line;
            }

            
            //2nd part of route: Go to exit
            int old_c = c-1;
            
            jumpParam.Reset(endPos, StartPos[whichAGV]);
            JumpPointsList = AStarFinder.FindPath(jumpParam, nud_weight.Value);
            AGVs[whichAGV].JumpPoints.AddRange(JumpPointsList);

            c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < StartPos.Count; i++)
                if ((c - 1) > 0)
                    Array.Resize(ref AGVs[i].Paths, old_c+(c - 1));

            
            for (int i = 0; i < StartPos.Count; i++)
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
            
            this.Invalidate();
        }

        //function that starts the needed timers
        private void timers() {
            //every timer is responsible for every agv for up to 5 AGVs
            
            int _c = 0;
            for (int i=0; i< TrappedStatus.Length;i++)
                if (!TrappedStatus[i]) //array containing the status of AGV
                    _c++; //counts the number of free-to-move AGVs

            switch(_c) //depending on the _c, the required timers will be started
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

        //Initializes all the objects in main_form
        private void initialization() {
            

            if ( Constants.__SemiTransparency)
               Constants.__SemiTransparent = Color.FromArgb( Constants.__Opacity,Color.WhiteSmoke);

            for (int i = 0; i < StartPos.Count; i++) {
                AGVs[i] = new Vehicle(this);
                AGVs[i].ID = i;
            }

            DoubleBuffered = true;
            Width = ((Constants.__WidthBlocks + 1) * Constants.__BlockSide) ; 
            Height = (Constants.__HeightBlocks + 1) * Constants.__BlockSide + Constants.__BottomBarOffset + 7; //+7 for borders
            Size = new Size(this.Width, this.Height + Constants.__BottomBarOffset);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            m_rectangles = new GridBox[Constants.__WidthBlocks][];

            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                //declares the length of the seconds 1d array
                m_rectangles[widthTrav] = new GridBox[Constants.__HeightBlocks];
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {

                    //dynamically add the gridboxes into the m_rectangles.
                    //size of the m_rectangels is constantly increasing (while adding
                    //the gridbox values) until size=height or size = width.
                    if (imported) { //this IF is executed as long as the user has imported a map of his choice
                        m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * Constants.__BlockSide, heightTrav * Constants.__BlockSide + Constants.__TopBarOffset, importmap[widthTrav, heightTrav]);
                        if (importmap[widthTrav, heightTrav] == BoxType.Load) {
                            isLoad[widthTrav, heightTrav] = 1;
                            loads++;
                        }
                    } else {
                        m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * Constants.__BlockSide, heightTrav * Constants.__BlockSide + Constants.__TopBarOffset, BoxType.Normal);
                        isLoad[widthTrav, heightTrav] = 2;
                    }

                  
                }
            }
            if (imported)
                imported = false;



            searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);
            jumpParam = new AStarParam(searchGrid, Convert.ToSingle(Constants.__AStarWeight));//Default value until user edit it
            jumpParam.SetHeuristic(HeuristicMode.MANHATTAN); //default value until user edit it


        }

        //Function for exporting the map
        private void export() {
            sfd_exportmap.FileName = "";
            sfd_exportmap.Filter = "kagv Map (*.kmap)|*.kmap";

            if (sfd_exportmap.ShowDialog() == DialogResult.OK) {
                StreamWriter writer = new StreamWriter(sfd_exportmap.FileName);
                writer.WriteLine("Map info:\r\nWidth blocks: " + Constants.__WidthBlocks + "  Height blocks: " + Constants.__HeightBlocks + "\r\n");
                for (int i = 0; i < Constants.__WidthBlocks; i++) {
                    for (int j = 0; j < Constants.__HeightBlocks; j++) {
                        writer.Write(m_rectangles[i][j].boxType + " ");
                    }
                    writer.Write("\r\n");
                }
                writer.Close();
            }

        }

        //Function for importing a map 
        private void import() {

            ofd_importmap.Filter = "kagv Map (*.kmap)|*.kmap";
            ofd_importmap.FileName = "";

            
            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                FullyRestore();

                bool proceed = false;
                StreamReader _tmp = new StreamReader(ofd_importmap.FileName);
                if (_tmp.ReadToEnd().Contains("Width blocks: 78  Height blocks: 44"))
                        proceed = true;
                    else
                        proceed = false;
                _tmp.Close();
                
                if (proceed) {
                    StreamReader reader = new StreamReader(ofd_importmap.FileName);
                    reader.ReadLine();

                    imported = true;

                    string map_details = reader.ReadLine();

                    char[] delim = { ' ' };
                    string[] words = map_details.Split(delim);

                    bool isNumber;
                    int _tempNumber;
                    int whichNumber = 1;

                    int width_blocks = 0;
                    int height_blocks = 0;

                    foreach (string _s in words) {
                        isNumber = int.TryParse(_s, out _tempNumber);
                        if (isNumber) {
                            if (whichNumber == 1) {
                                width_blocks = Convert.ToInt32(_s);
                                whichNumber++;
                            } else if (whichNumber == 2)
                                height_blocks = Convert.ToInt32(_s);
                        }
                    }

                    reader.ReadLine();
                   
                    importmap = new BoxType[width_blocks, height_blocks];
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
                    
                    for (int z = 0; z < importmap.GetLength(0); z++)
                        for (int i = 0; i < importmap.GetLength(1); i++)
                            m_rectangles[z][i].boxType = importmap[z, i];

                    nUD_AGVs.Value = starts_counter;
                    initialization();
                    Redraw();
                    if (overImage) {
                        importedLayout = p;
                        overImage = false;
                    }
                } else
                    MessageBox.Show(this, "You have chosen an incompatible file import.\r\nPlease try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //function for importing an image as background
        Image p;
        private void importImage() {
            ofd_importmap.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|(*.jpg)";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                importedLayout = Image.FromFile(ofd_importmap.FileName);
                p = Image.FromFile(ofd_importmap.FileName);
                overImage = true;
            }
            
        }

        //Function that validates the user's click 
        private bool isvalid(Point _temp) {

            //The function received the coordinates of the user's click.
            //Clicking anywhere but on the Grid itself, will cause a "false" return, preventing
            //the click from giving any results

            if (_temp.Y < menuPanel.Location.Y)
                return false;

            if (_temp.X > m_rectangles[Constants.__WidthBlocks - 1][Constants.__HeightBlocks - 1].boxRec.X + (Constants.__BlockSide - 1)
            || _temp.Y > m_rectangles[Constants.__WidthBlocks - 1][Constants.__HeightBlocks - 1].boxRec.Y + (Constants.__BlockSide - 1)) // 18 because its 20-boarder size
                return false;

            if (!m_rectangles[(_temp.X) / Constants.__BlockSide][(_temp.Y - Constants.__TopBarOffset) / Constants.__BlockSide].boxRec.Contains(_temp))
                return false;

            return true;
        }

    }
}
