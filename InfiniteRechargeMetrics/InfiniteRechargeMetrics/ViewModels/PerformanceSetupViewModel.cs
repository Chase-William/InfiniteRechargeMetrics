using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages.PerformancePages;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels
{
    /// <summary>
    ///     MVVM that handles the setup required for recording a team's performance.
    /// </summary>
    public class PerformanceSetupViewModel
    {
        public PerformanceSetup PerformanceSetupPage { get; private set; }
        public Performance Performance { get; set; } = new Performance();
        public ICommand StartRecordingCMD { get; private set; }
        public ICommand ClearCMD { get; private set; }

        public PerformanceSetupViewModel(PerformanceSetup _performanceSetup)
        {
            PerformanceSetupPage = _performanceSetup;
            StartRecordingCMD = new Command(StartRecording);
            ClearCMD = new Command(ClearFields);
        }        

        /// <summary>
        ///     Continues to the next page for recording
        /// </summary>
        private async void StartRecording()
        {
            // To use PushAsync we need to stack the page onto this pages own stack navigation
            await PerformanceSetupPage.Navigation.PushAsync(new MasterRecordPerformancePage(Performance));
        }

        /// <summary>
        ///     Sets the data for this page back to default values
        /// </summary>
        private void ClearFields()
        {
            PerformanceSetupPage.TeamPicker.SelectedItem = null;
        }
    }
}
