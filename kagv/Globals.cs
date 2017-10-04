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
using System.Linq;
using System.Drawing;

namespace kagv {
    public static class Globals {
        public const int _MaximumSteps = 2000;
        public const int _TopBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        public const int _BottomBarOffset = 50 +20;//distance between grid and the bottom of the form +20 for bottom toolstrip
        public const int _LeftBarOffset = 151; //150 is the treeview's width. +1 for border
        public const int _MaximumAGVs = 5;
        public const int _gb_monitor_width = 275;
        public const int _gb_monitor_height = 65;
        public const bool _SemiTransparency = true;

        

        //Grid's stats
        public static int _WidthBlocks = 70; //grid blocks
        public static int _HeightBlocks = 44; //grid blocks
        public static int _BlockSide = 15;
        public static byte _Opacity = (byte) ( (BitConverter.GetBytes(Color.WhiteSmoke.ToArgb()).Reverse().ToArray())[0] - (100) );
        public static Color _SemiTransparent;
        public static double _AStarWeight = 0.5;

        //This value is needed since the application does not allow me to change the Debug menu
        //visibility before form shown
        public static bool _Debug = true;
        public static bool _FirstFormLoad = true;
    }
}
