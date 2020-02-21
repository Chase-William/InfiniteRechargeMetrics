using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Templates;
using InfiniteRechargeMetrics.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.MatchPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizeRecordingPage : ContentPage
    {
        private Match Match { get; set; }

        public FinalizeRecordingPage(Match _match)
        {
            Match = _match;
            InitializeComponent();

            BindingContext = new FinalizeRecordingViewModel(Match);
            // Setting the contentview for our custom template
            ForEditingRobotsTemplate.Content = new EditMatchRobotsTemplate(Match);
        }
    }
}