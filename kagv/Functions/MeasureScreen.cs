using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        private void MeasureScreen() {
            Location = Screen.PrimaryScreen.Bounds.Location;

            int usableSize = Screen.PrimaryScreen.Bounds.Height - menuPanel.Height - Globals.BottomBarOffset - Globals.TopBarOffset;
            Globals.HeightBlocks = usableSize / Globals.BlockSide;

            usableSize = Screen.PrimaryScreen.Bounds.Width - tree_stats.Width - Globals.LeftBarOffset;
            Globals.WidthBlocks = usableSize / Globals.BlockSide;

        }
    }
}
