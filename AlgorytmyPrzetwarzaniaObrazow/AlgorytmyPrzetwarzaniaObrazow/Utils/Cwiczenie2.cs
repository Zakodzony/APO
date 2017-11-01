using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyPrzetwarzaniaObrazow.Utils
{
    public static class Cwiczenie2
    {
        public static FastBitmap Negacja(FastBitmap bmp)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    bmp[x, y] = (byte)(bmp.Levels - 1 - bmp[x, y]);
            return bmp;
        }

        public static FastBitmap Progowanie(FastBitmap bmp, int value)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    if (bmp[x, y] <= value) bmp[x, y] = 0;
                    else bmp[x, y] = (byte)(bmp.Levels - 1);
            return bmp;
        }

        public static FastBitmap Binaryzacja(FastBitmap bmp, int value)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    if (bmp[x, y] <= value) bmp[x, y] = 0;
                    else bmp[x, y] = 1;

            ColorPalette palette = bmp.Palette;
            palette.Entries[0] = Color.FromArgb(0, 0, 0);
            palette.Entries[1] = Color.FromArgb(255, 255, 255);
            bmp.Posterize(2, palette);

            return bmp;
        }

        public static FastBitmap Jasnosc(FastBitmap bmp, int value)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    double val = bmp[x, y] + value * (bmp.Levels / 256.0);
                    if (val < 0) val = 0;
                    else if (val > bmp.Levels - 1) val = bmp.Levels - 1;
                    bmp[x, y] = (byte)val;
                }
            }
            return bmp;
        }

        public static FastBitmap Kontrast(FastBitmap bmp, int value)
        {
            double multiplier = (100.0 + (double)value) / 100.0;
            double lmax = (double)(bmp.Levels - 1);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    double temp = ((double)bmp[x, y] / lmax) - 0.5;
                    temp = temp * multiplier + 0.5;
                    bmp[x, y] = (byte)Math.Max(0, Math.Min(bmp.Levels - 1, (int)(temp * lmax)));
                }
            }
            return bmp;
        }

        public static FastBitmap KorekcjaGamma(FastBitmap bmp, float value)
        {
            byte[] upo = new byte[bmp.Levels];
            double lmax = (double)(bmp.Levels - 1);
            for (int i = 0; i < bmp.Levels; ++i)
            {
                int pos = (int)((lmax * Math.Pow(i / lmax, 1.0 / value)) + 0.5);
                pos = Math.Min(Math.Max(pos, 0), bmp.Levels - 1);
                upo[i] = (byte)pos;
            }

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    bmp[x, y] = upo[bmp[x, y]];
            return bmp;
        }

        public static FastBitmap Rozciaganie(FastBitmap bmp, int p1, int p2)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                //if (p1 < p2)
                {
                    if (bmp[x, y] > p1 && bmp[x, y] <= p2)
                        bmp[x, y] = (byte)((bmp[x, y] - p1) * ((bmp.Levels - 1) / (p2 - p1)));
                }
            //else bmp[x, y] = 0;
            return bmp;
        }

        public static FastBitmap Redukcja(FastBitmap bmp, int value)
        {
            byte[] upo = new byte[bmp.Levels];
            float param1 = 255.0f / (value - 1);
            float param2 = (float)bmp.Levels / (value);
            for (int i = 0; i < bmp.Levels; ++i)
            {
                byte temp = (byte)(i / param2);
                upo[i] = (byte)(temp);
            }

            ColorPalette palette = bmp.Palette;
            for (int i = 0; i < value; i++)
            {
                byte color = (byte)(param1 * i);
                palette.Entries[i] = Color.FromArgb(color, color, color);
            }
            bmp.Posterize(value, palette);

            for (int i = 0; i < bmp.Size.Width; ++i)
            {
                for (int j = 0; j < bmp.Size.Height; ++j)
                {
                    bmp[i, j] = upo[bmp[i, j]];
                }
            }

            return bmp;
        }


        public static FastBitmap FiltracjaLiniowa2(FastBitmap bmp, int[,] mask, int divisor, int tryb, int dodaj, int typ_scalowania)
        {
            FastBitmap bitmap = (FastBitmap)bmp.Clone();
            int[,] dane = new int[bmp.Width + (2 * dodaj), bmp.Height + (2 * dodaj)];
            int[,] dane2 = new int[bmp.Width + (2 * dodaj), bmp.Height + (2 * dodaj)];
            if (divisor == 0) divisor = 1;

            int size = (int)Math.Sqrt(mask.Length);
            int index = size - size / 2 - 1;

            Point[,] temp = new Point[size, size];

            int odX = index;
            int doX = bmp.Width + index;
            int odY = index;
            int doY = bmp.Height + index;
            //  int typ_scalowania = 3;

            // przepisanie danych z bitmapy do tablicy 
            for (int i = 0; i <= bmp.Width; i++)
                for (int j = 0; j <= bmp.Height; j++)
                    dane[i + dodaj, j + dodaj] = (int)bmp[i, j];

            // przepisanie pierwszych/ostatnich linii dla y linii 
            for (int j = 0; j < dodaj; j++)
                for (int i = dodaj; i <= bmp.Width + dodaj; i++)
                {
                    dane[i, j] = dane[i, dodaj];
                    dane[i, bmp.Height + j] = dane[i, bmp.Height - 1];
                }

            // przepisanie pierwszych/ostatnich linii dla x linii 
            for (int j = 0; j < dodaj; j++)
                for (int i = 0; i <= bmp.Height + (2 * dodaj) - 1; i++)
                {
                    dane[j, i] = dane[dodaj, i];
                    dane[bmp.Width + j, i] = dane[bmp.Width - 1, i];
                }

            int oldColor = 0;
            for (int x = 0; x < doX + dodaj; x++)
            {
                for (int y = 0; y < doY + dodaj; y++)
                {
                    oldColor = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            oldColor = dane[x + temp[k, l].X, y + temp[k, l].Y];
                            dane2[x, y] += mask[k, l] * oldColor;
                        }
                    }
                    dane2[x, y] /= divisor;



                    //// Skalowanie: Metoda 1
                    //if (typ_scalowania == 1)
                    //{
                    //    //if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    //    //else if (newColor < 0) newColor = 0;
                    //}

                    ////// Skalowanie: Metoda 2
                    //if (typ_scalowania == 2)
                    //{
                    //    //if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    //    //else if (newColor < 0) newColor = 0;
                    //}

                    //// Skalowanie: Metoda 3
                    //if (typ_scalowania == 3)
                    //{
                    //    if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    //    else if (newColor < 0) newColor = 0;
                    //}

                    // bitmap[x, y] = (byte)dane[x,y];
                }
            }

            //skalowanie

            //1 wyliczenie wartosci Minimalnej, Maksymalnej, Sredniej
            int iMin = 0, iMax = 1, iSred = 0, ipunkt = 0;
            for (int i = 0; i <= bmp.Width; i++)
                for (int j = 0; j <= bmp.Height; j++)
                {
                    if (dane2[i + dodaj, j + dodaj] < iMin)
                    {
                        iMin = dane2[i + dodaj, j + dodaj];
                    }

                    if (dane2[i + dodaj, j + dodaj] > iMax)
                    {
                        iMax = dane2[i + dodaj, j + dodaj];
                    }
                    iSred += dane2[i + dodaj, j + dodaj];
                    ipunkt++;
                }

            {
                iSred = iSred / ipunkt;
            }
            for (int i = 0; i <= bmp.Width; i++)
                for (int j = 0; j <= bmp.Height; j++)
                {
                    if (typ_scalowania == 1)
                    { // proporcjonalny
                        dane2[i + dodaj, j + dodaj] = (dane2[i + dodaj, j + dodaj] - iMin) / (iMax - iMin + 1);
                    }

                    if (typ_scalowania == 2)
                    { // trójwartościowy
                        if (dane2[i + dodaj, j + dodaj] < 0)
                        {
                            dane2[i + dodaj, j + dodaj] = 0;
                        }
                        else
                        {
                            if (dane2[i + dodaj, j + dodaj] > bmp.Levels)
                            {
                                dane2[i + dodaj, j + dodaj] = bmp.Levels - 1;
                            }
                            else
                                dane2[i + dodaj, j + dodaj] = iSred;
                        }
                    }

                    if (typ_scalowania == 3)
                    { // obcinamy nadmiar
                        if (dane2[i + dodaj, j + dodaj] < 0)
                        {
                            dane2[i + dodaj, j + dodaj] = 0;
                        }
                        else
                        {
                            if (dane2[i + dodaj, j + dodaj] > bmp.Levels)
                            {
                                dane2[i + dodaj, j + dodaj] = bmp.Levels - 1;
                            }
                            // else
                            //dane[i + dodaj, j + dodaj] = iSred;
                        }
                    }
                }

            // przepisanie danych do bitmapy
            for (int i = 0; i <= bmp.Width; i++)
                for (int j = 0; j <= bmp.Height; j++)
                    bitmap[i, j] = (byte)dane2[i + dodaj - 1, j + dodaj - 1];

            return bitmap;
        }

        public static FastBitmap FiltracjaLiniowa(FastBitmap bmp, int[,] mask, int divisor, int tryb)
        {
            FastBitmap bitmap = (FastBitmap)bmp.Clone();

            if (divisor == 0) divisor = 1;

            int size = (int)Math.Sqrt(mask.Length);
            Point[,] temp = new Point[size, size];

            int index = size - size / 2 - 1;
            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            for (int x = index; x < bmp.Width - index; x++)
            {
                for (int y = index; y < bmp.Height - index; y++)
                {
                    int newColor = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                            newColor += mask[k, l] * oldColor;
                        }
                    }
                    newColor /= divisor;

                    // Skalowanie: Metoda 3
                    if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    else if (newColor < 0) newColor = 0;

                    bitmap[x, y] = (byte)newColor;
                }
            }

            return bitmap;
        }


        public static FastBitmap FiltracjaLiniowa3(FastBitmap bmp, int[,] mask, int divisor, int tryb)
        {
            FastBitmap bitmap = (FastBitmap)bmp.Clone();


            if (divisor == 0) divisor = 1;

            int size = (int)Math.Sqrt(mask.Length);
            int index = size - size / 2 - 1;

            Point[,] temp = new Point[size, size];

            //int odX = index;
            //int doX = bmp.Width - index;
            //int odY = index;
            //int doY = bmp.Height - index;
            int typ_scalowania = 3;

            int newColor = 0;  // do skasowania 

            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            //tryb = 1 wszystko wewnątrz bez linii granicznych
            PrzetworzObszar(bmp, mask, divisor, bitmap, size, temp, index, bmp.Width - index, index, bmp.Height - index, typ_scalowania);

            if (tryb == 3)
            {
                int divisor2 = 1;
                //dublujemy 1 linię 
                //punkt 0,0
                {
                    newColor = bmp[0, 0] * mask[1, 1] +
                                bmp[0, 1] * mask[1, 2] +
                                bmp[0, 0] * mask[2, 1] +
                                bmp[1, 1] * mask[2, 2];
                    divisor2 = mask[1, 1] +
                                mask[1, 2] +
                                mask[2, 1] +
                                mask[2, 2];
                    newColor /= divisor2;

                    // Skalowanie: Metoda 3
                    if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    else if (newColor < 0) newColor = 0;

                    bitmap[0, 0] = (byte)newColor;
                }
                for (int y = index; y < bmp.Height - index; y++)
                {
                    int x = 0;
                    {

                        newColor = bmp[-1, y] * mask[0, 1] +
                                   bmp[-1, y + 1] * mask[0, 2] +
                                   bmp[0, y] * mask[1, 1] +
                                   bmp[0, y + 1] * mask[1, 2] +
                                   bmp[1, y] * mask[2, 1] +
                                   bmp[1, y + 1] * mask[2, 2];
                        divisor2 = mask[0, 1] +
                                   mask[0, 2] +
                                   mask[1, 1] +
                                   mask[1, 2] +
                                   mask[2, 1] +
                                   mask[2, 2];
                        newColor /= divisor2;

                        // Skalowanie: Metoda 3
                        if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                        else if (newColor < 0) newColor = 0;

                        bitmap[x, y] = (byte)newColor;
                    }
                }

                {
                    int y = 0;
                    for (int x = index; x < bmp.Width - index; x++)
                    {

                        newColor = bmp[x, -1] * mask[1, 0] +
                                   bmp[x, 0] * mask[1, 1] +
                                   bmp[x, 1] * mask[1, 2] +
                                   bmp[x + 1, -1] * mask[2, 0] +
                                   bmp[x + 1, 0] * mask[2, 1] +
                                   bmp[x + 1, 1] * mask[2, 2];
                        divisor2 = mask[1, 0] +
                                   mask[1, 1] +
                                   mask[1, 2] +
                                   mask[2, 0] +
                                   mask[2, 1] +
                                   mask[2, 2];
                        newColor /= divisor2;

                        // Skalowanie: Metoda 3
                        if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                        else if (newColor < 0) newColor = 0;

                        bitmap[x, y] = (byte)newColor;
                    }
                }
            } //if (tryb == 3)

            if (tryb == 22)
            {
                //dublujemy 1 linię 
                //punkt 0,0
                {
                    newColor = bmp[0, 0] * mask[0, 0] +
                                bmp[0, 0] * mask[0, 1] +
                                bmp[0, 0] * mask[0, 2] +
                                bmp[0, 1] * mask[0, 3] +
                                bmp[0, 2] * mask[0, 4] +
                                bmp[0, 0] * mask[1, 0] +
                                bmp[0, 0] * mask[1, 1] +
                                bmp[0, 0] * mask[1, 2] +
                                bmp[0, 1] * mask[1, 3] +
                                bmp[0, 2] * mask[1, 4] +
                                bmp[0, 0] * mask[2, 0] +
                                bmp[0, 0] * mask[2, 1] +
                                bmp[0, 0] * mask[2, 2] +
                                bmp[0, 1] * mask[2, 3] +
                                bmp[0, 2] * mask[2, 4] +
                                bmp[1, 0] * mask[3, 0] +
                                bmp[1, 0] * mask[3, 1] +
                                bmp[1, 0] * mask[3, 2] +
                                bmp[1, 1] * mask[3, 3] +
                                bmp[1, 2] * mask[3, 4] +
                                bmp[2, 0] * mask[4, 0] +
                                bmp[2, 0] * mask[4, 1] +
                                bmp[2, 0] * mask[4, 2] +
                                bmp[2, 1] * mask[4, 3] +
                                bmp[2, 2] * mask[4, 4];

                    newColor /= divisor;

                    // Skalowanie: Metoda 3
                    if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    else if (newColor < 0) newColor = 0;

                    bitmap[0, 0] = (byte)newColor;
                }

                { // linia gdy x=0 dodajemy dwie linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {

                            for (int k = 0; k < size; k++)
                            {
                                //tryb = 1 wszystko wewnątrz bez linii granicznych
                                for (int l = 0; l < size; l++)
                                {
                                    byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                                    newColor += mask[k, l] * oldColor;

                                    newColor = bmp[0, y - 2] * mask[0, 0] +
                                                bmp[0, y - 1] * mask[0, 1] +
                                                bmp[0, y] * mask[0, 2] +
                                                bmp[0, y + 1] * mask[0, 3] +
                                                bmp[0, y + 2] * mask[0, 4] +
                                                bmp[0, y - 2] * mask[1, 0] +
                                                bmp[0, y - 1] * mask[1, 1] +
                                                bmp[0, y] * mask[1, 2] +
                                                bmp[0, y + 1] * mask[1, 3] +
                                                bmp[0, y + 2] * mask[1, 4] +
                                                bmp[0, y - 2] * mask[2, 0] +
                                                bmp[0, y - 1] * mask[2, 1] +
                                                bmp[0, y] * mask[2, 2] +
                                                bmp[0, y + 1] * mask[2, 3] +
                                                bmp[0, y + 2] * mask[2, 4] +
                                                bmp[1, y - 2] * mask[3, 0] +
                                                bmp[1, y - 1] * mask[3, 1] +
                                                bmp[1, y] * mask[3, 2] +
                                                bmp[1, y + 1] * mask[3, 3] +
                                                bmp[1, y + 2] * mask[3, 4] +
                                                bmp[2, y - 2] * mask[4, 0] +
                                                bmp[2, y - 1] * mask[4, 1] +
                                                bmp[2, y] * mask[4, 2] +
                                                bmp[2, y + 1] * mask[4, 3] +
                                                bmp[2, y + 2] * mask[4, 4];
                                }

                            }
                            newColor /= divisor;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy x=1 dodajemy 1 linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {

                            for (int k = 0; k < size; k++)
                            {
                                //tryb = 1 wszystko wewnątrz bez linii granicznych
                                for (int l = 0; l < size; l++)
                                {
                                    byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                                    newColor += mask[k, l] * oldColor;

                                    newColor = bmp[0, y - 2] * mask[0, 0] +
                                                bmp[0, y - 1] * mask[0, 1] +
                                                bmp[0, y] * mask[0, 2] +
                                                bmp[0, y + 1] * mask[0, 3] +
                                                bmp[0, y + 2] * mask[0, 4] +
                                                bmp[-1, y - 2] * mask[1, 0] +
                                                bmp[-1, y - 1] * mask[1, 1] +
                                                bmp[-1, y] * mask[1, 2] +
                                                bmp[-1, y + 1] * mask[1, 3] +
                                                bmp[-1, y + 2] * mask[1, 4] +
                                                bmp[0, y - 2] * mask[2, 0] +
                                                bmp[0, y - 1] * mask[2, 1] +
                                                bmp[0, y] * mask[2, 2] +
                                                bmp[0, y + 1] * mask[2, 3] +
                                                bmp[0, y + 2] * mask[2, 4] +
                                                bmp[1, y - 2] * mask[3, 0] +
                                                bmp[1, y - 1] * mask[3, 1] +
                                                bmp[1, y] * mask[3, 2] +
                                                bmp[1, y + 1] * mask[3, 3] +
                                                bmp[1, y + 2] * mask[3, 4] +
                                                bmp[2, y - 2] * mask[4, 0] +
                                                bmp[2, y - 1] * mask[4, 1] +
                                                bmp[2, y] * mask[4, 2] +
                                                bmp[2, y + 1] * mask[4, 3] +
                                                bmp[2, y + 2] * mask[4, 4];
                                }

                            }
                            newColor /= divisor;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy y=0 dodajemy dwie linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {
                            newColor = bmp[x - 2, 0] * mask[0, 0] +
                                        bmp[x - 1, 0] * mask[0, 1] +
                                        bmp[x, 0] * mask[0, 2] +
                                        bmp[x + 1, 0] * mask[0, 3] +
                                        bmp[x + 2, 0] * mask[0, 4] +
                                        bmp[x - 2, 0] * mask[1, 0] +
                                        bmp[x - 1, 0] * mask[1, 1] +
                                        bmp[x, 0] * mask[1, 2] +
                                        bmp[x + 1, 0] * mask[1, 3] +
                                        bmp[x + 2, 0] * mask[1, 4] +
                                        bmp[x - 2, 0] * mask[2, 0] +
                                        bmp[x - 1, 0] * mask[2, 1] +
                                        bmp[x, 0] * mask[2, 2] +
                                        bmp[x + 1, 1] * mask[2, 3] +
                                        bmp[x + 2, 2] * mask[2, 4] +
                                        bmp[x - 2, 0] * mask[3, 0] +
                                        bmp[x - 1, 0] * mask[3, 1] +
                                        bmp[x, 0] * mask[3, 2] +
                                        bmp[x + 1, 1] * mask[3, 3] +
                                        bmp[x + 2, 2] * mask[3, 4] +
                                        bmp[x - 2, 0] * mask[4, 0] +
                                        bmp[x - 1, 0] * mask[4, 1] +
                                        bmp[x, 0] * mask[4, 2] +
                                        bmp[x + 1, 1] * mask[4, 3] +
                                        bmp[x + 2, 2] * mask[4, 4];

                            newColor /= divisor;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy y=0 dodajemy 1 linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {
                            newColor = bmp[x - 2, 0] * mask[0, 0] +
                                        bmp[x - 1, 0] * mask[0, 1] +
                                        bmp[x, 1] * mask[0, 2] +
                                        bmp[x + 1, 2] * mask[0, 3] +
                                        bmp[x + 2, 3] * mask[0, 4] +
                                        bmp[x - 2, 0] * mask[1, 0] +
                                        bmp[x - 1, 0] * mask[1, 1] +
                                        bmp[x, 1] * mask[1, 2] +
                                        bmp[x + 1, 2] * mask[1, 3] +
                                        bmp[x + 2, 3] * mask[1, 4] +
                                        bmp[x - 2, 0] * mask[2, 0] +
                                        bmp[x - 1, 0] * mask[2, 1] +
                                        bmp[x, 1] * mask[2, 2] +
                                        bmp[x + 1, 2] * mask[2, 3] +
                                        bmp[x + 2, 3] * mask[2, 4] +
                                        bmp[x - 2, 0] * mask[3, 0] +
                                        bmp[x - 1, 0] * mask[3, 1] +
                                        bmp[x, 1] * mask[3, 2] +
                                        bmp[x + 1, 2] * mask[3, 3] +
                                        bmp[x + 2, 3] * mask[3, 4] +
                                        bmp[x - 2, 0] * mask[4, 0] +
                                        bmp[x - 1, 0] * mask[4, 1] +
                                        bmp[x, 1] * mask[4, 2] +
                                        bmp[x + 1, 2] * mask[4, 3] +
                                        bmp[x + 2, 3] * mask[4, 4];

                            newColor /= divisor;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }
            } // tryb 22


            if (tryb == 23)
            {
                //dublujemy 1 linię 
                //punkt 0,0
                {
                    newColor = bmp[0, 0] * mask[0, 2] +
                                bmp[0, 1] * mask[0, 3] +
                                bmp[0, 2] * mask[0, 4] +
                                bmp[0, 0] * mask[1, 2] +
                                bmp[0, 1] * mask[1, 3] +
                                bmp[0, 2] * mask[1, 4] +
                                bmp[0, 0] * mask[2, 0] +
                                bmp[0, 1] * mask[2, 3] +
                                bmp[0, 2] * mask[2, 4] +
                                bmp[1, 0] * mask[3, 2] +
                                bmp[1, 1] * mask[3, 3] +
                                bmp[1, 2] * mask[3, 4] +
                                bmp[2, 0] * mask[4, 2] +
                                bmp[2, 1] * mask[4, 3] +
                                bmp[2, 2] * mask[4, 4];

                    int divisor2 = 0;
                    divisor2 = mask[0, 2] +
                               mask[0, 3] +
                               mask[0, 4] +
                               mask[1, 2] +
                               mask[1, 3] +
                               mask[1, 4] +
                               mask[2, 0] +
                               mask[2, 3] +
                               mask[2, 4] +
                               mask[3, 2] +
                               mask[3, 3] +
                               mask[3, 4] +
                               mask[4, 2] +
                               mask[4, 3] +
                               mask[4, 4];
                    newColor /= divisor2;

                    // Skalowanie: Metoda 3
                    if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                    else if (newColor < 0) newColor = 0;

                    bitmap[0, 0] = (byte)newColor;
                }

                { // linia gdy x=0 dodajemy dwie linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {

                            for (int k = 0; k < size; k++)
                            {
                                //tryb = 1 wszystko wewnątrz bez linii granicznych
                                for (int l = 0; l < size; l++)
                                {
                                    byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                                    newColor += mask[k, l] * oldColor;

                                    newColor = bmp[0, y - 2] * mask[2, 0] +
                                                bmp[0, y - 1] * mask[2, 1] +
                                                bmp[0, y] * mask[2, 2] +
                                                bmp[0, y + 1] * mask[2, 3] +
                                                bmp[0, y + 2] * mask[2, 4] +
                                                bmp[1, y - 2] * mask[3, 0] +
                                                bmp[1, y - 1] * mask[3, 1] +
                                                bmp[1, y] * mask[3, 2] +
                                                bmp[1, y + 1] * mask[3, 3] +
                                                bmp[1, y + 2] * mask[3, 4] +
                                                bmp[2, y - 2] * mask[4, 0] +
                                                bmp[2, y - 1] * mask[4, 1] +
                                                bmp[2, y] * mask[4, 2] +
                                                bmp[2, y + 1] * mask[4, 3] +
                                                bmp[2, y + 2] * mask[4, 4];
                                }

                            }

                            int divisor2 = 0;
                            divisor2 = mask[2, 0] +
                                        mask[2, 1] +
                                        mask[2, 2] +
                                        mask[2, 3] +
                                        mask[2, 4] +
                                        mask[3, 0] +
                                        mask[3, 1] +
                                        mask[3, 2] +
                                        mask[3, 3] +
                                        mask[3, 4] +
                                        mask[4, 0] +
                                        mask[4, 1] +
                                        mask[4, 2] +
                                        mask[4, 3] +
                                        mask[4, 4];

                            newColor /= divisor2;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy x=1 dodajemy 1 linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {

                            for (int k = 0; k < size; k++)
                            {
                                //tryb = 1 wszystko wewnątrz bez linii granicznych
                                for (int l = 0; l < size; l++)
                                {
                                    byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                                    newColor += mask[k, l] * oldColor;

                                    newColor =
                                                bmp[-1, y - 2] * mask[1, 0] +
                                                bmp[-1, y - 1] * mask[1, 1] +
                                                bmp[-1, y] * mask[1, 2] +
                                                bmp[-1, y + 1] * mask[1, 3] +
                                                bmp[-1, y + 2] * mask[1, 4] +
                                                bmp[0, y - 2] * mask[2, 0] +
                                                bmp[0, y - 1] * mask[2, 1] +
                                                bmp[0, y] * mask[2, 2] +
                                                bmp[0, y + 1] * mask[2, 3] +
                                                bmp[0, y + 2] * mask[2, 4] +
                                                bmp[1, y - 2] * mask[3, 0] +
                                                bmp[1, y - 1] * mask[3, 1] +
                                                bmp[1, y] * mask[3, 2] +
                                                bmp[1, y + 1] * mask[3, 3] +
                                                bmp[1, y + 2] * mask[3, 4] +
                                                bmp[2, y - 2] * mask[4, 0] +
                                                bmp[2, y - 1] * mask[4, 1] +
                                                bmp[2, y] * mask[4, 2] +
                                                bmp[2, y + 1] * mask[4, 3] +
                                                bmp[2, y + 2] * mask[4, 4];
                                }

                            }
                            int divisor2 = 0;
                            divisor2 = mask[1, 0] +
                                        mask[1, 1] +
                                        mask[1, 2] +
                                        mask[1, 3] +
                                        mask[1, 4] +
                                        mask[2, 0] +
                                        mask[2, 1] +
                                        mask[2, 2] +
                                        mask[2, 3] +
                                        mask[2, 4] +
                                        mask[3, 0] +
                                        mask[3, 1] +
                                        mask[3, 2] +
                                        mask[3, 3] +
                                        mask[3, 4] +
                                        mask[4, 0] +
                                        mask[4, 1] +
                                        mask[4, 2] +
                                        mask[4, 3] +
                                        mask[4, 4];

                            newColor /= divisor2;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy y=0 dodajemy dwie linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {
                            newColor =
                                        bmp[x, 0] * mask[0, 2] +
                                        bmp[x + 1, 0] * mask[0, 3] +
                                        bmp[x + 2, 0] * mask[0, 4] +
                                        bmp[x, 0] * mask[1, 2] +
                                        bmp[x + 1, 0] * mask[1, 3] +
                                        bmp[x + 2, 0] * mask[1, 4] +
                                        bmp[x, 0] * mask[2, 2] +
                                        bmp[x + 1, 1] * mask[2, 3] +
                                        bmp[x + 2, 2] * mask[2, 4] +
                                        bmp[x, 0] * mask[3, 2] +
                                        bmp[x + 1, 1] * mask[3, 3] +
                                        bmp[x + 2, 2] * mask[3, 4] +
                                        bmp[x, 0] * mask[4, 2] +
                                        bmp[x + 1, 1] * mask[4, 3] +
                                        bmp[x + 2, 2] * mask[4, 4];

                            int divisor2 = 0;
                            divisor2 = mask[0, 2] +
                                        mask[0, 3] +
                                        mask[0, 4] +
                                        mask[1, 2] +
                                        mask[1, 3] +
                                        mask[1, 4] +
                                        mask[2, 2] +
                                        mask[2, 3] +
                                        mask[2, 4] +
                                        mask[3, 2] +
                                        mask[3, 3] +
                                        mask[3, 4] +
                                        mask[4, 2] +
                                        mask[4, 3] +
                                        mask[4, 4];
                            newColor /= divisor2;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }

                { // linia gdy y=0 dodajemy 1 linie
                    for (int x = index; x < bmp.Width - index; x++)
                    {
                        for (int y = index; y < bmp.Height - index; y++)
                        {
                            newColor =
                                        bmp[x - 1, 0] * mask[0, 1] +
                                        bmp[x, 1] * mask[0, 2] +
                                        bmp[x + 1, 2] * mask[0, 3] +
                                        bmp[x + 2, 3] * mask[0, 4] +

                                        bmp[x - 1, 0] * mask[1, 1] +
                                        bmp[x, 1] * mask[1, 2] +
                                        bmp[x + 1, 2] * mask[1, 3] +
                                        bmp[x + 2, 3] * mask[1, 4] +

                                        bmp[x - 1, 0] * mask[2, 1] +
                                        bmp[x, 1] * mask[2, 2] +
                                        bmp[x + 1, 2] * mask[2, 3] +
                                        bmp[x + 2, 3] * mask[2, 4] +

                                        bmp[x - 1, 0] * mask[3, 1] +
                                        bmp[x, 1] * mask[3, 2] +
                                        bmp[x + 1, 2] * mask[3, 3] +
                                        bmp[x + 2, 3] * mask[3, 4] +

                                        bmp[x - 1, 0] * mask[4, 1] +
                                        bmp[x, 1] * mask[4, 2] +
                                        bmp[x + 1, 2] * mask[4, 3] +
                                        bmp[x + 2, 3] * mask[4, 4];

                            int divisor2 = 0;
                            divisor2 = mask[0, 1] +
                                        mask[0, 2] +
                                        mask[0, 3] +
                                        mask[0, 4] +
                                        mask[1, 1] +
                                        mask[1, 2] +
                                        mask[1, 3] +
                                        mask[1, 4] +
                                        mask[2, 1] +
                                        mask[2, 2] +
                                        mask[2, 3] +
                                        mask[2, 4] +
                                        mask[3, 1] +
                                        mask[3, 2] +
                                        mask[3, 3] +
                                        mask[3, 4] +
                                        mask[4, 1] +
                                        mask[4, 2] +
                                        mask[4, 3] +
                                        mask[4, 4];

                            newColor /= divisor2;

                            // Skalowanie: Metoda 3
                            if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                            else if (newColor < 0) newColor = 0;

                            bitmap[x, y] = (byte)newColor;
                        }
                    }
                }
            } // tryb 23


            return bitmap;
        }

        private static void PrzetworzObszar(FastBitmap bmp, int[,] mask, int divisor, FastBitmap bitmap, int size, Point[,] temp, int odX, int doX, int odY, int doY, int typ_scalowania)
        {
            int newColor = 0;
            for (int x = odX; x <= doX; x++)
            {
                for (int y = odY; y <= doY; y++)
                {
                    newColor = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                            newColor += mask[k, l] * oldColor;
                        }
                    }
                    newColor /= divisor;


                    // Skalowanie: Metoda 1
                    if (typ_scalowania == 1)
                    {
                        //if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                        //else if (newColor < 0) newColor = 0;
                    }

                    //// Skalowanie: Metoda 2
                    if (typ_scalowania == 2)
                    {
                        if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                        else if (newColor < 0) newColor = 0;
                    }

                    // Skalowanie: Metoda 3
                    if (typ_scalowania == 3)
                    {
                        if (newColor > bitmap.Levels - 1) newColor = bitmap.Levels - 1;
                        else if (newColor < 0) newColor = 0;
                    }

                    bitmap[x, y] = (byte)newColor;
                }
            }
        }

        public static FastBitmap FiltracjaMedianowa(FastBitmap bmp, int value)
        {
            int filterSize = value;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    byte[] neighbours = new byte[filterSize * filterSize];
                    int a = 0;
                    for (int k = -filterSize / 2; k <= filterSize / 2; ++k)
                        for (int l = -filterSize / 2; l <= filterSize / 2; ++l)
                            neighbours[a++] = bmp[x + k, y + l];

                    Array.Sort(neighbours);
                    if (neighbours.Length % 2 == 1)
                        bmp[x, y] = neighbours[neighbours.Length / 2];
                    else
                        bmp[x, y] = (byte)Math.Min((neighbours[neighbours.Length / 2] + neighbours[(neighbours.Length / 2) + 1]) / 2, bmp.Levels - 1);
                }
            }

            return bmp;
        }
    }
}

