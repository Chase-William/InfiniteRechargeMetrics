using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using Plugin.GoogleClient;
using Sharpnado.Presentation.Forms.iOS;
using UIKit;

namespace InfiniteRechargeMetrics.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Initializing the GoogleClient plugin
            GoogleClientManager.Initialize(null, "205430491915-fotkdlnd7tn6e0m52as34782hb4cq9se.apps.googleusercontent.com");

            SharpnadoInitializer.Initialize();

            // Getting the folder path that already exist on the device and will be used to map a location to our database.
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            // Combining the two paths to create a completed path
            string completedPath = Path.Combine(folderPath, Data.Config.DATABASE_NAME);

            LoadApplication(new App(completedPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
