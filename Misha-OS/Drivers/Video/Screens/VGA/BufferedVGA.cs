
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Core;
using Cosmos.System.Graphics;

namespace Cosmos.HAL
{
    // vga mode identifiers
    public enum VGAMode
    {
        Text80x25,
        Text80x50,
        Text90x60,
        Pixel320x200,
        Pixel320x200DB,
    }

    // vga mode register dumps
    public static class VGAModeRegisters
    {
        // text 80x25
        public static byte[] Mode80x25_Text = new byte[]
        {
			/* MISC */
			0x67,
			/* SEQ */
			0x03, 0x00, 0x03, 0x00, 0x02,
			/* CRTC */
			0x5F, 0x4F, 0x50, 0x82, 0x55, 0x81, 0xBF, 0x1F,
            0x00, 0x4F, 0x0D, 0x0E, 0x00, 0x00, 0x00, 0x50,
            0x9C, 0x0E, 0x8F, 0x28, 0x1F, 0x96, 0xB9, 0xA3,
            0xFF,
			/* GC */
			0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x0E, 0x00,
            0xFF,
			/* AC */
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x14, 0x07,
            0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x0C, 0x00, 0x0F, 0x08, 0x00
        };

        // text 80x50
        public static byte[] Mode80x50_Text = new byte[]
        {
			/* MISC */
			0x67,
			/* SEQ */
			0x03, 0x00, 0x03, 0x00, 0x02,
			/* CRTC */
			0x5F, 0x4F, 0x50, 0x82, 0x55, 0x81, 0xBF, 0x1F,
            0x00, 0x47, 0x06, 0x07, 0x00, 0x00, 0x01, 0x40,
            0x9C, 0x8E, 0x8F, 0x28, 0x1F, 0x96, 0xB9, 0xA3,
            0xFF, 
			/* GC */
			0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x0E, 0x00,
            0xFF, 
			/* AC */
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x14, 0x07,
            0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x0C, 0x00, 0x0F, 0x08, 0x00,
        };

        // text 90x60
        public static byte[] Mode90x60_Text = new byte[]
        {
			/* MISC */
			0xE7,
			/* SEQ */
			0x03, 0x01, 0x03, 0x00, 0x02,
			/* CRTC */
			0x6B, 0x59, 0x5A, 0x82, 0x60, 0x8D, 0x0B, 0x3E,
            0x00, 0x47, 0x06, 0x07, 0x00, 0x00, 0x00, 0x00,
            0xEA, 0x0C, 0xDF, 0x2D, 0x08, 0xE8, 0x05, 0xA3,
            0xFF,
			/* GC */
			0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x0E, 0x00,
            0xFF,
			/* AC */
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x14, 0x07,
            0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x0C, 0x00, 0x0F, 0x08, 0x00,
        };

        // pixel 320x200x256
        public static byte[] Mode320x200x256_Pixel = new byte[]
        {
			/* MISC */
			0x63,
			/* SEQ */
			0x03, 0x01, 0x0F, 0x00, 0x0E,
			/* CRTC */
			0x5F, 0x4F, 0x50, 0x82, 0x54, 0x80, 0xBF, 0x1F,
            0x00, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x9C, 0x0E, 0x8F, 0x28, 0x40, 0x96, 0xB9, 0xA3,
            0xFF,
			/* GC */
			0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x05, 0x0F,
            0xFF,
			/* AC */
			0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x41, 0x00, 0x0F, 0x00, 0x00
        };
    }

    // vga controller class
    public static unsafe class VGADriverII
    {
        // data ports
        private static readonly IOPort PORT_SEQ_DATA = new IOPort(0x3C5);
        private static readonly IOPort PORT_GC_DATA = new IOPort(0x3CF);
        private static readonly IOPort PORT_CRTC_DATA = new IOPort(0x3D5);
        private static readonly IOPort PORT_MASK_DATA = new IOPort(0x3DA);
        private static readonly IOPort PORT_DAC_DATA = new IOPort(0x3C9);

