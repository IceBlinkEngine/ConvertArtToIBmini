using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConvertArtToIBmini
{
    public partial class Form1 : Form
    {
        public ImagePcx imagepcx;
        public string combatTokenFruaPath = "";
        public string combatTokenIB2Path = "";
        public string tilesFruaPath = "";
        public string tilesIB2Path = "";
        public string mainDirectory;

        public Form1()
        {
            InitializeComponent();
            mainDirectory = Directory.GetCurrentDirectory();
            combatTokenFruaPath = mainDirectory + "\\CombatTokensFRUA";
            combatTokenIB2Path = mainDirectory + "\\CombatTokensIBmini";
            tilesFruaPath = mainDirectory + "\\TilesFRUA";
            tilesIB2Path = mainDirectory + "\\TilesIBmini";
        }

        private void btnCreateTokens_Click(object sender, EventArgs e)
        {
            btnCreateTokens.Enabled = false;
            if (Directory.Exists(combatTokenFruaPath))
            {
                string[] files = Directory.GetFiles(combatTokenFruaPath, "*.pcx");
                foreach (string file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        string filenameNoExt = Path.GetFileNameWithoutExtension(file);
                        if ((filename.EndsWith(".pcx")) || (filename.EndsWith(".PCX")))
                        {
                            imagepcx = new ImagePcx(file);
                            Bitmap toSave = createCombatToken(imagepcx.PcxImage);
                            toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                            toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                            toSave.MakeTransparent(Color.FromArgb(100, 244, 156));
                            toSave.MakeTransparent(Color.FromArgb(127, 255, 255));
                            toSave.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            toSave.Save(combatTokenIB2Path + "\\tkn_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error While Creating Tokens: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Tokens Source directory does not exist");
            }
            btnCreateTokens.Enabled = true;
        }
        public Bitmap createCombatToken(Bitmap b)
        {
            Bitmap returnBitmap = new Bitmap(48, 96);
            if (isTokenArea(b, 175, 95)) //if normal token
            {
                returnBitmap = new Bitmap(24, 48);
            }
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                if (isTokenArea(b, 40, 95)) //wide
                {
                    //Draw top token
                    Rectangle source = new Rectangle(8, 84, 48, 24);
                    Rectangle target = new Rectangle(0, 12, 48, 24);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(64, 84, 48, 24);
                    target = new Rectangle(0, 60, 48, 24);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if (isTokenArea(b, 170, 50)) //tall
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 22, 24, 48);
                    Rectangle target = new Rectangle(12, 0, 24, 48);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(192, 22, 24, 48);
                    target = new Rectangle(12, 48, 24, 48);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if (isTokenArea(b, 40, 50)) //large
                {
                    //Draw top token
                    Rectangle source = new Rectangle(8, 22, 48, 48);
                    Rectangle target = new Rectangle(0, 0, 48, 48);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(64, 22, 48, 48);
                    target = new Rectangle(0, 48, 48, 48);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else //normal
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 84, 24, 24);
                    Rectangle target = new Rectangle(0, 0, 24, 24);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(192, 84, 24, 24);
                    target = new Rectangle(0, 24, 24, 24);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
            }
            return returnBitmap;
        }
        public Point findTopLeft(Bitmap b)
        {
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    var pixel = b.GetPixel(x, y);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //still black pixel
                    }
                    else
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }
        public Point findBottomRight(Bitmap b)
        {
            for (int x = b.Width - 1; x >= 0; x--)
            {
                for (int y = b.Height - 1; y >= 0; y--)
                {
                    var pixel = b.GetPixel(x, y);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //still black pixel
                    }
                    else
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }
        public bool isTokenArea(Bitmap b, int x, int y)
        {
            int blackPixelCount = 0;
            for (int xx = x - 2; xx < x + 3; xx++)
            {
                for (int yy = y - 2; yy < y + 3; yy++)
                {
                    var pixel = b.GetPixel(xx, yy);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //found a black pixel
                        blackPixelCount++;
                    }
                }
            }
            if (blackPixelCount > 20)
            {
                //most pixels are black so not token area
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCreateTiles_Click(object sender, EventArgs e)
        {
            btnCreateTiles.Enabled = false;
            if (Directory.Exists(tilesFruaPath))
            {
                string[] files = Directory.GetFiles(tilesFruaPath, "*.pcx");
                foreach (string file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        string filenameNoExt = Path.GetFileNameWithoutExtension(file);
                        if ((filename.EndsWith(".pcx")) || (filename.EndsWith(".PCX")))
                        {
                            imagepcx = new ImagePcx(file);
                            if (makeTileDown(imagepcx.PcxImage)) //make two versions, up and down
                            {
                                //at top
                                Bitmap toSave = createTile(imagepcx.PcxImage, true);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + "_U.png", System.Drawing.Imaging.ImageFormat.Png);
                                //at bottom
                                toSave = createTile(imagepcx.PcxImage, false);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + "_D.png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                            else
                            {
                                Bitmap toSave = createTile(imagepcx.PcxImage, true);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error While Creating Tiles: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Tile Source directory does not exist");
            }
            btnCreateTiles.Enabled = true;
        }
        public bool makeTileDown(Bitmap b)
        {
            int top = 0;
            int left = 0;
            int bottom = 0;
            int right = 0;

            if (WallIsAt90(b)) //wall is at 65,60
            {
                top = 60;
                left = 65;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }
            else //wall is at 130,60 -> 187,117 (56x56)
            {
                top = 60;
                left = 130;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }
            int width = right - left - 1;
            int height = bottom - top - 1;
            //if tile is 56 wide but not 56 tall then this is a tile that could be shifted down, return true
            if ((width == 56) && (height < 56))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Bitmap createTile(Bitmap b, bool makeTileUp)
        {
            Bitmap returnBitmap = new Bitmap(48, 48);
            int top = 0;
            int left = 0;
            int bottom = 0;
            int right = 0;

            if (WallIsAt90(b)) //wall is at 65,60
            {
                top = 60;
                left = 65;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }
            else //wall is at 130,60 -> 187,117 (56x56)
            {
                top = 60;
                left = 130;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }

            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Default;

                float scale = 48.0f / 56.0f;

                int width = right - left - 1;
                int height = bottom - top - 1;
                float widthScaled = width * scale;
                float heightScaled = height * scale;
                float shiftXscaled = 0f;
                float shiftYscaled = 0f;
                //if tile is 56 wide but not 56 tall then this is a tile that should be at the top of the tile, shift up only
                if ((width == 56) && (height < 56))
                {
                    shiftXscaled = 0f;
                    if (makeTileUp)
                    {
                        shiftYscaled = 0f;
                    }
                    else
                    {
                        shiftYscaled = (float)(56 - height) * scale;
                    }
                }
                //if tile is under 56x56 and not a top tile, shift to bottom and right 8 pixels
                else if ((width < 56) && (height < 56))
                {
                    shiftXscaled = 8f;
                    shiftYscaled = (float)(56 - height) * scale;
                }
                else //if tile is 56x56, do not shift
                {
                    shiftXscaled = 0f;
                    shiftYscaled = 0f;
                }

                //Draw tile centered at bottom
                //int shiftX = (int)(((56f - (float)width) / 2f) * (100.0f / 56.0f));
                //int shiftY = (int)((56f - (float)height) * (100.0f / 56.0f));
                RectangleF source = new RectangleF(left + 1, top + 1, width - 1, height - 1);
                RectangleF target = new RectangleF(shiftXscaled, shiftYscaled, widthScaled, heightScaled);
                g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
            }
            return returnBitmap;
        }
        public bool WallIsAt90(Bitmap b)
        {
            //try traversing vertically along x = 100 to find a non-black pixel, if you do then you found the wall tile.
            for (int y = 0; y < b.Height; y++)
            {
                var pixel = b.GetPixel(90, y);
                if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                {
                    //still black pixel
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public int traverseDown(int x, int startY, Bitmap b)
        {
            for (int y = startY; y < b.Height; y++)
            {
                var pixel = b.GetPixel(x, y);
                if ((pixel.A == 255) && (pixel.R == 252) && (pixel.G == 100) && (pixel.B == 252))
                {
                    //still magenta R=252, G=100, B=252
                }
                else
                {
                    return y - 1;
                }
            }
            return -1;
        }
        public int traverseRight(int startX, int y, Bitmap b)
        {
            for (int x = startX; x < b.Height; x++)
            {
                var pixel = b.GetPixel(x, y);
                if ((pixel.A == 255) && (pixel.R == 252) && (pixel.G == 100) && (pixel.B == 252))
                {
                    //still magenta R=252, G=100, B=252
                }
                else
                {
                    return x - 1;
                }
            }
            return -1;
        }

        private void btnCreatePortraits_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateItemIcons_Click(object sender, EventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string help = "To create combat tokens, place any FRUA combat token PCX files in the CombatTokensFRUA"
            + " folder and then press the 'Create Combat Tokens' button. All the converted tokens will"
            + " be placed in the CombatTokensIB2 folder." + Environment.NewLine
            + " To create tiles, place any FRUA wall tile PCX files in the TilesFRUA folder and then press the"
            + " 'Create Tiles' button. All the converted tiles will be placed in the TilesIB2 folder.";

            MessageBox.Show(help);
        }
    }
}
