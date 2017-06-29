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
    public partial class emissions : Form {
        public emissions() {
            InitializeComponent();
        }
        
        protected override CreateParams CreateParams {
            get {
                CreateParams param = base.CreateParams;
                param.ClassStyle = param.ClassStyle | 0x200;
                return param;
            }
        }

        private void emissions_Load(object sender, EventArgs e) {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

      
        
    }
}
