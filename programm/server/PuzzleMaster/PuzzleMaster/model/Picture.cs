using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PuzzleMaster.model
{
    public class Picture
    {

        public uint id { get; set; }

        public byte[] picture { get; set; }

        public Picture() { }

        public Picture(byte[] picture)
        {
            this.picture = picture;
            cropPictureToSquare();
        }

        public Picture(uint id, byte[] picture)
        {
            this.id = id;
            this.picture = picture;
        }

        public Image getPictureAsImage()
        {
            MemoryStream ms = new MemoryStream(picture);
            return Image.FromStream(ms);
        }

        public Bitmap getPictureAsBitmap()
        {
            MemoryStream ms = new MemoryStream(picture);
            return new Bitmap(ms);
        }

        /** <summary>Lowers the Quality of the picture.</summary>
         *  <param name="quality">Value between 0 and 100. 100 is best image quality.</param> */
        public void lowerPictureQuality(uint quality = 20)
        {
            Image image = getPictureAsImage();

            ImageCodecInfo encoder = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            MemoryStream os = new MemoryStream();
            image.Save(os, encoder, encoderParameters);
            picture = os.ToArray();
        }

        /** <summary>Crop the picture to the given area.</summary> */
        public void cropPicture(Rectangle area)
        {
            Bitmap originalImage = getPictureAsBitmap();
            Bitmap cropedImage = originalImage.Clone(area, originalImage.PixelFormat);
            foreach (int id in originalImage.PropertyIdList)
                cropedImage.SetPropertyItem(originalImage.GetPropertyItem(id));

            MemoryStream os = new MemoryStream();
            cropedImage.Save(os, ImageFormat.Jpeg);
            picture = os.ToArray();
        }

        /** <summary>Crop the picture to the biggest posible square.</summary> */
        private void cropPictureToSquare()
        {
            Image image = getPictureAsImage();
            cropPicture(calculateLargestCentreSquare(image.Width, image.Height));
        }

        private static Rectangle calculateLargestCentreSquare(int width, int height)
        {
            int size = width < height ? width : height;
            int x = (width - size) / 2;
            int y = (height - size) / 2;

            return new Rectangle(x, y, size, size);
        }

        public Picture copy()
        {
            return new Picture(id, picture);
        }

    }
}
