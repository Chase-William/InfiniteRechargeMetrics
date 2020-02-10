using InfiniteRechargeMetrics.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        protected async override void OnAppearing()
        {
            TeamsListView.IsEnabled = true;
            base.OnAppearing();
            Teams = new ObservableCollection<Team>(await DatabaseService.GetAllTeams());
            TeamsListView.ItemsSource = Teams;
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