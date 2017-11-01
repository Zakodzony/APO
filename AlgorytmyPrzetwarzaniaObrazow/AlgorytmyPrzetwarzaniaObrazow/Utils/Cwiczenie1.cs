using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AlgorytmyPrzetwarzaniaObrazow
{
    public static class Cwiczenie1
    {
        public enum Metody
        {
            Srednich,
            Losowa,
            Sasiedztwa,
            Wlasna,
            Wlasna2
        }

        public static FastBitmap Wyrownanie(FastBitmap bmp, Metody metoda)
        {
            int[] H = new int[bmp.Levels];
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    H[bmp[x, y]]++;

            int Havg = 0;
            for (int i = 0; i < bmp.Levels; i++)
                Havg += H[i];
            Havg /= bmp.Levels;

            int R = 0;
            int Hint = 0;
            int[] left = new int[bmp.Levels];
            int[] right = new int[bmp.Levels];
            int[] _new = new int[bmp.Levels];

            for (int z = 0; z < bmp.Levels; z++)
            {
                left[z] = R;
                Hint += H[z];
                while (Hint > Havg)
                {
                    Hint -= Havg;
                    R++;
                }
                if (R > bmp.Levels - 1) R = bmp.Levels - 1;
                right[z] = R;

                switch (metoda)
                {
                    case Metody.Srednich:
                        _new[z] = (left[z] + right[z]) / 2;
                        break;

                    case Metody.Losowa:
                        _new[z] = right[z] - left[z];
                        break;

                    case Metody.Wlasna:
                        _new[z] = (int)((left[z] + right[z]) / 2.0);
                        break;

                    case Metody.Wlasna2:
                        _new[z] = (int)(left[z]);
                        break;

                    default:
                        break;
                }
            }

            Random random = new Random();



            for (int i = 0; i < bmp.Size.Width; ++i)
            {
                for (int j = 0; j < bmp.Size.Height; ++j)
                {
                    byte color = 0;
                    int x = 0;

                    byte val = bmp[i, j];

                    if (left[val] == right[val])
                    {
                        color = (byte)left[val];
                    }
                    else
                    {
                        switch (metoda)
                        {
                            case Metody.Srednich:
                                color = (byte)_new[val];
                                break;

                            case Metody.Losowa:
                                int value = random.Next(0, _new[val]);
                                color = (byte)(left[val] + value);
                                break;

                            case Metody.Sasiedztwa:
                                double average = 0;
                                int count = 0;
                                foreach (Point offset in new Point[] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1), new Point(1, 1), new Point(-1, -1), new Point(-1, 1), new Point(1, -1) })
                                {
                                    if (i + offset.X >= 0 && i + offset.X < bmp.Width && j + offset.Y >= 0 && j + offset.Y < bmp.Height)
                                    {
                                        average += bmp[i + offset.X, j + offset.Y];
                                        ++count;
                                    }
                                }
                                average /= count;
                                if (average > right[val])
                                    color = (byte)right[val];
                                else if (average < left[val])
                                    color = (byte)left[val];
                                else
                                    color = (byte)((int)average);
                                break;

                            case Metody.Wlasna:
                                color = (byte)((_new[val] + random.Next(0, _new[val])) / 2);
                                break;
                            case Metody.Wlasna2:
                                x = random.Next(-10, 10);
                                if ((_new[val] + x) < bmp.Levels)
                                {
                                    if ((_new[val] + x) > 0)
                                        color = (byte)(_new[val] + x);
                                    else
                                        color = (byte)(_new[val]);
                                }
                                else
                                    color = (byte)(_new[val]);

                                break;

                            default:
                                break;
                        }
                    }

                    bmp[i, j] = color;
                }
            }

            return bmp;
        }
    }
}
