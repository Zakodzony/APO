using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyPrzetwarzaniaObrazow.Utils
{
    public static class Cwiczenie3
    {
        public enum Spojnosc
        {
            Czterospojne,
            Osmiospojne
        }

        public static FastBitmap Pocienianie(FastBitmap bmp)
        {
            return bmp;
        }

        public static FastBitmap Szkieletyzacja(FastBitmap bmp)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { 1, 1, 0, -1, -1, -1, 0, 1 };

            bool[,] img = new bool[bmp.Width, bmp.Height];
            int W = bmp.Width;
            int H = bmp.Height;
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    img[x, y] = bmp[x, y] == 0;

            bool pass = false;
            LinkedList<Point> list;

            do
            {
                pass = !pass;
                list = new LinkedList<Point>();

                for (int x = 1; x < W - 1; ++x)
                {
                    for (int y = 1; y < H - 1; ++y)
                    {
                        if (img[x, y])
                        {
                            int cnt = 0;
                            int hm = 0;
                            bool prev = img[x - 1, y + 1];
                            for (int i = 0; i < 8; ++i)
                            {
                                bool cur = img[x + dx[i], y + dy[i]];
                                hm += cur ? 1 : 0;
                                if (prev && !cur) ++cnt;
                                prev = cur;
                            }
                            if (hm > 1 && hm < 7 && cnt == 1)
                            {
                                if (pass && (!img[x + 1, y] || !img[x, y + 1] || !img[x, y - 1] && !img[x - 1, y]))
                                {
                                    list.AddLast(new Point(x, y));
                                }
                                if (!pass && (!img[x - 1, y] || !img[x, y - 1] || !img[x, y + 1] && !img[x + 1, y]))
                                {
                                    list.AddLast(new Point(x, y));
                                }
                            }
                        }

                    }
                }
                foreach (Point p in list)
                    img[p.X, p.Y] = false;
            } while (list.Count != 0);
            FastBitmap ret = new FastBitmap(W, H);
            for (int x = 0; x < W; ++x)
                for (int y = 0; y < H; ++y)
                    ret[x, y] = (byte)(img[x, y] ? 0 : ret.Levels - 1);

            return ret;
        }

        public static FastBitmap Dylatacja(FastBitmap bmp, Spojnosc spojnosc)
        {
            int i, j, pam;
            int[,] dilate = new int[bmp.Width, bmp.Height];
            int[,] tab = new int[bmp.Width, bmp.Height];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    tab[x, y] = bmp[x, y];

            for (i = 1; i < bmp.Height - 1; i++)
            {
                for (j = 1; j < bmp.Width - 1; j++)
                {
                    pam = tab[j, i];

                    if (spojnosc == Spojnosc.Osmiospojne)
                    {
                        if (pam <= tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam <= tab[j + 1, i + 1]) pam = tab[j + 1, i + 1];
                        if (pam <= tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam <= tab[j - 1, i + 1]) pam = tab[j - 1, i + 1];
                        if (pam <= tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam <= tab[j - 1, i - 1]) pam = tab[j - 1, i - 1];
                        if (pam <= tab[j, i - 1]) pam = tab[j, i - 1];
                        if (pam <= tab[j + 1, i - 1]) pam = tab[j + 1, i - 1];
                    }
                    else if (spojnosc == Spojnosc.Czterospojne)
                    {
                        if (pam <= tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam <= tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam <= tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam <= tab[j, i - 1]) pam = tab[j, i - 1];
                    }

                    dilate[j, i] = pam;
                }
            }

            for (i = 0; i < bmp.Height; i++)
                for (j = 0; j < bmp.Width; j++)
                    bmp[j, i] = (byte)dilate[j, i];

            return bmp;
        }

        public static FastBitmap Erozja(FastBitmap bmp, Spojnosc spojnosc)
        {
            int i, j, pam;
            int[,] erode = new int[bmp.Width, bmp.Height];
            int[,] tab = new int[bmp.Width, bmp.Height];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    tab[x, y] = bmp[x, y];

            for (i = 1; i < bmp.Height - 1; i++)
            {
                for (j = 1; j < bmp.Width - 1; j++)
                {
                    pam = tab[j, i];

                    if (spojnosc == Spojnosc.Osmiospojne)
                    {
                        if (pam > tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam > tab[j + 1, i + 1]) pam = tab[j + 1, i + 1];
                        if (pam > tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam > tab[j - 1, i + 1]) pam = tab[j - 1, i + 1];
                        if (pam > tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam > tab[j - 1, i - 1]) pam = tab[j - 1, i - 1];
                        if (pam > tab[j, i - 1]) pam = tab[j, i - 1];
                        if (pam > tab[j + 1, i - 1]) pam = tab[j + 1, i - 1];
                    }
                    else if (spojnosc == Spojnosc.Czterospojne)
                    {
                        if (pam > tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam > tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam > tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam > tab[j, i - 1]) pam = tab[j, i - 1];
                    }

                    erode[j, i] = pam;
                }
            }

            for (i = 0; i < bmp.Height; i++)
                for (j = 0; j < bmp.Width; j++)
                    bmp[j, i] = (byte)erode[j, i];

            return bmp;
        }

        private static bool pixelIsValid(Size size, int x, int y)
        {

            if (x >= size.Width || x < 0) return false;
            if (y >= size.Height || y < 0) return false;
            return true;
        }

        public enum GradientMetody
        {
            Pierwiastek,
            SumaModulow
        }

        public static FastBitmap Gradient(FastBitmap bmp, GradientMetody metoda)
        {
            FastBitmap result = new FastBitmap(bmp.Width, bmp.Height, bmp.Levels);
            int mask_size = 1;
            int suma = 1;
            int[,] maskaX = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] maskaY = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            float wynik = 0;
            int maxwart = -10000;
            int minwart = 10000;

            int[,] obraz = new int[bmp.Width, bmp.Height];

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    int Gx = 0;
                    int Gy = 0;
                    float punkty = 0;
                    for (int i = -mask_size; i < mask_size + 1; i++)
                    {
                        for (int j = -mask_size; j < mask_size + 1; j++)
                        {
                            if (pixelIsValid(result.Size, x + i, y + j))
                            {
                                Gx += maskaX[mask_size + i, mask_size + j] * bmp[x + i, y + j];
                                Gy += maskaY[mask_size + i, mask_size + j] * bmp[x + i, y + j];
                            }
                            else goto nastepny;
                        }
                    }

                    if (metoda == GradientMetody.Pierwiastek)
                        punkty = (float)Math.Sqrt(Math.Pow(Gx, 2) + Math.Pow(Gy, 2));
                    else punkty = Math.Abs(Gx) + Math.Abs(Gy);
                    wynik = punkty / (float)suma;
                    if (wynik > maxwart) maxwart = (int)wynik;
                    if (wynik < minwart) minwart = (int)wynik;
                    obraz[x, y] = (int)wynik;

                    nastepny:;
                }
            }

            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                    obraz[x, y] = (int)(((bmp.Levels - 1) * (obraz[x, y] - minwart)) / ((float)maxwart - (float)minwart));
            //obraz[x, y] = (int)((255*(obraz[x, y]-minwart))/((float)maxwart-(float)minwart));

            for (int x = 1; x < result.Width - 1; x++)
                for (int y = 1; y < result.Height - 1; y++)
                    result[x, y] = (byte)Math.Min(obraz[x, y], bmp.Levels - 1);

            return result;
        }
    }
}
