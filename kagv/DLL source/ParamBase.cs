
namespace kagv.DLL_source {
    public delegate float HeuristicDelegate(int iDx, int iDy);

    public abstract class ParamBase {
        protected ParamBase(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, DiagonalMovement iDiagonalMovement, HeuristicMode iMode) : this(iGrid, iDiagonalMovement, iMode) {
            MstartNode = MsearchGrid.GetNodeAt(iStartPos.X, iStartPos.Y);
            MendNode = MsearchGrid.GetNodeAt(iEndPos.X, iEndPos.Y);
            if (MstartNode == null)
                MstartNode = new Node(iStartPos.X, iStartPos.Y, true);
            if (MendNode == null)
                MendNode = new Node(iEndPos.X, iEndPos.Y, true);
        }

        protected ParamBase(BaseGrid iGrid, DiagonalMovement iDiagonalMovement, HeuristicMode iMode) {
            SetHeuristic(iMode);

            MsearchGrid = iGrid;
            DiagonalMovement = iDiagonalMovement;
            MstartNode = null;
            MendNode = null;
        }

        protected ParamBase(ParamBase param) {
            MsearchGrid = param.MsearchGrid;
            DiagonalMovement = param.DiagonalMovement;
            MstartNode = param.MstartNode;
            MendNode = param.MendNode;

        }

        internal abstract void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null);

        public void Reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null) {

            _reset(iStartPos, iEndPos, iSearchGrid);
            MstartNode = null;
            MendNode = null;

            if (iSearchGrid != null)
                MsearchGrid = iSearchGrid;
            MsearchGrid.Reset();
            MstartNode = MsearchGrid.GetNodeAt(iStartPos.X, iStartPos.Y);
            MendNode = MsearchGrid.GetNodeAt(iEndPos.X, iEndPos.Y);
            if (MstartNode == null)
                MstartNode = new Node(iStartPos.X, iStartPos.Y, true);
            if (MendNode == null)
                MendNode = new Node(iEndPos.X, iEndPos.Y, true);
        }

        public DiagonalMovement DiagonalMovement;
        public HeuristicDelegate HeuristicFunc
        {
            get {
                return Mheuristic;
            }
        }

        public BaseGrid SearchGrid
        {
            get {
                return MsearchGrid;
            }
        }

        public Node StartNode
        {
            get {
                return MstartNode;
            }
        }
        public Node EndNode
        {
            get {
                return MendNode;
            }
        }

        public void SetHeuristic(HeuristicMode iMode) {
            Mheuristic = null;
            switch (iMode) {
                case HeuristicMode.Manhattan:
                    Mheuristic = Heuristic.Manhattan;
                    break;
                case HeuristicMode.Euclidean:
                    Mheuristic = Heuristic.Euclidean;
                    break;
                case HeuristicMode.Chebyshev:
                    Mheuristic = Heuristic.Chebyshev;
                    break;
                default:
                    Mheuristic = Heuristic.Euclidean;
                    break;
            }
        }

        protected BaseGrid MsearchGrid;
        protected Node MstartNode;
        protected Node MendNode;
        protected HeuristicDelegate Mheuristic;
    }
}
