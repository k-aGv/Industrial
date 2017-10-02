using System;
using System.Windows.Forms;

namespace kagv {
    public partial class emissions : Form {
        public emissions() {
            InitializeComponent();
        }
        private void emissions_Load(object sender, EventArgs e) {
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;

        }
    }
}
