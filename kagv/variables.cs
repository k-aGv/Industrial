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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace kagv
{
    
    public partial class main_form
    {

        emissions emissions = new emissions();
        double CO2 = 0, CO = 0, NOx = 0, THC = 0, GlobalWarming = 0;


        //Handle our custom functions
        kagvFunctions.kFunctions __f = new kagvFunctions.kFunctions();

        
        bool[,] is_trapped;
        int[,] isLoad = new int[Constants.__WidthBlocks, Constants.__HeightBlocks];
        BoxType[,] importmap;

        GridBox[][] m_rectangles;//2d jagged array. Contains grid information (coords of each box, boxtype, etc etc)  

        int[] timer_counter;
        bool[] fromstart = new bool[Constants.__MaximumAGVs];
        Vehicle[] AGVs = new Vehicle[Constants.__MaximumAGVs];

        List<GridPos> JumpPointsList = new List<GridPos>();
        List<GridPos> StartPos = new List<GridPos>(); //Contains the coords of the Start boxes

        int a; //temporary X.Used to calculate the remained length of current line
        int b; //temporary Y.Used to calculate the remained length of current line
        int pos_index = 0;
        BaseGrid searchGrid;
        JumpPointParam jumpParam;//custom jump method with its features exposed
        static Graphics paper;//main graphics for grid
        HeuristicMode mode = HeuristicMode.MANHATTAN;
        GridBox m_lastBoxSelect;
        BoxType m_lastBoxType = new BoxType();
        ToolTip tp;
        Point endPointCoords = new Point(-1,-1);

        bool imported;
        bool importedImage = false;
        bool beforeStart = true;
        bool calibrated = false;//flag checking if current point is correctly callibrated in the middle of the rectangle
        bool isMouseDown = false;
        bool NoJumpPointsFound;
        bool mapHasLoads = false;      
        bool allowHighlight = true;
        bool useRecursive = false;
        bool crossAdjacent = false;
        bool crossCorners = true;
        int loads = 0;

        Color selectedColor=Color.DarkGray;
        Color boxDefaultColor = Color.WhiteSmoke;



        int reflectedBlock;
        int reflectedWidth;
        int reflectedHeight;
        bool reflected = false;
    }
}
