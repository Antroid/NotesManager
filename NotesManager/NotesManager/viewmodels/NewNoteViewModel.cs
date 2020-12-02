using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesManager.models;
using Xamarin.Forms;

namespace NotesManager.viewmodels
{
    public class NewNoteViewModel : INotifyPropertyChanged
    {
        private ICommand createNewNote { get; set; }
        private ICommand openMapView { get; set; }

        public string MapIconArrowResource { get; set; }

        private string _note;
        
        public string StartedDate { set; get; }

        public TimeSpan StartedTime { set; get; }

        public string EndedDate { set; get; }

        public string status { set; get; }

        private string format;
        
        public event PropertyChangedEventHandler PropertyChanged;

        private Note userNote; 
        
        public NewNoteViewModel()
        {
            CreateNewNote = new Command(async () => await ExecuteSaveNewNote());
            OpenMapView = new Command(async () => await ExecuteOpenMapsView());
            userNote = new Note();
            userNote.Text = "";
            userNote.ID = App.Database.GetLastId() + 1;
            userNote.Date = DateTime.Now;
            StartedDate = userNote.Date.ToString("MM/dd/yyyy");
            
            userNote.Date = new DateTime(userNote.Date.Year,userNote.Date.Month,userNote.Date.Day,userNote.Date.Hour,userNote.Date.Minute,0);
            
            
            StartedTime = TimeSpan.Parse(userNote.Date.ToString("HH:mm"));
            
            EndedDate = userNote.Date.AddYears(1).ToString("MM/dd/yyyy");
            
        }
        
        public ICommand CreateNewNote
        {
            get => createNewNote;
            set
            {
                createNewNote = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenMapView
        {
            get => openMapView;
            set
            {
                openMapView = value;
                OnPropertyChanged();
            }
        }
        
        public string Note {
            set
            {
                _note = value;
                userNote.Text = _note;
            }
            get => _note;
        }

        private async Task ExecuteOpenMapsView()
        {
            if (MapIconArrowResource == "resource://NotesManager.resources.keyboard_arrow_down-24px.svg")
            {
                MapIconArrowResource = "resource://NotesManager.resources.keyboard_arrow_up-24px.svg";
            }
            else
            {
                MapIconArrowResource = "resource://NotesManager.resources.keyboard_arrow_down-24px.svg";
            }
            OnPropertyChanged();
        }
        
        private async Task ExecuteSaveNewNote()
        {
            string timestamp = StartedDate + StartedTime;
            DateTimeOffset dtOffset;

            if(DateTimeOffset.TryParse(timestamp,null,DateTimeStyles.None,out dtOffset))
            {
                userNote.Date = dtOffset.DateTime;
            }
             // status = userNote.Date.ToString("MM/dd/yyyy hh:mm tt");
            await App.Database.SaveItemAsync(userNote);
            status = "The record was successfully saved";
            OnPropertyChanged("status");
            
            
            
        }

        public void setDate(DateTime updatedDate)
        {
            userNote.Date = updatedDate;
        }

        public void setTime(TimeSpan time)
        {
            userNote.Date = new DateTime(userNote.Date.Year, userNote.Date.Month, userNote.Date.Day, time.Hours,time.Minutes, time.Seconds);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}