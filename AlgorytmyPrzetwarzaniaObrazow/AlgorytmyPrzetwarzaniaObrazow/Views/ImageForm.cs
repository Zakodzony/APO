using AlgorytmyPrzetwarzaniaObrazow.Controls;
using AlgorytmyPrzetwarzaniaObrazow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace AlgorytmyPrzetwarzaniaObrazow
{
    public partial class ImageForm : Form
    {
        FastBitmap bmp;
        FastBitmap zoomBmp;
        Bitmap overlayBmp;
        string path;
        int zoomLevel = 1;
        int prevZoomLevel = 1;
        int drawZoomLevel = 1;
        ImageGrid grid;
        Histogram histogram;
        bool modified = false;
        private static int count = 0;
        private Rectangle cropRect = new Rectangle(0, 0, 0, 0);
        Point cropPoint = new Point(-1, -1);
        string tip = "";
        int val = 0;
        Point point;

        //************************************************************************
        Graphics wykresGraphics;
        List<Point> liniaProfilu = new List<Point>();
        List<int> punktyLiniiProfilu = new List<int>();
        List<Point> punktyLiniiProfiluXY = new List<Point>();
        int lmax, lmin, srednia;

        //************************************************************************//
        public delegate void ModifiedEventDelegate(ImageForm form);
        public event ModifiedEventDelegate Modified;

        public ImageForm()
        {
            InitializeComponent();
        }

        private void init()
        {
            zoomBmp = (FastBitmap)bmp.Clone();

            SetSize(bmp.Size);

            grid = new ImageGrid();
            tabPageTablica.Controls.Add(grid);
            grid.FixedColumns = 1;
            grid.FixedRows = 1;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.Dock = DockStyle.Fill;
            grid.DataSource = bmp;

            histogram = new Histogram(this);
            tabPageHistogram.Controls.Add(histogram);
            tabPageHistogram.ClientSize = histogram.ClientSize;
            histogram.Build();

            overlayBmp = new Bitmap(zoomBmp.Width, zoomBmp.Height);
            cropRect = new Rectangle();

            // StatusLabel.Text = "Wymiary obrazu: " + bmp.Width + "x" + bmp.Height;

            //************************************************************************
            wykresProfiluPictureBox.Image = new Bitmap(512, 256);
            wykresGraphics = Graphics.FromImage(wykresProfiluPictureBox.Image);
            liniaProfilu.Add(new Point(-1, -1));
            liniaProfilu.Add(new Point(-1, -1));
            //************************************************************************//

            graphicsPanel.Location = new Point(Math.Max((splitContainer1.Panel1.Width - graphicsPanel.Width - 10) / 2, 0), Math.Max((splitContainer1.Panel1.Height - graphicsPanel.Height - 31) / 2, 0));
        }

        public ImageForm(Size size, int levels)
        {
            InitializeComponent();
            path = "Obraz" + ++count;
            Text = path + "*";
            bmp = new FastBitmap(size.Width, size.Height, levels);
            bmp.PixelChanged += new FastBitmap.PixelChangedDelegate(bmp_PixelChanged);
            init();
        }

        public ImageForm(FastBitmap bmp)
        {
            InitializeComponent();
            path = "Obraz" + ++count;
            Text = path + "*";
            this.bmp = bmp;
            this.bmp.PixelChanged += new FastBitmap.PixelChangedDelegate(bmp_PixelChanged);
            init();
        }

        public ImageForm(string fileName)
        {
            InitializeComponent();
            path = fileName;
            Text = Path.GetFileName(path);

            StreamReader reader = new StreamReader(fileName);
            Bitmap bmpTemp = (Bitmap)Bitmap.FromStream(reader.BaseStream);
            reader.Close();
            bmp = new FastBitmap(bmpTemp);
            bmp.PixelChanged += new FastBitmap.PixelChangedDelegate(bmp_PixelChanged);

            init();
        }

        public ImageForm(ImageForm form, Rectangle src)
        {
            InitializeComponent();
            path = "Obraz" + ++count;
            Text = path + "*";
            Rectangle rect = new Rectangle(src.X / form.zoomLevel, src.Y / form.zoomLevel, src.Width / form.zoomLevel, src.Height / form.zoomLevel);
            bmp = new FastBitmap(form.Image, rect);
            bmp.PixelChanged += new FastBitmap.PixelChangedDelegate(bmp_PixelChanged);

            init();
        }

        void bmp_PixelChanged(Point position)
        {
            zoomBmp[position.X, position.Y] = bmp[position.X, position.Y];
            modified = true;
            if (Modified != null)
                Modified(this);

            if (bmp.Levels >= 4)
            {
                label_4.Text = bmp.Levels - 1 + " _";
                label_2.Text = (bmp.Levels / 4 * 2) - 1 + " _";
            }
            else
            {
                label_4.Text = bmp.Levels - 1 + " _";
            }

        }

        private void SetSize(Size size)
        {
            graphicsPanel.Size = size;
            int width = Math.Max(graphicsPanel.Width, 256);
            int height = Math.Max(graphicsPanel.Height, 256);
            Size diff = tabControl.ClientSize - tabPageObraz.ClientSize;
            tabPageObraz.ClientSize = new Size(width, height);
            if (diff.Height > 0)
            {
                tabControl.ClientSize = tabPageObraz.ClientSize + diff;
                tabPageTablica.ClientSize = tabPageObraz.ClientSize + diff;
            }
            else
            {
                tabControl.ClientSize = tabPageObraz.ClientSize;
                tabPageTablica.ClientSize = tabPageObraz.ClientSize;
            }
            graphicsPanel.Left = Math.Max((tabPageObraz.ClientSize.Width - graphicsPanel.Width) / 2, 0);
            graphicsPanel.Top = Math.Max((tabPageObraz.ClientSize.Height - graphicsPanel.Height) / 2, 0);
        }

        public bool IsModified
        {
            get { return modified; }
        }

        public FastBitmap Image
        {
            get { return bmp; }
            set
            {
                bmp = value;
                SetZoom();
                graphicsPanel.Refresh();
                grid.DataSource = bmp;

                // if (tabControl.SelectedIndex == 2)
                {
                    Size diff = tabControl.ClientSize - tabPageHistogram.ClientSize;
                    histogram.Build();
                    tabControl.ClientSize = histogram.ClientSize + diff;
                }

                Text = Text.TrimEnd('*') + "*";
                modified = true;
                if (Modified != null)
                    Modified(this);
            }
        }

        public string FileName
        {
            get { return Path.GetFileName(path); }
        }

        public Rectangle CropRectangle
        {
            get { return cropRect; }
        }

        //******************************************************************
        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            zoomBmp.Draw(e.Graphics, 0, 0, graphicsPanel.Width, graphicsPanel.Height);
            //bmp.Draw(e.Graphics, 0, 0, graphicsPanel.Width, graphicsPanel.Height);

            if (cropRect.Width > 0 && cropRect.Height > 0)
            {
                Graphics g = Graphics.FromImage(overlayBmp);
                g.Clear(Color.Transparent);
                g.DrawRectangle(new Pen(Color.FromArgb(0, 255, 0)), cropRect);
                g.Dispose();
                e.Graphics.DrawImageUnscaled(overlayBmp, 0, 0);
            }

            if (liniaProfilu[0].X > -1 && liniaProfilu[0].Y > -1)
            {
                Graphics g = Graphics.FromImage(overlayBmp);
                g.Clear(Color.Transparent);
                g.DrawLine(new Pen(Color.FromArgb(255, 0, 0), 2), liniaProfilu[0], liniaProfilu[1]); // rysowanie czerwonej linii
                g.Dispose();
                e.Graphics.DrawImageUnscaled(overlayBmp, 0, 0);
            }
        }
        //*****************************************************************************//
        private void ImageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parent = (MainForm)this.Parent.Parent;
            parent.OnImageClose();
        }

        public void UpdatePixel(int x, int y)
        {
            int xx = zoomLevel * (x - 1);
            int yy = zoomLevel * (y - 1);
            for (int i = 0; i < zoomLevel; i++)
                for (int j = 0; j < zoomLevel; j++)
                    zoomBmp[xx + i, yy + j] = bmp[x - 1, y - 1];
            graphicsPanel.Refresh();
        }

        public void Save()
        {
            Save(this.path);
        }

        public void Save(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            ImageFormat format;
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".tiff":
                    format = ImageFormat.Tiff;
                    break;
                default:
                    format = ImageFormat.Bmp;
                    break;
            }
            bmp.Save(writer.BaseStream, format);
            writer.Close();
            this.path = filePath;
            Text = filePath;
            modified = false;
        }

        public void SetZoom()
        {
            if (zoomLevel < 1)
            {
                zoomLevel = 1;
                return;
            }

            zoomBmp = new FastBitmap(bmp.Width * zoomLevel, bmp.Height * zoomLevel, bmp.Levels);
            overlayBmp = new Bitmap(zoomBmp.Width, zoomBmp.Height);

            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    for (int k = 0; k < zoomLevel; k++)
                        for (int l = 0; l < zoomLevel; l++)
                        {
                            zoomBmp[i * zoomLevel + k, j * zoomLevel + l] = bmp[i, j];
                            overlayBmp.SetPixel(i * zoomLevel + k, j * zoomLevel + l, Color.FromArgb(bmp[i, j], bmp[i, j], bmp[i, j]));
                        }

            SetSize(new Size(zoomBmp.Width, zoomBmp.Height));
            graphicsPanel.Invalidate();

            liniaProfilu[0] = new Point(((int)(liniaProfilu[0].X * zoomLevel / (double)prevZoomLevel)) / zoomLevel * zoomLevel + zoomLevel / 2, ((int)(liniaProfilu[0].Y * zoomLevel / (double)prevZoomLevel)) / zoomLevel * zoomLevel + zoomLevel / 2);
            liniaProfilu[1] = new Point(((int)(liniaProfilu[1].X * zoomLevel / (double)prevZoomLevel)) / zoomLevel * zoomLevel + zoomLevel / 2, ((int)(liniaProfilu[1].Y * zoomLevel / (double)prevZoomLevel)) / zoomLevel * zoomLevel + zoomLevel / 2);

            cropRect = new Rectangle(cropRect.X * zoomLevel / prevZoomLevel, cropRect.Y * zoomLevel / prevZoomLevel, cropRect.Width * zoomLevel / prevZoomLevel, cropRect.Height * zoomLevel / prevZoomLevel);
            if (zoomLevel > prevZoomLevel && cropRect.Width > 0 && cropRect.Height > 0 && cropPoint.X != -1)     // && cropPoint.X != -1)
            {
                cropRect.Width += zoomLevel - prevZoomLevel;
                cropRect.Height += zoomLevel - prevZoomLevel;
            }

            graphicsPanel.Refresh();

            prevZoomLevel = zoomLevel;
            RysujWykres();
        }

        private void ImageForm_Resize(object sender, EventArgs e)
        {
            graphicsPanel.Left = Math.Max((tabPageObraz.ClientSize.Width - graphicsPanel.Width) / 2, 0);
            graphicsPanel.Top = Math.Max((tabPageObraz.ClientSize.Height - graphicsPanel.Height) / 2, 0);
            label_0.Location = new Point(label_0.Location.X, wykresProfiluPictureBox.Height);
            label_2.Location = new Point(label_2.Location.X, wykresProfiluPictureBox.Height / 2);
            label_max.Location = new Point(label_max.Location.X, Convert.ToInt32(wykresProfiluPanel.Height - lmax * (wykresProfiluPanel.Height / 256.0) - 10));
            label_min.Location = new Point(label_min.Location.X, Convert.ToInt32(wykresProfiluPanel.Height - lmin * (wykresProfiluPanel.Height / 256.0) - 10));
            RysujWykres();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0 || tabControl.SelectedIndex == 1)
            {
                SetSize(bmp.Size);
            }
            else if (tabControl.SelectedIndex == 2)
            {
                Size diff = tabControl.ClientSize - tabPageHistogram.ClientSize;
                histogram.Build();
                tabControl1.ClientSize = histogram.ClientSize + diff;
            }
        }


        //******************************************************************      
        private void graphicsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (point == e.Location) return;
            point = e.Location;

            if (e.Button == MouseButtons.Left && cropPoint.X > -1 && cropPoint.Y > -1)
            {
                //int cropXaddon = 0;
                //int cropYaddon = 0;

                int x = e.X;
                if (x < 0) x = 0;
                else if (x >= zoomBmp.Width) x = zoomBmp.Width - 1;

                int y = e.Y;
                if (y < 0) y = 0;
                else if (y >= zoomBmp.Height) y = zoomBmp.Height - 1;

                cropRect.Width = Math.Abs((x - cropPoint.X + 1) / zoomLevel * zoomLevel) - 1;
                //if (Math.Abs(x - (x / zoomLevel * zoomLevel)) > zoomLevel / 2)
                //    cropRect.Width += cropXaddon = zoomLevel;

                //cropRect.Width = Math.Abs((x - cropPoint.X + 1) / zoomLevel * zoomLevel);
                //cropRect.Width = (int)(cropRect.Width + zoomLevel / (double)(Math.Abs(x - (double)x / zoomLevel * zoomLevel)) + 0.5);

                cropRect.Height = Math.Abs((y - cropPoint.Y + 1) / zoomLevel * zoomLevel) - 1;
                //if (Math.Abs(y - (y / zoomLevel * zoomLevel)) > zoomLevel / 2)
                //    cropRect.Height += cropYaddon = zoomLevel;

                if (x < cropPoint.X)    // jesli rysujesz od prawej do lewej
                {
                    cropRect.Width += 2 * zoomLevel;
                    cropRect.X = x / zoomLevel * zoomLevel;
                }
                else
                {
                    if (zoomBmp.Width - cropRect.Width - 1 > cropPoint.X)
                        cropRect.Width += zoomLevel;
                    else
                        cropRect.Width = zoomBmp.Width - cropPoint.X - 1;
                    cropRect.X = cropPoint.X / zoomLevel * zoomLevel;
                }
                if (y < cropPoint.Y)    // jesli rysujesz od dolu do gory
                {
                    cropRect.Height += 2 * zoomLevel;
                    cropRect.Y = y / zoomLevel * zoomLevel;
                }
                else
                {
                    if (zoomBmp.Height - cropRect.Height - 1 > cropPoint.Y)
                        cropRect.Height += zoomLevel;
                    else
                        cropRect.Height = zoomBmp.Height - cropPoint.Y - 1;
                    cropRect.Y = cropPoint.Y / zoomLevel * zoomLevel;
                }

                graphicsPanel.Refresh();
            }

            else if ((e.Button == MouseButtons.Right && liniaProfilu[0].X > -1 && liniaProfilu[0].Y > -1)) //jesli prawy przycisk myszy to:
            {
                int x = e.X;
                if (x < 0) x = 0;
                else if (x >= zoomBmp.Width) x = zoomBmp.Width - 1;

                int y = e.Y;
                if (y < 0) y = 0;
                else if (y >= zoomBmp.Height) y = zoomBmp.Height - 1;

                liniaProfilu[1] = new Point(x / zoomLevel * zoomLevel + zoomLevel / 2, y / zoomLevel * zoomLevel + zoomLevel / 2);

                //labelDlugosc.Text = "Długość linii profilu: " + (Math.Max(Math.Abs(liniaProfilu[0].X - liniaProfilu[1].X), Math.Abs(liniaProfilu[0].Y - liniaProfilu[1].Y)) + 1);

                graphicsPanel.Refresh();


                ///////////////////////////////
                punktyLiniiProfilu = new List<int>();
                punktyLiniiProfiluXY = new List<Point>();
                float b, c, ee, f;
                float A, B;

                int k = (256 / bmp.Levels);
                b = liniaProfilu[0].X / zoomLevel;
                c = liniaProfilu[0].Y / zoomLevel;
                ee = liniaProfilu[1].X / zoomLevel;
                f = liniaProfilu[1].Y / zoomLevel;

                //Linia nie pionowa
                if ((ee - b) != 0)
                {
                    B = (c * ee - b * f) / (ee - b);
                    A = (f - c) / (ee - b);

                    //funkcja rosnaca (linia rosnaca)
                    if (Math.Abs(liniaProfilu[0].X / zoomLevel - liniaProfilu[1].X / zoomLevel) > Math.Abs(liniaProfilu[0].Y / zoomLevel - liniaProfilu[1].Y / zoomLevel))
                        for (int i = Math.Min(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel); i <= Math.Max(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel); i++)
                        {

                            bmp.Unlock();
                            int kolor;
                            Point punkt = new Point(i, (int)(A * i + B));
                            //punktyLiniiProfiluXY.Add(punkt);
                            punktyLiniiProfiluXY.Add(new Point(punkt.X * zoomLevel, punkt.Y * zoomLevel));

                            if (punkt.X < 0) punkt.X = 0;
                            if (punkt.X > bmp.Width) punkt.X = bmp.Width - 1;
                            if (punkt.Y < 0) punkt.X = 0;
                            if (punkt.Y > bmp.Height) punkt.Y = bmp.Height - 1;

                            kolor = bmp.Bitmap.GetPixel(punkt.X, punkt.Y).R;
                            bmp.Lock();

                            if (i == Math.Min(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel))
                                lmax = lmin = srednia = kolor;
                            else
                            {
                                if (kolor > lmax) lmax = kolor;
                                if (kolor < lmin) lmin = kolor;
                                srednia += kolor;
                            }

                            punktyLiniiProfilu.Add(kolor);

                            //Tomek
                            if (((int)((A * (double)(double)i + 0.5) + B) == punkt.Y) && (A * (i + 1) + B != punkt.Y) && ((i + 1) <= Math.Max(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel)))
                            {
                                bmp.Unlock();
                                punkt = new Point(i + 1, (int)(A * i + B));
                                punktyLiniiProfiluXY.Add(new Point(punkt.X * zoomLevel, punkt.Y * zoomLevel));
                                if (punkt.X < 0) punkt.X = 0;
                                if (punkt.X > bmp.Width) punkt.X = bmp.Width - 1;
                                if (punkt.Y < 0) punkt.X = 0;
                                if (punkt.Y > bmp.Height) punkt.Y = bmp.Height - 1;
                                kolor = bmp.Bitmap.GetPixel(punkt.X, punkt.Y).R;
                                bmp.Lock();
                                if (i == Math.Min(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel))
                                    lmax = lmin = srednia = kolor;
                                else
                                {
                                    if (kolor > lmax) lmax = kolor;
                                    if (kolor < lmin) lmin = kolor;
                                    srednia += kolor;
                                }

                                punktyLiniiProfilu.Add(kolor);
                            }


                        }
                    //funkcja malejaca (linia malejaca)
                    else
                    {
                        for (int i = Math.Min(liniaProfilu[0].Y / zoomLevel, liniaProfilu[1].Y / zoomLevel); i <= Math.Max(liniaProfilu[0].Y / zoomLevel, liniaProfilu[1].Y / zoomLevel); i++)
                        {
                            bmp.Unlock();
                            int kolor;

                            Point punkt = new Point((int)((i - B) / A), i);
                            //punktyLiniiProfiluXY.Add(punkt);
                            punktyLiniiProfiluXY.Add(new Point(punkt.X * zoomLevel, punkt.Y * zoomLevel));

                            if (punkt.X < 0) punkt.X = 0;
                            if (punkt.X > bmp.Width) punkt.X = bmp.Width - 1;
                            if (punkt.Y < 0) punkt.X = 0;
                            if (punkt.Y > bmp.Height) punkt.Y = bmp.Height - 1;
                            kolor = bmp.Bitmap.GetPixel(punkt.X, punkt.Y).R;
                            bmp.Lock();

                            if (i == Math.Min(liniaProfilu[0].Y / zoomLevel, liniaProfilu[1].Y / zoomLevel))
                                lmax = lmin = srednia = kolor;
                            else
                            {
                                if (kolor > lmax) lmax = kolor;
                                if (kolor < lmin) lmin = kolor;
                                srednia += kolor;
                            }

                            punktyLiniiProfilu.Add(kolor);
                        }
                    }
                }
                //Linia pionowa
                else
                {
                    //Stały punkt X
                    A = liniaProfilu[0].X;
                    for (int i = Math.Min(liniaProfilu[0].Y / zoomLevel, liniaProfilu[1].Y / zoomLevel); i <= Math.Max(liniaProfilu[0].Y / zoomLevel, liniaProfilu[1].Y / zoomLevel); i++)
                    {

                        bmp.Unlock();
                        int kolor;
                        Point punkt = new Point((int)(A / zoomLevel), i);
                        punktyLiniiProfiluXY.Add(new Point(punkt.X * zoomLevel, punkt.Y * zoomLevel));

                        if (punkt.X < 0) punkt.X = 0;
                        if (punkt.X > bmp.Width) punkt.X = bmp.Width - 1;
                        if (punkt.Y < 0) punkt.X = 0;
                        if (punkt.Y > bmp.Height) punkt.Y = bmp.Height - 1;

                        kolor = bmp.Bitmap.GetPixel(punkt.X, punkt.Y).R;
                        bmp.Lock();

                        if (i == Math.Min(liniaProfilu[0].X / zoomLevel, liniaProfilu[1].X / zoomLevel))
                            lmax = lmin = srednia = kolor;
                        else
                        {
                            if (kolor > lmax) lmax = kolor;
                            if (kolor < lmin) lmin = kolor;
                            srednia += kolor;
                        }

                        punktyLiniiProfilu.Add(kolor);
                    }
                }

                label_max.Location = new Point(label_max.Location.X, Convert.ToInt32(wykresProfiluPanel.Height - lmax * (wykresProfiluPanel.Height / 256.0) - 5));
                label_min.Location = new Point(label_min.Location.X, Convert.ToInt32(wykresProfiluPanel.Height - lmin * (wykresProfiluPanel.Height / 256.0) - 5));
                label_max.Text = "_" + lmax / k;
                label_min.Text = "_" + lmin / k;
                label1.Text = "Max = " + lmax / k;
                label2.Text = "Min = " + lmin / k;
                if (punktyLiniiProfilu.Count > 0)
                    label3.Text = "Average = " + srednia / punktyLiniiProfilu.Count / k;

                drawZoomLevel = zoomLevel;
                RysujWykres();
                graphicsPanel.Refresh();
            }

            zoomBmp.Unlock();
            try
            {
                Color col = zoomBmp.Bitmap.GetPixel(e.X, e.Y);
                val = (col.R + col.G + col.B) / 3;
            }
            catch { }
            zoomBmp.Lock();

            tip = "X: " + e.X / zoomLevel + "  Y: " + e.Y / zoomLevel + "  Wartość: " + val;
            toolTipXY.SetToolTip(graphicsPanel, tip);

            labelDlugosc.Text = "Długość linii profilu: " + punktyLiniiProfilu.Count;

            //for (int i = 1; i < punktyLiniiProfilu.Count; i++)
            //{
            //    if (punktyLiniiProfiluXY[i].X / zoomLevel == punktyLiniiProfiluXY[i - 1].X / zoomLevel && punktyLiniiProfiluXY[i].Y / zoomLevel == punktyLiniiProfiluXY[i - 1].Y / zoomLevel)
            //    {
            //        punktyLiniiProfiluXY.RemoveAt(i);
            //        punktyLiniiProfilu.RemoveAt(i);
            //    }
            //}
            //RysujWykres();
        }

        private void RysujWykres()
        {
            int skaluj;
            int offset = (int)(1 * ((float)(wykresProfiluPictureBox.Width) / (float)(punktyLiniiProfilu.Count - 1)));
            float stretch = (509 / (float)wykresProfiluPictureBox.Width);
            wykresGraphics.Clear(Color.Transparent);
            for (int i = 0; i < punktyLiniiProfilu.Count - 1; i++)
            {
                skaluj = (int)(i * stretch * ((float)(wykresProfiluPictureBox.Width) / (float)(punktyLiniiProfilu.Count - 1)));
                if (radioButton1.Checked == true)
                    wykresGraphics.DrawLine(new Pen(Color.FromArgb(0, 0, 255)), skaluj, 255 - punktyLiniiProfilu[i], (int)(skaluj + offset * stretch) + 1, 255 - punktyLiniiProfilu[i + 1]);
                else if (radioButton2.Checked == true)
                {
                    wykresGraphics.DrawEllipse(new Pen(Color.FromArgb(0, 0, 255)), skaluj, 255 - punktyLiniiProfilu[i], 2, 2);
                    if (i == punktyLiniiProfilu.Count - 2)
                    {
                        skaluj = (int)((i + 1) * stretch * ((float)(wykresProfiluPictureBox.Width) / (float)(punktyLiniiProfilu.Count - 1)));
                        wykresGraphics.DrawEllipse(new Pen(Color.FromArgb(0, 0, 255)), skaluj, 255 - punktyLiniiProfilu[i + 1], 2, 2);
                    }
                }
                else
                {
                    wykresGraphics.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 3), skaluj, 255 - punktyLiniiProfilu[i], (int)(skaluj + offset * stretch) + 1, 255 - punktyLiniiProfilu[i + 1]);
                    wykresGraphics.DrawEllipse(new Pen(Color.FromArgb(70, 220, 30), 5), skaluj, 255 - punktyLiniiProfilu[i], 2, 2);
                    if (i == punktyLiniiProfilu.Count - 2)
                    {
                        skaluj = (int)((i + 1) * stretch * ((float)(wykresProfiluPictureBox.Width) / (float)(punktyLiniiProfilu.Count - 1)));
                        wykresGraphics.DrawEllipse(new Pen(Color.FromArgb(0, 0, 255), 5), skaluj, 255 - punktyLiniiProfilu[i + 1], 2, 2);
                    }
                }
            }

            wykresProfiluPictureBox.Refresh();
        }

        private void graphicsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cropRect = new Rectangle(e.X / zoomLevel * zoomLevel, e.Y / zoomLevel * zoomLevel, 0, 0);
                cropPoint = new Point(e.X / zoomLevel * zoomLevel, e.Y / zoomLevel * zoomLevel);
                graphicsPanel.Refresh();
            }
            else
            {
                liniaProfilu[0] = new Point(e.X / zoomLevel * zoomLevel + zoomLevel / 2, e.Y / zoomLevel * zoomLevel + zoomLevel / 2);
                liniaProfilu[1] = new Point(e.X / zoomLevel * zoomLevel + zoomLevel / 2, e.Y / zoomLevel * zoomLevel + zoomLevel / 2);
                graphicsPanel.Refresh();
            }
        }

        //***************************************************************************//

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RysujWykres();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RysujWykres();
        }

        private void graphicsPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ZoomIn();
            else if (e.Button == MouseButtons.Right)
                ZoomOut();
        }

        private void graphicsPanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add) ZoomIn();
            else if (e.KeyCode == Keys.Subtract) ZoomOut();
        }

        private void wykresProfiluPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            DrawVerticalLine(e);
        }

        private void DrawVerticalLine(MouseEventArgs e)
        {
            float przeskok = wykresProfiluPictureBox.Width / (float)(punktyLiniiProfilu.Count - 1);
            int index = (int)Math.Round((e.X / przeskok));

            // obraz
            if (liniaProfilu[0].X > -1 && liniaProfilu[0].Y > -1)
            {
                Bitmap tmpBmp = overlayBmp;
                Graphics g = Graphics.FromImage(tmpBmp);
                g.Clear(Color.Transparent);
                g.DrawLine(new Pen(Color.FromArgb(255, 0, 0)), liniaProfilu[0], liniaProfilu[1]); // rysowanie czerwonej linii
                try
                {
                    //g.FillEllipse(Brushes.Yellow, (punktyLiniiProfiluXY[index].X) - 2, (punktyLiniiProfiluXY[index].Y) - 2, 5, 5); // żółta kropka na linii
                    //g.FillRectangle(Brushes.Yellow, punktyLiniiProfiluXY[index].X, punktyLiniiProfiluXY[index].Y, zoomLevel, zoomLevel);
                    if (punktyLiniiProfilu[index] < 192)
                        g.DrawRectangle(new Pen(Brushes.Yellow), punktyLiniiProfiluXY[index].X * zoomLevel / drawZoomLevel - 1, punktyLiniiProfiluXY[index].Y * zoomLevel / drawZoomLevel - 1, zoomLevel + 1, zoomLevel + 1);   // zaznaczanie piksela
                    else
                        g.DrawRectangle(new Pen(Brushes.Black), punktyLiniiProfiluXY[index].X * zoomLevel / drawZoomLevel - 1, punktyLiniiProfiluXY[index].Y * zoomLevel / drawZoomLevel - 1, zoomLevel + 1, zoomLevel + 1);   // zaznaczanie piksela
                }
                catch { }
                g.Dispose();
                graphicsPanel.CreateGraphics().DrawImageUnscaled(tmpBmp, 0, 0);
            }

            if (point == e.Location) return;
            point = e.Location;

            graphicsPanel.Refresh();
            RysujWykres();

            // linia profilu
            Graphics tmp = wykresGraphics;
            float stretch = (509 / (float)wykresProfiluPictureBox.Width);
            tmp.DrawLine(new Pen(Color.FromArgb(0, 128, 0)), index * przeskok * stretch, 0, index * przeskok * stretch, this.Height - 1);
            tmp.DrawImageUnscaled(wykresProfiluPictureBox.Image, 0, 0);
            wykresProfiluPictureBox.Refresh();

            // dymek
            if (punktyLiniiProfiluXY.Count != 0 && punktyLiniiProfilu.Count != 0)
                try
                {
                    tip = "X: " + punktyLiniiProfiluXY[index].X / zoomLevel + "  Y: " + punktyLiniiProfiluXY[index].Y / zoomLevel + "  Wartość: " + punktyLiniiProfilu[index];
                }
                catch { }
            toolTipXY.SetToolTip(wykresProfiluPictureBox, tip);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }
        private void btnZoomReset_Click(object sender, EventArgs e)
        {
            zoomLevel = 1;
            SetZoom();
            lblZoomLvl.Text = "Powiększenie: " + zoomLevel + "x";
        }
        private void btnZoom_Click(object sender, EventArgs e)
        {
            int.TryParse(txtZoom.Text, out zoomLevel);
            SetZoom();
            lblZoomLvl.Text = "Powiększenie: " + zoomLevel + "x";
        }
        private void ZoomIn()
        {
            zoomLevel++;
            SetZoom();
            lblZoomLvl.Text = "Powiększenie: " + zoomLevel + "x";
        }
        private void ZoomOut()
        {
            zoomLevel--;
            SetZoom();
            lblZoomLvl.Text = "Powiększenie: " + zoomLevel + "x";
        }

        private void label_max_LocationChanged(object sender, EventArgs e)
        {
            if (label_max.Location.Y < 8)
                label_max.Location = new Point(label_max.Location.X, 8);
        }

        private void graphicsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //cropRect.Width = -1;
                //cropRect.Height = -1;
            }
        }

        private void graphicsPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && chkCut.Checked == true)
                if (this.CropRectangle.Width > 0 && this.CropRectangle.Height > 0)
                {
                    MainForm frm = (MainForm)this.MdiParent;
                    frm.CreateImage(this);
                }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RysujWykres();
        }
    }
}
