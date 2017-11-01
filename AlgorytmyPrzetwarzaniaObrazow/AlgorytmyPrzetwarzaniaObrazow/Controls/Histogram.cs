using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorytmyPrzetwarzaniaObrazow.Controls
{
    public partial class Histogram : UserControl
    {
        private int[,] values = new int[1, 256];
        ImageForm image = null;
        private Color bg;
        const int offset = 4;
        int max = 0;
        int selected = -1;
        int scale = 256;
        int barWidth = 1;
        bool editable = false;

        public delegate void ModifiedEventHandler(Histogram histogram);
        public event ModifiedEventHandler Modified;
        public delegate void ValueChangedEventHandler(Histogram histogram, int index);
        public event ValueChangedEventHandler ValueChanged;

        public int[,] Values
        {
            get { return values; }
            set
            {
                values = value;
                Build();
            }
        }

        public bool Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                if (editable)
                {
                    //   trackBar.Visible = false;
                    //   numericUpDownHeight.Visible = false;
                    //   labelSkala.Visible = false;
                }
                else
                {
                    //   trackBar.Visible = true;
                    //  numericUpDownHeight.Visible = true;
                    //  labelSkala.Visible = true;
                }
            }
        }

        public int BarScale
        {
            get { return scale; }
            set
            {
                scale = value;
                //   trackBar.Value = scale;
                //   numericUpDownHeight.Value = scale;
            }
        }

        public Histogram()
        {
            InitializeComponent();
            bg = this.BackColor;
            Size = new Size(256 + offset, 256 + offset);
        }

        public Histogram(ImageForm image)
        {
            InitializeComponent();
            this.image = image;
            bg = this.BackColor;
            Size = new Size(256 + offset, 256 + offset);
            //   trackBar.Value = scale;
            //   numericUpDownHeight.Value = scale;
            labelLevels.Text = image.Image.Levels.ToString();
        }

        public void Build()
        {
            if (image != null)
            {
                FastBitmap bmp = image.Image;
                values = new int[1, bmp.Levels];

                for (int x = 0; x < bmp.Width; x++)
                    for (int y = 0; y < bmp.Height; y++)
                        values[0, bmp[x, y]]++;
            }

            max = 0;
            for (int i = 0; i < values.Length; i++)
                max = Math.Max(values[0, i], max);

            if (values.Length > 128) barWidth = 2;
            else if (values.Length > 64) barWidth = 3;
            else if (values.Length > 32) barWidth = 4;
            else if (values.Length > 16) barWidth = 8;
            else if (values.Length > 8) barWidth = 16;
            else if (values.Length > 4) barWidth = 32;
            else if (values.Length > 2) barWidth = 64;
            else barWidth = 128;

            ClientSize = new Size(offset + barWidth * values.Length + 80, 300);
            bufferedPanel.ClientSize = new Size(offset + barWidth * values.Length + 5, 270);
            //   trackBar.Width = ClientSize.Width - 70;
            //    numericUpDownHeight.Location = new Point(ClientSize.Width - 5 - numericUpDownHeight.Width, trackBar.Location.Y);
            labelPos.Location = new Point(ClientSize.Width - 5 - labelPos.Width, labelPos.Location.Y);
            labelValue.Location = new Point(ClientSize.Width - 5 - labelValue.Width, labelValue.Location.Y);
            labelLevels.Location = new Point(ClientSize.Width - 5 - labelLevels.Width, labelLevels.Location.Y);
            label1.Location = new Point(ClientSize.Width - 5 - label1.Width, label1.Location.Y);
            label2.Location = new Point(ClientSize.Width - 5 - label2.Width, label2.Location.Y);
            label3.Location = new Point(ClientSize.Width - 5 - label3.Width, label3.Location.Y);
            // labelSkala.Location = new Point(ClientSize.Width - 5 - labelSkala.Width, labelSkala.Location.Y);

            labelLevels.Text = (values.Length - 1).ToString();

            bufferedPanel.Refresh();
        }

        public int Average
        {
            get
            {
                int average = 0;
                for (int i = 0; i < values.Length; i++)
                    average += values[0, i];
                return average / values.Length;
            }
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            //  scale = trackBar.Value;
            //  numericUpDownHeight.Value = scale;
            bufferedPanel.Refresh();
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            //  scale = (int)numericUpDownHeight.Value;
            //trackBar.Value = scale;
            bufferedPanel.Refresh();
        }

        private void bufferedPanel_Paint(object sender, PaintEventArgs e)
        {
            int x = offset;
            int y = 256;

            Graphics graphics = e.Graphics;
            graphics.Clear(BackColor);

            graphics.DrawLine(Pens.Black, new Point(offset - 1, offset + y), new Point(offset + barWidth * values.Length, offset + y));
            graphics.DrawLine(Pens.Black, new Point(offset - 1, offset), new Point(offset - 1, offset + y));
            /*
            for (int i = 0; i < 256; i += 5)
            {
                int yy = Height;
                if ((i % 5 == 0) && (i % 10 != 0)) yy -= 2;
                graphics.DrawLine(Pens.Black, new Point(offset + i, Height - offset + 1), new Point(offset + i, yy + offset));
            }
            */
            if (max > 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Brush brush = Brushes.CornflowerBlue;
                    if (i % 2 != 0) brush = Brushes.RoyalBlue;
                    if (i == selected)
                        brush = Brushes.Gold;
                    int height = (int)(values[0, i] * scale / max);
                    if (height > 0)
                        graphics.FillRectangle(brush, new Rectangle(x + i * barWidth, y - height + offset, barWidth, height));
                }
            }
        }

        private void bufferedPanel_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (e.X >= i * barWidth + offset && e.X < (i + 1) * barWidth + offset)
                {
                    selected = i;
                    labelPos.Text = selected.ToString();
                    labelValue.Text = values[0, selected].ToString();
                    break;
                }
            }

            if (editable && e.Button == MouseButtons.Left && selected >= 0)
                Edit(e.Y);

            bufferedPanel.Refresh();
        }

        private void bufferedPanel_Leave(object sender, EventArgs e)
        {
            labelPos.Text = string.Empty;
            labelValue.Text = string.Empty;
            selected = -1;
            bufferedPanel.Refresh();
        }

        public void Edit(int y)
        {
            int temp = 256 / values.Length;
            float value = 256 - (y - offset);
            if (value <= 256.0)
            {
                int val = (int)(value / temp);
                if (value < 0.0) val = 0;
                val = Math.Min(val, values.Length - 1);
                val = Math.Max(val, 0);
                values[0, selected] = val;
                bufferedPanel.Refresh();
                if (ValueChanged != null) ValueChanged(this, selected);
            }
        }

        public void Edit(int y, int x)
        {
            //MessageBox.Show("edit: "+ x.ToString() + ", " + y.ToString());
            selected = x;
            int temp = 256 / values.Length;
            float value = 256 - (y + 1);
            if (value <= 256.0)
            {
                int val = (int)(value / temp);
                if (value < 0.0) val = 0;
                val = Math.Min(val, values.Length - 1);
                val = Math.Max(val, 0);
                values[0, selected] = val;
                bufferedPanel.Refresh();
                if (ValueChanged != null) ValueChanged(this, selected);
            }
        }

        private void bufferedPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (editable && e.Button == MouseButtons.Left && selected >= 0)
                Edit(e.Y);
        }

        private void bufferedPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (Modified != null) Modified(this);
        }
    }
}
