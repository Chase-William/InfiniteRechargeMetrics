using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StageThreePage : ContentPage
    {
        private Match Match { get; set; }
        private StageCompletionManager stageCompletionManager;
        public StageThreePage(Match _match, StageCompletionManager _stageCompletionManager)
        {
            InitializeComponent();
            Match = _match;
            stageCompletionManager = _stageCompletionManager;

            BindingContext =  new StageThreeViewModel(this, Match, stageCompletionManager);
        }
    }
}