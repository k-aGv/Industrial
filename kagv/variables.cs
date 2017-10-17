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
using kagv.DLL_source;
namespace kagv {

    public partial class MainForm {

        double _CO2 = 0, _CO = 0, _NOx = 0, _THC = 0, _globalWarming = 0;


        //Handle our custom functions
        readonly k_aGv_functions.Functions _f = new k_aGv_functions.Functions();

        //cells that represent Load can have 4 vallues:
        //available Load = 1
        //not a Load = 2
        //Marked by an AGV Load = 3
        //Temporarily trapped Load = 4
        int[,] _isLoad; 

        BoxType[,] _importmap;

        GridBox[][] _rectangles;//2d jagged array. Contains grid information (coords of each box, boxtype, etc etc)  

        int[] _onWhichStep;
        bool[] _fromstart = new bool[Globals.MaximumAGVs];

        List<Vehicle> _AGVs = new List<Vehicle>();
        List<GridPos> _startPos = new List<GridPos>(); //Contains the coords of the Start boxes
        List<GridPos> _loadPos;
        readonly bool[] _trappedStatus = new bool[5];


        int _a; //temporary X.Used to calculate the remained length of current line
        int _b; //temporary Y.Used to calculate the remained length of current line
        int _posIndex = 0;
        BaseGrid _searchGrid;
        AStarParam _jumpParam;//custom jump method with its features exposed
        static Graphics _paper;//main graphics for grid

        GridBox _lastBoxSelect;
        BoxType _lastBoxType = new BoxType();
        ToolTip _tp;
        Point _endPointCoords = new Point(-1, -1);

        bool _holdCtrl;
        bool _useHalt = false;
        bool _overImage = false;
        bool _imported;
        bool _importedImage = false;
        bool _beforeStart = true;
        bool _calibrated = false;//flag checking if current point is correctly callibrated in the middle of the rectangle
        bool _isMouseDown = false;
        bool _mapHasLoads = false;
        bool _allowHighlight = true;

        bool _alwaysCross = true;
        bool _atLeastOneObstacle = false;
        bool _ifNoObstacles = false;
        bool _never = false;

        int _loads = 0; //index for keeping count of how many Loads there are in the Grid
        int _labeled_loads; //index that is used for displaying how many loads have not been picked up

        Color selectedColor = Color.DarkGray;
        Color _boxDefaultColor = (Globals.SemiTransparency) ? Color.FromArgb(Globals.Opacity, Color.WhiteSmoke) : Color.WhiteSmoke;

        Image _importedImageFile;
        Image _importedLayout = null;


    }
}
