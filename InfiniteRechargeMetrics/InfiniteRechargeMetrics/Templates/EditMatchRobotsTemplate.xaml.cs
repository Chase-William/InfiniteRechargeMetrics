using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels.StageVM.TemplateVM;
using InfiniteRechargeMetrics.Pages.MatchPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using System;

namespace InfiniteRechargeMetrics.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMatchRobotsTemplate : ContentView
    {
        public EditMatchRobotsTemplate(Match _match,[CallerMemberName] string _senderPage = "")
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(_senderPage))
            {
                throw new Exception("Was unable to retrieve the calling class for the bindingcontext of EditMatchRobotsTemplate");
            }
            // Depending on the caller we will be assigning a different context to give different functionality
            BindingContext = _senderPage == nameof(FinalizeRecordingPage) ? new EditMatchRobotsFinalizeViewModel(DataLayout ,_match) : new EditMatchRobotsViewModel(_match);  
        }
    }
}