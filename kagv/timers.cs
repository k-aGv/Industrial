using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kagv.cs
{
    public partial class Form1 : Form
    {
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            animator(timer_counter[0],0);
            timer_counter[0]++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            animator(timer_counter[1], 1);
            timer_counter[1]++;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
            animator(timer_counter[2], 2);
            timer_counter[2]++;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            
            animator(timer_counter[3], 3);
            timer_counter[3]++;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            
            animator(timer_counter[4], 4);
            timer_counter[4]++;
        }
    }
}