        // write ports
        private static readonly IOPortWrite PORT_AC_WRITE = new IOPortWrite(0x3C0);
        private static readonly IOPortWrite PORT_MISC_WRITE = new IOPortWrite(0x3C2);
        private static readonly IOPortWrite PORT_SEQ_WRITE = new IOPortWrite(0x3C4);
        private static readonly IOPortWrite PORT_DAC_WRITE = new IOPortWrite(0x3C8);
        private static readonly IOPortWrite PORT_GC_WRITE = new IOPortWrite(0x3CE);
        private static readonly IOPortWrite PORT_CRTC_WRITE = new IOPortWrite(0x3D4);

        // read ports
        private static readonly IOPortRead PORT_AC_READ = new IOPortRead(0x3C1);
        private static readonly IOPortRead PORT_DAC_READ = new IOPortRead(0x3C7);
        private static readonly IOPortRead PORT_INSTAT_READ = new IOPortRead(0x3DA);

        // mode properties
        public static ushort Width { get; private set; }
        public static ushort Height { get; private set; }
        public static byte Depth { get; private set; }
        public static bool IsTextMode { get; private set; }
        public static bool IsDoubleBuffered { get; private set; }
        public static VGAMode ModeID { get; private set; }

        // buffer
        private static byte* Buffer;
        private static MemoryBlock BackBuffer = new MemoryBlock(0x60000, 0x10000);

        // color palette - 8 bit
        public static uint[] Palette256 = new uint[256]
        {
            0x000000, 0x010103, 0x030306, 0x040409, 0x06060C, 0x07070F, 0x090913, 0x0B0B16, 0x0C0C19, 0x0E0E1C, 0x0F0F1F, 0x111123, 0x131326, 0x141429, 0x16162C, 0x17172F,
            0x000000, 0x010301, 0x030603, 0x040904, 0x060C06, 0x070F07, 0x091309, 0x0B160B, 0x0C190C, 0x0E1C0E, 0x0F1F0F, 0x112311, 0x132613, 0x142914, 0x162C16, 0x172F17,
            0x000000, 0x030101, 0x060303, 0x090404, 0x0C0606, 0x0F0707, 0x130909, 0x160B0B, 0x190C0C, 0x1C0E0E, 0x1F0F0F, 0x231111, 0x261313, 0x291414, 0x2C1616, 0x2F1717,
            0x000000, 0x000103, 0x000306, 0x000409, 0x00060C, 0x00070F, 0x000913, 0x000B16, 0x000C19, 0x000E1C, 0x000F1F, 0x001123, 0x001326, 0x001429, 0x00162C, 0x00172F,
            0x000000, 0x010003, 0x030006, 0x040009, 0x06000C, 0x07000F, 0x090013, 0x0B0016, 0x0C0019, 0x0E001C, 0x0F001F, 0x110023, 0x130026, 0x140029, 0x16002C, 0x17002F,
            0x000000, 0x000301, 0x000603, 0x000904, 0x000C06, 0x000F07, 0x001309, 0x00160B, 0x00190C, 0x001C0E, 0x001F0F, 0x002311, 0x002613, 0x002914, 0x002C16, 0x002F17,
            0x000000, 0x010300, 0x030600, 0x040900, 0x060C00, 0x070F00, 0x091300, 0x0B1600, 0x0C1900, 0x0E1C00, 0x0F1F00, 0x112300, 0x132600, 0x142900, 0x162C00, 0x172F00,
            0x000000, 0x030001, 0x060003, 0x090004, 0x0C0006, 0x0F0007, 0x130009, 0x16000B, 0x19000C, 0x1C000E, 0x1F000F, 0x230011, 0x260013, 0x290014, 0x2C0016, 0x2F0017,
            0x000000, 0x030100, 0x060300, 0x090400, 0x0C0600, 0x0F0700, 0x130900, 0x160B00, 0x190C00, 0x1C0E00, 0x1F0F00, 0x231100, 0x261300, 0x291400, 0x2C1600, 0x2F1700,
            0x000000, 0x030003, 0x060006, 0x090009, 0x0C000C, 0x0F000F, 0x130013, 0x160016, 0x190019, 0x1C001C, 0x1F001F, 0x230023, 0x260026, 0x290029, 0x2C002C, 0x2F002F,
            0x000000, 0x000303, 0x000606, 0x000909, 0x000C0C, 0x000F0F, 0x001313, 0x001616, 0x001919, 0x001C1C, 0x001F1F, 0x002323, 0x002626, 0x002929, 0x002C2C, 0x002F2F,
            0x000000, 0x030300, 0x060600, 0x090900, 0x0C0C00, 0x0F0F00, 0x131300, 0x161600, 0x191900, 0x1C1C00, 0x1F1F00, 0x232300, 0x262600, 0x292900, 0x2C2C00, 0x2F2F00,
            0x000000, 0x000003, 0x000006, 0x000009, 0x00000C, 0x00000F, 0x000013, 0x000016, 0x000019, 0x00001C, 0x00001F, 0x000023, 0x000026, 0x000029, 0x00002C, 0x00002F,
            0x000000, 0x000300, 0x000600, 0x000900, 0x000C00, 0x000F00, 0x001300, 0x001600, 0x001900, 0x001C00, 0x001F00, 0x002300, 0x002600, 0x002900, 0x002C00, 0x002F00,
            0x000000, 0x030000, 0x060000, 0x090000, 0x0C0000, 0x0F0000, 0x130000, 0x160000, 0x190000, 0x1C0000, 0x1F0000, 0x230000, 0x260000, 0x290000, 0x2C0000, 0x2F0000,
            0x000000, 0x030303, 0x060606, 0x090909, 0x0C0C0C, 0x0F0F0F, 0x131313, 0x161616, 0x191919, 0x1C1C1C, 0x1F1F1F, 0x232323, 0x262626, 0x292929, 0x2C2C2C, 0x2F2F2F,
        };

