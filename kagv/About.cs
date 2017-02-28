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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private Image _getEmbedResource(string a)
        {
            System.Reflection.Assembly _assembly;
            Stream _myStream;
            _assembly = System.Reflection.Assembly.GetExecutingAssembly();

            _myStream = _assembly.GetManifestResourceStream("kagv.Resources." + a);
            Image _b = Image.FromStream(_myStream);
            return _b;

        }

        private void About_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = _getEmbedResource("logo.png");
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.autom.teithe.gr/gr/index.php");
        }
    }
}
