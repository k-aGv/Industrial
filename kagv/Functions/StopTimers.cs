namespace kagv {

    public partial class MainForm {

        private void StopTimers(int agvIndex) {
            switch (agvIndex) {
                case 0:
                    timer0.Stop();
                    break;
                case 1:
                    timer1.Stop();
                    break;
                case 2:
                    timer2.Stop();
                    break;
                case 3:
                    timer3.Stop();
                    break;
                case 4:
                    timer4.Stop();
                    break;
            }
        }
    }
}
