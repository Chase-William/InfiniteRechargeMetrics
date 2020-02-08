using InfiniteRechargeMetrics.Data;
using System;
using System.Collections.ObjectModel;
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
            PerformanceHorizontalListView.Focused += PerformanceHorizontalListView_Focused;
        }

        private void PerformanceHorizontalListView_Focused(object sender, FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() =>
            {
                // Setting the Performances collection's data to the returned data from a database query.
                Performances = new ObservableCollection<Performance>(DatabaseService.GetPerformancesForTeam((Team)this.BindingContext));
            });
            PerformanceHorizontalListView.ItemsSource = Performances;
        }
    }
}