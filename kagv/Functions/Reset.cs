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
