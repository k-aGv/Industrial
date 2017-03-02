//#define USE_DEBUG
#undef USE_DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace kagv
{
    
    public partial class main_form : Form
    {
       

        public main_form()
        {
            InitializeComponent();
            initialization();
        }
        private void main_form_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            paper.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            try
            {
                //draws the grid
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                    {
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Normal);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Start);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.End);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Wall);
                        m_rectangles[widthTrav][heightTrav].DrawBox(paper, BoxType.Load);

                        if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Load 
                            && isLoad[widthTrav,heightTrav]==3 )
                            m_rectangles[widthTrav][heightTrav].SetAsTargetted(paper);
                    }
                }


                
                for (int i = 0; i < nUD_AGVs.Value; i++)
                {
                    new_steps_counter[i] = 0;
                    if (!lol_empty)
                    {
                        for (int resultTrav = 0; resultTrav < myresultList.Count; resultTrav++)
                        {
                            try
                            {
                                if (showLine)
                                    myLines[resultTrav, i].drawLine(paper);
                                if (!isMouseDown)
                                    DrawPoints(myLines[resultTrav, i], i);
                            }
                            catch (Exception z)
                            {
                                log = z.Data.ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception z)
            {
                log = z.Data.ToString();
            }

        }

        private void main_form_Load(object sender, EventArgs e)
        {
            cb_type.SelectedItem = "LPG";
            CO2_label.Text = "CO2: ";
            CO_label.Text = "CO: ";
            NOx_label.Text = "NOx: ";
            THC_label.Text = "THC: ";
            Global_label.Text = "Global Warming eq: ";

            agv1steps_LB.Text = "";
            agv2steps_LB.Text = "";
            agv3steps_LB.Text = "";
            agv4steps_LB.Text = "";
            agv5steps_LB.Text = "";


            refresh_label.Text = "Delay :" + timer1.Interval + " ms";

            nUD_AGVs.Value = 0;
            stepsToolStripMenuItem.Checked = true;
            linesToolStripMenuItem.Checked = true;
            dotsToolStripMenuItem.Checked = true;
            rb_start.Checked = true;
            this.BackColor = Color.DarkGray;

            this.StartPosition = FormStartPosition.CenterScreen;

            useRecursiveToolStripMenuItem.Checked = useRecursive;
            crossCornerToolStripMenuItem.Checked = crossCorners;
            crossAdjacentPointToolStripMenuItem.Checked = crossAdjacent;
            manhattanToolStripMenuItem.Checked = true;
            showEmissionsToolStripMenuItem.Checked = true;

            br = new SolidBrush(Color.BlueViolet);
            fontBR = new SolidBrush(Color.FromArgb(53, 153, 153));
            menuPanel.Location = new Point(0, 24 + 1);//24=menu bar Y
            // menuPanel.Height = topBarOffset -;
            menuPanel.Width = this.Width;

            tp = new ToolTip();

            tp.AutomaticDelay = 0;
            tp.ReshowDelay = 0;
            tp.InitialDelay = 0;
            tp.AutoPopDelay = 0;
            tp.IsBalloon = true;
            tp.ToolTipIcon = ToolTipIcon.Info;
            tp.ToolTipTitle = "Grid Block Information";

        }

        private void main_form_MouseDown(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled || timer2.Enabled || timer3.Enabled || timer4.Enabled || timer5.Enabled)
                return;

            isMouseDown = true;
            if ((e.Button == MouseButtons.Left) && (rb_wall.Checked))
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                    {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1))))
                        {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType)
                            {
                                case BoxType.Normal:
                                case BoxType.Wall:
                                    m_rectangles[widthTrav][heightTrav].SwitchBox();
                                    this.Invalidate();
                                    break;
                                case BoxType.Start:
                                case BoxType.End:
                                    break;
                            }
                        }
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {

                Point mycoords = new Point(e.X, e.Y);
                GridBox clickedBox = null;
                bool isBorder = true;
                string currentBoxCoords = "X: - Y: -\r\n";
                string currentBoxIndex = "Index: N/A\r\n";
                string currentBoxType = "Block type: Border\r\n";
                string isPath = "Is part of path: N/A\r\n";
                bool isPathBlock = false;
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                    {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(mycoords))
                        {
                            currentBoxType =
                                "Block type: " +
                                m_rectangles[widthTrav][heightTrav].boxType + "\r\n";
                            currentBoxCoords =
                                "X: " +
                                m_rectangles[widthTrav][heightTrav].boxRec.X + " " +
                                "Y: " +
                                m_rectangles[widthTrav][heightTrav].boxRec.Y + "\r\n";
                            currentBoxIndex =
                                "Index: " +
                                "iX: " + widthTrav + " " + "iY: " + heightTrav + "\r\n";

                            if (pos != null)
                            {
                                for (int j = 0; j < pos.Count; j++)
                                    for (int i = 0; i < newsteps.GetLength(2); i++)
                                    {
                                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains
                                            (
                                                   new Point(
                                                        Convert.ToInt32(newsteps[j, 0, i]),
                                                        Convert.ToInt32(newsteps[j, 1, i])
                                                        )
                                            ))
                                        {
                                           isPathBlock = true;
                                        }

                                    }

                                clickedBox = m_rectangles[widthTrav][heightTrav];
                            }
                            tp.ToolTipIcon = ToolTipIcon.Info;
                            if (isPathBlock && pos != null)
                            {
                                isPath = "Is part of path:Yes\r\n";
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X 
                                    , clickedBox.boxRec.Y - topBarOffset + 17);
                                isBorder = false;
                            }
                            else
                            {
                                isPath = "Is part of path:No\r\n";
                                clickedBox = new GridBox(e.X, e.Y, BoxType.Normal);
                                tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                                    , this
                                    , clickedBox.boxRec.X - 10
                                    , clickedBox.boxRec.Y - topBarOffset + 12);
                                isBorder = false;
                            }
                        }
                    }
                }

                if (isBorder)
                {
                    tp.ToolTipIcon = ToolTipIcon.Error;
                    tp.Show(currentBoxType + currentBoxCoords + currentBoxIndex + isPath
                               , this
                               , e.X - 8
                               , e.Y - topBarOffset + 14);
                }

            }

        }

        private void main_form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && rb_wall.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (m_lastBoxSelect == null)
                    {
                        for (int widthTrav = 0; widthTrav < width; widthTrav++)
                        {
                            for (int heightTrav = 0; heightTrav < height; heightTrav++)
                            {
                                if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1))))
                                {
                                    m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                                    m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                    switch (m_lastBoxType)
                                    {
                                        case BoxType.Normal:
                                        case BoxType.Wall:
                                            m_rectangles[widthTrav][heightTrav].SwitchBox();
                                            this.Invalidate();
                                            break;
                                        case BoxType.Start:
                                        case BoxType.End:
                                            break;
                                    }
                                }

                            }
                        }
                        
                        return;
                    }
                    else
                    {
                        for (int widthTrav = 0; widthTrav < width; widthTrav++)
                        {
                            for (int heightTrav = 0; heightTrav < height; heightTrav++)
                            {
                                if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1))))
                                {
                                    if (m_rectangles[widthTrav][heightTrav] == m_lastBoxSelect)
                                    {
                                        return;
                                    }
                                    else
                                    {

                                        switch (m_lastBoxType)
                                        {
                                            case BoxType.Normal:
                                            case BoxType.Wall:
                                                if (m_rectangles[widthTrav][heightTrav].boxType == m_lastBoxType)
                                                {
                                                    m_rectangles[widthTrav][heightTrav].SwitchBox();
                                                    m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                    this.Invalidate();
                                                }
                                                break;
                                            case BoxType.Start:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                m_lastBoxSelect.SetStartBox(1);
                                                m_lastBoxSelect.SetStartBox(2);
                                                this.Invalidate();
                                                break;
                                            case BoxType.End:
                                                m_lastBoxSelect.SetNormalBox();
                                                m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                                                m_lastBoxSelect.SetEndBox();
                                                this.Invalidate();
                                                break;

                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void main_form_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            for (int i = 0; i < new_steps_counter.Count(); i++)
                new_steps_counter[i] = 0;
            if (e.Button == MouseButtons.Right)
            {
                tp.Hide(this);
            }
            Redraw();
            this.Invalidate();

        }
      
        private void main_form_Shown(object sender, EventArgs e)
        {
#if USE_DEBUG
            debug d = new debug();
            d.Activate();
            d.BringToFront();
            d.Show();
#endif
        }


        private void nUD_AGVs_ValueChanged(object sender, EventArgs e)
        {
            int starts_counter = 0;
            int[,] starts_position = new int[2, Convert.ToInt32(nUD_AGVs.Value) + 1]; //keeps the size of the array +1 in relation with the nUD

            for (int widthTrav = 0; widthTrav < width; widthTrav++)
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {
                    if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                    {
                        starts_position[0, starts_counter] = widthTrav;
                        starts_position[1, starts_counter] = heightTrav;
                        starts_counter++;
                    }
                    if (starts_counter > nUD_AGVs.Value)
                    {
                        m_rectangles[starts_position[0, starts_counter - 1]][starts_position[1, starts_counter - 1]].SwitchEnd_StartToNormal(); //removes the very last

                        this.Invalidate();
                    }
                }
        }

        private void main_form_MouseClick(object sender, MouseEventArgs e)
        {

            Point click_coords = new Point(e.X, e.Y);
            if (!isvalid(click_coords) || e.Button != MouseButtons.Left || nUD_AGVs.Value == 0)
                return;

            if (rb_load.Checked )
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                    {
                        if (m_rectangles[widthTrav][heightTrav].boxRec.IntersectsWith(new Rectangle(e.Location, new Size(1, 1))))
                        {
                            m_lastBoxType = m_rectangles[widthTrav][heightTrav].boxType;
                            m_lastBoxSelect = m_rectangles[widthTrav][heightTrav];
                            switch (m_lastBoxType)
                            {
                                case BoxType.Normal:
                                    loads++;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 1;
                                    this.Invalidate();
                                    break;
                                case BoxType.Load:
                                    loads--;
                                    m_rectangles[widthTrav][heightTrav].SwitchLoad();
                                    isLoad[widthTrav, heightTrav] = 2;
                                    this.Invalidate();
                                    break;
                                case BoxType.Wall:
                                case BoxType.Start:
                                case BoxType.End:
                                    break;
                            }
                            Redraw();
                        }


                    }
                }
            }
            if (rb_start.Checked)
            {

                if (nUD_AGVs.Value == 1)//Saves only the last Double Click position to place the Start (1 start exists)
                {
                    for (int widthTrav = 0; widthTrav < width; widthTrav++)
                        for (int heightTrav = 0; heightTrav < height; heightTrav++)
                            if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                                m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();
                }
                else if (nUD_AGVs.Value > 1)//Deletes the start with the smallest iX - iY coords and keeps the rest
                {
                    int starts_counter = 0;
                    int[,] starts_position = new int[2, Convert.ToInt32(nUD_AGVs.Value)];


                    for (int widthTrav = 0; widthTrav < width; widthTrav++)
                        for (int heightTrav = 0; heightTrav < height; heightTrav++)
                        {
                            if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.Start)
                            {
                                starts_position[0, starts_counter] = widthTrav;
                                starts_position[1, starts_counter] = heightTrav;
                                starts_counter++;


                            }
                            if (starts_counter == nUD_AGVs.Value)
                            {
                                m_rectangles[starts_position[0, 0]][starts_position[1, 0]].SwitchEnd_StartToNormal();
                            }
                        }

                }


                //Converts the clicked box to Start point
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                            &&
                            m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal)
                        {
                            m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + topBarOffset, BoxType.Start);
                            //redraws the path if start is moved to another box
                            for (int i = 0; i < width; i++)
                                for (int j = 0; j < height; j++)
                                    if (m_rectangles[i][j].boxType == BoxType.End)
                                        Redraw();
                        }


            }
            //same for Stop
            if (rb_stop.Checked )
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxType == BoxType.End)
                            m_rectangles[widthTrav][heightTrav].SwitchEnd_StartToNormal();


                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                    for (int heightTrav = 0; heightTrav < height; heightTrav++)
                        if (m_rectangles[widthTrav][heightTrav].boxRec.Contains(click_coords)
                             &&
                            m_rectangles[widthTrav][heightTrav].boxType == BoxType.Normal)
                        {
                            m_rectangles[widthTrav][heightTrav] = new GridBox(widthTrav * 20, heightTrav * 20 + topBarOffset, BoxType.End);
                            for (int i = 0; i < width; i++)
                                for (int j = 0; j < height; j++)
                                    if (m_rectangles[i][j].boxType == BoxType.Start)
                                        Redraw();
                        }
            }
            this.Invalidate();
        }
       
        //parametres
        private void useRecursiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            useRecursive = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();

        }

        private void crossAdjacentPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            crossAdjacent = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();
        }

        private void crossCornerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            crossCorners = (sender as ToolStripMenuItem).Checked;
            updateParameters();
            Redraw();
        }

        //heurestic mode
        private void manhattanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.MANHATTAN;
            euclideanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.EUCLIDEAN;
            manhattanToolStripMenuItem.Checked = false;
            chebyshevToolStripMenuItem.Checked = false;
        }

        private void chebyshevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                return;
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            mode = HeuristicMode.CHEBYSHEV;
            manhattanToolStripMenuItem.Checked = false;
            euclideanToolStripMenuItem.Checked = false;
        }

        private void stepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;

            if (sender as ToolStripMenuItem == dotsToolStripMenuItem)
                showDots = dotsToolStripMenuItem.Checked;
            else if (sender as ToolStripMenuItem == linesToolStripMenuItem)
                showLine = linesToolStripMenuItem.Checked;
            else if (sender as ToolStripMenuItem == stepsToolStripMenuItem)
                showSteps = stepsToolStripMenuItem.Checked;
           
            Redraw();
            this.Invalidate();
            
        }

        private void borderColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cd_grid.ShowDialog() == DialogResult.OK)
                this.BackColor = cd_grid.Color;
        }

        private void wallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nUD_AGVs.Value != 0)
            {
                for (int agv = 0; agv < nUD_AGVs.Value; agv++)
                {
                    myresultList[agv].Clear();
                }
            }

            for (int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                for (int heightTrav = 0; heightTrav < height; heightTrav++)
                {
                    switch (m_rectangles[widthTrav][heightTrav].boxType)
                    {
                        case BoxType.Normal:
                        case BoxType.Start:
                        case BoxType.End:
                            break;
                        case BoxType.Wall:
                            m_rectangles[widthTrav][heightTrav].SetNormalBox();
                            break;
                    }
                }
            }
            this.Invalidate();
            Redraw();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exportMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export();
        }

        private void importMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            import();
        }

        private void importPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ofd_importpic.Filter = "png picture (*.png)|*.png";
            ofd_importpic.FileName = "";

            if (ofd_importpic.ShowDialog() == DialogResult.OK)
                this.BackgroundImage = Image.FromFile(ofd_importpic.FileName);
            else
                return;
        }

        private void showEmissionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Checked = !(sender as ToolStripMenuItem).Checked;
            CO2_label.Visible = CO_label.Visible = THC_label.Visible = Global_label.Visible = NOx_label.Visible = (sender as ToolStripMenuItem).Checked;
            
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nUD_AGVs.Value = getNumberOfAGVs();

            if (myresultList.Count == 0)
            {

                if (!menuPanel.Enabled)
                    menuPanel.Enabled = true;
                if (!settings_menu.Enabled)
                    settings_menu.Enabled = true;

                DialogResult result;
                result = MessageBox.Show(this, "No available path found.\r\nDo you wish to run without adding any elements?", "Empty layout", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    MessageBox.Show("That is not possible.\r\nPlease create a path first.", "Empty layout");
                else
                    MessageBox.Show("Then, please, create a path.", "Empty layout");

                return;
            }

            for (int i = 0; i < fromstart.Length; i++)
                fromstart[i] = true;

            markedbyagv = new Point[pos.Count];
            Redraw();
            AGVs = new Vehicle[pos.Count];
            
            
            for (int i = 0; i < pos.Count; i++)
            {
                //initialization of each AGV location
                AGVs[i] = new Vehicle(this,
                                      m_rectangles[pos[i].x][pos[i].y].boxRec.X, //real form X-coordinates
                                      m_rectangles[pos[i].x][pos[i].y].boxRec.Y, //real form Y-coordinates
                                      18, 18);
            }
            

            

            timer_counter = new int[pos.Count];
            timers(pos.Count);

            settings_menu.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void increaseSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = timer5.Interval = timer1.Interval + 100;
            refresh_label.Text = "Delay:" + timer1.Interval + " ms";
        }

        private void decreaseSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Interval == 100)
                return;

            timer1.Interval = timer2.Interval = timer3.Interval = timer4.Interval = timer5.Interval = timer1.Interval - 100;
            refresh_label.Text = "Delay:" + timer1.Interval + " ms";
           
        }

        private void implementGoogleMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            gmaps maps = new gmaps();
            maps.setFormSize(this.Width, this.Height);
            maps.ShowDialog();

        }
    }

}
