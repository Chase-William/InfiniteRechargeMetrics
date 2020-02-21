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
        private byte displayRobotFrameAmount;
        public byte DisplayRobotFrameAmount { 
            get => displayRobotFrameAmount; 
            set
            {
                // The user could add or subtract past the limit
                if (value <= 6 && value >= 0)
                {
                    displayRobotFrameAmount = value;
                    NotifyPropertyChanged();
                }                    
            } 
        }

        public MatchSetupPage MatchSetupPage { get; private set; }
        public Match Match { get; set; } = new Match();

        public ICommand RevealARobotCMD { get; set; }
        public ICommand HideRobotCMD { get; set; }
        public ICommand StartRecordingCMD { get; private set; }
        public ICommand ClearCMD { get; private set; }

        public MatchSetupViewModel(MatchSetupPage _matchSetupPage)
        {
            MatchSetupPage = _matchSetupPage;            
            StartRecordingCMD = new Command(ValidateStartRecording);
            // Adding a func for validation to make sure the user has entered the required information.
            ClearCMD = new Command(ClearFields);
            RevealARobotCMD = new Command(() => DisplayRobotFrameAmount++);
            HideRobotCMD = new Command(() => DisplayRobotFrameAmount--);
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
            if (string.IsNullOrWhiteSpace(MatchSetupPage.MatchNumberEntry.Text))
            {
                MatchSetupPage.DisplayAlert("Error", "You must provide a match number.", "OK");                
            }
            // No team name was provided in either fields
            else if (MatchSetupPage.TeamPicker.SelectedItem == null && string.IsNullOrWhiteSpace(MatchSetupPage.TeamNumberEntry.Text))
            {
                MatchSetupPage.DisplayAlert("Error", "You must provide a team name.", "OK");              
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
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MasterRecordMatchPage(MatchSetupPage, Match)));            
        }

        /// <summary>
        ///     Sets the data for this page back to default values
        /// </summary>
        private void ClearFields()
        {
            MatchSetupPage.MatchNumberEntry.Text = null;
            MatchSetupPage.TeamNumberEntry.Text = null;
            MatchSetupPage.TeamPicker.SelectedItem = null;
            MatchSetupPage.TitleEntry.Text = null;
        }
    }
}
