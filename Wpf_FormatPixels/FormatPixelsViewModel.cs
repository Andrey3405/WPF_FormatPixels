using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Wpf_FormatPixels
{
    class FormatPixelsViewModel : INotifyPropertyChanged
    {
        private bool standartFilterIsChecked = false;
        public bool StandartFilterIsChecked
        {
            get => standartFilterIsChecked;
            set
            {
                standartFilterIsChecked = value;
                OnPropertyChanged("StandartFilterIsChecked");
            }
        }

        private BitmapSource originalImage;
        public BitmapSource OriginalImage
        {
            get => originalImage;
            set
            {
                originalImage = value;
                OnPropertyChanged("OriginalImage");
            }
        }

        private BitmapSource currentImage;
        public BitmapSource CurrentImage
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
                   IImageService imageService = new ImageService();
                   BitmapImage openImage = imageService.OpenImage();
                   if (openImage != null)
                   {
                       OriginalImage = openImage;
                       StandartFilterIsChecked = true;
                       if (Command_SetOriginalImage.CanExecute(null))
                       {
                           Command_SetOriginalImage.Execute(null);
                       }
                   }
               }));
            }
        }

        private RelayCommand command_SaveImage;
        public RelayCommand Command_SaveImage
        {
            get
            {
                return command_SaveImage ??
                    (command_SaveImage = new RelayCommand((object obj) =>
                {
                    IImageService imageService = new ImageService();
                    imageService.SaveImage(CurrentImage);
                },
                (object obj) =>
                {
                    return CurrentImage != null;
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
                    CurrentImage = OriginalImage;
                },
                (object obj) =>
                { 
                        return OriginalImage != null;
                }));
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
                        CurrentImage =  ImageEditing.MakeImageBlackWhite(OriginalImage);
                    },
                (object obj) =>
                    {
                        return OriginalImage != null;
                    }
                ));
            }
        }

        private RelayCommand command_SetGrayImage;
        public RelayCommand Command_SetGrayImage
        {
            get
            {
                return command_SetGrayImage ??
                    (command_SetGrayImage = new RelayCommand((object obj) =>
                {
                    CurrentImage = ImageEditing.MakeImageGray(OriginalImage);
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