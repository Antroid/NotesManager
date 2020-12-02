using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NotesManager.models;
using Xamarin.Forms;

namespace NotesManager.viewmodels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command CreateNewNote { get; set; }
        public Command ReadNotes { get; set; }

        public Command UpdateNote { get; set; }

        public Command DeleteNote { get; set; }

        public string status { get; set; }

        public int selectedNoteId { get; set; }


        public SettingsViewModel()
        {
            CreateNewNote = new Command(async () => await ExecuteSaveNewNote());
            ReadNotes = new Command(async () => await ExecuteReadNotes());
            UpdateNote = new Command(async () => await ExecuteUpdateNote());
            DeleteNote = new Command(async () => await ExecuteDeleteNote());
        }


        private async Task ExecuteUpdateNote()
        {
            Note selectedNote = await App.Database.GetItemAsync(selectedNoteId);
            if (selectedNote != null)
            {
                int res = await App.Database.UpdateItemAsync(selectedNote);
                string updateStatus = (res == 1) ? "was" : "was not";
                status = String.Format("Note #{0} {1} updated", selectedNoteId,updateStatus);
            }
            else
            {
                status = String.Format("No #{0} was detected", selectedNoteId);
            }
            OnPropertyChanged("status");
        }

        private async Task ExecuteDeleteNote()
        {
            var selectedNote = await App.Database.GetItemAsync(selectedNoteId);
            if (selectedNote != null)
            {
                int res = await App.Database.DeleteItemAsync(selectedNote);
                string deleteStatus = (res == 1) ? "was" : "was not";
                status = String.Format("Note #{0} {1} deleted", selectedNoteId, deleteStatus);
            }
            else
            {
                status = String.Format("No #{0} was detected", selectedNoteId);
            }
            OnPropertyChanged("status");
        }

        private async Task ExecuteSaveNewNote()
        {
            var note = new Note();
            note.Text = "Hello world";
            note.ID = App.Database.GetLastId() + 1;
            note.Date = DateTime.Now;
            status = "New Record Id=" + note.ID;
            await App.Database.SaveItemAsync(note);
            OnPropertyChanged("status");
        }

        private async Task ExecuteReadNotes()
        {
            var allNotes = await App.Database.GetItemsAsync();
            status = "";
            if (allNotes == null)
            {
                status = "No Notes found";
            }
            else
            {
                foreach (var note in allNotes)
                {
                    string formattedDate = "Note date & time not set";
                    formattedDate = note.Date.ToString("MMMM dd, yyyy h:mm tt");
                    string formattedStr = $"#{note.ID}, {note.Text} {formattedDate}\n";
                    status += formattedStr;
                }
            }

            OnPropertyChanged("status");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}