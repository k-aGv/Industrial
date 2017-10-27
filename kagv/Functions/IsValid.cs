/*!
The Apache License 2.0 License

Copyright (c) 2017 Dimitris Katikaridis <dkatikaridis@gmail.com>,Giannis Menekses <johnmenex@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
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
