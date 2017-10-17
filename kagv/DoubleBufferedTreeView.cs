using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace kagv
{
    class BufferedTreeView : TreeView
    {

        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr) TVS_EX_DOUBLEBUFFER, (IntPtr) TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }


        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0014) //if message is background erase 
                msg.Msg = 0x0000; //reset message to null
            base.WndProc(ref msg);
        }

    }
}