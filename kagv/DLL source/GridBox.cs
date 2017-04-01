/*!
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kagv {
    enum BoxType { Start, End, Wall, Normal, Load };

    class GridBox : IDisposable {
        public int x, y, width, height;
        public Rectangle boxRec;
        public BoxType boxType;

        private SolidBrush brush;
        private Color myBrown = Color.FromArgb(138, 109, 86);
        private Graphics graphs;
        public GridBox(int iX, int iY, BoxType iType) {
            this.x = iX;
            this.y = iY;
            this.boxType = iType;
            switch (iType) {
                case BoxType.Normal:
                    brush = new SolidBrush(Color.WhiteSmoke);
                    break;
                case BoxType.End:
                    brush = new SolidBrush(Color.Red);
                    break;
                case BoxType.Start:
                    brush = new SolidBrush(Color.Green);
                    break;
                case BoxType.Wall:
                    brush = new SolidBrush(Color.Gray);
                    break;
                case BoxType.Load:
                    brush = new SolidBrush(myBrown);
                    break;

            }
            width = Constants.__BlockSide - 1;
            height = Constants.__BlockSide - 1;
            boxRec = new Rectangle(x, y, width, height);
        }

        public void DrawBox(Graphics iPaper, BoxType iType) {
            if (iType == boxType) {
                boxRec.X = x;
                boxRec.Y = y;
                iPaper.FillRectangle(brush, boxRec);
                graphs = iPaper;
            }
        }

        public void onHover(Color c) {
            this.brush = new SolidBrush(c);
        }
        public void BeTransparent() {
            switch (this.boxType) {
                case BoxType.Normal:
                    this.brush = new SolidBrush(Color.Transparent);
                    break;
            }
        }

        public void BeVisible() {
            switch (this.boxType) {
                case BoxType.Normal:
                    this.brush = new SolidBrush(Color.WhiteSmoke);
                    break;
            }
        }

        public void SwitchBox() {
            switch (this.boxType) {
                case BoxType.Normal:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(Color.Gray);
                    this.boxType = BoxType.Wall;
                    break;
                case BoxType.Wall:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(Color.WhiteSmoke);
                    this.boxType = BoxType.Normal;
                    break;

            }
        }
        public void SetAsTargetted(Graphics iPaper) {
            iPaper.FillRectangle(new SolidBrush(Color.Orange), boxRec);
        }


        public void SwitchEnd_StartToNormal() {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.WhiteSmoke);
            this.boxType = BoxType.Normal;

        }

        public void SwitchLoad() {
            switch (this.boxType) {
                case BoxType.Normal:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(myBrown);
                    this.boxType = BoxType.Load;
                    break;
                case BoxType.Load:
                    if (this.brush != null)
                        this.brush.Dispose();
#if industrial
                    this.brush = new SolidBrush(Color.WhiteSmoke);
#else
                    this.brush = new SolidBrush(Color.Transparent);
#endif
                    this.boxType = BoxType.Normal;
                    break;

            }
        }


        public void SetNormalBox() {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.WhiteSmoke);
            this.boxType = BoxType.Normal;
        }

        public void SetEndBox() {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.Red);
            this.boxType = BoxType.End;
        }


        public void Dispose() {
            if (this.brush != null)
                this.brush.Dispose();

        }
    }
}
