using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Pages;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Templates;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM
{
    public class SetHomeTeamViewModel : HomeTeamViewModelBase
    {        
        #region Props
        private string[] teamIdsAndAlias;
        public string[] TeamIdsAndAlias { 
            get => teamIdsAndAlias;
            set
            {
                teamIdsAndAlias = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Determines whether the picker for using an existing class should be visible.
        /// </summary>
        private bool isUsingExisting;
        public bool IsUsingExisting {
            get => isUsingExisting; 
            set
            {
                isUsingExisting = value;
                LoadDataForTeamPicker();
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        ///     Determines whether the save button should be visible (based off if something is selected inside the team picker)
        /// </summary>
        private bool isTeamSelected;
        public bool IsTeamSelected { 
            get => isTeamSelected; 
            set
            {
                isTeamSelected = value;
                NotifyPropertyChanged();
            } 
        }
        /// <summary>
        ///     Keeps the instance of the selected team string
        /// </summary>
        private string selectItemId;
        public string SelectedTeamId { 
            get => selectItemId;
            set
            {
                selectItemId = value;
                if (value != null)
                    IsTeamSelected = true;
                else
                    IsTeamSelected = false;
            } 
        }
        #endregion

        public View Content { get; set; }

        public ICommand SetExistHomeTeamCMD { get; set; }
        public ICommand CreateNewTeamCMD { get; set; }
        public ICommand UseExistingTeamCMD { get; set; }

        public SetHomeTeamViewModel(View _content)
        {
            Content = _content;
            CreateNewTeamCMD = new Command(() => App.Current.MainPage.Navigation.PushModalAsync(new EditTeamPage(new Team())));
            UseExistingTeamCMD = new Command(RevealOrHideTeamPicker);
            SetExistHomeTeamCMD = new Command(SetHomeTeam);
            base.IsTeamEdittingPossible = false;
        }

        /// <summary>
        ///     Loads data for the team picker
        /// </summary>
        private async void LoadDataForTeamPicker()
        {
            TeamIdsAndAlias = await Data.DatabaseService.Provider.GetAllTeamsIdAndAliasConcatenatedAsync();
        }

        /// <summary>
        ///     Reveals or hides the team picker apon clicked
        /// </summary>
        private void RevealOrHideTeamPicker()
        {
            IsUsingExisting = !IsUsingExisting;
        }

        /// <summary>
        ///     Sets the home team and returns the user to it     
        /// </summary>
        private async void SetHomeTeam()
        {
            // Setting the home team value using the id
            try
            {
                var test = selectItemId.Split(' ')[1];
                await Data.DatabaseService.Provider.SetHomeStatusForTeamAsync(selectItemId.Split(' ')[1]);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Unable to save as home team.", "OK");
            }
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new HomeTeamPage());
            //Content = new TeamStatsTemplate(Data.DatabaseService.Provider.GetHomeTeam());
        }
    }
}
