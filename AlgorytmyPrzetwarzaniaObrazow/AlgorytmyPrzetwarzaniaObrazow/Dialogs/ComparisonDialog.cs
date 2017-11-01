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
    public enum Operacje
    {
        Progowanie,
        Binaryzacja,
        Redukcja,
        Jasnosc,
        Kontrast,
        Mixer
    }

    public partial class ComparisonDialog : Form
    {
        Operacje operacja;
        FastBitmap orgBmp;
        FastBitmap newBmp;

        public ComparisonDialog(FastBitmap bitmap, Operacje operacja)
        {
            InitializeComponent();
            this.operacja = operacja;

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

            switch (operacja)
            {
                case Operacje.Progowanie:
                    slider.Minimum = 0;
                    slider.Maximum = newBmp.Levels - 1;
                    slider.Value = newBmp.Levels / 2 - 1;
                    this.Text = "Progowanie";
                    newBmp = Cwiczenie2.Progowanie(newBmp, slider.Value);
                    break;

                case Operacje.Binaryzacja:
                    slider.Minimum = 0;
                    slider.Maximum = newBmp.Levels - 1;
                    slider.Value = newBmp.Levels / 2 - 1;
                    this.Text = "Binaryzacja";
                    newBmp = Cwiczenie2.Binaryzacja(newBmp, slider.Value);
                    break;

                case Operacje.Redukcja:
                    slider.Minimum = 2;
                    slider.Maximum = newBmp.Levels;
                    slider.Value = newBmp.Levels;
                    this.Text = "Redukcja poziomów szarości";
                    newBmp = Cwiczenie2.Redukcja(newBmp, slider.Value);
                    break;

                case Operacje.Jasnosc:
                    slider.Minimum = -255;
                    slider.Maximum = 255;
                    slider.Value = 0;
                    this.Text = "Jasność";
                    newBmp = Cwiczenie2.Jasnosc(newBmp, slider.Value);
                    break;

                case Operacje.Kontrast:
                    slider.Minimum = -100;
                    slider.Maximum = 100;
                    slider.Value = 0;
                    this.Text = "Kontrast";
                    newBmp = Cwiczenie2.Kontrast(newBmp, slider.Value);
                    break;
            }

            textBox.Text = slider.Value.ToString();
        }

        public FastBitmap Bitmap
        {
            get { return newBmp; }
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            textBox.Text = slider.Value.ToString();
        }

        private void slider_MouseUp(object sender, MouseEventArgs e)
        {
            int levels = orgBmp.Levels;
            if (operacja == Operacje.Redukcja) levels = slider.Value;
            newBmp = (FastBitmap)orgBmp.Clone();

            switch (operacja)
            {
                case Operacje.Progowanie:
                    newBmp = Cwiczenie2.Progowanie(newBmp, slider.Value);
                    break;

                case Operacje.Binaryzacja:
                    newBmp = Cwiczenie2.Binaryzacja(newBmp, slider.Value);
                    break;

                case Operacje.Redukcja:
                    newBmp = Cwiczenie2.Redukcja(newBmp, slider.Value);
                    break;

                case Operacje.Jasnosc:
                    newBmp = Cwiczenie2.Jasnosc(newBmp, slider.Value);
                    break;

                case Operacje.Kontrast:
                    newBmp = Cwiczenie2.Kontrast(newBmp, slider.Value);
                    break;
            }
            panelNew.Refresh();
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int levels = orgBmp.Levels;
            if (operacja == Operacje.Redukcja) levels = slider.Value;
            newBmp = (FastBitmap)orgBmp.Clone();

            switch (operacja)
            {
                case Operacje.Progowanie:
                    newBmp = Cwiczenie2.Progowanie(newBmp, slider.Value);
                    break;

                case Operacje.Binaryzacja:
                    newBmp = Cwiczenie2.Binaryzacja(newBmp, slider.Value);
                    break;

                case Operacje.Redukcja:
                    newBmp = Cwiczenie2.Redukcja(newBmp, slider.Value);
                    break;

                case Operacje.Jasnosc:
                    newBmp = Cwiczenie2.Jasnosc(newBmp, slider.Value);
                    break;

                case Operacje.Kontrast:
                    newBmp = Cwiczenie2.Kontrast(newBmp, slider.Value);
                    break;
            }
        }

        private void textBox_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
