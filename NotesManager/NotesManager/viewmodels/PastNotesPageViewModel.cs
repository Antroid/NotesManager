using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotesManager.custom;
using NotesManager.models;

namespace NotesManager.viewmodels
{
    public class PastNotesPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Note> _notes;
        
        public ObservableCollection<Note> Notes
        {
            get { return _notes;}
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        
        public async void readPastNotes()
        {
            Notes = new ObservableCollection<Note>(await App.Database.getPastNotesAsync());
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}