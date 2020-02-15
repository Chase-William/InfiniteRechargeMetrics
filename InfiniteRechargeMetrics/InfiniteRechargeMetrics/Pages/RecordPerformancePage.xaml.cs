using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordPerformancePage : ContentPage
    {
        public RecordPerformancePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Setting the binding context to our viewmodel to handle the UI code side
            //BindingContext = new RecordPerformanceViewModel();

            //var teams = await DatabaseService.GetAllTeamsAsync();

            //string[] teamNames = new string[teams.Count];
            //for (int i = 0; i < teams.Count; i++)
            //{
            //    teamNames[i] = teams[i].Name;
            //}
            //TeamPicker.ItemsSource = teamNames;
        }
    }
}