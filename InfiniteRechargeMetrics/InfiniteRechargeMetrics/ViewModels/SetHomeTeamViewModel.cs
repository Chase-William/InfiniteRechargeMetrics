using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Pages;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class SetHomeTeamViewModel : NotifyClass
    {
        /// <summary>
        ///     Determines whether the picker for using an existing class should be visible.
        /// </summary>
        private bool isUsingExisting;
        public bool IsUsingExisting {
            get => isUsingExisting; 
            set
            {
                isUsingExisting = value;
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

        public ICommand CreateNewTeamCMD { get; set; }
        public ICommand UseExistingTeamCMD { get; set; }

        public SetHomeTeamViewModel()
        {
            CreateNewTeamCMD = new Command(() => App.Current.MainPage.Navigation.PushModalAsync(new EditTeamPage()));
            UseExistingTeamCMD = new Command(RevealOrHideTeamPicker);
        }

        /// <summary>
        ///     Reveals or hides the team picker apon clicked
        /// </summary>
        private void RevealOrHideTeamPicker()
        {
            IsUsingExisting = !IsUsingExisting;
        }
    }
}
