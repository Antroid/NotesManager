using Android.Content;
using Android.Locations;
using NotesManager.droid.services;
using NotesManager.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(GPSDependencyService))]
namespace NotesManager.droid.services
{
    public class GPSDependencyService : IGPSDependencyService
    {
        public bool IsGpsEnable()
        {
            LocationManager locationManager =
                (LocationManager) Android.App.Application.Context.GetSystemService(Context.LocationService);
            return locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }

        public void OpenSettings()
        {
            Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            try
            {
                Android.App.Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException activityNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine(activityNotFoundException.Message);
                Android.Widget.Toast.MakeText(Android.App.Application.Context, "Error: Gps Activity",
                        Android.Widget.ToastLength.Short)
                    ?.Show();
            }
        }
    }
}