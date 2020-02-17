using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InfiniteRechargeMetrics.Models;
using InfiniteRechargeMetrics.Pages;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Xamarin.Forms;
/// <summary>
/// 
///     This class is based off the samples here: https://github.com/CrossGeeks/GoogleClientPlugin
/// 
/// </summary>
namespace InfiniteRechargeMetrics.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        ///     The user profile which holds the data about a user we care about.
        /// </summary>
        public UserProfile User { get; set; } = new UserProfile();
        public string Name
        {
            get => User.Name;
            set {
                User.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get => User.Email;
            set {
                User.Email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        public Uri Avatar
        {
            get => User.Avatar;
            set
            {
                User.Avatar = value;
                NotifyPropertyChanged(nameof(Avatar));
            }
        }

        private bool isLoggedIn;
        /// <summary>
        ///     States whether we are logged into the app and also has notifyPropChanged for UI
        /// </summary>
        public bool IsLoggedIn { 
            get => isLoggedIn; 
            set
            {
                isLoggedIn = value;
                // Triggers the update for the MasterPage Header
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoggedIn)));
            }
        }
        public string Token { get; set; }

        /// <summary>
        ///     ICommands for handling login and logout button clicks.
        ///         Our UI elements are located in another class.
        /// </summary>
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        private readonly IGoogleClientManager googleClientManager;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        /// <summary>
        ///     Used for triggering UI updates.
        /// </summary>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoginPageViewModel()
        {
            // Using ICommand for attaching handlers because this isn't apart of the partial class.
            // Instead this class is treated as the bindingContext for it.
            LoginCommand = new Command(LoginAsync);
            LogoutCommand = new Command(Logout);
            
            googleClientManager = CrossGoogleClient.Current;
            // By default the user is not logged in
            IsLoggedIn = false;
        }

        /// <summary>
        ///     Provides the login functionality with expects for targeting specific problems.
        /// </summary>
        public async void LoginAsync()
        {
            googleClientManager.OnLogin += OnLoginCompleted;
            try
            {
                await googleClientManager.LoginAsync();               
            }
            catch (GoogleClientSignInNetworkErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInCanceledErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInternalErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientBaseException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }

        /// <summary>
        ///     Called when the login process has finished and is ready to have its data assigned into our program
        /// </summary>
        private void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;
                Name = googleUser.Name;
                Email = googleUser.Email;

                // The default size image the URI has is a 96x96.. which is too small
                // Here we modify the query URI to return a 512x512 which is better
                string absoluteURI = googleUser.Picture.AbsoluteUri;
                absoluteURI = absoluteURI.TrimEnd('9', '6', '-', 'c');               
                Avatar = new Uri(absoluteURI += "512-c");

                // Update the state variable for loggin status
                IsLoggedIn = true;

                var token = CrossGoogleClient.Current.ActiveToken;
                Token = token;

                // Popping off the login page once finished
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopToRootAsync();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }            

            googleClientManager.OnLogin -= OnLoginCompleted;
            UpdateUI();
        }

        /// <summary>
        ///     Called when The logout button is pressed
        /// </summary>
        public void Logout()
        {
            googleClientManager.OnLogout += OnLogoutCompleted;
            googleClientManager.Logout();
        }

        /// <summary>
        ///     Called when the logout process is complete.
        /// </summary>
        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            User.Email = "Offline";
            googleClientManager.OnLogout -= OnLogoutCompleted;
            UpdateUI();
        }

        /// <summary>
        ///     Updates the UI
        /// </summary>
        private void UpdateUI()
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
