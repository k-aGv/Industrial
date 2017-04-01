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

    public partial class main_form {

        protected override bool ProcessCmdKey(ref Message _msg, Keys _keyData) {
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
                    if (startToolStripMenuItem.Enabled)
                        startToolStripMenuItem_Click(new object(), new EventArgs());
                    else
                        MessageBox.Show(this,"Create a path please","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return true;
                default:
                    return false;
            }

        }
#if industrial
        private void show_emissions() {
            Point emissions_loc = new Point(this.Location.X + this.Size.Width - emissions.Size.Width, this.Location.Y);
            emissions.Show();
            emissions.Location = emissions_loc;
            emissions.BringToFront();
        }

        private void update_emissions(int whichAGV) {

            if (cb_type.SelectedItem.ToString() == "LPG") {
                if (AGVs[whichAGV].Status.Busy) {
                    CO2 += 2959.57;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO += 27.04;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx += 19.63;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC += 3.06;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 3.58;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                } else {
                    CO2 += 1935.16;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO += 13.36;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx += 13.90;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC += 1.51;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 2.33;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                }
            }

            if (cb_type.SelectedItem.ToString() == "DSL") {
                if (AGVs[whichAGV].Status.Busy) {
                    CO2 += 2130.11;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO += 7.28;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx += 20.16;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC += 1.77;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 2.49;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                } else {
                    CO2 += 1510.83;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO += 3.84;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx += 14.33;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC += 1.08;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 1.2;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                }
            }

            if (cb_type.SelectedItem.ToString() == "ELE") {
                if (AGVs[whichAGV].Status.Busy) {
                    CO2 = 0;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO = 0;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx = 0;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC = 0;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 0.67;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                } else {
                    CO2 = 0;
                    emissions.CO2_label.Text = "CO2: " + Math.Round(CO2, 2) + " gr";
                    CO = 0;
                    emissions.CO_label.Text = "CO: " + Math.Round(CO, 2) + " gr";
                    NOx = 0;
                    emissions.NOx_label.Text = "NOx: " + Math.Round(NOx, 2) + " gr";
                    THC = 0;
                    emissions.THC_label.Text = "THC: " + Math.Round(THC, 2) + " gr";
                    GlobalWarming += 0.64;
                    emissions.Global_label.Text = "Global Warming eq: " + Math.Round(GlobalWarming, 2) + " kgr";
                }
            }

        }
#endif
      
        private void animator(int steps_counter, int agv_index) {

            int stepx = Convert.ToInt32(AGVs[agv_index].Steps[steps_counter].X);
            int stepy = Convert.ToInt32(AGVs[agv_index].Steps[steps_counter].Y);

            if (stepx == 0 || stepx == 0)
                return;

            bool isfreeload = false;
            bool halted = false;
            displayStepsToLoad(steps_counter, agv_index);
#if industrial
            update_emissions(agv_index);
#endif
            //RULES OF WHICH AGV WILL STOP WILL BE ADDED
            for (int i = 0; i < nUD_AGVs.Value; i++) {

                if (agv_index != i
                    && AGVs[i].GetLocation() != new Point(0, 0)//i dont like that much tho
                    && AGVs[agv_index].GetLocation() == AGVs[i].GetLocation()
                    && AGVs[agv_index].GetLocation() != endPointCoords
                    ) {

                    halt(agv_index, steps_counter);
                    halted = true;
                } else {
                    if (!halted)
                        AGVs[agv_index].SetLocation(stepx - ((Constants.__BlockSide / 2) - 1), stepy - ((Constants.__BlockSide / 2) - 1));
                }

            }


            if (AGVs[agv_index].MarkedLoad.X * Constants.__BlockSide == AGVs[agv_index].GetLocation().X &&
                (AGVs[agv_index].MarkedLoad.Y * Constants.__BlockSide) + Constants.__TopBarOffset == AGVs[agv_index].GetLocation().Y &&
                !AGVs[agv_index].Status.Busy) {

                m_rectangles[AGVs[agv_index].MarkedLoad.X][AGVs[agv_index].MarkedLoad.Y].SwitchLoad();
                AGVs[agv_index].Status.Busy = true;
                AGVs[agv_index].setLoaded();
                if (fromstart[agv_index]) {
                    loads--;
                    isLoad[AGVs[agv_index].MarkedLoad.X, AGVs[agv_index].MarkedLoad.Y] = 2;

                    fromstart[agv_index] = false;
                }
            }

            if (!fromstart[agv_index]) {
                if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].x &&
                    AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].y) {


                    AGVs[agv_index].Status.Busy = false;

                    for (int k = 0; k < Constants.__WidthBlocks; k++) {
                        for (int b = 0; b < Constants.__HeightBlocks; b++) {
                            if (isLoad[k, b] == 1)
                                isfreeload = true;
                        }
                    }

                    if (loads > 0 && isfreeload) {

                        Reset(agv_index);
                        AGVs[agv_index].Status.Busy = true;
                        AGVs[agv_index].MarkedLoad = new Point();
                        getNextLoad(agv_index);


                        AGVs[agv_index].Status.Busy = false;
                        AGVs[agv_index].setEmpty();

                    } else {
                        AGVs[agv_index].setEmpty();
                        isfreeload = false;

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

                    timer_counter[agv_index] = -1;
                    steps_counter = 0;

                }
            } else {
                if (isLoad[AGVs[agv_index].MarkedLoad.X, AGVs[agv_index].MarkedLoad.Y] == 2)
                    if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].x &&
                        AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / Constants.__BlockSide][(endPointCoords.Y - Constants.__TopBarOffset) / Constants.__BlockSide].y)
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
            //end of handling


            if (!(timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)) //if at least 1 timer is active, do not let the user access the Checkboxes etc. etc
            {
                gb_settings.Enabled = true;
                settings_menu.Enabled = true;

            }

            if (!timer0.Enabled && !timer1.Enabled && !timer2.Enabled && !timer3.Enabled && !timer4.Enabled)//when all agvs has finished their tasks
            {
                //clear all the paths
                for (int i = 0; i < StartPos.Count(); i++) {
                    AGVs[i].JumpPoints = new List<GridPos>();
                }
                allowHighlight = false;
                highlightOverCurrentBoxToolStripMenuItem.Checked = allowHighlight;
                triggerStartMenu(false);
            }


            this.Invalidate();
            this.Refresh();

        }
        private void DrawPoints(GridLine x, int agv_index) {
            Point[] currentLinePoints;//1d array of points.used to track all the points of current line

            int x1 = x.fromX;
            int y1 = x.fromY;
            int x2 = x.toX;
            int y2 = x.toY;
            double distance = GetLength(x1, y1, x2, y2);

            double side = getSide(m_rectangles[0][0].height
                            , m_rectangles[0][0].height);

            int distanceBlocks = -1; //the quantity of blocks,matching the current line's length

            if ((x1 < x2) && (y1 < y2)) //diagonal-right bottom direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 < x2) && (y1 > y2)) //diagonal-right top direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 > x2) && (y1 < y2)) //diagonal-left bottom direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((x1 > x2) && (y1 > y2)) //diagonal-left top direction
                distanceBlocks = Convert.ToInt32(distance / side);
            else if ((y1 == y2) || (x1 == x2)) //horizontal or vertical
            {
                distanceBlocks = Convert.ToInt32(distance / m_rectangles[0][0].width);
                //we do not need hypotenuse here
            } else
                MessageBox.Show(this, "Unexpected error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);


            currentLinePoints = new Point[distanceBlocks];
            double t;

            for (int i = 0; i < distanceBlocks; i++) {
                calibrated = false;

                if (distance != 0)
                    t = ((side) / distance);
                else
                    return;


                a = Convert.ToInt32(((1 - t) * x1) + (t * x2));
                b = Convert.ToInt32(((1 - t) * y1) + (t * y2));

                Point _p = new Point(a, b);

                for (int k = 0; k < Constants.__WidthBlocks; k++) {

                    for (int l = 0; l < Constants.__HeightBlocks; l++) {

                        if (m_rectangles[k][l].boxRec.Contains(_p)) {
                            //+9 is the width/2 - handling boxes from their centre
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

                    }

                }

                if (calibrated) {
                    AGVs[agv_index].Steps[AGVs[agv_index].StepsCounter].X = currentLinePoints[i].X;
                    AGVs[agv_index].Steps[AGVs[agv_index].StepsCounter].Y = currentLinePoints[i].Y;
                    AGVs[agv_index].StepsCounter++;
                }
                //init next steps
                x1 = currentLinePoints[i].X;
                y1 = currentLinePoints[i].Y;
                distance = GetLength(x1, y1, x2, y2);

            }


        }

        private void FullyRestore() {
            if (is_trapped != null) {
                Array.Clear(is_trapped, 0, is_trapped.GetLength(0));
                Array.Clear(is_trapped, 0, is_trapped.GetLength(1));
            }

            if (timer_counter != null)
                Array.Clear(timer_counter, 0, timer_counter.GetLength(0));


            for (int i = 0; i < AGVs.GetLength(0); i++)
                AGVs[i].killIcon();


            if (importmap != null) {
                Array.Clear(importmap, 0, importmap.GetLength(0));
                Array.Clear(importmap, 0, importmap.GetLength(1));
            }

            if (this.BackgroundImage != null)
                this.BackgroundImage = null;

            fromstart = new bool[Constants.__MaximumAGVs];


            StartPos = new List<GridPos>();
            endPointCoords = new Point(-1, -1);
            selectedColor = Color.DarkGray;

            for (int i = 0; i < StartPos.Count(); i++) {
                AGVs[i].JumpPoints = new List<GridPos>();
            }
            JumpPointsList = new List<GridPos>();
            searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);

            beforeStart =
            allowHighlight =
            NoJumpPointsFound = true;

            imported =
            importedImage =
            calibrated =
            isMouseDown =
            mapHasLoads = false;

            jumpParam = null;
            paper = null;
            loads = pos_index = 0;

            a
            = b
            = new int();

            AGVs = new Vehicle[Constants.__MaximumAGVs];
#if industrial
            if (emissions != null) {
                emissions.Dispose();
                CO2 = CO = NOx = THC = GlobalWarming = 0;
                emissions = new emissions();
            }
#endif

            if (Constants.__ResolutionMultiplier == 2) {
                Constants.__BlockSide = reflectedBlock / Convert.ToInt32(Constants.__ResolutionMultiplier);
                Constants.__HeightBlocks = reflectedHeight * Convert.ToInt32(Constants.__ResolutionMultiplier);
                Constants.__WidthBlocks = reflectedWidth * Convert.ToInt32(Constants.__ResolutionMultiplier);
                allowHighlight = false;
                highlightOverCurrentBoxToolStripMenuItem.Enabled = false;
                highlightOverCurrentBoxToolStripMenuItem.Checked = false;
            } else {
                Constants.__BlockSide = reflectedBlock;
                Constants.__HeightBlocks = reflectedHeight;
                Constants.__WidthBlocks = reflectedWidth;
                allowHighlight = true;
                highlightOverCurrentBoxToolStripMenuItem.Enabled = true;
                highlightOverCurrentBoxToolStripMenuItem.Checked = true;
            }
            

            isLoad = new int[Constants.__WidthBlocks, Constants.__HeightBlocks];
            m_rectangles = new GridBox[Constants.__WidthBlocks][];
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++)
                m_rectangles[widthTrav] = new GridBox[Constants.__HeightBlocks];

            //jagged array has to be resetted like this
            for (int i = 0; i < Constants.__WidthBlocks; i++) {
                for (int j = 0; j < Constants.__HeightBlocks; j++) {
                    m_rectangles[i][j] = new GridBox(i * Constants.__BlockSide, j * Constants.__BlockSide + Constants.__TopBarOffset, BoxType.Normal);
                }
            }

            initialization();
            main_form_Load(new object(), new EventArgs());
            for (int i = 0; i < AGVs.GetLength(0); i++)
                AGVs[i].Status.Busy = false;


            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = 100;
            refresh_label.Text = "Delay:" + timer0.Interval + " ms";



        }

        T[,] ResizeArray<T>(T[,] original, int rows, int cols) {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];

            return newArray;
        }

        private void updateBorderVisibility(bool hide) {
            if (hide) {
                for (int i = 0; i < Constants.__WidthBlocks; i++)
                    for (int j = 0; j < Constants.__HeightBlocks; j++)
                        m_rectangles[i][j].BeTransparent();
                this.BackColor = Color.DarkGray;
            } else {
                for (int i = 0; i < Constants.__WidthBlocks; i++)
                    for (int j = 0; j < Constants.__HeightBlocks; j++) {
                        if (m_rectangles[i][j].boxType == BoxType.Normal) {
                            m_rectangles[i][j].BeVisible();
#if industrial
                        boxDefaultColor = Color.WhiteSmoke;
#else
                        boxDefaultColor = Color.Transparent;
#endif
                        }
                    }

                this.BackColor = selectedColor;
            }

            //no need of invalidation since its done after the call of this function
        }
        private void updateParameters() {
            jumpParam.CrossAdjacentPoint = crossAdjacent;
            jumpParam.CrossCorner = crossCorners;
            jumpParam.UseRecursive = useRecursive;
        }
        private int getNumberOfAGVs() {
            int agvs = 0;
            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++)
                    if (m_rectangles[i][j].boxType == BoxType.Start)
                        agvs++;

            return agvs;
        }

        private void Redraw() {

            if (loads > 0)
                mapHasLoads = true;
            else
                mapHasLoads = false;

            bool start_found = false;
            bool end_found = false;

            List<GridPos> loadPos = new List<GridPos>();
            int loadPos_index = 0;

            GridPos endPos = new GridPos();

            StartPos = new List<GridPos>();

            pos_index = 0;


            for (int i = 0; i < Constants.__WidthBlocks; i++)
                for (int j = 0; j < Constants.__HeightBlocks; j++) {
                    if (m_rectangles[i][j].boxType == BoxType.Wall)
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);
                    else
                        searchGrid.SetWalkableAt(new GridPos(i, j), true);

                    if (beforeStart) {
                        if (m_rectangles[i][j].boxType == BoxType.Start)
                            searchGrid.SetWalkableAt(new GridPos(i, j), false);

                    } else {
                        if (m_rectangles[i][j].boxType == BoxType.Start)
                            searchGrid.SetWalkableAt(new GridPos(i, j), true);
                    }

                    if (m_rectangles[i][j].boxType == BoxType.Normal)
                        m_rectangles[i][j].onHover(boxDefaultColor);

                    if (m_rectangles[i][j].boxType == BoxType.Start) {
                        start_found = true;

                        StartPos.Add(new GridPos(0, 0));//create space to add the next agv
                        StartPos[pos_index].x = i;
                        StartPos[pos_index].y = j;

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

                    if (mapHasLoads) {
                        if (isLoad[i, j] == 1 || isLoad[i, j] == 4) {
                            loadPos.Add(new GridPos());
                            loadPos[loadPos_index].x = i;
                            loadPos[loadPos_index].y = j;
                            loadPos_index++;
                        }
                    }
                }

            Reset();
            if (!start_found || !end_found)
                return;

            NoJumpPointsFound = true;

            if (AGVs != null)
                for (int i = 0; i < AGVs.Count(); i++)
                    if (AGVs[i] != null) {
                        AGVs[i].updateAGV();
                        AGVs[i].Status.Busy = false;//initialize the status of AGVs, as 'available'
                    }

            loadPos_index = 0;
            pos_index = 0;
            is_trapped = new bool[StartPos.Count, 2];

            for (int i = 0; i < StartPos.Count; i++) {
                AGVs[i].JumpPoints = new List<GridPos>();
            }

            for (int i = 0; i < StartPos.Count; i++) {
                if (AGVs[i].Status.Busy == false) {
                    if (loadPos.Count() == 0)
                        mapHasLoads = false;

                    //create the path from start to load,if load exists
                    switch (mapHasLoads) {
                        case true:

                            if(rb_wall.Checked || rb_load.Checked)
                                checkForTrappedLoads(loadPos);

                            if (loadPos.Count() == 0)
                                loadPos.Add(endPos); //if EVERY load is trapped, use the endPos as LoadPos so as the agvs can complete their basic route (start -> end)


                            jumpParam.Reset(StartPos[pos_index], loadPos[0]);
                            JumpPointsList = JumpPointFinder.FindPath(jumpParam, paper);
                            AGVs[i].Status.Busy = true;

                            //is_trapped[i,0] -> part of route agv -> load
                            //is_trapped[i,1] -> part of route load -> end
                            if (JumpPointsList.Count == 0)
                                is_trapped[i, 0] = true;
                            else
                                is_trapped[i, 0] = false;

                            if (!is_trapped[i, 0])
                                for (int j = 0; j < JumpPointsList.Count; j++)
                                    AGVs[i].JumpPoints.Add(JumpPointsList[j]);
                            else //leak catch
                                AGVs[i].JumpPoints.Add(new GridPos());  //increases the size of the AGV's embedded List so the JumpPoints can fit without causing overflow




                            //from load to end
                            jumpParam.Reset(loadPos[0], endPos);
                            JumpPointsList = JumpPointFinder.FindPath(jumpParam, paper);
                            if (JumpPointsList.Count == 0)//recheck if load-to-end is a trapped path
                                is_trapped[i, 1] = true;
                            else
                                is_trapped[i, 1] = false;


                            if (!is_trapped[i, 1])
                                for (int j = 0; j < JumpPointsList.Count; j++) {
                                    AGVs[i].JumpPoints.Add(JumpPointsList[j]);  //adds the list containing the AGV's path, to the AGV's embedded JumpPoint List
                                    NoJumpPointsFound = false;
                                }


                            if (!is_trapped[i, 0] && !is_trapped[i, 1]) {
                                //marks the load that each AGV picks up on the 1st route, as 3, so each agv knows where to go after delivering the 1st load
                                if (fromstart[i]) {
                                    for (int widthtrav = 0; widthtrav < Constants.__WidthBlocks; widthtrav++)
                                        for (int heightrav = 0; heightrav < Constants.__HeightBlocks; heightrav++)
                                            if (isLoad[widthtrav, heightrav] == 1) {
                                                isLoad[widthtrav, heightrav] = 3;
                                                AGVs[i].MarkedLoad = new Point(widthtrav, heightrav);

                                                widthtrav = Constants.__WidthBlocks;
                                                heightrav = Constants.__HeightBlocks;
                                            }
                                }
                                loadPos.Remove(loadPos[0]);
                            } else if (is_trapped[i, 1])
                                loadPos.Remove(loadPos[0]);
                            break;




                        case false:
                            jumpParam.Reset(StartPos[pos_index], endPos);
                            JumpPointsList = JumpPointFinder.FindPath(jumpParam, paper);

                            if (JumpPointsList.Count == 0)
                                is_trapped[i, 0] = true;
                            else
                                is_trapped[i, 0] = false;


                            if (!is_trapped[i, 0])
                                for (int j = 0; j < JumpPointsList.Count; j++) {
                                    AGVs[i].JumpPoints.Add(JumpPointsList[j]);  //adds the list containing the AGV's path, to the AGV's embedded JumpPoint List 

                                    NoJumpPointsFound = false;
                                } else //leak catch
                                AGVs[i].JumpPoints.Add(new GridPos()); //increases the size of the AGV's embedded List so the JumpPoints can fit without causing overflow

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
                    GridLine line = new GridLine(m_rectangles[AGVs[i].JumpPoints[j].x][AGVs[i].JumpPoints[j].y],
                                                m_rectangles[AGVs[i].JumpPoints[j + 1].x][AGVs[i].JumpPoints[j + 1].y]
                                               );



                    AGVs[i].Paths[j] = line;

                }

            for (int i = 0; i < StartPos.Count; i++) {
                if ((c - 1) > 0) {
                    Array.Resize(ref AGVs[i].Paths, c - 1);
                }
            }

            this.Invalidate();
        }

        private void checkForTrappedLoads(List<GridPos> pos) {
            int list_index = 0;
            bool removed;

            do {
                removed = false;
                jumpParam.Reset(StartPos[0], pos[list_index]);
                if (JumpPointFinder.FindPath(jumpParam, paper).Count == 0) { //if there's at LEAST 1 agv that cannot reach a Load, then that Load is  
                                                                             //removed from the loadPos and not considered as available - marked 4 
                    isLoad[pos[list_index].x, pos[list_index].y] = 4;
                    pos.Remove(pos[list_index]);
                    removed = true;
                } else
                    isLoad[pos[list_index].x, pos[list_index].y] = 1;

                if (!removed)
                    list_index++;
            } while (list_index < pos.Count);

        }

        private int getStepsToLoad(int whichAGV) {
            int ix = AGVs[whichAGV].MarkedLoad.X * Constants.__BlockSide;
            int iy = (AGVs[whichAGV].MarkedLoad.Y * Constants.__BlockSide) + Constants.__TopBarOffset;

            int step = -1;

            for (int i = 0; i < AGVs[whichAGV].Steps.GetLength(0); i++) {
                if (
                    AGVs[whichAGV].Steps[i].X - ((Constants.__BlockSide / 2) - 1) == ix &&
                    AGVs[whichAGV].Steps[i].Y - ((Constants.__BlockSide / 2) - 1) == iy
                    ) {
                    step = i;
                    i = AGVs[whichAGV].Steps.GetLength(0);
                }
            }
            if (step >= 0) return step;
            else return -1;

        }


        private void Reset() {

            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < AGVs.Length; i++) {
                for (int j = 0; j < Constants.__MaximumSteps; j++) {
                    AGVs[i].Steps[j].X = 0;
                    AGVs[i].Steps[j].Y = 0;
                    AGVs[i].StepsCounter = new int();
                    AGVs[i].Paths = new GridLine[Constants.__MaximumSteps];
                }
            }

        }
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

        private void halt(int index, int _c) {
            timer_counter[index]--;
            if (!(_c - 1 < 0)) //in case the intersection is in the 1st step of the route, then the index of that step will be 0. 
            {                  //this means that trying to get to the "_c -1" step, will have the index decreased to -1 causing the "index out of bound" crash
                int stepx = Convert.ToInt32(AGVs[index].Steps[_c - 1].X);
                int stepy = Convert.ToInt32(AGVs[index].Steps[_c - 1].Y);
                AGVs[index].SetLocation(stepx - ((Constants.__BlockSide / 2) - 1), stepy - ((Constants.__BlockSide / 2) - 1));
            }
        }



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
        private void getNextLoad(int whichAGV) {
            bool isAnyLoadLeft = false;
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load) {
                        isAnyLoadLeft = true;
                    }
                }
            }

            if (!isAnyLoadLeft)
                return;

            aGVIndexToolStripMenuItem.Checked = false;
            //convert the end point to start point
            GridPos endPos = new GridPos();
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {

                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End) {

                        try {
                            StartPos[whichAGV] = new GridPos(widthTrav, heightTrav);

                            a = StartPos[whichAGV].x;
                            b = StartPos[whichAGV].y;

                        } catch {  }

                    }
                }
            }
            //*******************************************************************
            //*******************************************************************
            //*******************************************************************
            //*******************************************************************
            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {
                    //this causes the 'bug' that agvs are scanning from left to right for loads
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load && isLoad[widthTrav, heightTrav] == 1) {
                        isLoad[widthTrav, heightTrav] = 3;
                        AGVs[whichAGV].MarkedLoad = new Point(widthTrav, heightTrav);

                        loads--;
                        endPos.x = widthTrav;
                        endPos.y = heightTrav;

                        widthTrav = Constants.__WidthBlocks;
                        heightTrav = Constants.__HeightBlocks;
                    }
                }
            }

            //*******************************************************************
            jumpParam.Reset(StartPos[whichAGV], endPos); 
            JumpPointsList = JumpPointFinder.FindPath(jumpParam, paper);


            for (int j = 0; j < JumpPointsList.Count; j++)
                AGVs[whichAGV].JumpPoints.Add(JumpPointsList[j]); //adds the list containing the AGV's path, to the AGV's embedded JumpPoint List 

            int c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < StartPos.Count; i++) {
                if ((c - 1) > 0) {
                    Array.Resize(ref AGVs[i].Paths, c - 1);
                }
            }


            for (int j = 0; j < AGVs[whichAGV].JumpPoints.Count - 1; j++) {
                GridLine line = new GridLine(m_rectangles[AGVs[whichAGV].JumpPoints[j].x][AGVs[whichAGV].JumpPoints[j].y],
                                        m_rectangles[AGVs[whichAGV].JumpPoints[j + 1].x][AGVs[whichAGV].JumpPoints[j + 1].y]
                                           );

                AGVs[whichAGV].Paths[j] = line;
            }

            
            //return to exit
            int old_c = c-1;

            jumpParam.Reset(endPos, StartPos[whichAGV]);
            JumpPointsList = JumpPointFinder.FindPath(jumpParam, paper);
            for (int j = 0; j < JumpPointsList.Count; j++)
                AGVs[whichAGV].JumpPoints.Add(JumpPointsList[j]); //adds the list containing the AGV's path, to the AGV's embedded JumpPoint List

            c = 0;
            for (int i = 0; i < StartPos.Count; i++)
                c += AGVs[i].JumpPoints.Count;

            for (int i = 0; i < StartPos.Count; i++) {
                if ((c - 1) > 0) {
                    Array.Resize(ref AGVs[i].Paths, old_c+(c - 1));
                }
            }

            
            for (int i = 0; i < StartPos.Count; i++) {
                for (int j = 0; j < AGVs[i].JumpPoints.Count - 1; j++) {
                    GridLine line = new GridLine(m_rectangles[AGVs[i].JumpPoints[j].x][AGVs[i].JumpPoints[j].y],
                                        m_rectangles[AGVs[i].JumpPoints[j + 1].x][AGVs[i].JumpPoints[j + 1].y]
                                           );

                    AGVs[i].Paths[j] = line;
                }

            }
            this.Invalidate();
        }

        private void timers(int agvs_number) {
            //agvs_number = how many agvs I have placed
            //every timer is responsible for every agv for up to 5 AGVs

            if (agvs_number == 1 && !is_trapped[0, 0] && !is_trapped[0, 1]) {
                timer0.Start();
            }

            if (agvs_number == 2) {
                if (!is_trapped[0, 0] && !is_trapped[0, 1]) {
                    timer0.Start();
                }
                if (!is_trapped[1, 0] && !is_trapped[1, 1]) {
                    timer1.Start();
                }
            }
            if (agvs_number == 3) {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer0.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer1.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer2.Start();
            }
            if (agvs_number == 4) {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer0.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer1.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer2.Start();
                if (!is_trapped[3, 0] && !is_trapped[3, 1])
                    timer3.Start();
            }
            if (agvs_number == 5) {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer0.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer1.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer2.Start();
                if (!is_trapped[3, 0] && !is_trapped[3, 1])
                    timer3.Start();
                if (!is_trapped[4, 0] && !is_trapped[4, 1])
                    timer4.Start();
            }


        }



        private void initialization() {
            for (int i = 0; i < AGVs.Count(); i++) {
                AGVs[i] = new Vehicle(this);
                AGVs[i].ID = i;
            }

            this.DoubleBuffered = true;
            this.Width = ((Constants.__WidthBlocks + 1) * Constants.__BlockSide) - 3; //3 because 2=boarder and the 1 comes from "width+1"
            this.Height = (Constants.__HeightBlocks + 1) * Constants.__BlockSide + Constants.__BottomBarOffset;
            this.Size = new Size(this.Width, this.Height + Constants.__BottomBarOffset);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            m_rectangles = new GridBox[Constants.__WidthBlocks][];

            for (int widthTrav = 0; widthTrav < Constants.__WidthBlocks; widthTrav++) {
                //declares the length of the seconds 1d array
                m_rectangles[widthTrav] = new GridBox[Constants.__HeightBlocks];
                for (int heightTrav = 0; heightTrav < Constants.__HeightBlocks; heightTrav++) {

                    //dynamically add the gridboxes into the m_rectangles.
                    //size of the m_rectangels is constantly increasing(while adding
                    //the gridbox values) until 
                    //size=height or size = width.
                    if (imported) {
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
            jumpParam = new JumpPointParam(searchGrid, false, crossCorners, crossAdjacent, mode);
            //syntax of jumpParam-> JumpPointParam(searchGrid, startPos, endPos, cbCrossCorners.Checked, HeuristicMode.EUCLIDEANSQR);


        }

        private double GetLength(double x1, double y1, double x2, double y2) {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        private double getSide(int w, int h) {
            return (System.Math.Sqrt((w * w) + (h * h)));
        }
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
        private void import() {

            ofd_importmap.Filter = "kagv Map (*.kmap)|*.kmap";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                FullyRestore();
                StreamReader reader = new StreamReader(ofd_importmap.FileName);
                if (reader.ReadLine().Count() == ((Constants.__BlockSide / 2) - 1)) {
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
                            } else if (whichNumber == 2) {
                                height_blocks = Convert.ToInt32(_s);
                            }

                        }
                    }

                    reader.ReadLine();
                   
                    importmap = new BoxType[width_blocks, height_blocks];

                    words = reader.ReadLine().Split(delim);

                    int starts_counter = 0;
                    for (int z = 0; z < importmap.GetLength(0); z++) {
                        int i = 0;
                        foreach (string _s in words) {
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
                        }
                        if (z == importmap.GetLength(0) - 1) { } else
                            words = reader.ReadLine().Split(delim);
                    }
                    reader.Close();
                    
                    for (int z = 0; z < importmap.GetLength(0); z++) {
                        for (int i = 0; i < importmap.GetLength(1); i++) {
                            m_rectangles[z][i].boxType = importmap[z, i];
                        }

                    }
                    nUD_AGVs.Value = starts_counter;
                    initialization();
                    Redraw();
                } else
                    MessageBox.Show(this, "You have chosen a not compatible file import.\r\nPlease try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool isvalid(Point _temp) {

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
