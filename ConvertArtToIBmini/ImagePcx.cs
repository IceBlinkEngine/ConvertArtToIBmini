using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ConvertArtToIBmini
{
    public class ImagePcx
    {
        private PCXHEAD m_Head = new PCXHEAD();

        private Bitmap m_Image;

        public Bitmap PcxImage { get { return m_Image; } set { m_Image = value; } }

        public ImagePcx(string p_FileFullName)
        {
            if (!File.Exists(p_FileFullName)) return;
            Load(File.ReadAllBytes(p_FileFullName));
        }

        public ImagePcx(byte[] p_Data)
        {
            Load(p_Data);
        }

        public ImagePcx()
        {

        }

        private void Load(byte[] p_Bytes)
        {
            byte[] _Bytes = p_Bytes;
            if (_Bytes[0] != 0x0A) return;
            m_Head = new PCXHEAD(_Bytes);
            m_ReadIndex = 128;
            PixelFormat _PixFormate = PixelFormat.Format24bppRgb;
            if (m_Head.Colour_Planes == 1)
            {
                switch (m_Head.Bits_Per_Pixel)
                {
                    case 8:
                        _PixFormate = PixelFormat.Format8bppIndexed;
                        break;
                    case 1:
                        _PixFormate = PixelFormat.Format1bppIndexed;
                        break;
                }
            }

            m_Image = new Bitmap(m_Head.Width, m_Head.Height, _PixFormate);
            BitmapData _Data = m_Image.LockBits(new Rectangle(0, 0, m_Image.Width, m_Image.Height), ImageLockMode.ReadWrite, _PixFormate);
            byte[] _BmpData = new byte[_Data.Stride * _Data.Height];

            for (int i = 0; i != m_Head.Height; i++)
            {
                byte[] _RowColorValue = new byte[0];
                switch (m_Head.Colour_Planes)
                {
                    case 3: //24  
                        _RowColorValue = LoadPCXLine24(_Bytes);
                        break;
                    case 1: //256  
                        switch (m_Head.Bits_Per_Pixel)
                        {
                            case 8:
                                _RowColorValue = LoadPCXLine8(_Bytes);
                                break;
                            case 1:
                                _RowColorValue = LoadPCXLine1(_Bytes);
                                break;
                        }

                        break;
                }
                int _Count = _RowColorValue.Length;
                Array.Copy(_RowColorValue, 0, _BmpData, i * _Data.Stride, _Data.Stride);
            }
            Marshal.Copy(_BmpData, 0, _Data.Scan0, _BmpData.Length);
            m_Image.UnlockBits(_Data);

            switch (m_Head.Colour_Planes)
            {
                case 1:
                    if (m_Head.Bits_Per_Pixel == 8)
                    {
                        ColorPalette _Palette = m_Image.Palette;
                        m_ReadIndex = p_Bytes.Length - 256 * 3;
                        for (int i = 0; i != 256; i++)
                        {
                            _Palette.Entries[i] = Color.FromArgb(p_Bytes[m_ReadIndex], p_Bytes[m_ReadIndex + 1], p_Bytes[m_ReadIndex + 2]);
                            m_ReadIndex += 3;
                        }
                        m_Image.Palette = _Palette;
                    }
                    break;
            }
        }

        #region
        private int m_ReadIndex = 0;
        private byte[] LoadPCXLine24(byte[] p_Data)
        {
            int _LineWidth = m_Head.Bytes_Per_Line;
            byte[] _ReturnBytes = new byte[_LineWidth * 3];
            int _EndBytesLength = p_Data.Length - 1;
            int _WriteIndex = 2;
            int _ReadIndex = 0;
            while (true)
            {
                if (m_ReadIndex > _EndBytesLength) break; //  
                byte _Data = p_Data[m_ReadIndex];

                if (_Data > 0xC0)
                {
                    int _Count = _Data - 0xC0;
                    m_ReadIndex++;
                    for (int i = 0; i != _Count; i++)
                    {
                        if (i + _ReadIndex >= _LineWidth)          //2009-6-12 RLE   
                        {
                            _WriteIndex--;
                            _ReadIndex = 0;
                            _Count = _Count - i;
                            i = 0;
                        }
                        int _RVA = ((i + _ReadIndex) * 3) + _WriteIndex;
                        _ReturnBytes[_RVA] = p_Data[m_ReadIndex];
                    }
                    _ReadIndex += _Count;
                    m_ReadIndex++;
                }
                else
                {
                    int _RVA = (_ReadIndex * 3) + _WriteIndex;
                    _ReturnBytes[_RVA] = _Data;
                    m_ReadIndex++;
                    _ReadIndex++;
                }
                if (_ReadIndex >= _LineWidth)
                {
                    _WriteIndex--;
                    _ReadIndex = 0;
                }

                if (_WriteIndex == -1) break;
            }

            return _ReturnBytes;
        }
        private byte[] LoadPCXLine8(byte[] p_Data)
        {
            int _LineWidth = m_Head.Bytes_Per_Line;
            byte[] _ReturnBytes = new byte[_LineWidth];
            int _EndBytesLength = p_Data.Length - 1 - (256 * 3);         //??  
            int _ReadIndex = 0;
            while (true)
            {
                if (m_ReadIndex > _EndBytesLength) break; //    

                byte _Data = p_Data[m_ReadIndex];
                if (_Data > 0xC0)
                {
                    int _Count = _Data - 0xC0;
                    m_ReadIndex++;
                    for (int i = 0; i != _Count; i++)
                    {
                        _ReturnBytes[i + _ReadIndex] = p_Data[m_ReadIndex];
                    }
                    _ReadIndex += _Count;
                    m_ReadIndex++;
                }
                else
                {
                    _ReturnBytes[_ReadIndex] = _Data;
                    m_ReadIndex++;
                    _ReadIndex++;
                }
                if (_ReadIndex >= _LineWidth) break;
            }
            return _ReturnBytes;
        }
        private byte[] LoadPCXLine1(byte[] p_Data)
        {
            int _LineWidth = m_Head.Bytes_Per_Line;
            byte[] _ReturnBytes = new byte[_LineWidth];
            int _ReadIndex = 0;
            while (true)
            {
                byte _Data = p_Data[m_ReadIndex];
                if (_Data > 0xC0)
                {
                    int _Count = _Data - 0xC0;
                    m_ReadIndex++;
                    for (int i = 0; i != _Count; i++)
                    {
                        _ReturnBytes[i + _ReadIndex] = p_Data[m_ReadIndex];
                    }
                    _ReadIndex += _Count;
                    m_ReadIndex++;
                }
                else
                {
                    _ReturnBytes[_ReadIndex] = _Data;
                    m_ReadIndex++;
                    _ReadIndex++;
                }
                if (_ReadIndex >= _LineWidth) break;
            }
            return _ReturnBytes;
        }
        #endregion 
    }
}
