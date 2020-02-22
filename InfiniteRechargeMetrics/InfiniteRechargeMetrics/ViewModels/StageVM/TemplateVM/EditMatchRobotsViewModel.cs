using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using System;
using Plugin.Media;
using System.Collections.Generic;

namespace InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM
{
    public class EditMatchRobotsViewModel : NotifyClass
    {
        public Match Match { get; set; }
        private string[] robotNameAndIds;
        public string[] RobotNameAndIds { 
            get => robotNameAndIds;
            set
            {
                robotNameAndIds = value;
                NotifyPropertyChanged();
            }
        }
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

        #region Robot Id Intermediate Props
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
        #endregion
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
            LoadRobotPicker();
        }

        private async void LoadRobotPicker()
        {
            RobotNameAndIds = await Data.DatabaseService.Provider.GetAllRobotIdAndAliasConcatenatedAsync();
        }

        /// <summary>
        ///     Validates that the user can store an image at runtime and generates required UI.
        ///     Retreives that picture using CrossMedia and saves the path into the corresponding robot's 
        ///         ImagePath property.
        /// </summary>
        /// <param name="_index"> Index passed to know which robot the request was called upon </param>
        private async void GetPicture(object _index)
        {
            int index  = int.Parse((string)_index);

            if (string.IsNullOrEmpty(Match.Robots[index].RobotId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "In order to save a picture of the robot you must enter a valid Id first (New or Existing).", "OK");
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

            RobotOneImagePath = file.Path;
            
            await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
                            
            //meow.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});
        }
    }
}
