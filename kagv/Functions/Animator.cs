using kagv.DLL_source;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace kagv {

    public partial class MainForm {

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
    }
}
