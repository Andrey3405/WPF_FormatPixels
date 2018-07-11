using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    interface IImageService
    {
        BitmapImage OpenImage();
        void SaveImage(BitmapSource image);
    }
}
