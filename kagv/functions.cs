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

namespace kagv
{
    
    public partial class main_form
    {

        private void FullyRestore(object s, EventArgs e)
        {

            Array.Clear(is_trapped, 0, is_trapped.GetLength(0));
            Array.Clear(is_trapped, 0, is_trapped.GetLength(1));

            if (timer_counter != null)
                Array.Clear(timer_counter, 0, timer_counter.GetLength(0));


            Array.Clear(currentLinePoints, 0, currentLinePoints.GetLength(0));

            //jagged array has to be resetted like this
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    m_rectangles[i][j] = new GridBox(i * 20, j * 20 + topBarOffset, BoxType.Normal);
                }
            }

            if (markedbyagv != null)
                Array.Clear(markedbyagv, 0, markedbyagv.GetLength(0));

            for (int i = 0; i < AGVs.GetLength(0); i++)
            {
                AGVs[i].killIcon();
            }

            if (importmap != null)
            {
                Array.Clear(importmap, 0, importmap.GetLength(0));
                Array.Clear(importmap, 0, importmap.GetLength(1));
            }


            myLines = new GridLine[2000, 5];
            newsteps = new double[5, 2, 2000];
            fromstart = new bool[5];
            isLoad = new int[width, height];
            AGVs = new Vehicle[5];
            new_steps_counter = new int[5];

            endPointCoords = new Point();

