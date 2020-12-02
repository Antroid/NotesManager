using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotesManager.custom;

namespace NotesManager.viewmodels
{
    public class DateTimeDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime StartedDate { set; get; }
        public DateTime EndDate { set; get; }

        public DateTimeDialogViewModel()
        {
            StartedDate = DateTime.Now;
            EndDate = DateTime.Now;
            EndDate = EndDate.Date.AddYears(1);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}