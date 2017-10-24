using kagv.DLL_source;
using System;
using System.IO;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        //Function for importing a map 
        private void Import() {

            ofd_importmap.Filter = "kagv Map (*.kmap)|*.kmap";
            ofd_importmap.FileName = "";


            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                bool proceed = false;
                string line = "";
                char[] sep = { ':', ' ' };

                StreamReader reader = new StreamReader(ofd_importmap.FileName);
                do {
                    line = reader.ReadLine();
                    if (line.Contains("Width blocks:") && line.Contains("Height blocks:") && line.Contains("BlockSide:"))
                        proceed = true;
                } while (!(line.Contains("Width blocks:") && line.Contains("Height blocks:") && line.Contains("BlockSide:")) &&
                         !reader.EndOfStream);
                string[] lineArray = line.Split(sep);


                if (proceed) {

                    Globals.WidthBlocks = Convert.ToInt32(lineArray[3]);
                    Globals.HeightBlocks = Convert.ToInt32(lineArray[8]);
                    Globals.BlockSide = Convert.ToInt32(lineArray[12]);

                    FullyRestore();


                    char[] delim = { ' ' };
                    reader.ReadLine();
                    _importmap = new BoxType[Globals.WidthBlocks, Globals.HeightBlocks];
                    string[] words = reader.ReadLine().Split(delim);

                    int startsCounter = 0;
                    for (int z = 0; z < _importmap.GetLength(0); z++) {
                        int i = 0;
                        foreach (string s in words)
                            if (i < _importmap.GetLength(1)) {
                                if (s == "Start") {
                                    _importmap[z, i] = BoxType.Start;
                                    startsCounter++;
                                } else if (s == "End")
                                    _importmap[z, i] = BoxType.End;
                                else if (s == "Normal")
                                    _importmap[z, i] = BoxType.Normal;
                                else if (s == "Wall")
                                    _importmap[z, i] = BoxType.Wall;
                                else if (s == "Load")
                                    _importmap[z, i] = BoxType.Load;
                                i++;
                            }
                        if (z == _importmap.GetLength(0) - 1) { } else
                            words = reader.ReadLine().Split(delim);
                    }
                    reader.Close();

                    nUD_AGVs.Value = startsCounter;
                    _imported = true;
                    Initialization();
                    Redraw();
                    if (_overImage) {
                        _importedLayout = _importedImageFile;
                        _overImage = false;
                    }
                } else
                    MessageBox.Show(this, "You have chosen an incompatible file import.\r\nPlease try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
