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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = App.LoginPageViewModel;

            this.Title = "Home";
        }

        /// <summary>
        ///     Handler for when the user pressed the cancel button.
        ///         Should remove the login page from view.
        /// </summary>
        private void OnCancelBtn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}