using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.MatchPages;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Data;
using System.Linq;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM that handles the setup required for recording a team's performance.
    /// </summary>
    public class MatchSetupViewModel : NotifyClass
    {
        public Match Match { get; set; }
        public ICommand StartRecordingCMD { get; private set; }
        public ICommand ClearCMD { get; private set; }
        private object teamPickerSelectedItem;
        public object TeamPickerSelectedItem { 
            get => teamPickerSelectedItem; 
            set
            {
                teamPickerSelectedItem = value;
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

        public MatchSetupViewModel(Match _match)
        {
            Match = _match;            
            StartRecordingCMD = new Command(ValidateStartRecording);
            // Adding a func for validation to make sure the user has entered the required information.
            ClearCMD = new Command(ClearFields);
        }

        /// <summary>
        ///     Validates the starting of recording the match
        ///     
        ///     To Check:
        ///         MatchNumberEntry
        ///         TeamNumberEntry
        ///         TeamPicker
        ///         
        /// </summary>
        private async void ValidateStartRecording()
        {
            // No match number was provided:
            if (string.IsNullOrWhiteSpace(Match.MatchId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must provide a match number.", "OK");
                return;
            }
            // If a team with the same id exist:
            if (await DatabaseService.Provider.DoesMatchExistAsync(Match.MatchId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "A match already exist with the same id", "OK");
                return;
            }

            // No team name was provided in either fields
            if (TeamPickerSelectedItem == null && string.IsNullOrWhiteSpace(Match.TeamId_FK))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must provide a team name.", "OK");
                return;
            }

            // If a robot has data put into its fields and doesnt have a pk set warn the user
            if (Match.Robots.Any(robot => string.IsNullOrEmpty(robot.RobotId) && !string.IsNullOrEmpty(robot.RobotInfo)))
            {
                await App.Current.MainPage.DisplayAlert("Warning", "You have put information into a robot entry and not given it an id. The robot will not be saved.", "OK");
            }

            // First if the Id is null or empty just return false and iterate to next
            // If not then compare robot id to all id inside robots collection for a match
            // If a match exist then return true for duplicate robot keys
            // If not then return false, not duplicate robot keys found
            if (Match.Robots.Any(robot => { return string.IsNullOrEmpty(robot.RobotId) ? false : robot.RobotId == (string)Match.Robots.SelectMany(x => x.RobotId) ? true : false; }))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Two robots have the same id.", "OK");
                return;
            }

            // If all those checkout, run
            StartRecording();            
        }

        /// <summary>
        ///     Continues the navigation to the next page for recording.
        /// </summary>
        private void StartRecording()
        {
            // Disabling the drawer from sliding out
            //((MainMasterPage)App.Current.MainPage).IsGestureEnabled = false;
            // To use PushAsync we need to stack the page onto this pages own stack navigation            
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MasterRecordMatchPage(Match)));            
        }

        /// <summary>
        ///     Sets the data for this page back to default values
        /// </summary>
        private void ClearFields()
        {
            MatchId = null;
            MatchName = null;
            TeamId = null;
            TeamPickerSelectedItem = null;
        }
    }
}
