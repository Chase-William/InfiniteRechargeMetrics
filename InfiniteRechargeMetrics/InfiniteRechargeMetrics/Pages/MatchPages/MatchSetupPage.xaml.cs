using InfiniteRechargeMetrics.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.ViewModels;
using InfiniteRechargeMetrics.Models;
using Xamarin.Essentials;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchSetupPage : ContentPage
    {
        public MatchSetupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new MatchSetupViewModel(this);                        

            PreparePickerOptions();
        }

        /// <summary>
        ///     Loads data from the database and assigns it to the picker
        /// </summary>
        private async void PreparePickerOptions()
        {
            try
            {
                var queriedTeamIds = await DatabaseService.Provider.GetAllTeamsIdPlusName();
                
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

            MatchNumberEntry.Text = null;
            TeamNumberEntry.Text = null;
            TeamPicker.SelectedItem = null;
            TitleEntry.Text = null;
        }
    }
}