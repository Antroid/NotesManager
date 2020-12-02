using System.Collections.Generic;
using System.Threading.Tasks;
using NotesManager.models;

namespace NotesManager.storage.database
{
    public interface INote
    {
        Task<List<Note>> GetNotesAsync();

        Task<Note> GetNoteAsync(int id);

        Task<int> SaveNoteAsync(Note note);

        Task<int> DeleteNoteAsync(Note note);
        
    }
}