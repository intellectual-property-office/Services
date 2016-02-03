using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace IPO.ImageUtilities.Contracts
{
    public interface IImageConverter
    {
        byte[] ConvertImage(byte[] sourceImageData, ImageFormat targetImageFormat);

        Task<byte[]> ConvertImageAsync(byte[] sourceImageData, ImageFormat targetImageFormat);
    }
}