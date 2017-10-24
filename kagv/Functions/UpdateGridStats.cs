using System;

namespace kagv {

    public partial class MainForm {

        private void UpdateGridStats() {
            var pixelsWidth = Globals.WidthBlocks * Globals.BlockSide;
            var pixelsHeight = Globals.HeightBlocks * Globals.BlockSide;
            lb_width.Text = "Width blocks: " + Globals.WidthBlocks + ".  " + pixelsWidth + " pixels";
            lb_height.Text = "Height blocks: " + Globals.HeightBlocks + ". " + pixelsHeight + " pixels";
            nud_side.Value = Convert.ToDecimal(Globals.BlockSide);
        }
    }
}
