/*! 
@file GridLine.cs
@author Woong Gyu La a.k.a Chris. <juhgiyo@gmail.com>
		<http://github.com/juhgiyo/eppathfinding.cs>
@date July 16, 2013
@brief GridLine Interface
@version 2.0

@section LICENSE

The MIT License (MIT)

Copyright (c) 2013 Woong Gyu La <juhgiyo@gmail.com>

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

@section DESCRIPTION

An Interface for the GridLine Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kagv
{
    class GridLine
    {
        public int fromX, fromY, toX, toY;
        public Pen pen;
        
        public GridLine(GridBox iFrom, GridBox iTo)
        {
            this.fromX = iFrom.boxRec.X + ((Constants.__BlockSide / 2) - 1);
            this.fromY = iFrom.boxRec.Y + ((Constants.__BlockSide / 2) - 1);
            this.toX = iTo.boxRec.X + ((Constants.__BlockSide / 2) - 1);
            this.toY = iTo.boxRec.Y + ((Constants.__BlockSide / 2) - 1);
            pen = new Pen(Color.BlueViolet);
            pen.Width = 1;
            
            
        }

        public void drawLine(Graphics iPaper)
        {
            iPaper.DrawLine(pen, fromX, fromY, toX, toY);
            
        }


        public void Dispose()
        {
            if (this.pen != null)
                this.pen.Dispose();

        }
    }
}
