using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.ViewModels;
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
    public partial class EditRobotPage : ContentPage
    {
        public EditRobotPage(Robot _robot)
        {
            InitializeComponent();
            BindingContext = new EditRobotViewModel(_robot);
        }
      
    }
}