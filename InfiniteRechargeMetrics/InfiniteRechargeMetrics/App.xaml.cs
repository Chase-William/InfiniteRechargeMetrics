using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();            
        }

        public App(string _filePath)
        {
            InitializeComponent();
            MainPage = new AppShell();
            DatabaseFilePath = _filePath;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
