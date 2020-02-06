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
    public partial class TeamDetails : ContentPage
    {
        /// <summary>
        ///     A collection of the all the performances the choosen team has.
        /// </summary>
        public ObservableCollection<Performance> Performances { get; set; }

        public TeamDetails()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() =>
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseFilePath))
                {
                    connection.CreateTable<Team>();
                    Performances = new ObservableCollection<Performance>(connection.Table<Performance>().ToList());
                }
            });
            PerformancesListView.ItemsSource = Performances;
        }

        private void OnNewTeam(object sender, EventArgs e)
        {
            //DataService.SaveToDatabase(new Performance() 
            //                            { AutoLowPoint = 9,
            //                              AutoMedPoint = 11,
            //                              AutoHighPoint = 3,
            //                              ManualLowPoint = 12,
            //                              ManualMedPoint = 10,
            //                              ManualHighPoint = 4 },
            //                            InfiniteRechargeType.Team);
        }
    }
}