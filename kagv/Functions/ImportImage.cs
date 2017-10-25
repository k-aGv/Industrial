using System.Drawing;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        //function for importing an image as background
        private void ImportImage() {
            ofd_importmap.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|(*.jpg)";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                _importedLayout = Image.FromFile(ofd_importmap.FileName);
                _importedImageFile = Image.FromFile(ofd_importmap.FileName);
                _overImage = true;

                Globals.SemiTransparency = true;
                _boxDefaultColor = (Globals.SemiTransparency) ? Color.FromArgb(Globals.Opacity, Color.WhiteSmoke) : Color.WhiteSmoke;
            }

        }
    }
}
