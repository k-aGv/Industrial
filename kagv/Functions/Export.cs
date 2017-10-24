using System.IO;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm  {

        //Function for exporting the map
        private void Export() {
            sfd_exportmap.FileName = "";
            sfd_exportmap.Filter = "kagv Map (*.kmap)|*.kmap";

            if (sfd_exportmap.ShowDialog() == DialogResult.OK) {
                StreamWriter writer = new StreamWriter(sfd_exportmap.FileName);
                writer.WriteLine(
                    "Map info:\r\n" +
                    "Width blocks: " + Globals.WidthBlocks +
                    "  Height blocks: " + Globals.HeightBlocks +
                    "  BlockSide: " + Globals.BlockSide +
                    "\r\n"
                    );
                for (var i = 0; i < Globals.WidthBlocks; i++) {
                    for (var j = 0; j < Globals.HeightBlocks; j++) {
                        writer.Write(_rectangles[i][j].BoxType + " ");
                    }
                    writer.Write("\r\n");
                }
                writer.Close();
            }

        }
    }
}
