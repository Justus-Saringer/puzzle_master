using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using Image = System.Drawing.Image;
using System.Windows.Media.Imaging;
using System.IO;

namespace PuzzleMaster
{
    // Das hat der Patrick gemacht
    class PuzzlePiece
    {
        public Point originalPos
        {
            get
            {
                return new Point(originalPosX, originalPosY);
            }
        }

        public int originalPosX { get; set; }
        public int originalPosY { get; set; }

        public byte[] picture { get; set; }

        // Konstruktoren
        public PuzzlePiece(Point originalPos, byte[] picture)
        {
            this.originalPosX = originalPos.X;
            this.originalPosY = originalPos.Y;
            this.picture = picture;
        }

        public PuzzlePiece()
        { }
        

        public Point getOriginalPos()
        {
            return originalPos;
        }

        public void setOriginalPos(Point newOriginalPos)
        {
            this.originalPosX = newOriginalPos.X;
            this.originalPosY = newOriginalPos.Y;
        }

        public byte[] getPicture()
        {
            return picture;
        }

        public BitmapImage getPictureAsBitmapImage()
        {
            if (picture != null)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(picture);
                bi.EndInit();
                return bi;
            }
                return null;
        }



        public void setPicture(byte[] newPicture)
        {
            picture = newPicture;
        }
    }
}
