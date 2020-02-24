using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages;
using InfiniteRechargeMetrics.Pages.MatchPages;
using InfiniteRechargeMetrics.ViewModels;
using Plugin.GoogleClient.Shared;
using Xamarin.Forms;

/// <summary>
/// 
///     The purpose of this application is to provide the teams and individuals 
///         of the Infinite Recharge Robotics tournament with a way to track / store data.
/// 
///     Currently all data is local, but the possibility to use a remote server in future 
///         versions is in mind. 
///         
///     
///     Plugins:
///         - https://github.com/praeclarum/sqlite-net
///         - https://github.com/xamarin/GooglePlayServicesComponents (Auth & Basement *ONLY*)
///         - https://github.com/roubachof/Sharpnado.Presentation.Forms
/// 
/// </summary>
namespace InfiniteRechargeMetrics
{
    /// <summary>
    ///     The main entry point for our program.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Location of our database (changes based off OS).
        /// </summary>
        public static string DatabaseFilePath;

        public static GoogleUser GoogleUser;
        public static LoginPageViewModel LoginPageViewModel { get; set; } = new LoginPageViewModel();       

        public App(string _filePath)
        {
            InitializeComponent();
            DatabaseFilePath = _filePath;            
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE1MDg5QDMxMzcyZTM0MmUzMEFvaWJYdzFIeFoxMDE5SEZWQ3FlRmF1VUgxelFvdklNaXNxZUFva25DYkU9");
            MainPage = InitMainMasterPage();            
        }

        /// <summary>
        ///     Sets the launch page based off whether the user has a home team set or not.
        /// </summary>
        private MainMasterPage InitMainMasterPage()
        {
            Team homeTeam = new Team();
            try
            {
                homeTeam = DatabaseService.Provider.GetHomeTeam();
            }
            catch { }
            MainMasterPage mainMasterDetail = new MainMasterPage();                        
            // Take the user to a record page
            if (homeTeam == null)
            {
                mainMasterDetail.Detail = new NavigationPage(new MatchSetupPage());
            }
            // Take the user to home page
            else
            {
                mainMasterDetail.Detail = new NavigationPage(new HomeTeamPage(homeTeam));
            }
            return mainMasterDetail;
        }

        protected async override void OnStart()
        {
            //await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team { TeamId = "0001", Alias = "Name 1", ImagePath = "blue_rebel_icon" });
            //await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team { TeamId = "0002", Alias = "Name 2", ImagePath = "red_rebel_icon" });
            //await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team { TeamId = "0003", Alias = "Name 3", ImagePath = "blue_rebel_icon" });
            //await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team { TeamId = "0004", Alias = "Name 4", ImagePath = "blue_rebel_icon" });
            //await DatabaseService.Provider.SaveTeamToLocalDBAsync(new Team { TeamId = "0005", Alias = "Name 5", ImagePath = "red_rebel_icon" });

            //DatabaseService.SaveToDatabase(new Match { Title = "Match 1", TeamOnePerformance_FK = 1 });
            //DatabaseService.SaveToDatabase(new Match { Title = "Match 2", TeamOnePerformance_FK = 1 });
            //DatabaseService.SaveToDatabase(new Match { Title = "Match 3", TeamOnePerformance_FK = 1 });
            //DatabaseService.SaveToDatabase(new Match { Title = "Match 4", TeamTwoPerformance_FK = 1 });
            //DatabaseService.SaveToDatabase(new Match { Title = "Match 5", TeamTwoPerformance_FK = 1 });

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 1",
            //    Id = 1,
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 2",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 3",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 4",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 5",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});


            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 1",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 1",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});

            //DatabaseService.SaveToDatabase(new Performance()
            //{
            //    TeamId_FK = "Name 1",
            //    AutoLowPoint = 9,
            //    AutoMedPoint = 11,
            //    AutoHighPoint = 3,
            //    ManualLowPoint = 12,
            //    ManualMedPoint = 10,
            //    ManualHighPoint = 4
            //});
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
