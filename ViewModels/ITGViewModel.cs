using ImageToGrayscale.Commands;
using ImageToGrayscale.Models;
using ImageToGrayscale.Services.Interfaces;
using PropertyChanged;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.ViewModels
{
    public class ITGViewModel : BaseViewModel
    {
        private readonly IImageProcessor _imageProcessor;
        private readonly IDialogService _dialogService;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IMessageService _messageService;
        private readonly HashSet<PixelFormat> _allowedColorFormats = new() { PixelFormats.Bgr32, PixelFormats.Bgra32, PixelFormats.Pbgra32, PixelFormats.Rgb24 };

        public ITGViewModel(IImageProcessor imageProcessor, IDialogService dialogService, IFileProcessingService fileProcessingService, IMessageService messageService)
        {
            _imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _fileProcessingService = fileProcessingService ?? throw new ArgumentNullException(nameof(fileProcessingService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
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
            string filePath = _dialogService.OpenFileDialog("Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png");
            if (!string.IsNullOrEmpty(filePath))
            {
                SelectedImagePath = filePath;
                ResetConvertedImage();
                DisplayOriginalImage();
            }
        }

        private void ConvertImage()
        {
            BitmapImage originalImage = new BitmapImage(new Uri(SelectedImagePath, UriKind.RelativeOrAbsolute));
            if (!_allowedColorFormats.Contains(originalImage.Format))
            {
                Debug.WriteLine("Unsupported color format");
                _messageService.ShowMessage("Unsupported color format", MessageType.Warning);
                return;
            }

            ConversionResultDTO result = UseParallelization
                ? _imageProcessor.ConvertToGrayscaleParallel(originalImage)
                : _imageProcessor.ConvertToGrayscaleSequential(originalImage);

            ConvertedImage = result.ConvertedImage;
            _messageService.ShowMessage($"The conversion took {result.TimeTaken} milliseconds", MessageType.Information);
        }

        private void SaveImage()
        {
            string filePath = _dialogService.SaveFileDialog("JPEG Image|*.jpg|PNG Image|*.png");
            if (!string.IsNullOrEmpty(filePath))
            {
                _fileProcessingService.WritableBitmapToFile(ConvertedImage, filePath);
            }
        }

        private void DisplayOriginalImage() => OriginalImage = _fileProcessingService.FileToBitmapImage(SelectedImagePath);
        private void ResetConvertedImage() => ConvertedImage = null;

        private void ExecuteSafeAction(Action action)
        {
            try { action(); }
            catch (Exception ex)
            {
                Debug.WriteLine($"Operation error: {ex.Message}");
                _messageService.ShowMessage(
                    $"Three things are certain: \n" +
                    $"Death, taxes and lost data.\n" +
                    $"Guess which has occured.",
                    MessageType.Error);
            }
        }
    }
}