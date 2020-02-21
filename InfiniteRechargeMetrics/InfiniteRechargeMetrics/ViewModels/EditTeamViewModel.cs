using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using System.Collections.Generic;

namespace InfiniteRechargeMetrics.ViewModels
{  
    public class EditTeamViewModel : NotifyClass
    {
        public ICommand SaveEditingCMD { get; set; }
        public ICommand SetTeamImageCMD { get; set; }
        public ICommand CancelEditingCMD { get; set; }

        public Team CurrentTeam { get; set; }

        /// <summary>
        ///     Determines whether this should be set as the home team
        /// </summary>
        public bool IsSetToBeHomeTeam { get; set; }

        private string teamId;
        public string TeamId { 
            get => teamId;
            set
            {
                teamId = value;
                NotifyPropertyChanged();
            } 
        }
        private string teamAlias;
        public string TeamAlias { 
            get => teamAlias;
            set
            {
                teamAlias = value;
                NotifyPropertyChanged();
            } 
        }

        public EditTeamViewModel(Team _team)
        {
            CurrentTeam = _team;
            SaveEditingCMD = new Command(SaveAndFinishEditting);
            SetTeamImageCMD = new Command(SetTeamImage);
            CancelEditingCMD = new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
        }

        /// <summary>
        ///     Saves the changes and sends the user to the home page for their team.
        /// </summary>
        private async void SaveAndFinishEditting()
        {
            try
            {
                // Does a team with the same name exist
                Team teamExist = await DatabaseService.Provider.GetTeamAsync(TeamId);
                
                // If there isnt a team that already exist do:
                if (teamExist == null)
                {
                    // Is the current team being set as the home team?
                    if (IsSetToBeHomeTeam)
                    {
                        Team currentHomeTeam = await DatabaseService.Provider.GetHomeTeamAsync();

                        // Does a home team already exist?
                        if (currentHomeTeam == null)
                        {
                            // Save Team as home team
                            SaveToDatabase();

                            await App.Current.MainPage.Navigation.PopModalAsync();                            
                        }   
                        // There was no team set as the home team so:
                        else
                        {
                            // A team with the same home team already exist so:
                            if (await App.Current.MainPage.DisplayAlert("Warning", $"The Team Id: {TeamId} already exist. Would you like to set this team as your new home team?", "Yes", "No"))
                            {
                                // Removing the home status from the current home team
                                await DatabaseService.Provider.RemoveHomeStatusFromTeamAsync(currentHomeTeam.TeamId);
                                // Save Team as home team
                                SaveToDatabase();

                                await App.Current.MainPage.Navigation.PopModalAsync();
                            }
                        }
                    }
                    // The current team is not being set as the home team so:
                    else
                    {
                        // save team not as home team
                        await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Models.Team()
                        {
                            TeamId = TeamId,
                            Alias = TeamAlias
                        });

                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }                                       
                }
                // A team with that the inputted Id already exist.. Overwrite or change                
                else
                {
                    // Overwrite
                    if(await App.Current.MainPage.DisplayAlert("Display", "A team with the Id: {} already exist. Would you like to override it?", "Yes", "No"))
                    {
                        // Overrwite with new Team.. check for performances and points linked to this
                    }
                }
            }
            catch 
            {
                await App.Current.MainPage.DisplayAlert("Error", "Was unable to successfully save your team.", "OK");
            }            

            // Helper function for saving to the database as the home
            async void SaveToDatabase()
            {
                // Save Team as home team
                await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Models.Team()
                {
                    TeamId = TeamId,
                    Alias = TeamAlias,
                    IsHomeTeam = true
                });
            }
        }        

        /// <summary>
        ///     Pulls up the UI required for the user to choose a way to set the image of their team.
        /// </summary>
        private void SetTeamImage()
        {

        }
    }
}
