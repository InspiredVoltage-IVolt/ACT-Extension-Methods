using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ACT.Core.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Get the Base64 String That Represents an Image <see cref="T:System.Drawing.Image" />
        /// </summary>
        /// <param name="image"><see cref="T:System.Drawing.Image" /> To Convert</param>
        /// <param name="format"><see cref="T:System.Drawing.Imaging.ImageFormat" /> Image Format to Save the Base64 Image As</param>
        /// <returns>Base 64 String: <see cref="T:System.String" /></returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, format);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Convert a Base64 Image String to a System.Drawing.Image
        /// </summary>
        /// <param name="ImgBase64">Base 64 Encoded Image</param>
        /// <returns></returns>
        public static Image ConvertBase64ToImage(this string ImgBase64)
        {
            Image image;
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(ImgBase64)))
            {
                image = Image.FromStream(memoryStream);
            }

            return image;
        }

        /// <summary>Convert The Image To a New Format</summary>
        /// <param name="theImage"></param>
        /// <param name="NewFormat"></param>
        /// <returns></returns>
        public static byte[] ConvertToFormat(this Image theImage, ImageFormat NewFormat)
        {
            byte[] numArray = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                theImage.Save(memoryStream, NewFormat);
                numArray = memoryStream.ToArray();
            }
            return numArray;
        }

        public static byte[] imageToByteArray(this Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }

        /// <summary>Save The Image To Location</summary>
        /// <param name="theImage"></param>
        /// <param name="saveLocation"></param>
        /// <param name="NewFormat"></param>
        public static void SaveImageToLocation(
          this Image theImage,
          string saveLocation,
          ImageFormat NewFormat)
        {
            string directoryName = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            theImage.Save(saveLocation, NewFormat);
        }

        /// <summary>
        /// Saves the image to specific location, save location includes filename
        /// </summary>
        /// <param name="theImage"></param>
        /// <param name="saveLocation"></param>
        public static void saveImageToLocation(this Image theImage, string saveLocation)
        {
            string directoryName = Path.GetDirectoryName(saveLocation);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            theImage.Save(saveLocation);
        }

        /// <summary>Resize The Image and Save IT</summary>
        /// <param name="ImageToResize"></param>
        /// <param name="MaxWidth"></param>
        /// <param name="UseHeightRatio"></param>
        /// <param name="FileName"></param>
        /// <param name="NewFormat"></param>
        public static void ResizeAndSave(
          this Image ImageToResize,
          int MaxWidth,
          bool UseHeightRatio,
          string FileName,
          ImageFormat NewFormat)
        {
            ImageToResize.resizeImage(MaxWidth, UseHeightRatio).SaveImageToLocation(FileName, NewFormat);
        }

        public static void resizeAndSave(
          this Image ImageToResize,
          int max,
          bool UseHeightRatio,
          string FileName)
        {
            ImageToResize.resizeImage(max, UseHeightRatio).saveImageToLocation(FileName);
        }

        public static void resizeImageAndSave(
          string imageLocation,
          int max,
          bool UseHeightRatio,
          string FileName)
        {
            Image.FromFile(imageLocation).resizeImage(max, UseHeightRatio).saveImageToLocation(FileName);
        }

        /// <summary>Convert the Image To Black And White</summary>
        /// <param name="image"></param>
        /// <param name="Threshold"></param>
        /// <returns></returns>
        public static Bitmap convertToBlackAndWhite(this Bitmap image, float Threshold)
        {
            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    if (image.GetPixel(x, y).GetBrightness() > (double)Threshold)
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return image;
        }

        /// <summary>Invert An Images Colors</summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Bitmap ApplyInvert(this Bitmap m)
        {
            BitmapData bitmapdata1 = m.LockBits(new Rectangle(0, 0, m.Width, m.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            int length = bitmapdata1.Stride * bitmapdata1.Height;
            byte[] numArray = new byte[length];
            Marshal.Copy(bitmapdata1.Scan0, numArray, 0, length);
            m.UnlockBits(bitmapdata1);
            for (int index = 0; index < length; index += 4)
            {
                numArray[index] = (byte)(byte.MaxValue - (uint)numArray[index]);
                numArray[index + 1] = (byte)(byte.MaxValue - (uint)numArray[index + 1]);
                numArray[index + 2] = (byte)(byte.MaxValue - (uint)numArray[index + 2]);
            }
            BitmapData bitmapdata2 = m.LockBits(new Rectangle(0, 0, m.Width, m.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            Marshal.Copy(numArray, 0, bitmapdata2.Scan0, length);
            m.UnlockBits(bitmapdata2);
            return m;
        }

        public static Image resizeImage(this Image ImageToResize, int max, bool UseHeightRatio)
        {
            if (UseHeightRatio)
            {
                double num = max / (double)ImageToResize.Height;
                int width = (int)(ImageToResize.Width * num);
                int height = (int)(ImageToResize.Height * num);
                Bitmap bitmap = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawImage(ImageToResize, 0, 0, width, height);
                }

                return bitmap;
            }
            double num1 = max / (double)ImageToResize.Width;
            int width1 = (int)(ImageToResize.Width * num1);
            int height1 = (int)(ImageToResize.Height * num1);
            Bitmap bitmap1 = new Bitmap(width1, height1);
            using (Graphics graphics = Graphics.FromImage(bitmap1))
            {
                graphics.DrawImage(ImageToResize, 0, 0, width1, height1);
            }

            return bitmap1;
        }

        public static Image resizeImage(string imageLocation, int max, bool UseHeightRatio) => Image.FromFile(imageLocation).resizeImage(max, UseHeightRatio);
    }
}
