using Microsoft.Win32;
using System;
using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    class ImageService : IImageService
    {
        private readonly string filterString = "*.JPG|*.JPG";

        public BitmapImage OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filterString;
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(openFileDialog.FileName);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                return img;
            }
            return null;
        }

        public void SaveImage(BitmapSource image)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filterString;
            if (saveFileDialog.ShowDialog() == true)
            {
                JpegBitmapEncoder imageEncoder = new JpegBitmapEncoder();
                imageEncoder.Frames.Add(BitmapFrame.Create(image));
                using (var stream = saveFileDialog.OpenFile())
                {
                    imageEncoder.Save(stream);
                }
            }
        }
    }
}
