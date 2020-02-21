using InfiniteRechargeMetrics.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Templates;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchSetupPage : ContentPage
    {
        public Match Match { get; set; } = new Match();

        public MatchSetupPage()
        {
            InitializeComponent();
            for (int i = 0; i < Match.Robots.Length; i++)
            {
                Match.Robots[i] = new Robot();
            }
            ForEditingRobotsTemplate.Content = new EditMatchRobotsTemplate(Match);
            BindingContext = new MatchSetupViewModel(Match);
            PreparePickerOptions();
        }

        /// <summary>
        ///     Loads data from the database and assigns it to the picker
        /// </summary>
        private async void PreparePickerOptions()
        {
            try
            {
                var queriedTeamIds = await DatabaseService.Provider.GetAllTeamsIdPlusNameAsync();
                
                if (queriedTeamIds == null) return;
                else TeamPicker.ItemsSource = queriedTeamIds;
            }
            catch
            {
                await DisplayAlert("Error", "Error populating the team picker view.", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MatchIdEntry.Text = null;
            MatchName.Text = null;
            TeamId.Text = null;
            TeamPicker.SelectedItem = null;
        }
    }
}