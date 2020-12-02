using NotesManager.interfaces;

namespace NotesManager.helpers
{
    public class GPSHelper
    {
        public static bool IsGPSEnabled()
        {
            return Xamarin.Forms.DependencyService.Get<IGPSDependencyService>().IsGpsEnable();
        }

        public static void OpenGPSSettings()
        {
            Xamarin.Forms.DependencyService.Get<IGPSDependencyService>().OpenSettings();
        }
    }
}