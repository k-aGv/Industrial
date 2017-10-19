using System;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace kagv {
    public partial class About : Form {
        public About() {
            InitializeComponent();
        }

        private Image _getEmbedResource(string a) {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = assembly.GetManifestResourceStream("kagv.Resources." + a);
            if (myStream != null) {
                Image b = Image.FromStream(myStream);
                return b;
            }
            return null;
           
        }

        private void About_Load(object sender, EventArgs e) {
            CenterToScreen();
            pb.Image = _getEmbedResource("logo.png");
            pb_divider.Image = _getEmbedResource("divider.png");
            pb_divider2.Image = _getEmbedResource("divider.png");

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("http://www.autom.teithe.gr");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/k-aGv");
        }
    }
}
