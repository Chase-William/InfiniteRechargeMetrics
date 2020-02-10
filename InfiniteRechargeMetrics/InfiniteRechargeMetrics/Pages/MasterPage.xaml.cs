using System;
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

        private void OnLoginBtn_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage.Navigation.PushModalAsync(new LoginPopupPage());
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }

        private void OnChangeLogin_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }

        /// <summary>
        ///     Needed because when the view is appearing, the masterpage header needs to update.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}