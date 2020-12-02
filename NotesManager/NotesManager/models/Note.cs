using System;
using SQLite;

namespace NotesManager.models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Note(int id, string text, DateTime date)
        {
            ID = id;
            Text = text;
            Date = date;
        }

        public Note()
        {
            
        }
    }
}