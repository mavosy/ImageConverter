
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Models
{
    public class ConversionResultDTO
    {
        public WriteableBitmap ConvertedImage { get; set; }
        public long TimeTaken { get; set; }
    }
}