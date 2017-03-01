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
    public partial class debug : Form
    {
        public debug()
        {
            InitializeComponent();
        }

        private void debug_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
           // textBox1.Text += Environment.StackTrace+"\r\n";

            main_form x = new main_form();
            
            //var fields = x.GetType().GetFields().Select(f => f.Name).ToList();
            System.Reflection.FieldInfo[] fields = x.GetType().GetFields(System.Reflection.BindingFlags.NonPublic
                                        | System.Reflection.BindingFlags.Instance);
            
            /*
            for (int i = 0; i < fields.Length; i++)
            {
                try
                {s = fields[i].ToString() + "=" + "\r\n";}
                catch (Exception myexception) { }
            }
               */ 
            
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        private void debug_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }
}
