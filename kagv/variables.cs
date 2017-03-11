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
       

        List<GridPos> StartPos = new List<GridPos>(); //Contains the coords of the Start boxes
        bool[,] is_trapped;
        int[] timer_counter;// = 0;
        public static int width = 64;//blocks of grid
        public static int height = 32; //32
        
        int a; //temporary X.Used to calculate the remained length of current line
        int b; //temporary Y.Used to calculate the remained length of current line
        int topBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        int bottomBarOffset = 50 + 10;//distance between grid and the bottom of the form
        
        BaseGrid searchGrid;
        JumpPointParam jumpParam;//custom jump method with its features exposed
        Graphics paper;//main graphics for grid
        GridBox[][] m_rectangles;//2d array. Contains grid information (coords of each box, boxtype, etc etc)  

        bool useRecursive = true;
        bool crossAdjacent = false;
        bool crossCorners = false;

        HeuristicMode mode = HeuristicMode.MANHATTAN;

        List<GridLine> load_line = new List<GridLine>();

        GridBox m_lastBoxSelect;
        BoxType m_lastBoxType;
        ToolTip tp;
        Point[] markedbyagv; //contains the relative coords of the marked loads (example: markedbyagv[0] has the coords that the 1st agv has marked)

        GridLine[,] AGVspath = new GridLine[2000, 5];

        int pos_index = 0;//index of GridPos[] pos array
        List<List<GridPos>> AllJumpPointsList = new List<List<GridPos>>();//Contains all the JumpPoints that are needed for the paths to be calculated & drawn
        List<GridPos> JumpPointsList = new List<GridPos>();
        bool NoJumpPointsFound;//confirms whether the list_of_lists is empty or not

        //current load x/y for 5 agv's currently targetting/carrying loads

        //0 dimension=x
        //1 dimension=y
        double[, ,] newsteps = new double[5, 2, 2000];
        int[] new_steps_counter = new int[5];

        //Vehicle myagv;
        Vehicle[] AGVs = new Vehicle[5];
        Point endPointCoords = new Point(-1,-1);

        //import stuff
        BoxType[,] importmap;
        bool imported;
        bool[] fromstart = new bool[5];
        bool beforeStart = true;
        bool calibrated = false;//flag checking if current point is correctly callibrated in the middle of the rectangle
        bool isMouseDown = false;

        //loads
        bool mapHasLoads = false;
        int[,] isLoad = new int[width, height];
        int loads = 0;

        bool showLine = true;
        bool showDots = true;
        bool showSteps = true;
        bool allowHighlight = true;

        Color selectedColor=Color.DarkGray;
        Color boxDefaultColor = Color.WhiteSmoke;
    }
}
