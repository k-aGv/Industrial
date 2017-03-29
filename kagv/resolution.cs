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
    public partial class resolution : Form {
        public resolution() {
            InitializeComponent();
        }

        private void resolution_Load(object sender, EventArgs e) {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            double z = -1;
            string s = "";
            if (tb_res.Value == 1) s = "0.5";
            else if (tb_res.Value == 2) s = "1 (Default)";
            else if (tb_res.Value == 3) s = "2";

            lb_multiplier.Text = "Multiplier: " + s;
        }
    }
}
