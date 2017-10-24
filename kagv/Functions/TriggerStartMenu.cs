namespace kagv {

    public partial class MainForm {

        //manipulates the text of the Start button 
        private void TriggerStartMenu(bool t) {
            startToolStripMenuItem.Enabled = t;
            if (!t) {
                startToolStripMenuItem.Text = "Start            Clear and redraw the components please.";
                if (_endPointCoords.X == -1 && _endPointCoords.Y == -1)
                    startToolStripMenuItem.Text = "Start            Create a complete path please.";
                startToolStripMenuItem.ShortcutKeyDisplayString = "";
            } else {
                startToolStripMenuItem.Text = "Start";
                startToolStripMenuItem.ShortcutKeyDisplayString = "(Space)";
            }
        }
    }
}
