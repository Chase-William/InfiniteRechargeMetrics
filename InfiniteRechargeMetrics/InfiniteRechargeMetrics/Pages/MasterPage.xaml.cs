using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniteRechargeMetrics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = App.LoginPageViewModel;
        }

        private void OnLoginBtnClicked(object sender, EventArgs e)
        {
            //App.Current.MainPage.Navigation.PushModalAsync(new LoginPopupPage());
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushModalAsync(new LoginPage());
        }
    }
}