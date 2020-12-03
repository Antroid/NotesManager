using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesManager.interfaces;
using NotesManager.viewmodels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesManager.views.schedule.tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureNotesPage : ContentPage
    {
        public FutureNotesPage()
        {
            InitializeComponent();
            Title = "Future";
        }


        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return; // has been set to null, do not 'process' tapped event
            DisplayAlert("Tapped", e.SelectedItem + " row was tapped", "OK");
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.BindingContext is FutureNotesPageViewModel scheduleVM)
            {
                scheduleVM.readFutureNotes();
            }

            double tabsHeight = DependencyService.Get<ITabBar>().GetHeaderHeight();
            Padding = new Thickness(0, tabsHeight, 0, 0);

        }

    }
}