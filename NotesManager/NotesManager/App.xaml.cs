using NotesManager.storage.database;
using NotesManager.views;
using NotesManager.views.main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace NotesManager
{
    public partial class App : Application
    {
        
        static NoteDataBase database;
        public static NoteDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDataBase();
                }
                return database;
            }
        }
        
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Markup_Experimental" });
            
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
        public static object GetResourceValue(string keyName)
        {
            // Search all dictionaries
            if (App.Current.Resources.TryGetValue(keyName, out var retVal)) {}
            return retVal;
        }
        
    }
}