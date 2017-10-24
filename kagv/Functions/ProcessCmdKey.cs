using kagv.DLL_source;
using System;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        //function for handling keystrokes and assigning specific actions to them
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            bool emptymap = true;
            if (ModifierKeys.HasFlag(Keys.Control) && !_holdCtrl) {

                if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                    return false;

                for (int k = 0; k < Globals.WidthBlocks; k++) {
                    for (int l = 0; l < Globals.HeightBlocks; l++) {
                        if (_rectangles[k][l].BoxType != BoxType.Normal) {
                            emptymap = false;
                            break;
                        }
                    }
                    if (!emptymap) {
                        break;
                    }
                }

                if (!emptymap) {
                    DialogResult s = MessageBox.Show("Grid resize is only possible in an empty grid\nThe grid will be deleted.\nProceed?"
                                   , "Grid Resize triggered", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (s == DialogResult.Yes) {
                        _holdCtrl = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        FullyRestore();
                        return true;
                    }
                    return false;
                }

                if (_overImage) {
                    DialogResult s = MessageBox.Show("Grid resize is only possible in an empty grid\nThe grid will be deleted.\nProceed?"
                                  , "Grid Resize triggered", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (s == DialogResult.Yes) {
                        _holdCtrl = true;
                        UpdateGridStats();
                        toolStripStatusLabel1.Text = "Release CTRL to return...";
                        panel_resize.Visible = true;
                        _overImage = false;
                        FullyRestore();
                        return true;
                    }
                    return false;
                }
                _holdCtrl = true;
                UpdateGridStats();
                toolStripStatusLabel1.Text = "Release CTRL to return...";
                panel_resize.Visible = true;
                return true;
            }

            switch (keyData) {
                case Keys.F5:
                    allToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Up:
                    increaseSpeedToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Down:
                    decreaseSpeedToolStripMenuItem_Click(new object(), new EventArgs());
                    return true;
                case Keys.Space:
                    if (timer0.Enabled || timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled)
                        return false;
                    int c = 0;
                    for (int i = 0; i < _startPos.Count; i++)
                        c += _AGVs[i].JumpPoints.Count;

                    if (c > 0)
                        TriggerStartMenu(true);

                    if (startToolStripMenuItem.Enabled)
                        startToolStripMenuItem_Click(new object(), new EventArgs());
                    else
                        MessageBox.Show(this, "Create a path please", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                default:
                    return false;
            }

        }
    }
}