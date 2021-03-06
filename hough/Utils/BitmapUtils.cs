﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Hough.Utils
{
    public static class BitmapUtils
    {
        public static unsafe Bitmap ConvertToBitmap(this int[,] bytes)
        {
            var dimensions = new[]
            {
                bytes.GetLength(0),
                bytes.GetLength(1),
            };
            var width = dimensions[0];
            var height = dimensions[1];

            int maxValue = bytes.Cast<int>().Max();

            Bitmap bitmap  = new Bitmap(dimensions[0],dimensions[1]);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color color;
                    if (bytes[x, y] == maxValue)
                    {
                        color = Color.Red;
                    }
                    else
                    {
                        //int value = (int) (255 - ((double) bytes[x, y]*255/maxValue));
                        int value = (int)(255 - Math.Log10(1 + (double) bytes[x, y]*255/maxValue)*100);
                        
                        color = Color.FromArgb(value, value, value);
                    }
                    bitmap.SetPixel(x,y, color);
//                    double color = bytes[x, y];
//                    byte rgb = (byte)(color * 255);
//
//                    ColorARGB* position = startingPosition + x + y * width;
//                    position->A = 255;
//                    position->R = rgb;
//                    position->G = rgb;
//                    position->B = rgb;
                }

//            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        private struct ColorARGB
        {
            public byte B;
            public byte G;
            public byte R;
            public byte A;

            public ColorARGB(Color color)
            {
                A = color.A;
                R = color.R;
                G = color.G;
                B = color.B;
            }

            public ColorARGB(byte a, byte r, byte g, byte b)
            {
                A = a;
                R = r;
                G = g;
                B = b;
            }

            public Color ToColor()
            {
                return Color.FromArgb(A, R, G, B);
            }
        }
    }


}
