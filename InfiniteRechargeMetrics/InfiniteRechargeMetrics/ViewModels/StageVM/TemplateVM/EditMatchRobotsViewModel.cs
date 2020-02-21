using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using System;
using Plugin.Media;

namespace InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM
{
    public class EditMatchRobotsViewModel : NotifyClass
    {
        public Match Match { get; set; }

        private byte displayRobotFrameAmount;
        public byte DisplayRobotFrameAmount
        {
            get => displayRobotFrameAmount;
            set
            {
                // The user could add or subtract past the limit
                if (value <= 6 && value >= 0)
                {
                    displayRobotFrameAmount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public Image image = new Image();

        public Robot[] Robots { 
            get => Match.Robots;
            set
            {
                Match.Robots = value;
            }
        }

        public string RobotOneId { 
            get => Match.Robots[0].RobotId;
            set
            {
                Match.Robots[0].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotTwoId {
            get => Match.Robots[1].RobotId;
            set
            {
                Match.Robots[1].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotThreeId {
            get => Match.Robots[2].RobotId;
            set
            {
                Match.Robots[2].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFourId {
            get => Match.Robots[3].RobotId;
            set
            {
                Match.Robots[3].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotFiveId {
            get => Match.Robots[4].RobotId;
            set
            {
                Match.Robots[4].RobotId = value;
                NotifyPropertyChanged();
            }
        }
        public string RobotSixId {
            get => Match.Robots[5].RobotId;
            set
            {
                Match.Robots[5].RobotId = value;
                NotifyPropertyChanged();
            }
        }

        public string RobotOneImagePath { 
            get => Match.Robots[0].ImagePath;
            set
            {
                Match.Robots[0].ImagePath = value;
                NotifyPropertyChanged();
            }                
        }

        public ICommand GetPictureCMD { get; set; }
        public ICommand RevealARobotCMD { get; set; }
        public ICommand HideRobotCMD { get; set; }

        public EditMatchRobotsViewModel(Match _match)
        {
            Match = _match;            
            RevealARobotCMD = new Command(() => DisplayRobotFrameAmount++);
            HideRobotCMD = new Command(() => DisplayRobotFrameAmount--);
            GetPictureCMD = new Command(GetPicture);            
        }

        private async void GetPicture(object _index)
        {
            int index  = int.Parse((string)_index);

            if (string.IsNullOrEmpty(Match.Robots[index].RobotId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "In order to save a picture of the robot you must enter a valid Id first.", "OK");
                return;
            }

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                // We dont need a massive image when inside the app.. make this changable at some point
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                Directory = "Robots",
                Name = $"{Match.Robots[index].RobotId}.jpg"
            });

            if (file == null)
                return;

            Robots[0].ImagePath = file.Path;

            await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");

            var meow = new Image();
                
            
                
            meow.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
    }
}
