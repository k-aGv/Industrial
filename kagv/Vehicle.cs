using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace kagv {

     class Vehicle {

         //AGV Status
         internal class AGVStatus {
             public bool Busy { get; set; }
             public bool Loaded { get; set; }
         }

         private AGVStatus status=new AGVStatus();
         public AGVStatus Status {
             get { return this.status; }
         }
         //=========================================
         
         //AGV steps
         internal class AGVSteps {
             public double X { get; set; }
             public double Y { get; set; }
         }

         private AGVSteps[] steps ;
         public AGVSteps [] Steps {
             get { return this.steps; }
         }
         //=========================================

        private Panel AgvPortrait;
        private PictureBox AgvIcon;
        private Point AgvLocation;
        private Form mirroredForm;


        private List<GridPos> jmp_pnts = new List<GridPos>();
        //private double[,] steps = new double[2, 2000];// to do:internal class steps.x steps.y
        private int steps_counter;

        public List<GridPos> JumpPoints {
            get {
                return this.jmp_pnts;
            }
            set {
                this.jmp_pnts = value;
            }
        }

        public int StepsCounter {
            get {
                return this.steps_counter;
            }
            set {
                this.steps_counter = value;
            }
        }


        public Point Location;
        public Point StartPoint;
        public Point MarkedLoad;

        //get-set is not a mandatory here
        public int StartX;
        public int StartY;
        public int SizeX;
        public int SizeY;

        public Vehicle(Form handle) { //constructor
            mirroredForm = handle;
            this.status.Busy = false;
            this.status.Loaded = false;
            this.steps = new AGVSteps[2000];
            for (int i = 0; i < steps.Length; i++) {
                steps[i] = new AGVSteps();
                steps[i].X = -1;
                steps[i].Y = -1;
            }
        }

        public void Init() {

            //init vars
            this.status.Busy = false;
            this.status.Loaded = false;

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

            mirroredForm.Controls.Add(AgvPortrait);

            AgvIcon.BackColor = mirroredForm.BackColor;
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

        

        private Image _getEmbedResource(string a) {
            System.Reflection.Assembly _assembly;
            Stream _myStream;
            _assembly = System.Reflection.Assembly.GetExecutingAssembly();

            _myStream = _assembly.GetManifestResourceStream("kagv.Resources." + a);
            Image _b = Image.FromStream(_myStream);
            return _b;

        }

        public void killIcon() {
            try {
                this.AgvIcon.Dispose();
                this.AgvPortrait.Dispose();
            } catch { }
        }
 

        public void setLoaded() {
            this.AgvIcon.Image = _getEmbedResource("loaded.png");
            this.status.Loaded = true;
        }

        public void setEmpty() {
            this.AgvIcon.Image = _getEmbedResource("empty.png");
            this.status.Loaded = false;
        }

        public void updateAGV() {
            if (mirroredForm.Controls.Count != 0) {
                foreach (Control p in mirroredForm.Controls) {
                    if (p == AgvIcon)
                        mirroredForm.Controls.Remove(p);
                    if (p.Name == "AGVPORTRAIT")
                        mirroredForm.Controls.Remove(p);
                }

            } else
                return;
        }

        
        public void SetLocation(int X, int Y) {
            AgvLocation = new Point(X, Y);
            AgvPortrait.Location = AgvLocation;
            Location = AgvLocation;
        }
        public void SetLocation(Point loc) {
            AgvLocation = loc;
            AgvPortrait.Location = AgvLocation;
            Location = AgvLocation;
        }

        public Point GetLocation() {
            return new Point(AgvLocation.X, AgvLocation.Y);
        }

    }

}
