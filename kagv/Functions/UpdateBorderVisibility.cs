using kagv.DLL_source;
using System.Drawing;

namespace kagv {

    public partial class MainForm {

        //has to do with optical features in the Grid option from the menu
        private void UpdateBorderVisibility(bool hide) {
            if (hide) {
                for (var i = 0; i < Globals.WidthBlocks; i++)
                    for (var j = 0; j < Globals.HeightBlocks; j++)
                        _rectangles[i][j].BeTransparent();
                BackColor = Color.DarkGray;
            } else {
                for (var i = 0; i < Globals.WidthBlocks; i++)
                    for (var j = 0; j < Globals.HeightBlocks; j++)
                        if (_rectangles[i][j].BoxType == BoxType.Normal) {
                            _rectangles[i][j].BeVisible();

                            _boxDefaultColor = Globals.SemiTransparency ? Color.FromArgb(128, 255, 0, 255) : Color.WhiteSmoke;
                        }
                BackColor = _selectedColor;
            }

            //no need of invalidation since its done after the call of this function
        }
    }
}
