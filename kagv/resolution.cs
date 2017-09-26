/*!
The MIT License (MIT)

Copyright (c) 2017 Dimitris Katikaridis <dkatikaridis@gmail.com>,Giannis Menekses <johnmenex@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Windows.Forms;

namespace kagv {
    public partial class resolution : Form {
        public resolution() {
            InitializeComponent();
        }

        private void resolution_Load(object sender, EventArgs e) {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.CenterToScreen();
            tb_res.Value = Constants.__ResolutionMultiplier;

            if (tb_res.Value == 1) lb_multiplier.Text = "Multiplier: " + tb_res.Value + " (Default)";
            else lb_multiplier.Text = "Multiplier: " + tb_res.Value;
        }

        private void tb_res_Scroll(object sender, EventArgs e) {
            if (tb_res.Value == 1) lb_multiplier.Text = "Multiplier: " + tb_res.Value +" (Default)";
            else lb_multiplier.Text = "Multiplier: " + tb_res.Value;
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            Constants.__ResolutionMultiplier = tb_res.Value;
        }
    }
}
