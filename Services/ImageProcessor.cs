using ImageToGrayscale.Models;
using ImageToGrayscale.Services.Interfaces;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageToGrayscale.Services
{
    public class ImageProcessor : IImageProcessor
    {
        // colorimetric weighted multipliers, correcting for human vision
        private const double redWeightedMultiplier = 0.2126;
        private const double greenWeightedMultiplier = 0.7152;
        private const double blueWeightedMultiplier = 0.0722;
        private int _bytesPerPixel, _redOffset, _greenOffset, _blueOffset;

        private void SetPixelFormatValues(PixelFormat pixelFormat)
        {
            if (pixelFormat == PixelFormats.Bgr32 || pixelFormat == PixelFormats.Bgra32 || pixelFormat == PixelFormats.Pbgra32)
            {
                _bytesPerPixel = 4;
                _blueOffset = 0;
                _greenOffset = 1;
                _redOffset = 2;
            }
            else if (pixelFormat == PixelFormats.Rgb24)
            {
                _bytesPerPixel = 3;
                _redOffset = 0;
                _greenOffset = 1;
                _blueOffset = 2;
            }
            else
            {
                throw new NotSupportedException("Pixel format not supported");
            }
        }

        public ConversionResultDTO ConvertToGrayscaleSequential(BitmapImage originalImage)
        {
            SetPixelFormatValues(originalImage.Format);
            WriteableBitmap writableOriginal = new WriteableBitmap(originalImage);
            WriteableBitmap grayscaleImage = new WriteableBitmap(originalImage.PixelWidth, originalImage.PixelHeight, originalImage.DpiX, originalImage.DpiY, PixelFormats.Gray8, null);

            Stopwatch stopwatch = Stopwatch.StartNew();

            grayscaleImage.Lock();
            writableOriginal.Lock();

            unsafe
            {
                byte* originalBuffer = (byte*)writableOriginal.BackBuffer.ToPointer();
                byte* grayBuffer = (byte*)grayscaleImage.BackBuffer.ToPointer();

                int originalStride = writableOriginal.BackBufferStride;
                int grayStride = grayscaleImage.BackBufferStride;

                for (int y = 0; y < grayscaleImage.PixelHeight; y++)
                {
                    for (int x = 0; x < grayscaleImage.PixelWidth; x++)
                    {
                        byte* pixel = originalBuffer + (y * originalStride) + (x * _bytesPerPixel);

                        byte blue = pixel[_blueOffset];
                        byte green = pixel[_greenOffset];
                        byte red = pixel[_redOffset];

                        byte grayValue = (byte)(red * redWeightedMultiplier + green * greenWeightedMultiplier + blue * blueWeightedMultiplier);

                        grayBuffer[y * grayStride + x] = grayValue;
                    }
                }
                grayscaleImage.AddDirtyRect(new Int32Rect(0, 0, grayscaleImage.PixelWidth, grayscaleImage.PixelHeight));
            }

            writableOriginal.Unlock();
            grayscaleImage.Unlock();

            stopwatch.Stop();

            return new ConversionResultDTO
            {
                ConvertedImage = grayscaleImage,
                TimeTaken = stopwatch.ElapsedMilliseconds
            };
        }

        public ConversionResultDTO ConvertToGrayscaleParallel(BitmapImage originalImage)
        {
            SetPixelFormatValues(originalImage.Format);
            WriteableBitmap writableOriginal = new WriteableBitmap(originalImage);
            int width = writableOriginal.PixelWidth;
            int height = writableOriginal.PixelHeight;
            int originalStride = writableOriginal.BackBufferStride;
            byte[] originalPixels = new byte[height * originalStride];
            writableOriginal.CopyPixels(originalPixels, originalStride, 0);

            byte[] grayscalePixels = new byte[width * height];

            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * originalStride + x * _bytesPerPixel;
                    byte blue = originalPixels[index + _blueOffset];
                    byte green = originalPixels[index + _greenOffset];
                    byte red = originalPixels[index + _redOffset];
                    byte grayValue = (byte)(red * redWeightedMultiplier + green * greenWeightedMultiplier + blue * blueWeightedMultiplier);
                    grayscalePixels[y * width + x] = grayValue;
                }
            });

            WriteableBitmap grayscaleImage = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                grayscaleImage = new WriteableBitmap(width, height, originalImage.DpiX, originalImage.DpiY, PixelFormats.Gray8, null);
                grayscaleImage.WritePixels(new Int32Rect(0, 0, width, height), grayscalePixels, width, 0);
            });

            stopwatch.Stop();

            return new ConversionResultDTO
            {
                ConvertedImage = grayscaleImage,
                TimeTaken = stopwatch.ElapsedMilliseconds
            };
        }
    }
}