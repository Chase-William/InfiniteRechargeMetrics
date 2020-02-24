using InfiniteRechargeMetrics.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Templates;
using InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchSetupPage : ContentPage
    {
        public Match Match { get; set; } = new Match();
        public EditMatchRobotsViewModel EditRobotCtx { get; set; }

        public MatchSetupPage()
        {
            InitializeComponent();
            for (int i = 0; i < Match.Robots.Length; i++)
            {
                Match.Robots[i] = new Robot
                {
                    ImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH
                };
            }
            ForEditingRobotsTemplate.Content = new EditMatchRobotsTemplate(Match);
           
            BindingContext = new MatchSetupViewModel(this, Match);

            EditRobotCtx = (EditMatchRobotsViewModel)((EditMatchRobotsTemplate)ForEditingRobotsTemplate.Content).BindingContext;

            PreparePickerOptions();
        }

        /// <summary>
        ///     Loads data from the database and assigns it to the picker
        /// </summary>
        private async void PreparePickerOptions()
        {
            try
            {
                var queriedTeamIds = await DatabaseService.Provider.GetAllTeamsIdAndAliasConcatenatedAsync();
                
                if (queriedTeamIds == null) return;
                else TeamPicker.ItemsSource = queriedTeamIds;
            }
            catch
            {
                await DisplayAlert("Error", "Error populating the team picker view.", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ClearFields();
        }

        private void HandleClearBtnClicked(object sender, EventArgs e)
        {
            EditRobotCtx.ShouldFieldsReset = true;
            ClearFields();
        }

        private void ClearFields()
        {
            var masterCtx = (MatchSetupViewModel)BindingContext;

            if (EditRobotCtx.ShouldFieldsReset)
            {
                masterCtx.TeamId = null;
                masterCtx.TeamPickerSelectedItem = null;
                masterCtx.MatchId = null;
                masterCtx.MatchName = null;

                EditRobotCtx.RobotOneId = null;
                EditRobotCtx.RobotTwoId = null;
                EditRobotCtx.RobotThreeId = null;
                EditRobotCtx.RobotFourId = null;
                EditRobotCtx.RobotFiveId = null;
                EditRobotCtx.RobotSixId = null;

                //editRobotCtx.RobotOneAlias = null;
                //editRobotCtx.RobotTwoAlias = null;
                //editRobotCtx.RobotThreeAlias = null;
                //editRobotCtx.RobotFourAlias = null;
                //editRobotCtx.RobotFiveAlias = null;
                //editRobotCtx.RobotSixAlias = null;

                EditRobotCtx.RobotOneInfo = null;
                EditRobotCtx.RobotTwoInfo = null;
                EditRobotCtx.RobotThreeInfo = null;
                EditRobotCtx.RobotFourInfo = null;
                EditRobotCtx.RobotFiveInfo = null;
                EditRobotCtx.RobotSixInfo = null;

                EditRobotCtx.RobotOneImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                EditRobotCtx.RobotTwoImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                EditRobotCtx.RobotThreeImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                EditRobotCtx.RobotFourImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                EditRobotCtx.RobotFiveImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;
                EditRobotCtx.RobotSixImagePath = StageConstants.DEFAULT_ROBOT_IMAGEPATH;

                var robotTemplate = ((EditMatchRobotsTemplate)ForEditingRobotsTemplate.Content);
                robotTemplate.RobotOneIdPicker.SelectedItem = null;
                robotTemplate.RobotTwoIdPicker.SelectedItem = null;
                robotTemplate.RobotThreeIdPicker.SelectedItem = null;
                robotTemplate.RobotFourIdPicker.SelectedItem = null;
                robotTemplate.RobotFiveIdPicker.SelectedItem = null;
                robotTemplate.RobotSixIdPicker.SelectedItem = null;
            }
        }
    }
}