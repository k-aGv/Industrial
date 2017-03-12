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
                if (AGVs[0].Steps[0, i] == 0 || AGVs[0].Steps[1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[0].StepsCounter = mysteps;

            animator(timer_counter[0], 0);

            timer_counter[0]++;
        }

        private void timer2_Tick(object sender, EventArgs e) {


            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (AGVs[1].Steps[0, i] == 0 || AGVs[1].Steps[1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[1].StepsCounter = mysteps;

            animator(timer_counter[1], 1);

            timer_counter[1]++;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (AGVs[2].Steps[0, i] == 0 || AGVs[2].Steps[1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[2].StepsCounter = mysteps;

            animator(timer_counter[2], 2);

            timer_counter[2]++;
        }

        private void timer4_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (AGVs[3].Steps[0, i] == 0 || AGVs[3].Steps[1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[3].StepsCounter = mysteps;

            animator(timer_counter[3], 3);

            timer_counter[3]++;
        }

        private void timer5_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < 2000; i++) {
                if (AGVs[4].Steps[0, i] == 0 || AGVs[4].Steps[1, i] == 0)
                    i = 2000;
                else
                    mysteps++;
            }
            AGVs[4].StepsCounter = mysteps;

            animator(timer_counter[4], 4);

            timer_counter[4]++;
        }
    }
}
