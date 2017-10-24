using System;

namespace kagv {

    public partial class MainForm {

        /*-----------------------------------------------------*/
        //function for updating the values that are shown in the emissions Form
        private void Update_emissions(int whichAgv) {

            switch (cb_type.SelectedItem.ToString()) {
                case "LPG":
                    if (_AGVs[whichAgv].Status.Busy) {
                        _CO2 += 2959.57;
                        _CO += 27.04;
                        _NOx += 19.63;
                        _THC += 3.06;
                        _globalWarming += 3.58;
                    } else {
                        _CO2 += 1935.16;
                        _CO += 13.36;
                        _NOx += 13.90;
                        _THC += 1.51;
                        _globalWarming += 2.33;
                    }
                    break;
                case "DSL":
                    if (_AGVs[whichAgv].Status.Busy) {
                        _CO2 += 2130.11;
                        _CO += 7.28;
                        _NOx += 20.16;
                        _THC += 1.77;
                        _globalWarming += 2.49;
                    } else {
                        _CO2 += 1510.83;
                        _CO += 3.84;
                        _NOx += 14.33;
                        _THC += 1.08;
                        _globalWarming += 1.2;
                    }
                    break;
                default:
                    _CO2 = 0;
                    _CO = 0;
                    _NOx = 0;
                    _THC = 0;
                    if (_AGVs[whichAgv].Status.Busy)
                        _globalWarming += 0.67;
                    else
                        _globalWarming += 0.64;
                    break;
            }


            if (!tree_stats.Nodes[1].IsExpanded)
                tree_stats.Nodes[1].Expand();

            tree_stats.Nodes[1].Nodes[0].Text = "CO: " + Math.Round(_CO, 2) + " gr";
            tree_stats.Nodes[1].Nodes[1].Text = "CO2: " + Math.Round(_CO2, 2) + " gr";
            tree_stats.Nodes[1].Nodes[2].Text = "NOx: " + Math.Round(_NOx, 2) + " gr";
            tree_stats.Nodes[1].Nodes[3].Text = "THC: " + Math.Round(_THC, 2) + " gr";
            tree_stats.Nodes[1].Nodes[4].Text = "Global Warming eq: " + Math.Round(_globalWarming, 2) + " kgr";

        }
    }
}
