using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.System.Graphics
{
    public class VGAImage
    {
        // properties
        public int Width { get; private set; }
        public int Height { get; private set; }
        public byte[] Data { get; private set; }

        // constructor - new
        public VGAImage(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Data = new byte[w * h];
        }

        // constructor - load
        public VGAImage(string file)
        {
            this.FromFile(file);
        }

        // set data
        public void LoadData(int w, int h, byte[] data)
        {
            this.Width = w;
            this.Height = h;
            this.Data = data;
        }

        // load from file system
        public bool FromFile(string file)
        {
            if (File.Exists(file))
            {
                byte[] data = File.ReadAllBytes(file);
                return ParseData(data);
            }
            else { return false; }
        }

        // shrink image by multiple
        public void Shrink(int mult)
        {
            byte[] data = new byte[(Width * Height) / mult];
            for (int yy = 0; yy < Height; yy++)
            {
                int yyScaled = yy / mult;
                for (int xx = 0; xx < Width; xx++)
                {
                    int xxScaled = xx / mult;
                    data[(xxScaled + (yyScaled * (Width / mult)))] = Data[xx + (yy * Width)];
                }
            }

            this.Width = (int)(Width / mult);
            this.Height = (int)(Height / mult);
            this.Data = data;
        }

        // expand image by multiple
        public void Expand(int mult)
        {
            byte[] data = new byte[(Width * Height) * mult];
            for (int yy = 0; yy < Height; yy++)
            {
                int yyScaled = yy * mult;
                for (int xx = 0; xx < Width; xx++)
                {
                    int xxScaled = xx * mult;
                    data[(xxScaled + (yyScaled * (Width * mult)))] = Data[xx + (yy * Width)];
                }
            }

            this.Width = (int)(Width * mult);
            this.Height = (int)(Height * mult);
            this.Data = data;
        }

        // parse data
        public bool ParseData(byte[] data)
        {
            // valid file
            if (data.Length > 4)
            {
                Width = (ushort)((data[0] << 8) | data[1]);
                Height = (ushort)((data[2] << 8) | data[3]);
                this.Data = new byte[Width * Height];
                // pixel count is valid
                if (data.Length - 4 == Width * Height)
                {
                    // loop through data
                    for (int i = 0; i < Width * Height; i++)
                    {
                        // copy data(4 is width and height offset)
                        Data[i] = data[i + 4];
                    }
                    return true;
                }
                // pixel count doesn't match image size
                else { return false; }
            }
            // invalid file type - too small
            else { return false; }
        }
    }
}