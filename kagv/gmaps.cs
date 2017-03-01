using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Awesomium.Core;
using Awesomium.Windows.Forms;
namespace kagv
{
    public partial class gmaps : Form
    {
       
        public gmaps()
        {
            InitializeComponent();
        }

        private int w;
        private int h;
        public void setFormSize(int a, int b)
        {
            w = a;
            h = b;
        }
        private void gmaps_Load(object sender, EventArgs e)
        {
			this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = w;
            this.Height = h;
            mapControl.Width = w-10;
            mapControl.Height = h - menuStrip1.Height - 40;

            mapControl.Location = new Point(1, menuStrip1.Height + 1);
            try
            {
                mapControl.Source = new Uri("https://www.google.gr/maps/@40.6631136,22.9237417,15z?hl=en");
            }
            catch (Exception z)
            {
                MessageBox.Show("An error occured while trying to load the Google Maps\r\n.Is there an internet connection?\r\n"+z);
            }
        }

       

        Font myFont = new Font("Tahoma", 8, FontStyle.Bold);
        private void getScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            this.Cursor = Cursors.Hand;

            screenshot s = new screenshot();
            s.Opacity = 0.5;
            s.Left = this.Left;
            s.Top = this.Top;
            s.Size = this.Size;
            s.ShowDialog(this);
            
        }
       
        
      
    }
}
