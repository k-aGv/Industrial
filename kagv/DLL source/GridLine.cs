using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kagv {
    class GridLine {
        public int fromX, fromY, toX, toY;
        public Pen pen;

        public GridLine(GridBox iFrom, GridBox iTo) {
            this.fromX = iFrom.boxRec.X + ((Constants.__BlockSide / 2) - 1);
            this.fromY = iFrom.boxRec.Y + ((Constants.__BlockSide / 2) - 1);
            this.toX = iTo.boxRec.X + ((Constants.__BlockSide / 2) - 1);
            this.toY = iTo.boxRec.Y + ((Constants.__BlockSide / 2) - 1);
            pen = new Pen(Color.BlueViolet);
            pen.Width = 1;


        }

        public void drawLine(Graphics iPaper) {
            iPaper.DrawLine(pen, fromX, fromY, toX, toY);
        }



        public void Dispose() {
            if (this.pen != null)
                this.pen.Dispose();

        }
    }
}
