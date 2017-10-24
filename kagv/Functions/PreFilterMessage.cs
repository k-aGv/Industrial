using System.Windows.Forms;

namespace kagv {

    public partial class MainForm : IMessageFilter {

        //Message callback of key
        public bool PreFilterMessage(ref Message msg) {
            if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                return false;
            if (msg.Msg == 0x101) //0x101 means key is up
            {

                _holdCtrl = false;
                panel_resize.Visible = false;
                toolStripStatusLabel1.Text = "Hold CTRL for grid configuration...";
                Refresh();
                return true;
            }
            return false;
        }
    }
}