        // color palette - 4 bit
        public static uint[] Palette16 = new uint[16]
        { 0x000000, 0x00001F, 0x001F00, 0x001F1F, 0x1F0000, 0x1F001F, 0x2F1F00, 0x2F2F2F, 0x1F1F1F, 0x00103F, 0x003F00, 0x003F3F, 0x3F0000, 0x3F003F, 0x3F3F00, 0x3F3F3F, };

        // initialization
        public static void Initialize(VGAMode mode)
        {
            SetMode(mode);
        }

        #region Graphics Handling

        // clear screen
        public static void Clear(byte color)
        {
            uint i = 0;
            // text mode
            if (IsTextMode) { for (i = 0; i < (Width * Height) * 2; i += 2) { Buffer[i] = 0x20; Buffer[i + 1] = color; } }
            // graphics mode
            else if (!IsTextMode && !IsDoubleBuffered) { for (i = 0; i < Width * Height; i++) { Buffer[i] = color; } }
            // double buffered graphics mode
            else if (!IsTextMode && IsDoubleBuffered) { BackBuffer.Fill(color); }
        }

        // draw pixel
        public static void DrawPixel(ushort x, ushort y, byte color)
        {
            if (x >= Width || y >= Height) { return; }
            if (IsTextMode) { return; }
            uint offset = (uint)(x + (y * Width));

            // double buffered
            if (IsDoubleBuffered) { BackBuffer.Bytes[offset] = color; }
            // direct
            else { Buffer[offset] = color; }
        }

        // swap back buffer
        public static void Display()
        {
            // check mode
            if (ModeID != VGAMode.Pixel320x200DB) { return; }

            byte* src = (byte*)BackBuffer.Base;
            MemoryOperations.Copy(Buffer, src, Width * Height);
        }

        // set text-mode cursor position
        public static void SetCursorPos(ushort x, ushort y)
        {
            if (!IsTextMode) { return; }
            uint offset = (uint)(x + (y * Width));
            PORT_CRTC_WRITE.Byte = 14;
            PORT_CRTC_DATA.Byte = (byte)((offset & 0xFF00) >> 8);
            PORT_CRTC_WRITE.Byte = 15;
            PORT_CRTC_DATA.Byte = (byte)(offset & 0x00FF);
        }

        // enable cursor
        public static void EnableCursor(byte start, byte end)
        {
            PORT_CRTC_WRITE.Byte = 0x0A;
            PORT_CRTC_DATA.Byte = (byte)((PORT_CRTC_DATA.Byte & 0xC0) | start);
            PORT_CRTC_WRITE.Byte = 0x0B;
            PORT_CRTC_DATA.Byte = (byte)((PORT_CRTC_DATA.Byte & 0xE0) | end);
        }

