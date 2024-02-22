using ImageToGrayscale.Models;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Services.Interfaces
{
    public interface IImageProcessor
    {
        ConversionResultDTO ConvertToGrayscaleSequential(BitmapImage originalImage);
        ConversionResultDTO ConvertToGrayscaleParallel(BitmapImage originalImage);
    }
}