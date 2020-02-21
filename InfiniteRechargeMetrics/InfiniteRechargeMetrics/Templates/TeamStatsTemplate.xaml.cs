using InfiniteRechargeMetrics.Data;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels.HomeVM;
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
        public TeamStatsTemplate(Team _team)
        {
            InitializeComponent();
            BindingContext = new TeamStatsViewModel(_team);
        }
    }
}