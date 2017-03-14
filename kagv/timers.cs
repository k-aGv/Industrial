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
            for (int i = 0; i < Constants.__MaximumSteps; i++) {
                if (AGVs[0].Steps[i].X == 0 || AGVs[0].Steps[i].Y == 0)
                    i = Constants.__MaximumSteps;
                else
                    mysteps++;
            }
            AGVs[0].StepsCounter = mysteps;

            animator(timer_counter[0], 0);

            timer_counter[0]++;
        }

        private void timer2_Tick(object sender, EventArgs e) {


            int mysteps = 0;
            for (int i = 0; i < Constants.__MaximumSteps; i++) {
                if (AGVs[1].Steps[i].X == 0 || AGVs[1].Steps[i].Y == 0)
                    i = Constants.__MaximumSteps;
                else
                    mysteps++;
            }
            AGVs[1].StepsCounter = mysteps;

            animator(timer_counter[1], 1);

            timer_counter[1]++;
        }

        private void timer3_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Constants.__MaximumSteps; i++) {
                if (AGVs[2].Steps[i].X == 0 || AGVs[2].Steps[i].Y == 0)
                    i = Constants.__MaximumSteps;
                else
                    mysteps++;
            }
            AGVs[2].StepsCounter = mysteps;

            animator(timer_counter[2], 2);

            timer_counter[2]++;
        }

        private void timer4_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Constants.__MaximumSteps; i++) {
                if (AGVs[3].Steps[i].X == 0 || AGVs[3].Steps[i].Y == 0)
                    i = Constants.__MaximumSteps;
                else
                    mysteps++;
            }
            AGVs[3].StepsCounter = mysteps;

            animator(timer_counter[3], 3);

            timer_counter[3]++;
        }

        private void timer5_Tick(object sender, EventArgs e) {
            int mysteps = 0;
            for (int i = 0; i < Constants.__MaximumSteps; i++) {
                if (AGVs[4].Steps[i].X == 00 || AGVs[4].Steps[i].Y == 0)
                    i = Constants.__MaximumSteps;
                else
                    mysteps++;
            }
            AGVs[4].StepsCounter = mysteps;

            animator(timer_counter[4], 4);

            timer_counter[4]++;
        }
    }
}
