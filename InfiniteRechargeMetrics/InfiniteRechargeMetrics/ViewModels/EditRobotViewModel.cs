using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class EditRobotViewModel : NotifyClass
    {
        public Robot OldRobot { get; set; }
        public Robot NewRobot { get; set; } = new Robot();

        public string RobotComments { 
            get => NewRobot.RobotInfo;
            set
            {
                NewRobot.RobotInfo = value;
                NotifyPropertyChanged();
            } 
        }

        public string RobotId
        {
            get => NewRobot.RobotId;
            set
            {
                NewRobot.RobotId = value;
                if (NewRobot.ImagePath != StageConstants.DEFAULT_ROBOT_IMAGEPATH)
                {
                    NewRobot.ImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                }
                NotifyPropertyChanged();
            }
        }

        public string RobotImagePath
        {
            get => NewRobot.ImagePath;
            set
            {
                NewRobot.ImagePath = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand FinishedEditingCMD => new Command(SaveAndFinishEditting);
        public ICommand SetRobotImageCMD => new Command(TakePicture);
        public ICommand CancelEditingCMD => new Command(async () => await App.Current.MainPage.Navigation.PopModalAsync());

        public EditRobotViewModel(Robot _robot)
        {
            NewRobot = _robot;
            NewRobot.ImagePath = string.IsNullOrEmpty(_robot.ImagePath) ? StageConstants.DEFAULT_ROBOT_IMAGEPATH : _robot.ImagePath;
            OldRobot = new Robot
            {
                RobotId = _robot.RobotId,
                RobotInfo = _robot.RobotInfo,
                ImagePath = _robot.ImagePath
            };
        }

        private async void SaveAndFinishEditting()
        {
            try
            {
                // Does a robot with the same name exist
                Robot preExistingRobot = await DatabaseService.Provider.GetRobotAsync(NewRobot.RobotId);

                // Check if home robot already exist
                // Check if robot exist

                // If a robot with the same Id 
                if (preExistingRobot != null)
                {
                    // Overwrite the existing robot with the new robot
                    if (await App.Current.MainPage.DisplayAlert("Warning", "A team already exist with the Id you have entered. Do you wish to overwrite it?", "Yes", "No"))
                    {
                        await DatabaseService.Provider.OverwriteRobotDataWithNewRobotAsync(preExistingRobot, NewRobot);
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }
                    // The user didn't want to overwrite so therefore just exit this method
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // Replace the clicked robot
                    if (!string.IsNullOrEmpty(OldRobot.RobotId))
                    {
                        await DatabaseService.Provider.OverwriteRobotDataWithNewRobotAsync(OldRobot, NewRobot);
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }
                    // Save a new instance
                    else
                    {
                        await DatabaseService.Provider.SaveRobotToLocalDbASync(NewRobot);
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }
                }                                               
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Was unable to successfully save your team.", "OK");
            }
        }

        private async void TakePicture()
        {
            if (string.IsNullOrEmpty(RobotId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please give the robot an Id before you assign an image.", "OK");
                return;
            }

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    // We dont need a massive image when inside the app.. make this changable at some point
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    Directory = "Robots",
                    Name = $"{NewRobot.RobotId}.jpg"
                });

                RobotImagePath = file.Path;
            }
            catch
            {

            }
        }
    }
}
