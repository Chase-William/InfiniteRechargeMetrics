using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamStatsTemplate : ContentView
    {
        // <summary>
        ///     A collection of the all the performances the choosen team has.
        /// </summary>
        private ObservableCollection<Match> performances = new ObservableCollection<Match>();
        //private ObservableCollection<Match> matches = new ObservableCollection<Match>();
        private int totalMatches;
        private int totalPerformances;

        public ObservableCollection<Match> Performances { 
            get => performances;
            set
            {
                performances = value;
                TotalPerformances = value.Count;
            }
        }

        /// <summary>
        ///     Tracks the total number of Performances.
        /// </summary>
        public int TotalPerformances { 
            get => totalPerformances;
            private set
            {
                totalPerformances = value;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    PerformancesNum.Text = TotalPerformances.ToString();
                });
            }
        }

        /// <summary>
        ///     Tracks the total number of Matches.
        /// </summary>
        public int TotalMatches { get => totalMatches; 
            private set
            {
                totalMatches = value;
                // Updating the text for number of matches 
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MatchesNum.Text = TotalMatches.ToString();
                });
            }
        }

        public View Header
        {
            get => HeaderContent.Content;
            set => HeaderContent.Content = value;
        }
        public View EachPerformance
        {
            get => EachPerformanceContent.Content;
            set => EachPerformanceContent.Content = value;
        }

        public TeamStatsTemplate()
        {
            InitializeComponent();
        }     
    }
}