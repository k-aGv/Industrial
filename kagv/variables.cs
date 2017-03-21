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

        //is_trapped[i,0] -> unavailable path for Start to Load
        //is_trapped[i,1] -> unavailable path for Load to End
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
        Graphics paper;//main graphics for grid
        HeuristicMode mode = HeuristicMode.MANHATTAN;
        GridBox m_lastBoxSelect;
        BoxType m_lastBoxType;
        ToolTip tp;
        Point endPointCoords = new Point(-1,-1);

        bool imported;
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
    }
}
