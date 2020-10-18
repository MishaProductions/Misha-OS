using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui
{
    public static class ImageUtil
    {
        /// <summary>
        /// Draws an image that is in the format of int[].
        /// Example:
        /// int img[] = new int[] { 0, 0, 0};
        /// 
        /// 0 is transperant.
        /// 1 is Black.
        /// 2 is White
        /// Anything else will be Magenta.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void DrawImage(int[] image,int x, int y,int width,int height)
        {
            int i = 0;
            for (uint h = 0; h < height; h++)
            {
                for (uint w = 0; w < width; w++)
                {
                    int color = image[h * width + w];
                    if (color != 0)
                    {
                        Display.setPixel((int)w + x, (int)h + y, IntTOColor(color));
                    }
                    i++;
                }
                i++;
            }
        }

        public static Color IntTOColor(int color)
        {
            if (color == 1)
                return Color.Black;
            else if (color == 2)
                return Color.White;

            return Color.Magenta;
        }
    }
}
