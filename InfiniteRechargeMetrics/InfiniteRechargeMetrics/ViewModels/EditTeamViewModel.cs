using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Pages;

namespace InfiniteRechargeMetrics.ViewModels
{  
    public class EditTeamViewModel
    {
        public EditTeamPage EditTeamPage { get; set; }
        public ICommand FinishedEditingCMD { get; set; }
        public ICommand SetTeamImageCMD { get; set; }
        public ICommand CancelEditingCMD { get; set; }

        public EditTeamViewModel(EditTeamPage _editTeamPage)
        {
            EditTeamPage = _editTeamPage;
            FinishedEditingCMD = new Command(SaveAndFinishEditting);
            SetTeamImageCMD = new Command(SetTeamImage);
            CancelEditingCMD = new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
        }

        /// <summary>
        ///     Saves the changes and sends the user to the home page for their team.
        /// </summary>
        private void SaveAndFinishEditting()
        {

        }

        /// <summary>
        ///     Pulls up the UI required for the user to choose a way to set the image of their team.
        /// </summary>
        private void SetTeamImage()
        {

        }
    }
}
