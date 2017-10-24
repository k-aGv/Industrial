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
