using Xamarin.Forms;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Data;
using System;

namespace InfiniteRechargeMetrics.ViewModels
{
    public class FinalizeRecordingViewModel : StageEditRobotBase
    {
        public ICommand FinishedRecordingCMD { get; set; }
        public FinalizeRecordingViewModel(Match _match) : base(_match)
        {
            FinishedRecordingCMD = new Command(FinishRecording);            
        }

        /// <summary>
        ///     Starts the finishing process for this performance.
        /// </summary>
        private async void FinishRecording()
        {         
            if (ValidateRobotsInputs())
            {
                // if the team doesn't exist generate it
                if (!await DatabaseService.Provider.DoesTeamExistAsync(Match.TeamId_FK))
                {
                    Random rnJesus = new Random();
                    await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team()
                    {
                        TeamId = Match.TeamId_FK,
                        ImagePath = rnJesus.Next(0, 2) == 0 ? StageConstants.RED_REBEL : StageConstants.BLUE_REBEL
                    });           
                }

                foreach (Robot robot in Match.Robots)
                {
                    if (!string.IsNullOrEmpty(robot.RobotId))
                    {
                        await DatabaseService.Provider.SaveRobotToLocalDbASync(robot);
                    }
                }

                await DatabaseService.Provider.SaveMatchToLocalDBAsync(Match);
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }              
    }
}