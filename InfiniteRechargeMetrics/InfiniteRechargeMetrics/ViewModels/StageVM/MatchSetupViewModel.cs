using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.MatchPages;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Data;
using System.Linq;
using System.Threading.Tasks;
using InfiniteRechargeMetrics.Pages;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM that handles the setup required for recording a team's performance.
    /// </summary>
    public class MatchSetupViewModel : StageEditRobotBase
    {
        public MatchSetupPage MatchSetupPage { get; set; }
        public ICommand StartRecordingCMD { get; private set; }
        private object teamPickerSelectedItem;
        public object TeamPickerSelectedItem { 
            get => teamPickerSelectedItem; 
            set
            {
                teamPickerSelectedItem = value;
                if (value != null)
                    Match.TeamId_FK = ((string)value).Split(' ')[1];
                NotifyPropertyChanged();
            }
        }
        public string MatchId { 
            get => Match.MatchId;
            set
            {
                Match.MatchId = value;
                NotifyPropertyChanged();
            } 
        }
        public string TeamId
        {
            get => Match.TeamId_FK;
            set
            {
                Match.TeamId_FK = value;
                NotifyPropertyChanged();
            }
        }
        public string MatchName
        {
            get => Match.MatchName;   
            set
            {
                Match.MatchName = value;
                NotifyPropertyChanged();
            }
        }      

        public MatchSetupViewModel(MatchSetupPage _matchSetupPage, Match _match) : base(_match)
        {
            MatchSetupPage = _matchSetupPage;
            StartRecordingCMD = new Command(ValidateStartRecording);
        }

        /// <summary>
        ///     Validates the recording to start and then proceeds to run
        /// </summary>
        private async void ValidateStartRecording()
        {
            // if all validiations pass save
            if (await ValidateMatchInputs() && ValidateRobotsInputs())
            {
                StartRecording();
            }           
        }
        
        /// <summary>
        ///     Validates the match specific data
        /// </summary>
        private async Task<bool> ValidateMatchInputs()
        {
            // No match number was provided:
            if (string.IsNullOrWhiteSpace(Match.MatchId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must provide a match number.", "OK");
                return false;
            }
            // If a team with the same id exist:
            if (await DatabaseService.Provider.DoesMatchExistAsync(Match.MatchId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "A match already exist with the same id", "OK");
                return false;
            }

            // No team name was provided in either fields
            if (TeamPickerSelectedItem == null && string.IsNullOrWhiteSpace(Match.TeamId_FK))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must provide a team name.", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Continues the navigation to the next page for recording.
        /// </summary>
        private void StartRecording()
        {
            // Disabling the drawer from sliding out
            //((MainMasterPage)App.Current.MainPage).IsGestureEnabled = false;
            // To use PushAsync we need to stack the page onto this pages own stack navigation    
            MatchSetupPage.EditRobotCtx.ShouldFieldsReset = true;
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MasterRecordMatchPage(Match)));            
        }
    }
}
