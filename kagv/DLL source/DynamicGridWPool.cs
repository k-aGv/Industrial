/*! 
@file DynamicGridWPool.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief DynamicGrid with Pool Interface
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
using System.Collections.Generic;


namespace kagv.DLL_source {
    public class DynamicGridWPool : BaseGrid {
        private bool _mNotSet;
        private readonly NodePool _mNodePool;

        public override int Width
        {
            get {
                if (_mNotSet)
                    SetBoundingBox();
                return MGridRect.MaxX - MGridRect.MinX;
            }
            protected set {

            }
        }

        public override int Height
        {
            get {
                if (_mNotSet)
                    SetBoundingBox();
                return MGridRect.MaxY - MGridRect.MinY;
            }
            protected set {

            }
        }

        public DynamicGridWPool(NodePool iNodePool) {
            MGridRect = new GridRect()
            {
                MinX = 0,
                MinY = 0,
                MaxX = 0,
                MaxY = 0
            };
            _mNotSet = true;
            _mNodePool = iNodePool;
        }

        public DynamicGridWPool(DynamicGridWPool b)
            : base(b) {
            _mNotSet = b._mNotSet;
            _mNodePool = b._mNodePool;
        }

        public override Node GetNodeAt(int iX, int iY) {
            GridPos pos = new GridPos(iX, iY);
            return GetNodeAt(pos);
        }

        public override bool IsWalkableAt(int iX, int iY) {
            GridPos pos = new GridPos(iX, iY);
            return IsWalkableAt(pos);
        }

        private void SetBoundingBox() {
            foreach (KeyValuePair<GridPos, Node> pair in _mNodePool.Nodes) {
                if (pair.Key.X < MGridRect.MinX || _mNotSet)
                    MGridRect.MinX = pair.Key.X;
                if (pair.Key.X > MGridRect.MaxX || _mNotSet)
                    MGridRect.MaxX = pair.Key.X;
                if (pair.Key.Y < MGridRect.MinY || _mNotSet)
                    MGridRect.MinY = pair.Key.Y;
                if (pair.Key.Y > MGridRect.MaxY || _mNotSet)
                    MGridRect.MaxY = pair.Key.Y;
                _mNotSet = false;
            }
            _mNotSet = false;
        }

        public override bool SetWalkableAt(int iX, int iY, bool iWalkable) {
            GridPos pos = new GridPos(iX, iY);
            _mNodePool.SetNode(pos, iWalkable);
            if (iWalkable) {
                if (iX < MGridRect.MinX || _mNotSet)
                    MGridRect.MinX = iX;
                if (iX > MGridRect.MaxX || _mNotSet)
                    MGridRect.MaxX = iX;
                if (iY < MGridRect.MinY || _mNotSet)
                    MGridRect.MinY = iY;
                if (iY > MGridRect.MaxY || _mNotSet)
                    MGridRect.MaxY = iY;
                _mNotSet = false;
            } else {
                if (iX == MGridRect.MinX || iX == MGridRect.MaxX || iY == MGridRect.MinY || iY == MGridRect.MaxY)
                    _mNotSet = true;

            }
            return true;
        }

        public override Node GetNodeAt(GridPos iPos) {
            return _mNodePool.GetNode(iPos);
        }

        public override bool IsWalkableAt(GridPos iPos) {
            return _mNodePool.Nodes.ContainsKey(iPos);
        }

        public override bool SetWalkableAt(GridPos iPos, bool iWalkable) {
            return SetWalkableAt(iPos.X, iPos.Y, iWalkable);
        }


        public override void Reset() {
            foreach (KeyValuePair<GridPos, Node> keyValue in _mNodePool.Nodes) {
                keyValue.Value.Reset();
            }
        }

        public override BaseGrid Clone() {
            DynamicGridWPool tNewGrid = new DynamicGridWPool(_mNodePool);
            return tNewGrid;
        }
    }

}