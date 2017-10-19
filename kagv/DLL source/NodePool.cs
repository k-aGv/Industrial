/*! 
@file NodePool.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief NodePool Interface
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
    public class NodePool {
        protected Dictionary<GridPos, Node> Mnodes;

        public NodePool() {
            Mnodes = new Dictionary<GridPos, Node>();
        }

        public Dictionary<GridPos, Node> Nodes
        {
            get => Mnodes;
        }
        public Node GetNode(int iX, int iY) {
            GridPos pos = new GridPos(iX, iY);
            return GetNode(pos);
        }

        public Node GetNode(GridPos iPos) {
            Mnodes.TryGetValue(iPos, out var retVal);
            return retVal;
        }

        public Node SetNode(int iX, int iY, bool? iWalkable = null) {
            GridPos pos = new GridPos(iX, iY);
            return SetNode(pos, iWalkable);
        }

        public Node SetNode(GridPos iPos, bool? iWalkable = null) {
            if (iWalkable.HasValue) {
                if (iWalkable.Value) {
                    if (Mnodes.TryGetValue(iPos, out var retVal)) {
                        return retVal;
                    }
                    Node newNode = new Node(iPos.X, iPos.Y, iWalkable);
                    Mnodes.Add(iPos, newNode);
                    return newNode;
                } 
                RemoveNode(iPos);
                

            } else {
                Node newNode = new Node(iPos.X, iPos.Y, true);
                Mnodes.Add(iPos, newNode);
                return newNode;
            }
            return null;
        }
        protected void RemoveNode(int iX, int iY) {
            GridPos pos = new GridPos(iX, iY);
            RemoveNode(pos);
        }
        protected void RemoveNode(GridPos iPos) {
            if (Mnodes.ContainsKey(iPos))
                Mnodes.Remove(iPos);
        }
    }
}