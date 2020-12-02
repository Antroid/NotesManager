using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using NotesManager.interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NotesManager.droid
{
    [Activity(Label = "NotesManager", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity, PlatformTimeFormat
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            
            CachedImageRenderer.Init(true);
            var ignore = typeof(SvgCachedImage);
           
           
            
            
            LoadApplication(new App());


            Color statusColor = (Color) App.GetResourceValue("StatusBarColor");
            Window.SetStatusBarColor(statusColor.ToAndroid());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        public bool is24HoursFormat()
        {
            return Android.Text.Format.DateFormat.Is24HourFormat(this);
        }
    }
}