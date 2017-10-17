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
        public const int MaximumSteps = 2000;
        public const int TopBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        public const int BottomBarOffset = 50 +20;//distance between grid and the bottom of the form +20 for bottom toolstrip
        public const int LeftBarOffset = 151; //150 is the treeview's width. +1 for border
        public const int MaximumAGVs = 5;
        public const int GbMonitorWidth = 275;
        public const int GbMonitorHeight = 65;
        public const bool SemiTransparency = true;


        //Grid's stats
        public static int TimerInterval =50; //the actual timer interval in ms
        public static int TimerStep = 50; //the step of increase/decrease the interval

        public static int WidthBlocks; //grid blocks
        public static int HeightBlocks; //grid blocks
        public static int BlockSide = 15;
        public static byte Opacity = (byte) ( (BitConverter.GetBytes(Color.WhiteSmoke.ToArgb()).Reverse().ToArray())[0] - (100) );
        public static Color SemiTransparent;
        public static double AStarWeight = 0.5;

        //This value is needed since the application does not allow me to change the Debug menu
        //visibility before form shown
        public static bool Debug = true;
        public static bool FirstFormLoad = true;
    }
}
