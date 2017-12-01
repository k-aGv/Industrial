using System.Collections.Generic;
using kagv.DLL_source;
namespace kagv {
    internal class WMS {
        public WMS() {


        }
        public WMS(int[,] _isLoad, GridBox[][] _m_rectangles, List<GridPos> _loadPos, BaseGrid _searchGrid) {
            IsLoad = _isLoad;
            Rectangles = _m_rectangles;
            LoadPos = _loadPos;
            SearchGrid = _searchGrid;
        }

        private BaseGrid _SearchGrid;
        internal BaseGrid SearchGrid { get => _SearchGrid; set => _SearchGrid = value; }

        private GridBox[][] _m_rectangles;
        internal GridBox[][] Rectangles { get => _m_rectangles; set => _m_rectangles = value; }
        

        private int[,] _isLoad;
        internal int[,] IsLoad { get => _isLoad; set => _isLoad = value; }
        

        private List<GridPos> _LoadPos;
        internal List<GridPos> LoadPos { get => _LoadPos; set => _LoadPos = value; }
        
        private int _LoadsCount=0;
        public int LoadsCount { get => _LoadsCount; set => _LoadsCount = value; }
    }
}
