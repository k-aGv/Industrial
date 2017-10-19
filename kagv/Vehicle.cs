/*!
The MIT License (MIT)

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
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using kagv.DLL_source;

namespace kagv {

    internal class Vehicle {

        //****************************************
        //AGV Status
        internal class AGVStatus {
            public bool Busy { get; set; }
            public bool Loaded { get; set; }
        }

        public AGVStatus Status { get; } = new AGVStatus();
        //=========================================

        //*****************************************
        //AGV Steps
        internal class AGVSteps {
            public double X { get; set; }
            public double Y { get; set; }
        }

        private readonly AGVSteps[] _steps;
        public AGVSteps[] Steps
        {
            get => _steps; 
        }
        //=========================================
        //AGV Path
        public GridLine[] Paths = new GridLine[Globals.MaximumSteps];
        public Point Location;
        public Point MarkedLoad;

        //get-set is not a mandatory here
        public int StartX;
        public int StartY;
        public int SizeX;
        public int SizeY;
        public int ID = -1;
        public int LoadsDelivered = 0;

        private Panel _agvPortrait;
        private PictureBox _agvIcon;
        private Point _agvLocation;
        private readonly Form _mirroredForm;

        public bool HasLoadToPick;

        //*****************************************
        //AGV JumpPoints
        private List<GridPos> _jmpPnts = new List<GridPos>();
        public List<GridPos> JumpPoints
        {
            get => _jmpPnts;
            set => _jmpPnts=value;
        }
 
        public int StepsCounter { get; set; }

        //=========================================
        /// <summary>
        /// Returns the absolute Location of the Marked Load on the Grid
        /// </summary>
        /// <returns></returns>
        public Point GetMarkedLoad() {
            Point p = new Point(
                (MarkedLoad.X * Globals.BlockSide) + Globals.LeftBarOffset,
                (MarkedLoad.Y * Globals.BlockSide) + Globals.TopBarOffset
                );
            return p;
        }



        public Vehicle(Form handle) { //constructor
            _mirroredForm = handle;
            Status.Busy = false;
            Status.Loaded = false;
            _steps = new AGVSteps[Globals.MaximumSteps];
            for (int i = 0; i < _steps.Length; i++) {
                _steps[i] = new AGVSteps
                {
                    X = -1,
                    Y = -1
                };
            }
        }
     
        public void Init() {
            //init vars
            Status.Busy = false;
            Status.Loaded = false;

            _agvPortrait = new Panel
            {
                Name = "AGVPORTRAIT"
            };
            _agvIcon = new PictureBox();

            _agvPortrait.Controls.Add(_agvIcon);

            Size size = new Size(Globals.BlockSide - 2, Globals.BlockSide - 2);
            Point location = new Point(StartX, StartY);
            _agvPortrait.Size = size;
            _agvPortrait.Location = location;
            _agvPortrait.Visible = true;
            _agvPortrait.BringToFront();
            _agvPortrait.BackColor = Color.Transparent;

            _mirroredForm.Controls.Add(_agvPortrait);

            _agvIcon.BackColor = _mirroredForm.BackColor;
            _agvIcon.BorderStyle = BorderStyle.None;
            _agvIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            _agvIcon.Size = size;
            _agvIcon.Visible = true;

            _agvIcon.Image = _getEmbedResource("empty.png");

            _agvIcon.BackColor = Color.Transparent;

            //public exports
            Location = _agvPortrait.Location;

        }



        private Image _getEmbedResource(string a) {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = assembly.GetManifestResourceStream("kagv.Resources." + a);
            if (myStream == null) return null;
            Image b = Image.FromStream(myStream);
            return b;
        }

        public void KillIcon() {
            try {
                _agvIcon.Dispose();
                _agvPortrait.Dispose();
            } catch { }
        }


        public void SetLoaded() {
            _agvIcon.Image = _getEmbedResource("loaded.png");
            Status.Loaded = true;
        }

        public void SetEmpty() {
            _agvIcon.Image = _getEmbedResource("empty.png");
            Status.Loaded = false;
        }

        public void UpdateAGV() {
            if (_mirroredForm.Controls.Count != 0) {
                foreach (Control p in _mirroredForm.Controls) {
                    if (p == _agvIcon)
                        _mirroredForm.Controls.Remove(p);
                    if (p.Name == "AGVPORTRAIT")
                        _mirroredForm.Controls.Remove(p);
                }

            } 
        }

        public void SetLocation(int x, int y) {
            _agvLocation = new Point(x, y);
            _agvPortrait.Location = _agvLocation;
            Location = _agvLocation;
        }
        public void SetLocation(Point loc) {
            _agvLocation = loc;
            _agvPortrait.Location = _agvLocation;
            Location = _agvLocation;
        }

        //AGVs[agv_index].SetLocation(stepx - ((Constants.BlockSide / 2) - 1) +1, stepy - ((Constants.BlockSide / 2) - 1) + 1); //this is how we move the AGV on the grid (Setlocation function)
        //                                                                     ^
        public Point GetLocation() { //has to be -1 to balance this            |   from functions.cs
            return new Point(_agvLocation.X - 1, _agvLocation.Y - 1);//          |  
        }


    }

}
