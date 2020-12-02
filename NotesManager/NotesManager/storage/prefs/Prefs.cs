using Xamarin.Essentials;

namespace NotesManager.storage.prefs
{
    public class Prefs : IPrefs
    {
        public const string USER_EMAIL_KEY = "USER_EMAIL_KEY";
        public const string USER_PASSWORD_KEY = "USER_PASSWORD_KEY";
        
        public void setEmail(string email)
        {
            Preferences.Set(USER_EMAIL_KEY,email);
        }

        public string getEmail()
        {
            return Preferences.Get(USER_EMAIL_KEY, null);
        }

        public void setPassword(string password)
        {
            Preferences.Set(USER_PASSWORD_KEY,password);
        }

        public string getPassword()
        {
            return Preferences.Get(USER_PASSWORD_KEY, null);
        }
    }
}