using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kagv
{

    //enum FuelType { battery,gas,tsangk};
    
    public class Vehicle : IDisposable
    {
        private Panel AgvPortrait;
        private PictureBox AgvIcon;
        private Point AgvLocation;
        private Form mirroredForm;
        private bool isBusyVar=false;
        private bool isLoadedVar = false; //WILL BE USED LATER
        

        public Point Location;
        public Point StartPoint;

        public Point targetPoint;

        public Vehicle(Form handle, int StartX, int StartY, int SizeX, int SizeY)
        {
            //private exports
            mirroredForm = handle;
            isBusyVar = false;
            
            AgvPortrait = new Panel();
            AgvPortrait.Name = "AGVPORTRAIT";
            AgvIcon = new PictureBox();
            
            AgvPortrait.Controls.Add(AgvIcon);

            Size _size = new Size(18, 18);
            Point _location = new Point(StartX, StartY);
            AgvPortrait.Size = _size;
            AgvPortrait.Location = _location;
            AgvPortrait.Visible = true;
            AgvPortrait.BringToFront();
            AgvPortrait.BackColor = Color.Transparent;

            handle.Controls.Add(AgvPortrait);

            AgvIcon.BackColor = handle.BackColor;
            AgvIcon.BorderStyle = BorderStyle.None;
            AgvIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            AgvIcon.Size = new Size(18, 18);
            AgvIcon.Visible = true;

            AgvIcon.Image = _getEmbedResource("empty.png");

            AgvIcon.BackColor = Color.Transparent;

            //public exports
            Location = AgvPortrait.Location;
            StartPoint = new Point(StartX, StartY);

            
        }

        private Image _getEmbedResource(string a)
        {
            System.Reflection.Assembly _assembly;
            Stream _myStream;
            _assembly = System.Reflection.Assembly.GetExecutingAssembly();

            _myStream = _assembly.GetManifestResourceStream("kagv.Resources."+a);
            Image _b = Image.FromStream(_myStream);
            return _b;
            
        }

        
        public Vehicle(Form handle) //overloaded constructor
        {
            mirroredForm = handle;
        }
       
        public void killIcon()
        {
            try
            {
                this.AgvIcon.Dispose();
                this.AgvPortrait.Dispose();
            }
            catch { }
        }
        public bool isLoaded() //WILL BE USED LATER
        {
            if (isLoadedVar)
                return true;
            else
                return false;
        }

        public void Busy(bool x)
        {
            isBusyVar = x;
        }

        public bool isBusy()
        {
            return isBusyVar;
        }

        public void setLoaded()
        {
            this.AgvIcon.Image = _getEmbedResource("loaded.png");
            this.isLoadedVar = true;
        }

        public void setEmpty()
        {
            this.AgvIcon.Image = _getEmbedResource("empty.png");
            this.isLoadedVar = false;
        }

        public void updateAGV()
        {
            if (mirroredForm.Controls.Count != 0)
            {
                foreach (Control p in mirroredForm.Controls)
                {
                    if (p == AgvIcon)
                        mirroredForm.Controls.Remove(p);
                    if (p.Name=="AGVPORTRAIT")
                        mirroredForm.Controls.Remove(p);
                }
                
            }
            else
                return;
        }

        private int steps;
        public int Steps
        {
            get
            {
                return this.steps;
            }
            set
            {
                this.steps = Steps;
            }
        }

        public void SetLocation(int X,int Y)
        {
            AgvLocation = new Point(X, Y);
            AgvPortrait.Location = AgvLocation;
            Location = AgvLocation;
        }
        public void SetLocation(Point loc)
        {
            AgvLocation = loc;
            AgvPortrait.Location = AgvLocation;
            Location = AgvLocation;
        }
       
        public Point GetLocation()
        {
            return new Point(AgvLocation.X, AgvLocation.Y);
        }


        public void Dispose()
        {
            AgvPortrait.Dispose();
        }
       
    }
     
}
