using InfiniteRechargeMetrics.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsPage : ContentPage
    {
        /// <summary>
        ///     Collection of all the Teams in the local database.
        /// </summary>
        public ObservableCollection<Team> Teams { get; set; }

        public TeamsPage()
        {
            InitializeComponent();
            SearchBar.TextChanged += SearchBar_TextChanged;
        }
        protected async override void OnAppearing()
        {
            TeamsListView.IsEnabled = true;
            base.OnAppearing();
            Teams = new ObservableCollection<Team>(await DatabaseService.GetAllTeamsAsync());
            TeamsListView.ItemsSource = Teams;
        }

        /// <summary>
        ///     Handles the text inside the searchbar being changed, then request a query to the databaseservice.
        /// </summary>
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                TeamsListView.ItemsSource = new ObservableCollection<Team>(await DatabaseService.QueryTeamsByName(e.NewTextValue));
            }
            else
            {
                TeamsListView.ItemsSource = Teams;
            }
        }       

        private void OnNewTeam(object sender, EventArgs e)
        {
            //DataService.SaveToDatabase(new Team() { Name = "Test" }, InfiniteRechargeType.Team);
        }

        /// <summary>
        ///     Event handler for the TeamsListView's items being tapped.
        /// </summary>
        private void OnTeamTapped(object sender, ItemTappedEventArgs e)
        {           
            if (e.Item == null) return;
            // Prevent the clicking of multiple items
            TeamsListView.IsEnabled = false;

            App.Current.MainPage.Navigation.PushModalAsync(new TeamDetails()
            {
                BindingContext = (Team)e.Item
            });
        }
    }
}