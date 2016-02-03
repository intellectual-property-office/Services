using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using ImageResizer;
using IPO.ImageUtilities.Contracts;

namespace IPO.ImageResizer
{
    public class ImageResizer : IImageResizer
    {
        public Image ResizeImage(Image image, int width, int height)
        {
            using (var sourceImage = new MemoryStream())
            {
                image.Save(sourceImage, image.RawFormat);

                using (var destinationImage = new MemoryStream())
                {
                    var resizeSettings = new ResizeSettings { Width = width, Height = height };

                    ImageBuilder.Current.Build(sourceImage, destinationImage, resizeSettings);
                    return new Bitmap(destinationImage);
                }
            }
        }

        public byte[] ResizeImage(byte[] bytes, int maxWidth, int maxHeight)
        {
            using (var sourceImage = new MemoryStream(bytes))
            using (var destinationImage = new MemoryStream())
            {
                var resizeSettings = new ResizeSettings { MaxHeight = maxHeight, MaxWidth = maxWidth };

                ImageBuilder.Current.Build(sourceImage, destinationImage, resizeSettings);
                return destinationImage.ToArray();
            }
        }

        public async Task<byte[]> ResizeImageAsync(byte[] bytes, int maxWidth, int maxHeight)
        {
            return await Task.Run(() => ResizeImage(bytes, maxWidth, maxHeight));
        }
    }
}