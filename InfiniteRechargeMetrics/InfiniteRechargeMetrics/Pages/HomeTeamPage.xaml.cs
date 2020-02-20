using InfiniteRechargeMetrics.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InfiniteRechargeMetrics.Templates;
using InfiniteRechargeMetrics.Data;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTeamPage : ContentPage
    {
        public HomeTeamPage()
        {
            InitializeComponent();

            Team homeTeam = new Team();
            try
            {
                homeTeam = DatabaseService.Provider.GetHomeTeam();
            }
            catch { }

            // if their is a home team set, display the UI for the team
            if (homeTeam == null)
            {
                Content = new SetHomeTeamTemplate();
            }
            // otherwise there is no home team set, therefore display the set home team view
            else
            {
                Content = new TeamStatsTemplate();
            }
        }

        /// <summary>
        ///     Parameterized Contructor used by the APP on startup based off if a hometeam is set in the local database.
        /// </summary>
        public HomeTeamPage(Team _homeTeam) 
        { 
            InitializeComponent();
            BindingContext = _homeTeam;
        }

    }
}