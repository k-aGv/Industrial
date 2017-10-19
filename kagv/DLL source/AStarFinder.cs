using System.Threading.Tasks;
using C5;
using System;
using System.Collections.Generic;

namespace kagv.DLL_source {
    public class AStarParam : ParamBase {
        public delegate float HeuristicDelegate(int iDx, int iDy);


        public float Weight;

        public AStarParam(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, float iweight, DiagonalMovement iDiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iStartPos, iEndPos, iDiagonalMovement, iMode) {
            Weight = iweight;
        }

        public AStarParam(BaseGrid iGrid, float iweight, DiagonalMovement iDiagonalMovement = DiagonalMovement.IfAtLeastOneWalkable, HeuristicMode iMode = HeuristicMode.Euclidean)
            : base(iGrid, iDiagonalMovement, iMode) {
            Weight = iweight;
        }

        internal override void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null) {

        }
    }
    public static class AStarFinder {


        public static List<GridPos> FindPath(AStarParam iParam, decimal iWeight) {
            return FindPath(iParam, Convert.ToDouble(iWeight));
        }

        public static List<GridPos> FindPath(AStarParam iParam, double iWeight) {
            object lo = new object();
            //var openList = new IntervalHeap<Node>(new NodeComparer());
            var openList = new IntervalHeap<Node>();
            var startNode = iParam.StartNode;
            var endNode = iParam.EndNode;
            var heuristic = iParam.HeuristicFunc;
            var grid = iParam.SearchGrid;
            var diagonalMovement = iParam.DiagonalMovement;
            // var weight = iParam.Weight;
            var weight = iWeight;

            startNode.StartToCurNodeLen = 0;
            startNode.HeuristicStartToEndLen = 0;

            openList.Add(startNode);
            startNode.IsOpened = true;

            while (openList.Count != 0) {
                var node = openList.DeleteMin();
                node.IsClosed = true;

                if (node == endNode) 
                    return Node.Backtrace(endNode);
                
                var neighbors = grid.GetNeighbors(node, diagonalMovement);


                Parallel.ForEach(neighbors, neighbor =>
                {

                    if (neighbor.IsClosed) return;
                    var x = neighbor.X;
                    var y = neighbor.Y;
                    float ng = node.StartToCurNodeLen + (float)((x - node.X == 0 || y - node.Y == 0) ? 1 : Math.Sqrt(2));

                    if (!neighbor.IsOpened || ng < neighbor.StartToCurNodeLen) {
                        neighbor.StartToCurNodeLen = ng;
                        if (neighbor.HeuristicCurNodeToEndLen == null)
                            neighbor.HeuristicCurNodeToEndLen = Convert.ToSingle(weight) * heuristic(Math.Abs(x - endNode.X), Math.Abs(y - endNode.Y));
                        if (neighbor.HeuristicCurNodeToEndLen != null)
                            neighbor.HeuristicStartToEndLen =
                                neighbor.StartToCurNodeLen + neighbor.HeuristicCurNodeToEndLen.Value;
                        neighbor.Parent = node;
                        if (!neighbor.IsOpened) {
                            lock (lo) {
                                openList.Add(neighbor);
                            }
                            neighbor.IsOpened = true;
                        } 
                    }
                });

            }
            return new List<GridPos>();

        }
    }
}
