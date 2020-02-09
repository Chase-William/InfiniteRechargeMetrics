using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Pages;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public static LoginPageViewModel LoginPageViewModel { get; set; } = new LoginPageViewModel();

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();         
        }

        public App(string _filePath)
        {
            InitializeComponent();
            
            MainPage = new MainPage();
            DatabaseFilePath = _filePath;
        }



        protected override void OnStart()
        {
            //DatabaseService.SaveToDatabase(new Team { Name = "Name 1" });
            //DatabaseService.SaveToDatabase(new Team { Name = "Name 2" });
            //DatabaseService.SaveToDatabase(new Team { Name = "Name 3" });
            //DatabaseService.SaveToDatabase(new Team { Name = "Name 4" });
            //DatabaseService.SaveToDatabase(new Team { Name = "Name 5" });

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
