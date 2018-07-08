using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    interface IImageService
    {
        BitmapImage OpenImage(string path);
        void SaveImage(string path, BitmapImage image);
    }
}
