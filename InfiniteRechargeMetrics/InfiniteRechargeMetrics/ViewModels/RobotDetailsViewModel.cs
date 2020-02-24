using InfiniteRechargeMetrics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class RobotDetailsViewModel
    {
        public Robot Robot { get; set; }

        public ICommand EmailDataCMD => new Command(async () => {
            var current = Connectivity.NetworkAccess;

            // If we have wifi:
            if (current == NetworkAccess.Internet)
            {
                if (App.GoogleUser == null || string.IsNullOrEmpty(App.GoogleUser?.Email))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Please login to use gmail.", "OK");
                    return;
                }
                List<string> emails = new List<string>();
                emails.Add(App.GoogleUser.Email);
                await EmailService.SendEmail("Robot's Data", JsonConvert.SerializeObject(Robot), emails, Robot.ImagePath == StageConstants.DEFAULT_ROBOT_IMAGEPATH ? "" : Robot.ImagePath);
               
            }
            // no wifi
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must be connected to wifi to send your data.", "OK");
                return;
            }
        });

        public ICommand ReturnCMD => new Command(() => App.Current.MainPage.Navigation.PopModalAsync());

        public RobotDetailsViewModel(Robot _robot)
        {
            Robot = _robot; 
        }
    }
}
