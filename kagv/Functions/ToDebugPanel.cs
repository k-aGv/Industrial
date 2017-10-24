using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        private void ToDebugPanel(object var, string varname) {
            TreeNode node = new TreeNode(varname.ToString()) {
                Name = varname,
                Text = varname + ":" + var,
            };
            tree_stats.Nodes[0].Nodes.Add(node);
            tree_stats.Nodes[0].Expand();
        }
    }
}
