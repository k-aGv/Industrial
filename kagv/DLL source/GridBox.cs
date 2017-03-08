using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kagv
{
    enum BoxType { Start, End, Wall, Normal , Load};

    class GridBox:IDisposable
    {
        public int x, y, width, height;
        public SolidBrush brush;
        public Rectangle boxRec;
        public BoxType boxType;
        public int startID;

        private Color myBrown=Color.FromArgb(138,109,86);
        private Graphics graphs;
        public GridBox(int iX, int iY,BoxType iType)
        {
            this.x = iX;
            this.y = iY;
            this.boxType = iType;
            switch (iType)
            {
                case BoxType.Normal:
                    brush = new SolidBrush(Color.WhiteSmoke);
                    break;
                case BoxType.End:
                    brush = new SolidBrush(Color.Red);
                    break;
                case BoxType.Start:
                    brush = new SolidBrush(Color.Green);
                    break;
                case BoxType.Wall:
                    brush = new SolidBrush(Color.Gray);
                    break;
                case BoxType.Load:
                    brush = new SolidBrush(myBrown);
                    break;
            
            }
            width = 19;
            height = 19;
            boxRec = new Rectangle(x, y, width, height);
        }

        public void DrawBox(Graphics iPaper,BoxType iType)
        {
            if (iType == boxType)
            {
                boxRec.X = x;
                boxRec.Y = y;
                iPaper.FillRectangle(brush, boxRec);
                graphs = iPaper;
            }
        }

       
        public void BeTransparent()
        {
            switch (this.boxType)
            {
                case BoxType.Normal:
                    this.brush = new SolidBrush(Color.Transparent);
                    break;
            }
        }

        public void SwitchBox()
        {
            switch (this.boxType)
            {
                case BoxType.Normal:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(Color.Gray);
                    this.boxType = BoxType.Wall;
                    break;
                case BoxType.Wall:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(Color.WhiteSmoke);
                    this.boxType = BoxType.Normal;
                    break;

            }
        }
        public void SetAsTargetted(Graphics iPaper)
        {
            iPaper.FillRectangle(new SolidBrush(Color.Orange),boxRec);
        }


        public void SwitchEnd_StartToNormal()
        {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.WhiteSmoke);
            this.boxType = BoxType.Normal;

        }

        public void SwitchLoad()
        {
            switch (this.boxType)
            {
                case BoxType.Normal:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(myBrown);
                    this.boxType = BoxType.Load;
                    break;
                case BoxType.Load:
                    if (this.brush != null)
                        this.brush.Dispose();
                    this.brush = new SolidBrush(Color.WhiteSmoke);
                    this.boxType = BoxType.Normal;
                    break;

            }
        }

              
        public void SetNormalBox()
        {
            if (this.brush != null)
                this.brush.Dispose();
           this.brush = new SolidBrush(Color.WhiteSmoke);
           this.boxType = BoxType.Normal;
        }

        public void SetStartBox(int _id)
        {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.Green);
            this.boxType = BoxType.Start;
            startID = _id;             
        }

        public void SetEndBox()
        {
            if (this.brush != null)
                this.brush.Dispose();
            this.brush = new SolidBrush(Color.Red);
            this.boxType = BoxType.End; 
        }


        public void Dispose()
        {
            if(this.brush!=null)
                this.brush.Dispose();

        }
    }
}
