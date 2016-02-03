using System.Drawing;
using System.Threading.Tasks;

namespace IPO.ImageUtilities.Contracts
{
    public interface IImageResizer
    {
        Task<byte[]> ResizeImageAsync(byte[] bytes, int maxWidth, int maxHeight);

        byte[] ResizeImage(byte[] bytes, int maxWidth, int maxHeight);

        Image ResizeImage(Image image, int width, int height);
    }
}