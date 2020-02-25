using Android.App;
using Android.Content.PM;
using Android.OS;
using System.IO;
using Plugin.GoogleClient;
using Android.Content;
using Sharpnado.Presentation.Forms.Droid;
using Plugin.CurrentActivity;
/// <summary>
/// 
///     IMPORTANT: For GoogleAuth to work you must have: 
///         - Xamarin.GooglePlayServices.Auth
///         - Xamarin.GooglePlayServices.Basement
/// 
/// </summary>
namespace InfiniteRechargeMetrics.Droid
{
    [Activity(Label = "IRM", Icon = "@drawable/logo_icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);

            // Initializing the GoogleClient plugin
            GoogleClientManager.Initialize(this, null, "205430491915-fotkdlnd7tn6e0m52as34782hb4cq9se.apps.googleusercontent.com");

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            SharpnadoInitializer.Initialize();

            // Getting the folder path that already exist on the device and will be used to map a location to our database.
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            // Combining the two paths to create a completed path
            string completedPath = Path.Combine(folderPath, Data.Config.DATABASE_NAME);           

            LoadApplication(new App(completedPath));
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}