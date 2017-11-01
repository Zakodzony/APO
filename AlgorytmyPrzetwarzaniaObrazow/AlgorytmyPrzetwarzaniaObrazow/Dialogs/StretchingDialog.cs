using AlgorytmyPrzetwarzaniaObrazow.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorytmyPrzetwarzaniaObrazow.Dialogs
{

    public partial class StretchingDialog : Form
    {
        FastBitmap orgBmp;
        FastBitmap newBmp;

        public StretchingDialog(FastBitmap bitmap)
        {
            InitializeComponent();
            orgBmp = bitmap;
            panelOrg.Size = orgBmp.Size;
            panelOrg.Location = new Point(0, 0);
            //if (orgBmp.Width < panelOrgContainer.ClientSize.Width)
            //    panelOrg.Left = (panelOrgContainer.ClientSize.Width - orgBmp.Width) / 2;
            //if (orgBmp.Height < panelOrgContainer.ClientSize.Height)
            //    panelOrg.Top = (panelOrgContainer.ClientSize.Height - orgBmp.Height) / 2;

            newBmp = (FastBitmap)bitmap.Clone();
            panelNew.Size = newBmp.Size;
            panelNew.Location = panelOrg.Location;
            panelNew.Left = panelOrg.Left;
            panelNew.Top = panelOrg.Top;

            textBox1.Text = trackBar1.Value.ToString();
            textBox2.Text = trackBar2.Value.ToString();

            trackBar1.Maximum = bitmap.Levels - 1;
            trackBar2.Maximum = bitmap.Levels - 1;
        }

        public FastBitmap Bitmap
        {
            get { return newBmp; }
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelOrg_Paint(object sender, PaintEventArgs e)
        {
            orgBmp.Draw(e.Graphics, 0, 0);
        }

        private void panelNew_Paint(object sender, PaintEventArgs e)
        {
            newBmp.Draw(e.Graphics, 0, 0);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            newBmp = (FastBitmap)orgBmp.Clone();
            if (trackBar2.Value < trackBar1.Value)
                trackBar2.Value = trackBar1.Value + 1;

            newBmp = Cwiczenie2.Rozciaganie(newBmp, trackBar1.Value, trackBar2.Value);
            panelNew.Invalidate();
        }
    }
}
