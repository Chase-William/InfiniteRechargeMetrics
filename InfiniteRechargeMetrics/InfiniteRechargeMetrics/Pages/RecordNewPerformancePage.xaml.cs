using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordNewPerformancePage : ContentPage
    {
        public RecordNewPerformancePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var teams = await Data.DatabaseService.GetAllTeamsAsync();

            string[] teamNames = new string[teams.Count];
            for (int i = 0; i < teams.Count; i++)
            {
                teamNames[i] = teams[i].Name;
            }
            TeamPicker.ItemsSource = teamNames;
        }
    }
}