        // disable cursor
        public static void DisableCursor()
        {
            PORT_CRTC_WRITE.Byte = 0x0A;
            PORT_CRTC_DATA.Byte = 0x20;
        }

        #endregion

        #region Hardware Control

        // set current video mode properties
        private static void SetModeProperties(ushort w, ushort h, byte depth, bool text, bool db)
        {
            Width = w; Height = h; Depth = depth;
            IsTextMode = text;
            IsDoubleBuffered = db;
        }

        // set current video mode
        public static void SetMode(VGAMode mode)
        {
            // set mode id
            ModeID = mode;

            // set mode
            switch (mode)
            {
                // 80x25 text mode
                case VGAMode.Text80x25:
                    {
                        SetModeProperties(80, 25, 4, true, false);
                        fixed (byte* ptr = VGAModeRegisters.Mode80x25_Text) { WriteRegisters(ptr); }
                        SetFont(VGAFontData.Font8x16_Data, 16);
                        SetColorPalette(Palette16);
                        break;
                    }
                // 80x50 text mode
                case VGAMode.Text80x50:
                    {
                        SetModeProperties(80, 50, 4, true, false);
                        fixed (byte* ptr = VGAModeRegisters.Mode80x50_Text) { WriteRegisters(ptr); }
                        SetFont(VGAFontData.Font8x8_Data, 8);
                        SetColorPalette(Palette16);
                        break;
                    }
                // 90x60 text mode
                case VGAMode.Text90x60:
                    {
                        SetModeProperties(90, 60, 4, true, false);
                        fixed (byte* ptr = VGAModeRegisters.Mode90x60_Text) { WriteRegisters(ptr); }
                        SetFont(VGAFontData.Font8x8_Data, 8);
                        SetColorPalette(Palette16);
                        break;
                    }
                // 320x200 graphics mode
                case VGAMode.Pixel320x200:
                    {
                        SetModeProperties(320, 200, 8, false, false);
                        fixed (byte* ptr = VGAModeRegisters.Mode320x200x256_Pixel) { WriteRegisters(ptr); }
                        ClearColorPalette();
                        SetColorPalette(Palette256);
                        break;
                    }
                // 320x200 double buffered graphics mode
                case VGAMode.Pixel320x200DB:
                    {
                        SetModeProperties(320, 200, 8, false, true);
                        fixed (byte* ptr = VGAModeRegisters.Mode320x200x256_Pixel) { WriteRegisters(ptr); }
                        ClearColorPalette();
                        SetColorPalette(Palette256);
                        break;
                    }
                // default to 80x25 text mode
                default: { break; }
            }

            // clear the screen
            Clear(0);
        }

        // get frame buffer segment
        private static byte* GetFrameBufferSegment()
        {
            PORT_GC_WRITE.Byte = 0x06;
            byte segNum = (byte)(PORT_GC_DATA.Byte & (3 << 2));
            switch (segNum)
            {
                default:
                case 0 << 2: return (byte*)0x00000;
                case 1 << 2: return (byte*)0xA0000;
                case 2 << 2: return (byte*)0xB0000;
                case 3 << 2: return (byte*)0xB8000;
            }
        }

        // write data to vga registers
        private static void WriteRegisters(byte* regs)
        {
            // misc
            PORT_MISC_WRITE.Byte = *(regs++);

            // sequencer
            for (byte i = 0; i < 5; i++) { PORT_SEQ_WRITE.Byte = i; PORT_SEQ_DATA.Byte = *(regs++); }

            // crtc
            PORT_CRTC_WRITE.Byte = 0x03;
            PORT_CRTC_DATA.Byte = (byte)(PORT_CRTC_DATA.Byte | 0x80);
            PORT_CRTC_WRITE.Byte = 0x11;
            PORT_CRTC_DATA.Byte = (byte)(PORT_CRTC_DATA.Byte | ~0x80);

            // registers
            regs[0x03] = (byte)(regs[0x03] | 0x80);
            regs[0x11] = (byte)(regs[0x11] & ~0x80);
            for (byte i = 0; i < 25; i++) { PORT_CRTC_WRITE.Byte = i; PORT_CRTC_DATA.Byte = *(regs++); }

            // graphics controller
            for (byte i = 0; i < 9; i++) { PORT_GC_WRITE.Byte = i; PORT_GC_DATA.Byte = *(regs++); }

            // attribute controller
            byte val = 0;
            for (byte i = 0; i < 21; i++)
            {
                val = PORT_INSTAT_READ.Byte;
                PORT_AC_WRITE.Byte = i;
                PORT_AC_WRITE.Byte = *(regs++);
            }

            val = PORT_INSTAT_READ.Byte;
            PORT_AC_WRITE.Byte = 0x20;


            // set buffer address
            Buffer = GetFrameBufferSegment();
        }

