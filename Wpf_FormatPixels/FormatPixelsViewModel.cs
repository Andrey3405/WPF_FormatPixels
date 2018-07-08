using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    class FormatPixelsViewModel : INotifyPropertyChanged
    {
        private BitmapImage originalImage;
        public BitmapImage OriginalImage
        {
            get => originalImage;
            set
            {
                originalImage = value;
                OnPropertyChanged("OriginalImage");
            }
        }

        private BitmapImage currentImage;
        public BitmapImage CurrentImage
        {
            get => currentImage;
            set
            {
                currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand command_OpenImage;
        public RelayCommand Command_OpenImage
        {
            get
            {
                return command_OpenImage ??
                    (command_OpenImage = new RelayCommand((object obj) =>
               {
                   OpenFileDialog openFileDialog = new OpenFileDialog();
                   openFileDialog.Filter = "*.BMP|*.BMP|*.JPG|*.JPG";
                   if (openFileDialog.ShowDialog() == true)
                   {
                       Stream stream = File.Open(openFileDialog.FileName, FileMode.Open);
                       BitmapImage img = new BitmapImage();
                       img.BeginInit();
                       img.StreamSource = stream;
                       img.EndInit();
                       CurrentImage = OriginalImage = img;
                   }
               }));
            }
        }

        private RelayCommand command_SetOriginalImage;
        public RelayCommand Command_SetOriginalImage
        {
            get
            {
                return command_SetOriginalImage ??
                    (command_SetOriginalImage = new RelayCommand((object obj) =>
                {
                    CurrentImage = OriginalImage.Clone();
                },
                (object obj) =>
                    {
                        return OriginalImage != null;
                    }
                ));
            }
        }

        private RelayCommand command_SetBlackWhiteImage;
        public RelayCommand Command_SetBlackWhiteImage
        {
            get
            {
                return command_SetBlackWhiteImage ??
                    (command_SetBlackWhiteImage = new RelayCommand((object obj) =>
                    {
                        FormatConvertedBitmap blackWhiteImage = new FormatConvertedBitmap();
                        blackWhiteImage.BeginInit();
                        blackWhiteImage.Source = CurrentImage;
                        blackWhiteImage.DestinationFormat = PixelFormats.BlackWhite;
                        blackWhiteImage.EndInit();
                        //Stream stream = File.Open("C:\\Снимок.PNG", FileMode.Open);
                        //CurrentImage.StreamSource = stream;
                        //stream.Close();
                        CurrentImage = blackWhiteImage.Source as BitmapImage;
                    },
                (object obj) =>
                    {
                        return OriginalImage != null;
                    }
                ));
            }
        }
    }
}