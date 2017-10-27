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
    }
}
