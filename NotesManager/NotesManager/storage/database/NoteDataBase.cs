using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesManager.custom;
using NotesManager.models;
using SQLite;

namespace NotesManager.storage.database
{
     public class NoteDataBase //: INote
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DataBaseConstants.DatabasePath, DataBaseConstants.Flags);
        });

        static SQLiteAsyncConnection DatabaseConn => lazyInitializer.Value;
        static bool initialized = false;

        public NoteDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (DatabaseConn.TableMappings.All(m => m.MappedType.Name != nameof(Note)))
                {
                    await DatabaseConn.CreateTablesAsync(CreateFlags.None, typeof(Note)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }
        
        public Task<List<Note>> GetItemsAsync()
        {
            return DatabaseConn.Table<Note>().ToListAsync();
        }

        public Task<List<Note>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return DatabaseConn.QueryAsync<Note>("SELECT * FROM [Note]");
        }

        public Task<List<Note>> getPastNotesAsync()
        {
            return DatabaseConn.Table<Note>().Where(x => x.Date < DateTime.Now).ToListAsync();
        }

        public Task<List<Note>> getFutureNotesAsync()
        {
            return DatabaseConn.Table<Note>().Where(x => x.Date >= DateTime.Now).ToListAsync();
        }

        public Task<Note> GetItemAsync(int id)
        {
            return DatabaseConn.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public int GetLastId()
        {
            int lastId = 0;
            Note lastNote = DatabaseConn.Table<Note>().OrderByDescending(n => n.ID).FirstOrDefaultAsync().Result;
            if (lastNote != null)
                lastId = lastNote.ID;
            

            return lastId;
        }
        
        public Task<int> SaveItemAsync(Note item)
        {
            return DatabaseConn.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(Note item)
        {
            return DatabaseConn.UpdateAsync(item);
        }
        
        public Task<int> DeleteItemAsync(Note item)
        {
            return DatabaseConn.DeleteAsync(item);
        }

        public void CloseDataBase()
        {
            DatabaseConn.CloseAsync();
        }
        
    }
}