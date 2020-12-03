using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesManager.viewmodels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesManager.views.new_notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewNotePage : ContentPage
    {
        private NewNoteViewModel NewNoteVM;

        public NewNotePage()
        {
            InitializeComponent();
            Title = "New";

            NewNoteVM = BindingContext as NewNoteViewModel;
        }

        void DPicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            NewNoteVM.setDate(DPicker.Date);
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                NewNoteVM?.setTime(TPicker.Time);
            }
        }
    }
}