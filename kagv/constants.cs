using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kagv {
    public static class Constants {
        public const int __MaximumSteps = 2000;
        public const int __TopBarOffset = 75 + 24 + 2;//distance from top to the grid=offset+menubar+2pixel of gray border
        public const int __BottomBarOffset = 50 + 10;//distance between grid and the bottom of the form
        public static int __WidthBlocks = 64; //grid blocks
        public static int __HeightBlocks = 32; //grid blocks
        public static int __MaximumAGVs = 5;
    }
}
