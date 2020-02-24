using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.ViewModels;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsPage : ContentPage
    {
        private TeamsViewModel TeamsViewModel = new TeamsViewModel();

        public TeamsPage()
        {
            InitializeComponent();
            BindingContext = TeamsViewModel;
            searchBar.TextChanged += SearchBar_TextChanged;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                TeamsViewModel.TeamsSearchResults = new ObservableCollection<Team>(await DatabaseService.Provider.GetSearchResultsForTeamAliasAsync(e.NewTextValue));
            else
                TeamsViewModel.TeamsSearchResults = new ObservableCollection<Team>(await DatabaseService.Provider.GetAllTeamsAsync());
        }

        /// <summary>
        ///     This handles the tapped event after the itemselection in our viewmodel has gotten the click.
        ///     Reseting to null will allow us to select the same item again after we've just selected it.
        /// </summary>
        private void TeamsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TeamsViewModel.RefreshCollection();
        }

        /// <summary>
        ///     Handles the text inside the searchbar being changed, then request a query to the databaseservice.
        /// </summary>
        //private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length >= 1)
        //    {
        //        //TeamsListView.ItemsSource = new ObservableCollection<Team>(await DatabaseService.QueryTeamsByName(e.NewTextValue));
        //    }
        //    else
        //    {
        //        TeamsListView.ItemsSource = Teams;
        //    }
        //}       

        //private void OnNewTeam(object sender, EventArgs e)
        //{
        //    //DataService.SaveToDatabase(new Team() { Name = "Test" }, InfiniteRechargeType.Team);
        //}

        ///// <summary>
        /////     Event handler for the TeamsListView's items being tapped.
        ///// </summary>
        //private void OnTeamTapped(object sender, ItemTappedEventArgs e)
        //{           
        //    if (e.Item == null) return;
        //    // Prevent the clicking of multiple items
        //    TeamsListView.IsEnabled = false;

        //    //App.Current.MainPage.Navigation.PushModalAsync(new TeamDetails()
        //    //{
        //    //    BindingContext = (Team)e.Item
        //    //});
        //}
    }
}