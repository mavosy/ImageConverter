using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Services.Interfaces
{
    public interface IFileProcessingService
    {
        BitmapImage FileToBitmapImage(string filePath);
        void WritableBitmapToFile(WriteableBitmap image, string filePath);
    }
}