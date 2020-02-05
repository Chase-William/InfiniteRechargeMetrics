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

            await Task.Run(() =>
            {
                // Creating a connection to the db
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseFilePath))
                {
                    // Creates a table if it doesn't already exist
                    connection.CreateTable<Team>();
                    // Returns all the Teams from the db
                    Teams = new ObservableCollection<Team>(connection.Table<Team>().ToList());
                }
                // Showing this does run asynchronously
                System.Threading.Thread.Sleep(2500);    // ---------------------------------------------------------------  remove later           
            });
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

            Shell.Current.Navigation.PushAsync(new TeamDetails()
            {
                BindingContext = (Team)e.Item
            });
        }
    }
}