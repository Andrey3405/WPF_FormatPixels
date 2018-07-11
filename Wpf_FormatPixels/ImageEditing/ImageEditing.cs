using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    class ImageEditing
    {
        public static FormatConvertedBitmap MakeImageBlackWhite(BitmapSource bitmapSource)
        {
            FormatConvertedBitmap blackWhiteImage = new FormatConvertedBitmap();
            blackWhiteImage.BeginInit();
            blackWhiteImage.Source = bitmapSource;
            blackWhiteImage.DestinationFormat = PixelFormats.BlackWhite;
            blackWhiteImage.EndInit();
            return blackWhiteImage;
        }

        public static FormatConvertedBitmap MakeImageGray(BitmapSource bitmapSource)
        {
            FormatConvertedBitmap grayImage = new FormatConvertedBitmap();
            grayImage.BeginInit();
            grayImage.Source = bitmapSource;
            grayImage.DestinationFormat = PixelFormats.Gray4;
            grayImage.EndInit();
            return grayImage;
        }
    }
}
