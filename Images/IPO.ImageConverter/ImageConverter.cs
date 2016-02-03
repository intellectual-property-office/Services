using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using IPO.ImageUtilities.Contracts;

namespace IPO.ImageConverter
{
    public class ImageConverter : IImageConverter
    {
        public byte[] ConvertImage(byte[] sourceImageData, ImageFormat targetImageFormat)
        {
            using (var sourceImageStream = new MemoryStream(sourceImageData))
            {
                var sourceImage = Image.FromStream(sourceImageStream);

                using (var targetStream = new MemoryStream())
                {
                    sourceImage.Save(targetStream, targetImageFormat);

                    return targetStream.GetBuffer();
                }
            }
        }

        public async Task<byte[]> ConvertImageAsync(byte[] sourceImageData, ImageFormat targetImageFormat)
        {
            return await Task.Run(() => ConvertImage(sourceImageData, targetImageFormat));
        }
    }
}