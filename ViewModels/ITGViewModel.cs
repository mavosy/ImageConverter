using ImageToGrayscale.Commands;
using ImageToGrayscale.Models;
using Microsoft.Win32;
using PropertyChanged;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.ViewModels
{
    public class ITGViewModel : BaseViewModel
    {
        private readonly ImageProcessor _imageProcessor;
        private readonly HashSet<PixelFormat> _allowedColorFormats = new() { PixelFormats.Bgr32, PixelFormats.Bgra32, PixelFormats.Pbgra32, PixelFormats.Rgb24 };

        public ITGViewModel(ImageProcessor imageProcessor)
        {
            _imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
        }

        [DependsOn(nameof(SelectedImagePath))]
        public bool CanConvertImage => !string.IsNullOrEmpty(SelectedImagePath);

        [DependsOn(nameof(ConvertedImage))]
        public bool CanSaveImage => ConvertedImage is not null;

        public BitmapImage OriginalImage { get; private set; }
        public WriteableBitmap? ConvertedImage { get; private set; }
        public bool UseParallelization { get; set; }
        public string SelectedImagePath { get; private set; }

        public ICommand ChooseImageCommand => new RelayCommand(_ => ExecuteSafeAction(LoadImage));
        public ICommand ConvertImageCommand => new RelayCommand(_ => ExecuteSafeAction(ConvertImage), _ => CanConvertImage);
        public ICommand SaveImageCommand => new RelayCommand(_ => ExecuteSafeAction(SaveImage), _ => CanSaveImage);

        private void LoadImage()
        {
            string filePath = OpenFileDialog("Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png");
            if (!string.IsNullOrEmpty(filePath))
            {
                SelectedImagePath = filePath;
                DisplayOriginalImage();
                ResetConvertedImage();
            }
        }

        private void ConvertImage()
        {
            BitmapImage originalImage = new BitmapImage(new Uri(SelectedImagePath, UriKind.RelativeOrAbsolute));
            if (!_allowedColorFormats.Contains(originalImage.Format))
            {
                Debug.WriteLine("Unsupported color format");
                return;
            }

            ConversionResult result = UseParallelization
                ? _imageProcessor.ConvertToGrayscaleParallel(originalImage)
                : _imageProcessor.ConvertToGrayscaleSequential(originalImage);

            ConvertedImage = result.ConvertedImage;
            MessageBox.Show($"The conversion took {result.TimeTaken} milliseconds");
        }

        private void SaveImage()
        {
            string filePath = SaveFileDialog("JPEG Image|*.jpg|PNG Image|*.png");
            if (!string.IsNullOrEmpty(filePath))
            {
                SaveImageToFile(filePath);
            }
        }

        private string OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        private string SaveFileDialog(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = filter };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }

        private void SaveImageToFile(string filePath)
        {
            BitmapEncoder encoder = filePath.EndsWith(".png") ? new PngBitmapEncoder() : new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(ConvertedImage));

            using (FileStream file = File.OpenWrite(filePath))
            {
                encoder.Save(file);
            }
        }

        private void DisplayOriginalImage() => OriginalImage = new BitmapImage(new Uri(SelectedImagePath, UriKind.RelativeOrAbsolute));
        private void ResetConvertedImage() => ConvertedImage = null;

        private void ExecuteSafeAction(Action action)
        {
            try { action(); }
            catch (Exception ex)
            {
                Debug.WriteLine($"Operation error: {ex.Message}");
                MessageBox.Show(
                    $"Three things are certain: \n" +
                    $"Death, taxes and lost data.\n" +
                    $"Guess which has occured.");
            }
        }
    }
}