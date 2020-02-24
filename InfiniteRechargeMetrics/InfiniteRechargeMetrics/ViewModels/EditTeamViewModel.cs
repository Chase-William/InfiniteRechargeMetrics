using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using System;
using InfiniteRechargeMetrics.Pages;

namespace InfiniteRechargeMetrics.ViewModels
{  
    public class EditTeamViewModel : NotifyClass
    {        
        public ICommand SaveEditingCMD { get; set; }
        public ICommand SetTeamImageCMD { get; set; }
        public ICommand CancelEditingCMD { get; set; }

        public Team NewTeam { get; set; } = new Team();
        public Team OldTeam { get; set; }
        /// <summary>
        ///     Determines whether this should be set as the home team
        /// </summary>
        public bool IsSetToBeHomeTeam { 
            get => NewTeam.IsHomeTeam;
            set => NewTeam.IsHomeTeam = value;
        }
        
        public string TeamId { 
            get => NewTeam.TeamId;
            set
            {
                NewTeam.TeamId = value;
                NotifyPropertyChanged();
            } 
        }

        public string TeamAlias { 
            get => NewTeam.TeamAlias;
            set
            {
                NewTeam.TeamAlias = value;
                NotifyPropertyChanged();
            } 
        }

        public EditTeamViewModel(Team _team)
        {
            OldTeam = new Team
            {
                TeamId = _team.TeamId,
                TeamAlias = _team.TeamAlias,
                DateCreated = _team.DateCreated,
                ImagePath = _team.ImagePath,
                IsHomeTeam = _team.IsHomeTeam
            };
            NewTeam = _team;

            // Picking a random avatar for this team, if the id isn't set..then this team is new
            if (string.IsNullOrEmpty(NewTeam.TeamId))
            {
                Random rnJesus = new Random();
                NewTeam.ImagePath = rnJesus.Next(0, 2) == 0 ? StageConstants.RED_REBEL : StageConstants.BLUE_REBEL;
            }

            SaveEditingCMD = new Command(SaveAndFinishEditting);
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
                Team preExistingTeam = await DatabaseService.Provider.GetTeamAsync(TeamId);

                // Set to being home team:
                if (IsSetToBeHomeTeam)
                {
                    // Check if home team already exist
                    // Check if team exist
                    //      Overwrite and set as home
                    //      New and set as home
                    if (await DatabaseService.Provider.GetHomeTeamAsync() == null)
                    {
                        // If a team with the same Id matches the current team, offer a overwrite option
                        if (preExistingTeam != null)
                        {
                            // Overwrite the the existing team with the new team and make sure to set it as the home
                            if (await App.Current.MainPage.DisplayAlert("Warning", "A team already exist with the Id you have entered. Do you wish to overwrite it? (All the data connected to the old team will transfer to your new team.)", "Yes", "No"))
                            {
                                await DatabaseService.Provider.OverwriteTeamDataWithNewTeamAsync(preExistingTeam, NewTeam, true);
                                LoadNewHomePage();
                            }
                            // The user didn't want to overwrite so therefore just exit this method
                            else
                            {
                                return;
                            }
                        }
                        // there is not pre-Existing team therefore just save and set as home as normal
                        else
                        {
                            await DatabaseService.Provider.SaveTeamToLocalDBAsync(NewTeam);
                            LoadNewHomePage();
                        }
                    }
                    else
                    {
                        if (await App.Current.MainPage.DisplayAlert("Warning", "A team is already set as the home team. Assigning this to be your home team will uncheck the other team as your home team. Is that okay?", "Yes", "No"))
                        {
                            // If a team with the same Id matches the current team, offer a overwrite option
                            if (preExistingTeam != null)
                            {
                                // Overwrite the the existing team with the new team and make sure to set it as the home
                                if (await App.Current.MainPage.DisplayAlert("Warning", "A team already exist with the Id you have entered. Do you wish to overwrite it? (All the data connected to the old team will transfer to your new team.)", "Yes", "No"))
                                {
                                    await DatabaseService.Provider.OverwriteTeamDataWithNewTeamAsync(preExistingTeam, NewTeam, true);
                                    LoadNewHomePage();
                                }
                                // The user didn't want to overwrite so therefore just exit this method
                                else
                                {
                                    return;
                                }
                            }
                            // there is not pre-Existing team therefore just save and set as home as normal
                            else
                            {
                                await DatabaseService.Provider.SaveTeamToLocalDBAsync(NewTeam, true);
                                LoadNewHomePage();
                            }
                        }
                        else
                        {
                            return;
                        }                        
                    }
                                                           
                }
                // Not set to be home team:
                else
                {
                    // Check if team exist
                    //      Overwrite not as home
                    //      New

                    // If a pre-existing team exist give the user an option to overwrite
                    if (preExistingTeam != null)
                    {
                        // Overrwrite the old team:
                        if (await App.Current.MainPage.DisplayAlert("Warning", "A team already exist with the Id you have entered. Do you wish to overwrite it? (All the data connected to the old team will transfer to your new team.)", "Yes", "No"))
                        {
                            await DatabaseService.Provider.OverwriteTeamDataWithNewTeamAsync(preExistingTeam, NewTeam);
                            await App.Current.MainPage.Navigation.PopModalAsync();                            
                        }
                        // Return the user
                        else
                        {
                            return;
                        }
                    }
                    // If there is not pre-existing team then just save the team and send the user back a page
                    else
                    {
                        // Replace the clicked team
                        if (!string.IsNullOrEmpty(OldTeam.TeamId))
                        {
                            await DatabaseService.Provider.OverwriteTeamDataWithNewTeamAsync(OldTeam, NewTeam);
                            await App.Current.MainPage.Navigation.PopModalAsync();
                        }
                        // Save a new instance
                        else
                        {
                            await DatabaseService.Provider.SaveTeamToLocalDBAsync(NewTeam);
                            await App.Current.MainPage.Navigation.PopModalAsync();
                        }
                    }                    
                }
            }
            catch 
            {
                await App.Current.MainPage.DisplayAlert("Error", "Was unable to successfully save your team.", "OK");
            }           
        }        

        /// <summary>
        ///     Loads the home page to display their newly created team.
        /// </summary>
        private void LoadNewHomePage()
        {
            App.Current.MainPage.Navigation.PopModalAsync();
            //App.Current.MainPage.Navigation.PushAsync(new HomeTeamPage());
        }
    }
}
