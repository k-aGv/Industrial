using kagv.DLL_source;

namespace kagv {

    public partial class MainForm {

        //function that determines which loads are valid to keep and which are not
        private void KeepValidLoads(GridPos endPoint) {
            int listIndex = 0;
            for (int i = 0; i < _loadPos.Count; i++)
                _searchGrid.SetWalkableAt(_loadPos[i], true); //assumes that all loads are walkable
                                                              //and only walls are in fact the only obstacles in the grid

            do {
                bool removed = false;
                _jumpParam.Reset(_loadPos[listIndex], endPoint); //tries to find path between each Load and the exit
                if (AStarFinder.FindPath(_jumpParam, nud_weight.Value).Count == 0) //if no path is found
                {
                    _isLoad[_loadPos[listIndex].X, _loadPos[listIndex].Y] = 2; //mark the corresponding load as NOT available
                    _loads--; //decrease the counter of total loads in the grid
                    _loadPos.RemoveAt(listIndex); //remove that load from the list
                    removed = true;
                }
                if (!removed) {
                    listIndex++;
                }

            } while (listIndex < _loadPos.Count); //loop repeats untill all loads are checked

            if (_loadPos.Count == 0)
                _mapHasLoads = false;
        }
    }
}
