using kagv.DLL_source;

namespace kagv {

    public partial class MainForm  {

        //returns the number of _AGVs
        private int GetNumberOfAGVs() {
            short agvs = 0;
            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++)
                    if (_rectangles[i][j].BoxType == BoxType.Start)
                        agvs++;

            return agvs;
        }
    }
}
