namespace NotesManager.storage.prefs
{
    public interface IPrefs
    {
        void setEmail(string email);
        string getEmail();

        void setPassword(string password);
        string getPassword();
    }
}