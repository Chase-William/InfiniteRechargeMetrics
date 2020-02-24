using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Templates;
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
    public partial class ViewTeamDetailsPage : ContentPage
    {
        public ViewTeamDetailsPage(Team _team)
        {
            InitializeComponent();
            Content = new TeamStatsTemplate(_team);
        }
    }
}