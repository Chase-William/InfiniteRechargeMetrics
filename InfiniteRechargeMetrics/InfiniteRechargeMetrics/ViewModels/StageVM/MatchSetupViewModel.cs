using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.MatchPages;
using System.Windows.Input;
using Xamarin.Forms;
using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Pages;

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
            
            
            //Match.RobotOneId
            //Match.RobotOneInfo
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
        private void ValidateStartRecording()
        {
            // No match number was provided
            if (string.IsNullOrWhiteSpace(Match.MatchId))
            {
                App.Current.MainPage.DisplayAlert("Error", "You must provide a match number.", "OK");                
            }
            // No team name was provided in either fields
            else if (TeamPickerSelectedItem == null && string.IsNullOrWhiteSpace(Match.TeamId_FK))
            {
                App.Current.MainPage.DisplayAlert("Error", "You must provide a team name.", "OK");              
            }
            // Everything require has been furfilled
            else
            {
                StartRecording();
            }
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
