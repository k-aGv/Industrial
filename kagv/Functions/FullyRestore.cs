using kagv.DLL_source;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace kagv {

    public partial class MainForm {

        //function that resets all of the used objects so they are ready for reuse, preventing memory leaks
        private void FullyRestore() {


            if (tree_stats.Nodes[1].IsExpanded)
                tree_stats.Nodes[1].Collapse();

            tree_stats.Nodes[1].Nodes[0].Text = "CO: -";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: -";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: -";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: -";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: -";

            _labeled_loads = 0;

            if (_onWhichStep != null)
                Array.Clear(_onWhichStep, 0, _onWhichStep.GetLength(0));

            if (_trappedStatus != null)
                Array.Clear(_trappedStatus, 0, _trappedStatus.GetLength(0));


            for (short i = 0; i < _AGVs.Count; i++)
                _AGVs[i].KillIcon();


            if (_importmap != null) {
                Array.Clear(_importmap, 0, _importmap.GetLength(0));
                Array.Clear(_importmap, 0, _importmap.GetLength(1));
            }

            if (BackgroundImage != null)
                BackgroundImage = null;

            _fromstart = new bool[Globals.MaximumAGVs];


            _startPos = new List<GridPos>();
            _endPointCoords = new Point(-1, -1);
            _selectedColor = Color.DarkGray;

            for (short i = 0; i < _startPos.Count(); i++)
                _AGVs[i].JumpPoints = new List<GridPos>();


            _searchGrid = new StaticGrid(Globals.WidthBlocks, Globals.HeightBlocks);

            _alwaysCross =
            aGVIndexToolStripMenuItem.Checked =
            _beforeStart =
            _allowHighlight = true;

            _atLeastOneObstacle =
            _ifNoObstacles =
            _never =
            _imported =
            _calibrated =
            _isMouseDown =
            _mapHasLoads = false;

            _useHalt = false;
            priorityRulesbetaToolStripMenuItem.Checked = false;

            _importedLayout = null;
            _jumpParam = null;
            _paper = null;
            _loads = _posIndex = 0;

            _a
            = _b
            = new int();


            _AGVs = new List<Vehicle>();
            _CO2 = _CO = _NOx = _THC = _globalWarming = 0;

            _allowHighlight = true;
            highlightOverCurrentBoxToolStripMenuItem.Enabled = true;
            highlightOverCurrentBoxToolStripMenuItem.Checked = true;



            _isLoad = new int[Globals.WidthBlocks, Globals.HeightBlocks];
            _rectangles = new GridBox[Globals.WidthBlocks][];
            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++)
                _rectangles[widthTrav] = new GridBox[Globals.HeightBlocks];

            //jagged array has to be resetted like this
            for (var i = 0; i < Globals.WidthBlocks; i++)
                for (var j = 0; j < Globals.HeightBlocks; j++)
                    _rectangles[i][j] = new GridBox(i * Globals.BlockSide, j * Globals.BlockSide + Globals.TopBarOffset, BoxType.Normal);


            Initialization();

            main_form_Load(new object(), new EventArgs());

            for (short i = 0; i < _AGVs.Count; i++)
                _AGVs[i].Status.Busy = false;

            Globals.TimerStep = 0;
            timer0.Interval = timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = Globals.TimerInterval;


            nUD_AGVs.Value = _AGVs.Count;
            tree_stats.Nodes[2].Text = "Loads remaining: ";


        }
    }
}
