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
using System.Collections.Generic;

namespace kagv {

    public partial class MainForm {

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
    }
}
