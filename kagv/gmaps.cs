using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace kagv
{
    public partial class gmaps : Form
    {
        internal readonly GMapOverlay myobjects = new GMapOverlay("objects");

        public gmaps()
        {
            InitializeComponent();
        }

        private void gmaps_Load(object sender, EventArgs e)
        {
           
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;


            //map implementation
            mymap.MapProvider = GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance;//using it as FULL reference to have the complete list of providers
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            mymap.SetPositionByKeywords("greece,thessaloniki");
            mymap.MinZoom = 0;
            mymap.MaxZoom = 18;
            mymap.Zoom = 8;
            mymap.Overlays.Add(myobjects);
            mymap.DragButton = MouseButtons.Left;

           
            cb_provider.Items.Add("GoogleMapProvider");
            cb_provider.Items.Add("GoogleTerrainMapProvider");
            cb_provider.Text = "GoogleMapProvider";
            //its not a joke ->
            //____________________________________________________________________opacity______________R___________________________G_______________________B
            mymap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            
          
        }

        private void btn_visit_Click(object sender, EventArgs e)
        {
            mymap.SetPositionByKeywords(tb_location.Text);
        }

        private void cb_cross_CheckedChanged(object sender, EventArgs e)
        {
            mymap.ShowCenter = cb_cross.Checked;
            mymap.Refresh();
        }

        private void btn_marker_Click(object sender, EventArgs e)
        {
            Screenshot st = new Screenshot(this);
            st.Owner = this;
            st.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mymap.Zoom += 1;
            mymap.Refresh();
        }

        private void mymap_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void cb_provider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( cb_provider.SelectedItem.ToString() == "GoogleTerrainMapProvider")
                mymap.MapProvider = GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance;
            if (cb_provider.SelectedItem.ToString() == "GoogleMapProvider")
                mymap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;

            mymap.Refresh();
        }
      
    }
}
