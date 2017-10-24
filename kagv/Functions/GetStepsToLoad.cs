using System.Drawing;

namespace kagv {

    public partial class MainForm  {

        //returns the number of steps until AGV reaches the marked Load
        private int GetStepsToLoad(int whichAgv) {
            Point iCords = new Point(_AGVs[whichAgv].GetMarkedLoad().X, _AGVs[whichAgv].GetMarkedLoad().Y);
            int step = -1;

            for (int i = 0; i < _AGVs[whichAgv].Steps.GetLength(0); i++)
                if (_AGVs[whichAgv].Steps[i].X - ((Globals.BlockSide / 2) - 1) == iCords.X &&
                    _AGVs[whichAgv].Steps[i].Y - ((Globals.BlockSide / 2) - 1) == iCords.Y) {
                    step = i;
                    i = _AGVs[whichAgv].Steps.GetLength(0);
                }

            if (step >= 0) return step;
            return -1;
        }
    }
}
