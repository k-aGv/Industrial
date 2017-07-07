/*!
The MIT License (MIT)

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
    public static class Constants {
        public const int __MaximumSteps = 2000;
        public const int __TopBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        public const int __BottomBarOffset = 50 + 10;//distance between grid and the bottom of the form
        public static int __MaximumAGVs = 5;

        //Grid's stats
        public static int __WidthBlocks = 64; //grid blocks
        public static int __HeightBlocks = 32; //grid blocks
        public static int __BlockSide = 20;

        public static int __ResolutionMultiplier = 1;

        public static bool __SemiTransparency = true;
        public static byte __Opacity = (byte) ( (BitConverter.GetBytes(Color.WhiteSmoke.ToArgb()).Reverse().ToArray())[0] - (100) );
        public static Color __SemiTransparent;
    }
}
