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

        private double _CO2 , _CO , _NOx , _THC, _globalWarming ;//default value =0 


        //Handle our custom functions
        private readonly k_aGv_functions.Functions _f = new k_aGv_functions.Functions();

        //cells that represent Load can have 4 vallues:
        //available Load = 1
        //not a Load = 2
        //Marked by an AGV Load = 3
        //Temporarily trapped Load = 4
        private int[,] _isLoad;

        private BoxType[,] _importmap;

        private GridBox[][] _rectangles;//2d jagged array. Contains grid information (coords of each box, boxtype, etc etc)  

        private int[] _onWhichStep;
        private bool[] _fromstart = new bool[Globals.MaximumAGVs];

        private List<Vehicle> _AGVs = new List<Vehicle>();
        private List<GridPos> _startPos = new List<GridPos>(); //Contains the coords of the Start boxes
        private List<GridPos> _loadPos;
        private readonly bool[] _trappedStatus = new bool[5];


        private int _a; //temporary X.Used to calculate the remained length of current line
        private int _b; //temporary Y.Used to calculate the remained length of current line
        private int _posIndex; //=0 s the default value anyway
        private BaseGrid _searchGrid;
        private AStarParam _jumpParam;//custom jump method with its features exposed
        private static Graphics _paper;//main graphics for grid

        private GridBox _lastBoxSelect;
        private BoxType _lastBoxType;
        private ToolTip _tp;
        private Point _endPointCoords = new Point(-1, -1);

        private bool _holdCtrl;
        private bool _useHalt;
        private bool _overImage;
        private bool _imported;
        private bool _beforeStart = true;
        private bool _calibrated ;//flag checking if current point is correctly callibrated in the middle of the rectangle
        private bool _isMouseDown;
        private bool _mapHasLoads;
        private bool _allowHighlight = true;

        private bool _alwaysCross = true;
        private bool _atLeastOneObstacle ;
        private bool _ifNoObstacles ;
        private bool _never ;

        private int _loads ; //default=0 anyways...index for keeping count of how many Loads there are in the Grid
        private int _labeled_loads; //index that is used for displaying how many loads have not been picked up

        private Color _selectedColor = Color.DarkGray;
        private Color _boxDefaultColor = (Globals.SemiTransparency) ? Color.FromArgb(Globals.Opacity, Color.WhiteSmoke) : Color.WhiteSmoke;

        private Image _importedImageFile;
        private Image _importedLayout = null;


    }
}
