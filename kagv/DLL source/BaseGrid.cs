/*! 
@file BaseGrid.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief BaseGrid Interface
@version 2.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>
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
using System;
using System.Collections.Generic;

namespace kagv.DLL_source {
    public class Node : IComparable<Node> {
        public readonly int X;
        public readonly int Y;
        public bool Walkable;
        public float HeuristicStartToEndLen; // which passes current node
        public float StartToCurNodeLen;
        public float? HeuristicCurNodeToEndLen;
        public bool IsOpened;
        public bool IsClosed;
        public Object Parent;

        public Node(int iX, int iY, bool? iWalkable = null) {
            X = iX;
            Y = iY;
            Walkable = iWalkable.HasValue && iWalkable.Value ;
            HeuristicStartToEndLen = 0;
            StartToCurNodeLen = 0;
            // this must be initialized as null to verify that its value never initialized
            // 0 is not good candidate!!
            HeuristicCurNodeToEndLen = null;
            IsOpened = false;
            IsClosed = false;
            Parent = null;

        }

        public Node(Node b) {
            X = b.X;
            Y = b.Y;
            Walkable = b.Walkable;
            HeuristicStartToEndLen = b.HeuristicStartToEndLen;
            StartToCurNodeLen = b.StartToCurNodeLen;
            HeuristicCurNodeToEndLen = b.HeuristicCurNodeToEndLen;
            IsOpened = b.IsOpened;
            IsClosed = b.IsClosed;
            Parent = b.Parent;
        }

        public void Reset(bool? iWalkable = null) {
            if (iWalkable.HasValue)
                Walkable = iWalkable.Value;
            HeuristicStartToEndLen = 0;
            StartToCurNodeLen = 0;
            // this must be initialized as null to verify that its value never initialized
            // 0 is not good candidate!!
            HeuristicCurNodeToEndLen = null;
            IsOpened = false;
            IsClosed = false;
            Parent = null;
        }

        public int CompareTo(Node iObj) {
            float result = HeuristicStartToEndLen - iObj.HeuristicStartToEndLen;
            if (result > 0.0f)
                return 1;
            else if (result == 0.0f)
                return 0;
            return -1;
        }


        public static List<GridPos> Backtrace(Node iNode) {
            List<GridPos> path = new List<GridPos> {new GridPos(iNode.X, iNode.Y)};
            while (iNode.Parent != null) {
                iNode = (Node)iNode.Parent;
                path.Add(new GridPos(iNode.X, iNode.Y));
            }
            path.Reverse();
            return path;
        }


        public override int GetHashCode() {
            return X ^ Y;
        }

        public override bool Equals(Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Node p = obj as Node;
            if ((Object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (X == p.X) && (Y == p.Y);
        }

        public bool Equals(Node p) {
            // If parameter is null return false:
            if ((object)p == null) {
                return false;
            }

            // Return true if the fields match:
            return (X == p.X) && (Y == p.Y);
        }

        public static bool operator ==(Node a, Node b) {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) {
                return false;
            }

            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Node a, Node b) {
            return !(a == b);
        }

    }

    public abstract class BaseGrid {

        protected BaseGrid() {
            MGridRect = new GridRect();
        }

        protected BaseGrid(BaseGrid b) {
            MGridRect = new GridRect(b.MGridRect);
            Width = b.Width;
            Height = b.Height;
        }

        protected GridRect MGridRect;

        public abstract int Width { get; protected set; }

        public abstract int Height { get; protected set; }

        public abstract Node GetNodeAt(int iX, int iY);

        public abstract bool IsWalkableAt(int iX, int iY);

        public abstract bool SetWalkableAt(int iX, int iY, bool iWalkable);

        public abstract Node GetNodeAt(GridPos iPos);

        public abstract bool IsWalkableAt(GridPos iPos);

        public abstract bool SetWalkableAt(GridPos iPos, bool iWalkable);

        public List<Node> GetNeighbors(Node iNode, DiagonalMovement diagonalMovement) {
            int tX = iNode.X;
            int tY = iNode.Y;
            List<Node> neighbors = new List<Node>();
            bool tS0 = false, tD0 = false,
                tS1 = false, tD1 = false,
                tS2 = false, tD2 = false,
                tS3 = false, tD3 = false;

            GridPos pos = new GridPos();
            if (IsWalkableAt(pos.Set(tX, tY - 1))) {
                neighbors.Add(GetNodeAt(pos));
                tS0 = true;
            }
            if (IsWalkableAt(pos.Set(tX + 1, tY))) {
                neighbors.Add(GetNodeAt(pos));
                tS1 = true;
            }
            if (IsWalkableAt(pos.Set(tX, tY + 1))) {
                neighbors.Add(GetNodeAt(pos));
                tS2 = true;
            }
            if (IsWalkableAt(pos.Set(tX - 1, tY))) {
                neighbors.Add(GetNodeAt(pos));
                tS3 = true;
            }

            switch (diagonalMovement) {
                case DiagonalMovement.Always:
                    tD0 = true;
                    tD1 = true;
                    tD2 = true;
                    tD3 = true;
                    break;
                case DiagonalMovement.Never:
                    break;
                case DiagonalMovement.IfAtLeastOneWalkable:
                    tD0 = tS3 || tS0;
                    tD1 = tS0 || tS1;
                    tD2 = tS1 || tS2;
                    tD3 = tS2 || tS3;
                    break;
                case DiagonalMovement.OnlyWhenNoObstacles:
                    tD0 = tS3 && tS0;
                    tD1 = tS0 && tS1;
                    tD2 = tS1 && tS2;
                    tD3 = tS2 && tS3;
                    break;
            }

            if (tD0 && IsWalkableAt(pos.Set(tX - 1, tY - 1))) {
                neighbors.Add(GetNodeAt(pos));
            }
            if (tD1 && IsWalkableAt(pos.Set(tX + 1, tY - 1))) {
                neighbors.Add(GetNodeAt(pos));
            }
            if (tD2 && IsWalkableAt(pos.Set(tX + 1, tY + 1))) {
                neighbors.Add(GetNodeAt(pos));
            }
            if (tD3 && IsWalkableAt(pos.Set(tX - 1, tY + 1))) {
                neighbors.Add(GetNodeAt(pos));
            }
            return neighbors;
        }


        public abstract void Reset();

        public abstract BaseGrid Clone();

    }
}