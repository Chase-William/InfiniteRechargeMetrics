using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages.PerformancePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizeRecordingPage : ContentPage
    {
        private const string ADD_REMOVE_BTN_LAYOUT_ID = "add_remove_layout";

        private readonly Performance performance;
        private readonly PerformanceSetupPage performanceSetupPage;

        public FinalizeRecordingPage(PerformanceSetupPage _performanceSetupPage, Performance _performance)
        {
            performanceSetupPage = _performanceSetupPage;
            performance = _performance;
            InitializeComponent();

            BindingContext = new FinalizeRecordingViewModel(performanceSetupPage, this, RobotCommentLayout, performance);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // if a add robot btn doesnt already exist inside the grid.. add one
            if (!(RobotCommentLayout.Children.Any(view => view.AutomationId == ADD_REMOVE_BTN_LAYOUT_ID)))
            {
                StackLayout btnLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    AutomationId = ADD_REMOVE_BTN_LAYOUT_ID
                };

                // https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/resourcedictionary-and-xaml-resource-references
                // We can retrieve the style using the Resources Dictionary, application level
                Style cyanBtnStyle = (Style)App.Current.Resources["CyanBtnStyle"];


                var addRobotBtn = new Button
                {
                    Text = "Add Robot",
                    Style = cyanBtnStyle
                    
                };
                var removeRobotBtn = new Button
                {
                    Text = "Remove Robot",
                    Style = cyanBtnStyle
                };

                // Setting our binding for the CommandProperty
                addRobotBtn.SetBinding(Button.CommandProperty, "AddRobotCMD");
                addRobotBtn.SetBinding(Button.IsVisibleProperty, "IsRobotThreeBeingRecorded", BindingMode.Default, new Converters.InvertBooleanConverter());
                removeRobotBtn.SetBinding(Button.CommandProperty, "RemoveRobotCMD");
                removeRobotBtn.SetBinding(Button.IsVisibleProperty, "IsRobotOneBeingRecorded");

                // Adding to the UI
                btnLayout.Children.Add(addRobotBtn);
                btnLayout.Children.Add(removeRobotBtn);
                RobotCommentLayout.Children.Add(btnLayout);
            }                                   
        }
    }
}