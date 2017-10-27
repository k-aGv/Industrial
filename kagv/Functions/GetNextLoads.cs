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
using kagv.DLL_source;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace kagv {

    public partial class MainForm {

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
            loadPos = CheckForTrappedLoads(loadPos, new GridPos(_a, _b)); //scans the loadPos list to check which loads are available
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
            List<GridPos> jumpPointsList = AStarFinder.FindPath(_jumpParam, Globals.AStarWeight);
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
            jumpPointsList = AStarFinder.FindPath(_jumpParam, Globals.AStarWeight);
            _AGVs[whichAgv].JumpPoints.AddRange(jumpPointsList);

            c = 0;
            for (int i = 0; i < _startPos.Count; i++) {
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
    }
}
