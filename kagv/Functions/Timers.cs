namespace kagv {

    public partial class MainForm {

        //function that starts the needed timers
        private void Timers() {
            //every timer is responsible for every agv for up to 5 _AGVs

            int c = 0;
            for (int i = 0; i < _trappedStatus.Length; i++)
                if (!_trappedStatus[i]) //array containing the status of AGV
                    c++; //counts the number of free-to-move _AGVs

            switch (c) //depending on the _c, the required timers will be started
            {
                case 1:
                    timer0.Start();
                    break;
                case 2:
                    timer0.Start();
                    timer1.Start();
                    break;
                case 3:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    break;
                case 4:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    break;
                case 5:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    timer4.Start();
                    break;
            }
        }
    }
}
