using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;


namespace PuzzleMaster
{
    public class Picture
    {
        public uint id { get; set; }

        public byte[] picture { get; set; }

        public Picture() { }

        public Picture(byte[] picture)
        {
            this.picture = picture;
        }

        public Picture(uint id, byte[] picture)
        {
            this.id = id;
            this.picture = picture;
        }

        // Imag hat viele unnötige Daten, daher wird dies in einen Base64String konvertiert um daten zu sparen

        public BitmapImage getPictureAsImageForButtons()
        {
            string str = Encoding.ASCII.GetString(picture);
            byte[] tmp = Convert.FromBase64String(str);
            using (var ms = new MemoryStream())
            {
                ms.Write(tmp, 0, tmp.Length);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }            
        }

        public BitmapImage getPictureAsBitmapImage()
        {
            //string str = Encoding.ASCII.GetString(picture);
            //byte[] tmp = Convert.FromBase64String(str);


            //using (var ms = new MemoryStream())
            //{
            //    ms.Write(tmp, 0, tmp.Length);
            //    ms.Seek(0, SeekOrigin.Begin);

            //    var convertedImage = new BitmapImage();
            //    convertedImage.BeginInit();
            //    convertedImage.CacheOption = BitmapCacheOption.OnLoad;
            //    convertedImage.StreamSource = ms;
            //    convertedImage.EndInit();

            //    return convertedImage;
            //}

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(picture);
            bi.EndInit();
            return bi;
        }

        public Image getPictureAsImage()
        {
            string str = Encoding.ASCII.GetString(picture);
            byte[] tmp = Convert.FromBase64String(str);

            using (var ms = new MemoryStream())
            {
                ms.Write(tmp, 0, tmp.Length);
                ms.Seek(0, SeekOrigin.Begin);

                Image convertedImage = Image.FromStream(ms);
                return convertedImage;
            }
        }
    }
}