        // set plane data
        private static void SetPlane(byte ap)
        {
            // calculate
            byte pmask;
            ap &= 3;
            pmask = (byte)(1 << ap);

            // set read plane
            PORT_GC_WRITE.Byte = 4;
            PORT_GC_DATA.Byte = ap;

            // set write plane
            PORT_SEQ_WRITE.Byte = 2;
            PORT_SEQ_DATA.Byte = pmask;
        }

        // set font
        private static void SetFont(byte[] font, byte height)
        {
            byte seq2, seq4, gc4, gc5, gc6;

            // sequencer
            PORT_SEQ_WRITE.Byte = 2;
            seq2 = PORT_SEQ_DATA.Byte;
            PORT_SEQ_WRITE.Byte = 4;
            seq4 = PORT_SEQ_DATA.Byte;
            PORT_SEQ_DATA.Byte = (byte)(seq4 | 0x04);

            // gc
            PORT_GC_WRITE.Byte = 4;
            gc4 = PORT_GC_DATA.Byte;
            PORT_GC_WRITE.Byte = 5;
            gc5 = PORT_GC_DATA.Byte;
            PORT_GC_DATA.Byte = (byte)(gc5 & ~0x10);
            PORT_GC_WRITE.Byte = 6;
            gc6 = PORT_GC_DATA.Byte;
            PORT_GC_DATA.Byte = (byte)(gc6 & ~0x02);

            // write font to plane 4
            SetPlane(2);

            // write font
            byte* seg = GetFrameBufferSegment();
            for (uint i = 0; i < 256; i++)
            { for (uint j = 0; j < height; j++) { seg[(i * 32) + j] = font[(i * height) + j]; } }

            // restore registers
            PORT_SEQ_WRITE.Byte = 2;
            PORT_SEQ_DATA.Byte = seq2;
            PORT_SEQ_WRITE.Byte = 4;
            PORT_SEQ_DATA.Byte = seq4;
            PORT_GC_WRITE.Byte = 4;
            PORT_GC_DATA.Byte = gc4;
            PORT_GC_WRITE.Byte = 5;
            PORT_GC_DATA.Byte = gc5;
            PORT_GC_WRITE.Byte = 6;
            PORT_GC_DATA.Byte = gc6;
        }

        // set color palette
        public static void SetColorPalette(uint[] colors)
        {
            for (uint i = 0; i < colors.Length; i++)
            {
                if (!IsTextMode) { PORT_MASK_DATA.Byte = 0xFF; } else { PORT_MASK_DATA.Byte = 0x0F; }
                PORT_DAC_WRITE.Byte = (byte)i;
                PORT_DAC_DATA.Byte = (byte)((colors[i] & 0xFF0000) >> 16);
                PORT_DAC_DATA.Byte = (byte)((colors[i] & 0x00FF00) >> 8);
                PORT_DAC_DATA.Byte = (byte)(colors[i] & 0x0000FF);
            }
        }

        // clear color palette
        private static void ClearColorPalette()
        {
            for (uint i = 0; i < 256; i++)
            {
                if (!IsTextMode) { PORT_MASK_DATA.Byte = 0xFF; } else { PORT_MASK_DATA.Byte = 0x0F; }
                PORT_DAC_WRITE.Byte = (byte)i;
                PORT_DAC_DATA.Byte = 0;
                PORT_DAC_DATA.Byte = 0;
                PORT_DAC_DATA.Byte = 0;
            }
        }

        #endregion
    }
}