using CoreLocation;
using Foundation;
using NotesManager.interfaces;
using NotesManager.iOS.services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GPSDependencyService))]
namespace NotesManager.iOS.services
{
    public class GPSDependencyService : IGPSDependencyService
    {
        public bool IsGpsEnable()
        {
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OpenSettings()
        {
            var WiFiURL = new NSUrl("prefs:root=WIFI");
            
            if (UIApplication.SharedApplication.CanOpenUrl(WiFiURL))
            {   //> Pre iOS 10
                UIApplication.SharedApplication.OpenUrl(WiFiURL);
            }
            else
            {   //> iOS 10
                UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
            }
        }
    }
}