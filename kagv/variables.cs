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
       

        List<GridPos> pos = new List<GridPos>(); //public declaration / partial redeclaration occurs in Reset(with overload)
        bool[,] is_trapped;
        string log;
        int resultCount;
        int[] timer_counter;// = 0;
        public static int width = 64;//blocks of grid
        public static int height = 32; //32
        int formHeight;//form's height
        int formWidth;//form's width
        //int steps_counter = 0;//amount of steps done
        //int[] new_steps_counter;//new - not used yet - will be needed for when we get to animate the move of more vehicles
        int[] new_steps_counter = new int[5];
        int distanceBlocks;//the quantity of blocks,matching the current line's length
        int a; //temporary X.Used to calculate the remained length of current line
        int b; //temporary Y.Used to calculate the remained length of current line
        int topBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        int bottomBarOffset = 50 + 10;//distance between grid and the bottom of the form

        //Used to find the final point
        Font stepFont = new Font("Tahoma", 8, FontStyle.Bold);//Font used for numbering the steps/current block
        Point[] currentLinePoints;//1d array of points.used to track all the points of current line
        BaseGrid searchGrid;
        JumpPointParam jumpParam;//custom jump method.we disabled all features hohoho
        Graphics paper;//main graphics for grid etc.
        GridBox[][] m_rectangles;//2d array.contains ALL coords    

        bool useRecursive = true;
        bool crossAdjacent = false;
        bool crossCorners = false;
        HeuristicMode mode = HeuristicMode.MANHATTAN;

        List<GridLine> load_line = new List<GridLine>();

        SolidBrush br;
        SolidBrush fontBR;

        GridBox m_lastBoxSelect;
        BoxType m_lastBoxType;
        ToolTip tp;
        Point[] markedbyagv; //contains the relative coords of the marked loads (example: markedbyagv[0] has the coords that the 1st agv has marked)

        GridLine[,] myLines = new GridLine[2000, 5];

        int pos_index = 0;//index of GridPos[] pos array
        List<List<GridPos>> myresultList = new List<List<GridPos>>();//new -> List of Lists (let's say Parent List)
        List<GridPos> resultList = new List<GridPos>();
        bool lol_empty;//confirms whether the list_of_lists is empty or not

        //current load x/y for 5 agv's currently targetting/carrying loads

        //0 dimension=x
        //1 dimension=y
        double[, ,] newsteps = new double[5, 2, 2000];

        //Vehicle myagv;
        Vehicle[] AGVs = new Vehicle[5];
        Point endPointCoords = new Point();

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

    }
}
