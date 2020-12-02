using System;
using NotesManager.custom;
using NotesManager.storage.prefs;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NotesManager.views.main
{
    public partial class MainPage : TabPage
    {
        private Prefs prefs;

        public MainPage()
        {
            InitializeComponent();

            prefs = new Prefs();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}