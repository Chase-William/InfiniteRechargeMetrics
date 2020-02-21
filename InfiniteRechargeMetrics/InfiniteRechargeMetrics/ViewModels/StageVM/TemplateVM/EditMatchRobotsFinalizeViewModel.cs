using InfiniteRechargeMetrics.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM
{
    public class EditMatchRobotsFinalizeViewModel : EditMatchRobotsViewModel
    {
        public StackLayout DataLayout { get; set; }
        /// <summary>
        ///     These are all the actions that will be filled with our reusable code for setting the properties
        /// </summary>
        private readonly Action<string> RobotOneIdAction;
        private readonly Action<string> RobotTwoIdAction;
        private readonly Action<string> RobotThreeIdAction;
        private readonly Action<string> RoboFourIdAction;
        private readonly Action<string> RobotFiveIdAction;
        private readonly Action<string> RobotSixIdAction;

       

        public EditMatchRobotsFinalizeViewModel(StackLayout _dataLayout, Match _match) : base(_match)
        {
            DataLayout = _dataLayout;


            DetermineUIToRevealOnStart();
        }       

        /// <summary>
        ///     Reveals the robots that should be 
        /// </summary>
        private void DetermineUIToRevealOnStart()
        {

        }

    }
}
