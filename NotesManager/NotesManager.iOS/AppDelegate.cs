using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Foundation;
using NotesManager.interfaces;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace NotesManager.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, PlatformTimeFormat
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
            Xamarin.FormsMaps.Init();
            
            //for svg init
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);

            UITabBar.Appearance.BackgroundImage = new UIImage();
            UITabBar.Appearance.ShadowImage = new UIImage();


            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            UIColor StatusBarColor = ((Color) App.GetResourceValue("StatusBarColor")).ToUIColor();
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If VS has updated to the latest version , you can use StatusBarManager , else use the first line code
                // UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                if (UIApplication.SharedApplication.KeyWindow.WindowScene != null && UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager != null)
                {
                    UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager
                        .StatusBarFrame) {BackgroundColor = StatusBarColor};
                    UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                }
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar!=null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = StatusBarColor;
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
                }
            }
            base.OnActivated(uiApplication);
        }

        public bool is24HoursFormat()
        {
            var dateFormatter = new NSDateFormatter();
            dateFormatter.DateStyle = NSDateFormatterStyle.None;
            dateFormatter.TimeStyle = NSDateFormatterStyle.Short;

            var dateString = dateFormatter.ToString(NSDate.Now);
            var isTwelveHourFormat = 
                dateString.Contains(dateFormatter.AMSymbol) || 
                dateString.Contains(dateFormatter.PMSymbol);
            return !isTwelveHourFormat;
        }
    }
}