            load_line = new List<GridLine>();
            myresultList = new List<List<GridPos>>();
            pos = resultList = new List<GridPos>();
            searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);


            lol_empty = true;
            imported =
            calibrated =
            isMouseDown =
            mapHasLoads = false;

            currentLinePoints = null;
            jumpParam = null;
            paper = null;
            loads = pos_index = 0;
            log = "";

            distanceBlocks
                = a
                = b
                = resultCount
                = formHeight
                = formWidth
                = new int();

            initialization();
            main_form_Load(s, e);

            timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = timer5.Interval = 100;
            refresh_label.Text = "Delay:" + timer1.Interval + " ms";

        }

        T[,] ResizeArray<T>(T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];

            return newArray;
        }

        private void updateParameters()
        {
            jumpParam.CrossAdjacentPoint = crossAdjacent;
            jumpParam.CrossCorner = crossCorners;
            jumpParam.UseRecursive = useRecursive; 
        }
        private int getNumberOfAGVs()
        {
            int agvs=0;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                if(m_rectangles[i][j].boxType==BoxType.Start)
                    agvs++;

            return agvs;
        }

        private void Redraw()
        {
            if (loads > 0)
                mapHasLoads = true;
            else
                mapHasLoads = false;

            bool start_found = false;
            bool end_found = false;

            List<GridPos> loadPos = new List<GridPos>();
            int loadPos_index = 0;

            GridPos endPos = new GridPos();

            pos = new List<GridPos>();
            pos_index = 0;


            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (m_rectangles[i][j].boxType != BoxType.Wall)
                        searchGrid.SetWalkableAt(new GridPos(i, j), true);
                    else
                        searchGrid.SetWalkableAt(new GridPos(i, j), false);

                    if (m_rectangles[i][j].boxType == BoxType.Start)
                    {
                        start_found = true;

                        pos.Add(new GridPos(0, 0));//create space to add the next agv
                        pos[pos_index].x = i;
                        pos[pos_index].y = j;

                        a = pos[pos_index].x;
                        b = pos[pos_index].y;

                        if (pos_index < pos.Count)
                        {
                            pos[pos_index] = new GridPos(pos[pos_index].x, pos[pos_index].y);
                            pos_index++;
                        }
                    }

                    if (m_rectangles[i][j].boxType == BoxType.End)
                    {
                        end_found = true;
                        endPos.x = i;
                        endPos.y = j;
                        endPointCoords = new Point(i * 20, j * 20 + topBarOffset);
                    }

                    if (mapHasLoads)
                    {
                        if (isLoad[i, j] == 1 || isLoad[i, j] == 4)
                        {
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

            lol_empty = true;

            if (AGVs != null)
                for (int i = 0; i < AGVs.Count(); i++)
                    if (AGVs[i] != null)
                    {
                        AGVs[i].updateAGV();
                        AGVs[i].Busy(false);//initialize the status of AGVs, as 'available'
                    }

            loadPos_index = 0;
            pos_index = 0;
            is_trapped = new bool[pos.Count, 2];
            //is_trapped[i,0] -> unavailable path for Start to Load
            //is_trapped[i,1] -> unavailable path for Load to End


            //architecture of myresultList
            //myresultList [number of agv] [number of part-line].element
            myresultList = new List<List<GridPos>>(); //List containing Lists -> List<GridPos>

            for (int i = 0; i < pos.Count; i++)
            {
                if (AGVs[i].isBusy() == false)
                {
                    if (loadPos.Count() == 0)
                        mapHasLoads = false;

                    //create the path from start to load,if load exists
                    switch (mapHasLoads)
                    {
                        case true:

                            int list_index = 0;
                            bool removed;
                            do
                            {
                                removed = false;
                                jumpParam.Reset(pos[0], loadPos[list_index]);
                                if (JumpPointFinder.FindPath(jumpParam, paper).Count == 0) //if there's at LEAST 1 agv that cannot reach a Load, then that Load is  
                                {                                                          //removed from the loadPos and not considered as available - marked 4 
                                    isLoad[loadPos[list_index].x, loadPos[list_index].y] = 4;
                                    loadPos.Remove(loadPos[list_index]);
                                    removed = true;
                                }
                                else
                                    isLoad[loadPos[list_index].x, loadPos[list_index].y] = 1;

                                if (!removed)
                                    list_index++;
                            } while (list_index < loadPos.Count);

                            if (loadPos.Count() == 0)
                                loadPos.Add(endPos); //if EVERY load is trapped, use the endPos as LoadPos so as the agvs can complete their basic route (start -> end)


                            jumpParam.Reset(pos[pos_index], loadPos[0]);
                            resultList = JumpPointFinder.FindPath(jumpParam, paper);
                            AGVs[i].Busy(true);

                            //is_trapped[i,0] -> part of route agv -> load
                            //is_trapped[i,1] -> part of route load -> end
                            if (resultList.Count == 0)
                                is_trapped[i, 0] = true;
                            else
                                is_trapped[i, 0] = false;

                            if (!is_trapped[i, 0])
                                for (int j = 0; j < resultList.Count; j++)
                                {
                                    myresultList.Add(new List<GridPos>());
                                    myresultList[i].Add(resultList[j]); //adds the list containing all the JumpPoints, for every direction, to a parent List 
                                }
                            else //leak catch
                                myresultList.Add(new List<GridPos>()); //increases the size of the List so the resultList can fit without causing overflow




                            //from load to end
                            jumpParam.Reset(loadPos[0], endPos);
                            resultList = JumpPointFinder.FindPath(jumpParam, paper);
                            if (resultList.Count == 0)//recheck if load-to-end is a trapped path
                                is_trapped[i, 1] = true;
                            else
                                is_trapped[i, 1] = false;


                            if (!is_trapped[i, 1])
                                for (int j = 0; j < resultList.Count; j++)
                                {
                                    myresultList.Add(new List<GridPos>());

                                    myresultList[i].Add(resultList[j]); //adds the list containing all the JumpPoints, for every direction, to a parent List 
                                    lol_empty = false;
                                }


                            if (!is_trapped[i, 0] && !is_trapped[i, 1])
                            {
                                //marks the load that each AGV picks up on the 1st route, as 3, so each agv knows where to go after delivering the 1st load
                                if (fromstart[i])
                                {
                                    for (int widthtrav = 0; widthtrav < width; widthtrav++)
                                        for (int heightrav = 0; heightrav < height; heightrav++)
                                            if (isLoad[widthtrav, heightrav] == 1)
                                            {
                                                isLoad[widthtrav, heightrav] = 3;
                                                markedbyagv[i] = new Point(widthtrav, heightrav);

                                                widthtrav = width;
                                                heightrav = height;
                                            }
                                }
                                loadPos.Remove(loadPos[0]);
                            }
                            else if (is_trapped[i, 1])
                                loadPos.Remove(loadPos[0]);
                            break;




                        case false:
                            jumpParam.Reset(pos[pos_index], endPos);
                            resultList = JumpPointFinder.FindPath(jumpParam, paper);

                            if (resultList.Count == 0)
                                is_trapped[i, 0] = true;
                            else
                                is_trapped[i, 0] = false;


                            if (!is_trapped[i, 0])
                                for (int j = 0; j < resultList.Count; j++)
                                {
                                    myresultList.Add(new List<GridPos>());
                                    myresultList[i].Add(resultList[j]); //adds the list containing all the JumpPoints, for every direction, to a parent List 

                                    lol_empty = false;
                                }
                            else //leak catch
                                myresultList.Add(new List<GridPos>()); //increases the size of the List so the resultList can fit without causing overflow

                            break;

                    }
                }
                pos_index++;
            }



            GridLine[,] tempLines = new GridLine[2000, myresultList.Count];// myresultList[0].Count + myresultList[1].Count]; 


            for (int i = 0; i < pos.Count; i++)
                for (int j = 0; j < myresultList[i].Count - 1; j++)
                {
                    //side:adds line to linearray.since it adds a new line,that means 
                    //that the new line IS the correct path
                    GridLine line = new GridLine(m_rectangles[myresultList[i][j].x][myresultList[i][j].y],
                                            m_rectangles[myresultList[i][j + 1].x][myresultList[i][j + 1].y]
                                               ); //changed the indexes so it pulls data from the CONTAINED lists INSIDE the Parent List

                    tempLines[j, i] = line;
                }
            resultCount = myresultList.Count - 1;

            if (resultCount > 0)
                myLines = ResizeArray(tempLines, resultCount, 5);

            this.Invalidate();
        }

        private int getStepsToLoad(int whichAGV)
        {
            int ix = markedbyagv[whichAGV].X * 20;
            int iy = (markedbyagv[whichAGV].Y * 20)+topBarOffset;

            int step=-1;

            for (int i = 0; i < newsteps.GetLength(2); i++)
            {
                if (
                    newsteps[whichAGV, 0, i] - 9 == ix &&
                    newsteps[whichAGV, 1, i] - 9 == iy
                    )
                {
                    step = i;
                    i = newsteps.GetLength(2);
                }
            }
            if (step >= 0) return step;
            else return -1;
            
        }


        private void Reset()
        {
            //+1 will safely recreate the array without any
            //overflow issues like above situation
            myLines = new GridLine[resultCount + 1, 5];

            newsteps = new double[5, 2, 2000];

            for (int i = 0; i < new_steps_counter.Count(); i++)
                new_steps_counter[i] = 0;

        }
        private void Reset(int whichAGV) //overloaded Reset
        {
            //+1 will safely recreate the array without any
            //overflow issues like above situation

            int c = myLines.GetLength(0);

            myresultList[whichAGV] = new List<GridPos>(); //empties the correct cell of the 2D-List containing each AGVs routes
            pos[whichAGV] = new GridPos(); //empties the correct start Pos for each AGV

            for (int i = 0; i < c; i++)
                myLines[i, whichAGV] = null;

            //  myLines = new GridLine[resultCount + 1, whichAGV];

            // newsteps = new double[5, 2, 2000];

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2000; j++)
                    newsteps[whichAGV, i, j] = 0;

            new_steps_counter[whichAGV] = 0;
        }
        private void halt(int index, int _c)
        {
            timer_counter[index]--;
            if (!(_c - 1 < 0)) //in case the intersection is in the 1st step of the route, then the index of that step will be 0. 
            {                  //this means that trying to get to the "_c -1" step, will have the index decreased to -1 causing the "index out of bound" crash
                int stepx = Convert.ToInt32(newsteps[index, 0, _c - 1]);
                int stepy = Convert.ToInt32(newsteps[index, 1, _c - 1]);
                AGVs[index].SetLocation(stepx - 9, stepy - 9);
            }
        }
        
        private void displayStepsToLoad(int counter, int agv_index)
        {
            int stepstoload;
            string agvinfo;

            int steps;
            switch (agv_index)
            {
                case 0:
                    steps = getStepsToLoad(agv_index);
                    if (steps == -1)
                        agvinfo = "AGV 1: Moving straight to the end point";
                    else
                    {
                        stepstoload = (getStepsToLoad(agv_index) - counter);
                        agvinfo = "AGV 1: Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                        if (stepstoload < 0)
                            agvinfo = "AGV " + (agv_index + 1) + " is Loaded.";
                    }
                    agv1steps_LB.Text = agvinfo;

                    break;
                case 1:
                    steps = getStepsToLoad(agv_index);
                    if (steps == -1)
                        agvinfo = "AGV 2: Moving straight to the end point";
                    else
                    {
                        stepstoload = (getStepsToLoad(agv_index) - counter);
                        agvinfo = "AGV 2: Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                        if (stepstoload < 0)
                            agvinfo = "AGV " + (agv_index + 1) + " is Loaded.";
                    }
                    agv2steps_LB.Text = agvinfo;

                    break;
                case 2:
                    steps = getStepsToLoad(agv_index);
                    if (steps == -1)
                        agvinfo = "AGV 3: Moving straight to the end point";
                    else
                    {
                        stepstoload = (getStepsToLoad(agv_index) - counter);
                        agvinfo = "AGV 3: Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                        if (stepstoload < 0)
                            agvinfo = "AGV " + (agv_index + 1) + " is Loaded.";
                    }
                    agv3steps_LB.Text = agvinfo;

                    break;
                case 3:
                    steps = getStepsToLoad(agv_index);
                    if (steps == -1)
                        agvinfo = "AGV 4: Moving straight to the end point";
                    else
                    {
                        stepstoload = (getStepsToLoad(agv_index) - counter);
                        agvinfo = "AGV 4: Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                        if (stepstoload < 0)
                            agvinfo = "AGV " + (agv_index + 1) + " is Loaded.";
                    }
                    agv4steps_LB.Text = agvinfo;

                    break;
                case 4:
                    steps = getStepsToLoad(agv_index);
                    if (steps == -1)
                        agvinfo = "AGV 5: Moving straight to the end point";
                    else
                    {
                        stepstoload = (getStepsToLoad(agv_index) - counter);
                        agvinfo = "AGV 5: Marked load @" + getStepsToLoad(agv_index) + ". Steps remaining to Load: " + stepstoload;
                        if (stepstoload < 0)
                            agvinfo = "AGV " + (agv_index + 1) + " is Loaded.";
                    }
                    agv5steps_LB.Text = agvinfo;

                    break;
            }
        }

        private void animator(int counter, int agv_index)
        {
            bool isfreeload = false;

            int stepx = Convert.ToInt32(newsteps[agv_index, 0, counter]);
            int stepy = Convert.ToInt32(newsteps[agv_index, 1, counter]);

            if (stepx == 0 || stepx == 0)
                return;

            displayStepsToLoad(counter, agv_index);

            //RULES OF WHICH AGV WILL STOP WILL BE ADDED
            for (int i = 0; i < nUD_AGVs.Value; i++)
            {

                if (agv_index != i
                    && AGVs[i].GetLocation() != new Point(0, 0)//i dont like that much tho
                    && AGVs[agv_index].GetLocation() == AGVs[i].GetLocation()
                    && AGVs[agv_index].GetLocation() != endPointCoords
                    )
                {

                    halt(agv_index, counter);
                }
                else
                    AGVs[agv_index].SetLocation(stepx - 9, stepy - 9);

            }


            if (markedbyagv[agv_index].X * 20 == AGVs[agv_index].GetLocation().X &&
                (markedbyagv[agv_index].Y * 20) + topBarOffset == AGVs[agv_index].GetLocation().Y &&
                !AGVs[agv_index].isBusy())
            {

                m_rectangles[markedbyagv[agv_index].X][markedbyagv[agv_index].Y].SwitchLoad();
                AGVs[agv_index].Busy(true);
                AGVs[agv_index].setLoaded();
                if (fromstart[agv_index])
                {
                    loads--;
                    isLoad[markedbyagv[agv_index].X, markedbyagv[agv_index].Y] = 2;

                    fromstart[agv_index] = false;
                }
            }

            if (!fromstart[agv_index])
            {
                if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / 20][(endPointCoords.Y - topBarOffset) / 20].x &&
                    AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / 20][(endPointCoords.Y - topBarOffset) / 20].y)
                {


                    AGVs[agv_index].Busy(false);

                    for (int k = 0; k < width; k++)
                    {
                        for (int b = 0; b < height; b++)
                        {
                            if (isLoad[k, b] == 1)
                                isfreeload = true;
                        }
                    }

                    if (loads > 0 && isfreeload)
                    {

                        Reset(agv_index);
                        AGVs[agv_index].Busy(true);
                        markedbyagv[agv_index] = new Point();
                        getNextLoad(agv_index);

                        AGVs[agv_index].Busy(false);
                        AGVs[agv_index].setEmpty();

                    }
                    else
                    {
                        AGVs[agv_index].setEmpty();
                        isfreeload = false;

                        switch (agv_index)
                        {
                            case 0:
                                timer1.Stop();
                                agv1steps_LB.Text = "AGV 1: Finished";
                                break;
                            case 1:
                                timer2.Stop();
                                agv2steps_LB.Text = "AGV 2: Finished";
                                break;
                            case 2:
                                timer3.Stop();
                                agv3steps_LB.Text = "AGV 3: Finished";
                                break;
                            case 3:
                                timer4.Stop();
                                agv4steps_LB.Text = "AGV 4: Finished";
                                break;
                            case 4:
                                timer5.Stop();
                                agv5steps_LB.Text = "AGV 5: Finished";
                                break;
                        }

                    }

                    timer_counter[agv_index] = -1;
                    counter = 0;

                }
            }
            else
            {
                if (isLoad[markedbyagv[agv_index].X, markedbyagv[agv_index].Y] == 2)
                    if (AGVs[agv_index].GetLocation().X == m_rectangles[endPointCoords.X / 20][(endPointCoords.Y - topBarOffset) / 20].x &&
                        AGVs[agv_index].GetLocation().Y == m_rectangles[endPointCoords.X / 20][(endPointCoords.Y - topBarOffset) / 20].y)
                        switch (agv_index)
                        {
                            case 0:
                                timer1.Stop();
                                agv1steps_LB.Text = "AGV 1: Finished";
                                break;
                            case 1:
                                timer2.Stop();
                                agv2steps_LB.Text = "AGV 2: Finished";
                                break;
                            case 2:
                                timer3.Stop();
                                agv3steps_LB.Text = "AGV 3: Finished";
                                break;
                            case 3:
                                timer4.Stop();
                                agv4steps_LB.Text = "AGV 4: Finished";
                                break;
                            case 4:
                                timer5.Stop();
                                agv5steps_LB.Text = "AGV 5: Finished";
                                break;
                        }
            }
            //end of handling


            if (!(timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled || timer5.Enabled)) //if at least 1 timer is active, do not let the user access the Checkboxes etc. etc
            {
                menuPanel.Enabled = true;
                settings_menu.Enabled = true;

            }

            this.Invalidate();

        }
        //will be only called when the first load is unloaded to the end point

        
        private void getNextLoad(int whichAGV)
        {
            bool isAnyLoadLeft = false;
            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load)
                    {
                        isAnyLoadLeft = true;
                    }
                }
            }

            if (!isAnyLoadLeft)
                return;

            //convert the end point to start point
            GridPos endPos = new GridPos();
            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {

                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                    {

                        try
                        {
                            pos[whichAGV] = new GridPos(widthTrav, heightTrav);
                            
                            a = pos[whichAGV].x;
                            b = pos[whichAGV].y;

                        }
                        catch (Exception e) { MessageBox.Show(e + "\r\n getNextLoad()"); }

                    }
                }
            }
            //*******************************************************************
            //*******************************************************************
            //*******************************************************************
            //*******************************************************************
            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {
                    //this causes the 'bug' that agvs are scanning from left to right for loads
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load && isLoad[widthTrav, heightTrav] == 1)
                    {
                        isLoad[widthTrav, heightTrav] = 3;
                        markedbyagv[whichAGV] = new Point(widthTrav, heightTrav);

                        loads--;
                        endPos.x = widthTrav;
                        endPos.y = heightTrav;

                        widthTrav = width;
                        heightTrav = height;
                    }
                }
            }




            //*******************************************************************
            try
            {
                jumpParam.Reset(pos[whichAGV], endPos); //THIS was the problem why the 2nd agv had no route. Pos[] was redeclared with 1 cell. The incoming whichAGV
                //had its value set to 1 (for the 2nd agv), causing the pos[] to overflow, catching the exception and skipping
                //the calculation of the route
                resultList = JumpPointFinder.FindPath(jumpParam, paper);


                for (int j = 0; j < resultList.Count; j++)
                {
                    myresultList.Add(new List<GridPos>());
                    myresultList[whichAGV].Add(resultList[j]); //adds the list containing all the JumpPoints, for every direction, to a parent List 
                }

                GridLine[,] tempLines = new GridLine[2000, myresultList.Count];// myresultList[0].Count + myresultList[1].Count]; 


                for (int j = 0; j < myresultList[whichAGV].Count - 1; j++)
                {
                    //side:adds line to linearray.since it adds a new line,that means 
                    //that the new line IS the correct path
                    GridLine line = new GridLine(m_rectangles[myresultList[whichAGV][j].x][myresultList[whichAGV][j].y],
                                            m_rectangles[myresultList[whichAGV][j + 1].x][myresultList[whichAGV][j + 1].y]
                                               ); //changed the indexes so it pulls data from the CONTAINED lists INSIDE the Parent List

                    tempLines[j, whichAGV] = line;
                }


                resultCount = myresultList.Count - 1;

                if (resultCount > 0)
                    myLines = ResizeArray(tempLines, resultCount, 5);


                //return to exit
                jumpParam.Reset(endPos, pos[whichAGV]);
                resultList = JumpPointFinder.FindPath(jumpParam, paper);
                for (int j = 0; j < resultList.Count; j++)
                {
                    myresultList.Add(new List<GridPos>());
                    myresultList[whichAGV].Add(resultList[j]); //adds the list containing all the JumpPoints, for every direction, to a parent List 
                }

                tempLines = new GridLine[2000, myresultList.Count];// myresultList[0].Count + myresultList[1].Count]; 

                for (int i = 0; i < pos.Count; i++)
                {
                    for (int j = 0; j < myresultList[i].Count - 1; j++)
                    {
                        //side:adds line to linearray.since it adds a new line,that means 
                        //that the new line IS the correct path
                        GridLine line = new GridLine(m_rectangles[myresultList[i][j].x][myresultList[i][j].y],
                                                m_rectangles[myresultList[i][j + 1].x][myresultList[i][j + 1].y]
                                                   ); //changed the indexes so it pulls data from the CONTAINED lists INSIDE the Parent List

                        tempLines[j, i] = line;
                    }

                }

                resultCount = myresultList.Count - 1;

                if (resultCount > 0)
                    myLines = ResizeArray(tempLines, resultCount, 5);

                


                this.Invalidate();

                

                //return true;//if any other load exist
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "");
            }
            // return false;

            
        }
       
        private void timers(int agvs_number)
        {
            //agvs_number = how many agvs I have placed
            //every timer is responsible for every agv for up to 5 AGVs

            if (agvs_number == 1 && !is_trapped[0, 0] && !is_trapped[0, 1])
            {
                timer1.Start();
            }

            if (agvs_number == 2)
            {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                {
                    timer1.Start();
                }
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                {
                    timer2.Start();
                }
            }
            if (agvs_number == 3)
            {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer1.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer2.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer3.Start();
            }
            if (agvs_number == 4)
            {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer1.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer2.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer3.Start();
                if (!is_trapped[3, 0] && !is_trapped[3, 1])
                    timer4.Start();
            }
            if (agvs_number == 5)
            {
                if (!is_trapped[0, 0] && !is_trapped[0, 1])
                    timer1.Start();
                if (!is_trapped[1, 0] && !is_trapped[1, 1])
                    timer2.Start();
                if (!is_trapped[2, 0] && !is_trapped[2, 1])
                    timer3.Start();
                if (!is_trapped[3, 0] && !is_trapped[3, 1])
                    timer4.Start();
                if (!is_trapped[4, 0] && !is_trapped[4, 1])
                    timer5.Start();
            }


        }


        
        private void initialization()
        {
            for (int i = 0; i < AGVs.Count(); i++)
            {
                AGVs[i] = new Vehicle(this);
            }

            this.DoubleBuffered = true;
            this.Width = ((width + 1) * 20) - 3; //3 because 2=boarder and the 1 comes from "width+1"
            this.Height = (height + 1) * 20 + bottomBarOffset;
            this.Size = new Size(this.Width, this.Height + bottomBarOffset);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            formHeight = height;
            formWidth = width;


            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            m_rectangles = new GridBox[width][];

            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                //declares the length of the seconds 1d array
                m_rectangles[widthTrav] = new GridBox[height];
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {

                    //dynamically add the gridboxes into the m_rectangles.
                    //size of the m_rectangels is constantly increasing(while adding
                    //the gridbox values) until 
                    //size=height or size = width.
                    if (imported)
                    {
                        m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + topBarOffset, importmap[widthTrav, heightTrav]);
                        if (importmap[widthTrav, heightTrav] == BoxType.Load)
                        {
                            isLoad[widthTrav, heightTrav] = 1;
                            loads++;
                        }
                    }
                    else
                    {
                        m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + topBarOffset, BoxType.Normal);
                        isLoad[widthTrav, heightTrav] = 2;//is normal
                    }
                }
            }
            if (imported)
                imported = false;



            searchGrid = new DynamicGridWPool(SingletonHolder<NodePool>.Instance);
            jumpParam = new JumpPointParam(searchGrid, false, crossCorners, crossAdjacent, mode);
            //syntax of jumpParam-> JumpPointParam(searchGrid, startPos, endPos, cbCrossCorners.Checked, HeuristicMode.EUCLIDEANSQR);


        }
      
        private double GetLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        private double getSide(int w, int h)
        {
            return (System.Math.Sqrt((w * w) + (h * h)));
        }
        private void DrawPoints(GridLine x, int line_index)
        {

            int x1 = x.fromX;
            int y1 = x.fromY;
            int x2 = x.toX;
            int y2 = x.toY;
            double distance = GetLength(x1, y1, x2, y2);

            double side = getSide(m_rectangles[0][0].height
                            , m_rectangles[0][0].height);
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
            }
            else
                MessageBox.Show("Unexpected error");

            currentLinePoints = new Point[distanceBlocks];
            double t;

            for (int i = 0; i < distanceBlocks; i++)
            {
                calibrated = false;

                if (distance != 0)
                    t = ((side) / distance);
                else
                    return;


                a = Convert.ToInt32(((1 - t) * x1) + (t * x2));
                b = Convert.ToInt32(((1 - t) * y1) + (t * y2));

                Point _p = new Point(a, b);

                for (int k = 0; k < formWidth; k++)
                {

                    for (int l = 0; l < formHeight; l++)
                    {

                        if (m_rectangles[k][l].boxRec.Contains(_p))
                        {
                            int sideX = m_rectangles[k][l].boxRec.X + 9;//9 is the width/2
                            int sideY = m_rectangles[k][l].boxRec.Y + 9;
                            currentLinePoints[i].X = sideX;
                            currentLinePoints[i].Y = sideY;

                            if (showDots)
                                paper.FillEllipse(br, currentLinePoints[i].X - 3,
                                    currentLinePoints[i].Y - 3,
                                    5, 5);

                            if (showSteps)
                                paper.DrawString(new_steps_counter[line_index] + ""
                                   , stepFont
                                   , fontBR
                                   , currentLinePoints[i]);


                            calibrated = true;

                        }

                    }

                }

                if (calibrated)
                {
                    newsteps[line_index, 0, new_steps_counter[line_index]] = currentLinePoints[i].X;
                    newsteps[line_index, 1, new_steps_counter[line_index]] = currentLinePoints[i].Y;
                    new_steps_counter[line_index]++;

                }
                //init next steps
                x1 = currentLinePoints[i].X;
                y1 = currentLinePoints[i].Y;
                distance = GetLength(x1, y1, x2, y2);

            }


        }
        private void export()
        {
            sfd_exportmap.FileName = "";
            sfd_exportmap.Filter = "kagv Map (*.kmap)|*.kmap";

            if (sfd_exportmap.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(sfd_exportmap.FileName);
                writer.WriteLine("Map info:\r\nWidth blocks: " + width + "  Height blocks: " + height + "\r\n");
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        writer.Write(m_rectangles[i][j].boxType + " ");
                    }
                    writer.Write("\r\n");
                }
                writer.Close();
            }

        }
        private void import()
        {

            ofd_importmap.Filter = "kagv Map (*.kmap)|*.kmap";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK)
            {
                imported = true;
                StreamReader reader = new StreamReader(ofd_importmap.FileName);
                if (reader.ReadLine().Count() == 9)
                {
                    string map_details = reader.ReadLine();

                    char[] delim = { ' ' };
                    string[] words = map_details.Split(delim);

                    bool isNumber;
                    int _tempNumber;
                    int whichNumber = 1;

                    int width_blocks = 0;
                    int height_blocks = 0;

                    foreach (string _s in words)
                    {
                        isNumber = int.TryParse(_s, out _tempNumber);
                        if (isNumber)
                        {
                            if (whichNumber == 1)
                            {
                                width_blocks = Convert.ToInt32(_s);
                                whichNumber++;
                            }
                            else if (whichNumber == 2)
                            {
                                height_blocks = Convert.ToInt32(_s);
                            }

                        }
                    }

                    reader.ReadLine();

                    importmap = new BoxType[width_blocks, height];

                    words = reader.ReadLine().Split(delim);

                    int starts_counter = 0;
                    for (int z = 0; z < importmap.GetLength(0); z++)
                    {
                        int i = 0;
                        foreach (string _s in words)
                        {
                            if (i < importmap.GetLength(1))
                            {
                                if (_s == "Start")
                                {
                                    importmap[z, i] = BoxType.Start;
                                    starts_counter++;
                                }
                                else if (_s == "End")
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
                        if (z == importmap.GetLength(0) - 1)
                        { }
                        else
                            words = reader.ReadLine().Split(delim);
                    }
                    reader.Close();

                    for (int z = 0; z < importmap.GetLength(0); z++)
                    {
                        for (int i = 0; i < importmap.GetLength(1); i++)
                        {
                            m_rectangles[z][i].boxType = importmap[z, i];
                        }

                    }
                    nUD_AGVs.Value = starts_counter;
                    initialization();
                    Redraw();
                }
                else
                    MessageBox.Show("You have chosen a not compatible file import.\r\nPlease try again.");
            }
        }
        private bool isvalid(Point _temp)
        {

            if (_temp.X > m_rectangles[width - 1][height - 1].boxRec.X + 19
            || _temp.Y > m_rectangles[width - 1][height - 1].boxRec.Y + 19) // 18 because its 20-boarder size
                return false;

            if (!m_rectangles[(_temp.X) / 20][(_temp.Y - topBarOffset) / 20].boxRec.Contains(_temp))
                return false;

            return true;
        }

    }
}
