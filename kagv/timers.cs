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
    public partial class main_form : Form {
        private void timer1_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (newsteps[0, 0, i] == 0 || newsteps[0, 1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[0].Steps = mysteps;

            animator(timer_counter[0], 0);

            timer_counter[0]++;
        }

        private void timer2_Tick(object sender, EventArgs e) {


            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (newsteps[1, 0, i] == 0 || newsteps[1, 1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[1].Steps = mysteps;

            animator(timer_counter[1], 1);

            timer_counter[1]++;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (newsteps[2, 0, i] == 0 || newsteps[2, 1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[2].Steps = mysteps;

            animator(timer_counter[2], 2);

            timer_counter[2]++;
        }

        private void timer4_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (newsteps[3, 0, i] == 0 || newsteps[3, 1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[3].Steps = mysteps;

            animator(timer_counter[3], 3);

            timer_counter[3]++;
        }

        private void timer5_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (newsteps[4, 0, i] == 0 || newsteps[4, 1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[4].Steps = mysteps;

            animator(timer_counter[4], 4);

            timer_counter[4]++;
        }
    }
}
