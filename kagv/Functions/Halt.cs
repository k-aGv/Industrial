using System;

namespace kagv {

    public partial class MainForm  {

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
    }
}
