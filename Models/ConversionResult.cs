
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Models
{
    public class ConversionResult
    {
        public WriteableBitmap ConvertedImage { get; set; }
        public long TimeTaken { get; set; }
    }
}