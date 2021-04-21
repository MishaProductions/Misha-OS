using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Drivers.Video
{
    /// <summary>
    /// From: https://github.com/raxracks/NapalmTorchVGA-RGB-Color/blob/main/RGBColor.cs
    /// </summary>
    public static class RGBColor
    {
        //the freezing bug occurs here....
        private static int ClosestColor(List<int[]> colors, int[] target)
        {
            int colorDiffs = 99999999;
            int index = 0;
            for (int i = 0; i < colors.Count; i++)
            {
                int[] color = colors[i];
                int dif = ColorDiff(color, target);
                if (dif < colorDiffs)
                {
                    index = i;
                    colorDiffs = dif;
                }
            }
            target = null;
            colors = null;
            return index;
        }

        private static int ColorDiff(int[] c1, int[] c2)
        {
            return (int)Math.Sqrt((c1[0] - c2[0]) * (c1[0] - c2[0])
                                   + (c1[1] - c2[1]) * (c1[1] - c2[1])
                                   + (c1[2] - c2[2]) * (c1[2] - c2[2]));
        }

        public static VGAColor Get(int r, int g, int b)
        {
            return (VGAColor)ClosestColor(ColorPalette.colors, new int[] { r, g, b });
        }
    }
}
