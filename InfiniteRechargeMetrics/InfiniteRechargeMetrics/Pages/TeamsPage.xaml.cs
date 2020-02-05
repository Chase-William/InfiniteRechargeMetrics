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
        private ObservableCollection<Team> teams = new ObservableCollection<Team>();
        public ObservableCollection<Team> Teams 
        { 
            get => teams; 
            set 
            {
                teams = value;
            } 
        }

        public TeamsPage()
        {
            InitializeComponent();            
        }

        protected async override void OnAppearing()
        {
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

        //private Task LoadLocalDataAsync()
        //{
        //    // Creating a connection to the db
        //    using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseFilePath))
        //    {
        //        // Creates a table if it doesn't already exist
        //        connection.CreateTable<Team>();
        //        // Returns all the Teams from the db
        //        Teams = new ObservableCollection<Team>(connection.Table<Team>().ToList());
        //    }
        //    // Showing this does run asynchronously
        //    System.Threading.Thread.Sleep(2000);

        //    // Applying changes on main thread
        //    Device.BeginInvokeOnMainThread(() => TeamsListView.ItemsSource = Teams);

        //    return 
        //}

        private void OnNewTeam(object sender, EventArgs e)
        {
            DataService.SaveToDatabase(new Team() { Name = "Test" }, InfiniteRechargeType.Team);
        }
    }
}