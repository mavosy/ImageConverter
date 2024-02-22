using ImageToGrayscale.Services.Interfaces;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Services
{
    class FileProcessingService : IFileProcessingService
    {
        public BitmapImage FileToBitmapImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
        }

        public void WritableBitmapToFile(WriteableBitmap image, string filePath)
        {
            BitmapEncoder encoder = filePath.EndsWith(".png") ? new PngBitmapEncoder() : new JpegBitmapEncoder();
            Guid photoID = Guid.NewGuid();
            string photolocation = filePath;
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                encoder.Save(file);
            }
        }
    }
}