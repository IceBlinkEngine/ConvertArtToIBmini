using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvertArtToIBmini
{
    public class PCXHEAD
    {
        public byte[] m_Data = new byte[128];

        /// <summary>  
        ///  0A;  
        /// </summary>  
        public byte Manufacturer { get { return m_Data[0]; } }
        /// <summary>  
        /// 0?PC Paintbrush 2.5  ??2?PC Paintbrush 2.8 ??5?PC Paintbrush 3.0   
        /// </summary>  
        public byte Version { get { return m_Data[1]; } set { m_Data[1] = value; } }
        /// <summary>  
        /// 1RLE  
        /// </summary>  
        public byte Encoding { get { return m_Data[2]; } set { m_Data[2] = value; } }
        /// <summary>  
        ///   
        /// </summary>  
        public byte Bits_Per_Pixel { get { return m_Data[3]; } set { m_Data[3] = value; } }

        public ushort Xmin { get { return BitConverter.ToUInt16(m_Data, 4); } set { SetUshort(4, value); } }
        public ushort Ymin { get { return BitConverter.ToUInt16(m_Data, 6); } set { SetUshort(6, value); } }
        public ushort Xmax { get { return BitConverter.ToUInt16(m_Data, 8); } set { SetUshort(8, value); } }
        public ushort Ymax { get { return BitConverter.ToUInt16(m_Data, 10); } set { SetUshort(10, value); } }
        /// <summary>  
        ///   
        /// </summary>  
        public ushort Hres1 { get { return BitConverter.ToUInt16(m_Data, 12); } set { SetUshort(12, value); } }
        /// <summary>  
        ///   
        /// </summary>  
        public ushort Vres1 { get { return BitConverter.ToUInt16(m_Data, 14); } set { SetUshort(14, value); } }

        public byte[] Palette
        {
            get
            {
                byte[] _Palette = new byte[48];
                Array.Copy(m_Data, 16, _Palette, 0, 48);
                return _Palette;
            }
            set
            {
                if (value.Length != 48) throw new Exception("byte[]48");
                Array.Copy(value, 0, m_Data, 16, 48);
            }

        }
        /// <summary>  
        ///   
        /// </summary>  
        public byte Reserved { get { return m_Data[64]; } set { m_Data[64] = value; } }
        /// <summary>  
        ///   
        /// </summary>  
        public byte Colour_Planes { get { return m_Data[65]; } set { m_Data[65] = value; } }
        /// <summary>  
        ///   
        /// </summary>  
        public ushort Bytes_Per_Line { get { return BitConverter.ToUInt16(m_Data, 66); } set { SetUshort(66, value); } }
        /// <summary>  
        ///   
        /// </summary>  
        public ushort Palette_Type { get { return BitConverter.ToUInt16(m_Data, 68); } set { SetUshort(68, value); } }
        /// <summary>  
        ///   
        /// </summary>  
        public byte[] Filler
        {
            get
            {
                byte[] m_Bytes = new byte[58];
                Array.Copy(m_Data, 70, m_Bytes, 0, 58);
                return m_Bytes;
            }
        }

        public PCXHEAD(byte[] p_Data)
        {
            Array.Copy(p_Data, m_Data, 128);
        }

        public PCXHEAD()
        {
            m_Data[0] = 0xA;
            Version = 0x5;
            Encoding = 0x1;
            Bits_Per_Pixel = 0x8;
            Palette = new byte[] { 0x00, 0x00, 0xCD, 0x00, 0x90, 0xE7, 0x37, 0x01, 0x80, 0xF6, 0x95, 0x7C, 0x28, 0xFB, 0x95, 0x7C, 0xFF, 0xFF, 0xFF, 0xFF, 0x23, 0xFB, 0x95, 0x7C, 0xB3, 0x16, 0x34, 0x7C, 0x00, 0x00, 0xCD, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB8, 0x16, 0x34, 0x7C, 0x64, 0xF3, 0x37, 0x01, 0xD8, 0x54, 0xB8, 0x00 };
            Reserved = 0x01;
            Colour_Planes = 0x03;
            Palette_Type = 1;
        }

        public int Width { get { return Xmax - Xmin + 1; } }

        public int Height { get { return Ymax - Ymin + 1; } }

        /// <summary>  
        /// 16  
        /// </summary>  
        /// <param name="p_Index"></param>  
        /// <param name="p_Data"></param>  
        private void SetUshort(int p_Index, ushort p_Data)
        {
            byte[] _ValueBytes = BitConverter.GetBytes(p_Data);
            m_Data[p_Index] = _ValueBytes[0];
            m_Data[p_Index + 1] = _ValueBytes[1];
        }
    }
}
