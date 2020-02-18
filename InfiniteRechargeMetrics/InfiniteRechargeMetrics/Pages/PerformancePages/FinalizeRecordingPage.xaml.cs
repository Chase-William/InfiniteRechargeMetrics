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

        private Performance performance;

        public FinalizeRecordingPage(Performance _performance)
        {
            performance = _performance;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new FinalizeRecordingViewModel(RobotCommentLayout, performance);

            // if a add robot btn doesnt already exist inside the grid.. add one
            if (!(RobotCommentLayout.Children.Any(view => view.AutomationId == ADD_REMOVE_BTN_LAYOUT_ID)))
            {
                StackLayout btnLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    AutomationId = ADD_REMOVE_BTN_LAYOUT_ID
                };
                var addRobotBtn = new Button
                {
                    Text = "Add Robot"
                };
                var removeRobotBtn = new Button
                {
                    Text = "Remove Robot"
                };

                // Setting our binding for the CommandProperty
                addRobotBtn.SetBinding(Button.CommandProperty, "AddRobotCMD");
                removeRobotBtn.SetBinding(Button.CommandProperty, "RemoveRobotCMD");

                // Adding to the UI
                btnLayout.Children.Add(addRobotBtn);
                btnLayout.Children.Add(removeRobotBtn);
                RobotCommentLayout.Children.Add(btnLayout);
            }                                   
        }
    }
}