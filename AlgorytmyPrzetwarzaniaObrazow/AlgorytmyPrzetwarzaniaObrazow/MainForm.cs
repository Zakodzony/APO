using AlgorytmyPrzetwarzaniaObrazow.Dialogs;
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

namespace AlgorytmyPrzetwarzaniaObrazow
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        internal void OnImageClose()
        {
            //throw new NotImplementedException();
        }

        private void CreateImage(FastBitmap bmp)
        {
            ImageForm form = new ImageForm(bmp);
            form.MdiParent = this;
            form.Modified += new ImageForm.ModifiedEventDelegate(form_Modified);
            OnImageOpen(form);
            form.Show();
        }

        private void CreateImage(Size size, int levels)
        {
            ImageForm form = new ImageForm(size, levels);
            form.MdiParent = this;
            form.Modified += new ImageForm.ModifiedEventDelegate(form_Modified);
            OnImageOpen(form);
            form.Show();
        }

        public void CreateImage(ImageForm form)
        {
            Rectangle rect = form.CropRectangle;
            rect.Width++;
            rect.Height++;
            ImageForm newForm = new ImageForm(form, rect);
            newForm.MdiParent = this;
            newForm.Modified += new ImageForm.ModifiedEventDelegate(form_Modified);
            OnImageOpen(newForm);
            newForm.Show();
        }

        private void form_Modified(ImageForm form)
        {
            //throw new NotImplementedException();
        }

        private void OnImageOpen(ImageForm newForm)
        {
            //throw new NotImplementedException();
        }

        private void CreateImage(string filename)
        {
            ImageForm form = new ImageForm(filename);
            form.MdiParent = this;
            form.Modified += new ImageForm.ModifiedEventDelegate(form_Modified);
            OnImageOpen(form);
            form.Show();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                foreach (string filename in openFileDialog1.FileNames)
                {
                    CreateImage(filename);
                }
                Cursor = Cursors.Default;
            }
        }

        private void losowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie1.Wyrownanie(form.Image, Cwiczenie1.Metody.Losowa);
            Cursor = Cursors.Default;
        }

        private void sasiedztwaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie1.Wyrownanie(form.Image, Cwiczenie1.Metody.Sasiedztwa);
            Cursor = Cursors.Default;
        }

        private void srednichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie1.Wyrownanie(form.Image, Cwiczenie1.Metody.Srednich);
            Cursor = Cursors.Default;
        }

        private void negacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie2.Negacja(form.Image);
            Cursor = Cursors.Default;
        }

        private void progowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            ComparisonDialog dialog = new ComparisonDialog(form.Image, Operacje.Progowanie);
            if (dialog.ShowDialog() == DialogResult.OK)
                form.Image = dialog.Bitmap;
        }

        private void bit1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie2.Redukcja(form.Image, 2);
            Cursor = Cursors.Default;
        }

        private void bity4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie2.Redukcja(form.Image, 16);
            Cursor = Cursors.Default;
        }

        private void bitow7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            Cursor = Cursors.WaitCursor;
            form.Image = Cwiczenie2.Redukcja(form.Image, 128);
            Cursor = Cursors.Default;
        }

        private void rozciaganieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            StretchingDialog dialog = new StretchingDialog(form.Image);
            if (dialog.ShowDialog() == DialogResult.OK)
                form.Image = dialog.Bitmap;
        }

        private void houghToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ImageForm form = (ImageForm)ActiveMdiChild;
            HoughDialog dialog = new HoughDialog((Bitmap)form.Image.Bitmap.Clone());
            dialog.ShowDialog();
        }
    }
}
