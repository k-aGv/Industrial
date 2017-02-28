using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kagv
{
    public partial class screenshot : Form
    {
        Size size;
        Point loc;
        Graphics gp;
        private string resources_path = System.IO.Directory.GetCurrentDirectory();
        int shotcounter = 0;
        bool ismousedown = false;
        Point ClickedCoords;
        int firstClick = 1;//true
        public Rectangle shot;
        bool takingShot = false;

        public screenshot()
        {
            InitializeComponent();
        }
       
        private void screenshot_Load(object sender, EventArgs e)
        {
            loc.X = Owner.Location.X+7 ;
            loc.Y = Owner.Location.Y+50;
            this.Location = loc;

            size.Height = Owner.Height -60;
            size.Width = Owner.Width-15;
            this.Size = size;
        }

        private void screenshot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !takingShot)
                this.Close();
        }
        
        private void screenshot_MouseDown(object sender, MouseEventArgs e)
        {
            ismousedown = true;
            if (firstClick==1)
            {
                ClickedCoords = new Point(e.X, e.Y);
                firstClick = 2;//false
            }
        }

        private void screenshot_MouseUp(object sender, MouseEventArgs e)
        {
            ismousedown = false;
            firstClick = 3;//not true not false :P

            if (e.Button == MouseButtons.Left)
            {
                
                this.Opacity = 0;
                using (Bitmap bitmap = new Bitmap(shot.Width, shot.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(this.Location.X + shot.Left, this.Location.Y + shot.Top), Point.Empty, shot.Size);
                    }
                    while (System.IO.File.Exists(resources_path + "//screenshot_map_" + shotcounter + ".png")) 
                    {
                        shotcounter++;
                    }
                    bitmap.Save(resources_path+"//screenshot_map_"+shotcounter+".png", System.Drawing.Imaging.ImageFormat.Png);
                }
                this.Dispose();
               
            }
            else
                this.Dispose();


        }
        
        private void screenshot_MouseMove(object sender, MouseEventArgs e)
        {
            if( ismousedown)
            {
                gp = this.CreateGraphics();
                gp.Clear(this.BackColor);
                Pen p = new Pen(Color.Red);
                p.Width = 4;

                int rec_width=0, rec_height=0;
                if (e.X > ClickedCoords.X)
                {
                    rec_width = e.X - ClickedCoords.X;
                    rec_height = e.Y - ClickedCoords.Y;
                    shot = new Rectangle(
                    ClickedCoords.X
                    , ClickedCoords.Y
                    , rec_width
                    , rec_height);
                }
                else
                {
                    shot = new Rectangle(e.X, ClickedCoords.Y,
                                         ClickedCoords.X-e.X, e.Y-ClickedCoords.Y
                                            );

                }

                if (shot.Width <= 0 || shot.Height <= 0)
                    return;
                
                gp.DrawRectangle(
                    p
                    , shot);
            }
        }
    }
}
