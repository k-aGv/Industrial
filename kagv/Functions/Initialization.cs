using kagv.DLL_source;
using System;
using System.IO;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        //Initializes all the objects in main_form
        private void Initialization() {

            DiagonalMovement diagonalMovement=DiagonalMovement.Always;
            HeuristicMode heuristicMode = HeuristicMode.Manhattan;
            char[] delim = { ':',' ','(' };
            if (File.Exists("info.txt"))
            {
                if (Globals.FirstFormLoad) {
                    StreamReader reader = new StreamReader("info.txt");
                    try {
                        Globals.WidthBlocks = Convert.ToInt32(reader.ReadLine().Split(delim)[1]);
                        Globals.HeightBlocks = Convert.ToInt32(reader.ReadLine().Split(delim)[1]);
                        Globals.BlockSide = Convert.ToInt32(reader.ReadLine().Split(delim)[1]);
                        diagonalMovement = (DiagonalMovement)Enum.Parse(typeof(DiagonalMovement), reader.ReadLine().Split(delim)[1]);
                        heuristicMode = (HeuristicMode)Enum.Parse(typeof(HeuristicMode), reader.ReadLine().Split(delim)[2]);
                        stepsToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                        linesToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                        dotsToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                        bordersToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                        highlightOverCurrentBoxToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                        aGVIndexToolStripMenuItem.Checked = Convert.ToBoolean(reader.ReadLine().Split(delim)[1]);
                    } catch {
                        MessageBox.Show("An error has occured while parsing the file to initialize form.\nPlease delete the file.");
                    }
                    reader.Close();
                }
                Globals.FirstFormLoad = false;
            }

            

            _isLoad = new int[Globals.WidthBlocks, Globals.HeightBlocks];
            //m_rectangels is an array of two 1d arrays
            //declares the length of the first 1d array
            _rectangles = new GridBox[Globals.WidthBlocks][];


            for (var widthTrav = 0; widthTrav < Globals.WidthBlocks; widthTrav++) {
                //declares the length of the seconds 1d array
                _rectangles[widthTrav] = new GridBox[Globals.HeightBlocks];
                for (var heightTrav = 0; heightTrav < Globals.HeightBlocks; heightTrav++) {

                    //dynamically add the gridboxes into the _rectangles.
                    //size of the m_rectangels is constantly increasing (while adding
                    //the gridbox values) until size=height or size = width.
                    if (_imported) { //this IF is executed as long as the user has imported a map of his choice
                        _rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals.BlockSide) + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, _importmap[widthTrav, heightTrav]);
                        if (_importmap[widthTrav, heightTrav] == BoxType.Load) {
                            _isLoad[widthTrav, heightTrav] = 1;
                            _loads++;
                        }
                    } else {
                        _rectangles[widthTrav][heightTrav] = new GridBox((widthTrav * Globals.BlockSide) + Globals.LeftBarOffset, heightTrav * Globals.BlockSide + Globals.TopBarOffset, BoxType.Normal);
                        _isLoad[widthTrav, heightTrav] = 2;
                    }


                }
            }
            if (_imported)
                _imported = false;


            _searchGrid = new StaticGrid(Globals.WidthBlocks, Globals.HeightBlocks);
            _jumpParam = new AStarParam (
                _searchGrid,
                Convert.ToSingle(Globals.AStarWeight),
                diagonalMovement,
                heuristicMode
                );
            
            ConfigUi();
        }
    }
}
