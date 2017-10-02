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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kagv {

    public partial class main_form {

        emissions emissions = new emissions();
        double CO2 = 0, CO = 0, NOx = 0, THC = 0, GlobalWarming = 0;


        //Handle our custom functions
        k_aGv_functions.Functions __f = new k_aGv_functions.Functions();

        //cells that represent Load can have 4 vallues:
        //available Load = 1
        //not a Load = 2
        //Marked by an AGV Load = 3
        //Temporarily trapped Load = 4
        int[,] isLoad = new int[Constants._WidthBlocks, Constants._HeightBlocks];

        BoxType[,] importmap;

        GridBox[][] m_rectangles;//2d jagged array. Contains grid information (coords of each box, boxtype, etc etc)  

        int[] timer_counter;
        bool[] fromstart = new bool[Constants._MaximumAGVs];

        List<Vehicle> AGVs = new List<Vehicle>();
        List<GridPos> startPos = new List<GridPos>(); //Contains the coords of the Start boxes
        List<GridPos> loadPos;
        bool[] trappedStatus = new bool[5];


        int a; //temporary X.Used to calculate the remained length of current line
        int b; //temporary Y.Used to calculate the remained length of current line
        int pos_index = 0;
        BaseGrid searchGrid;
        AStarParam jumpParam;//custom jump method with its features exposed
        static Graphics paper;//main graphics for grid

        GridBox m_lastBoxSelect;
        BoxType m_lastBoxType = new BoxType();
        ToolTip tp;
        Point endPointCoords = new Point(-1, -1);


        bool use_Halt = false;
        bool overImage = false;
        bool imported;
        bool importedImage = false;
        bool beforeStart = true;
        bool calibrated = false;//flag checking if current point is correctly callibrated in the middle of the rectangle
        bool isMouseDown = false;
        bool mapHasLoads = false;
        bool allowHighlight = true;

        bool alwaysCross = true;
        bool atLeastOneObstacle = false;
        bool ifNoObstacles = false;
        bool never = false;

        int loads = 0; //index for keeping count of how many Loads there are in the Grid
        int labeled_loads; //index that is used for displaying how many loads have not been picked up

        Color selectedColor = Color.DarkGray;
        Color boxDefaultColor = (Constants._SemiTransparency) ? Color.FromArgb(Constants._Opacity, Color.WhiteSmoke) : Color.WhiteSmoke;

        Image importedImageFile;
        Image importedLayout = null;

      
    }
}
