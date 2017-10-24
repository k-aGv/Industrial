using System.Drawing;

namespace kagv {

    public partial class MainForm  {

        //Function that validates the user's click 
        private bool Isvalid(Point temp) {

            //The function received the coordinates of the user's click.
            //Clicking anywhere but on the Grid itself, will cause a "false" return, preventing
            //the click from giving any results

            if (temp.Y < menuPanel.Location.Y)
                return false;

            if (temp.X > _rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].BoxRec.X + (Globals.BlockSide - 1) + Globals.LeftBarOffset
            || temp.Y > _rectangles[Globals.WidthBlocks - 1][Globals.HeightBlocks - 1].BoxRec.Y + (Globals.BlockSide - 1)) // 18 because its 20-boarder size
                return false;

            if (!_rectangles[(temp.X - Globals.LeftBarOffset) / Globals.BlockSide][(temp.Y - Globals.TopBarOffset) / Globals.BlockSide].BoxRec.Contains(temp))
                return false;

            return true;
        }
    }
}
