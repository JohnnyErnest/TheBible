using Microsoft.Data.Sqlite;
using Windows.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TheBible.Model.Model.Views;
using Windows.UI.Popups;
using System.IO;

namespace TheBible.Model.DataLoader
{
    /// <summary>
    /// SQLiteData holds all of the actual Bible data, using the SQLite System.Data provider
    /// rather than Entity Framework Core for performance in reading.
    /// </summary>
    public class SQLiteData : IDisposable
    {
        SqliteConnection connection;

        public IQueryable<AllTranslations> GetAllTranslations()
        {
            string exceptionLine = @"Error on retrieving Transactions";
            List<AllTranslations> translations = new List<AllTranslations>();
            try
            {
                SqliteCommand cmd = connection.CreateCommand();
                string sql = String.Format("select {0} from Translation order by TranslationIndex",
                    "ID,TranslationShortName,TranslationLongName,TranslationIndex");
                cmd.CommandText = sql;
                SqliteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AllTranslations translation = new AllTranslations()
                    {
                        TranslationID = reader.GetInt64(0),
                        TranslationShortName = reader.GetString(1),
                        TranslationLongName = reader.GetString(2),
                        TranslationIndex = reader.GetInt32(3),
                    };
                    translations.Add(translation);
                }
            }
            catch(Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.ToString(), exceptionLine);
                dialog.ShowAsync();
            }
            return translations.AsQueryable();
        }

        public IQueryable<AllBooks> GetAllBooks(string translationShortName)
        {
            SqliteCommand cmd = connection.CreateCommand();
            string sql = String.Format("select {0} from VIEW_ALLBOOKS where TranslationShortName=@transShort order by TranslationIndex, BookIndex",
                "TranslationID,TranslationShortName,TranslationLongName,TranslationIndex,BookID,BookIndex,BookName,BookShortName,Testament,TotalChapters");
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@transShort", translationShortName);
            SqliteDataReader reader = cmd.ExecuteReader();
            List<AllBooks> books = new List<AllBooks>();
            while (reader.Read())
            {
                AllBooks book = new AllBooks()
                {
                    TranslationID = reader.GetInt64(0),
                    TranslationShortName = reader.GetString(1),
                    TranslationLongName = reader.GetString(2),
                    TranslationIndex = reader.GetInt32(3),
                    BookID = reader.GetInt64(4),
                    BookIndex = reader.GetInt32(5),
                    BookName = reader.GetString(6),
                    BookShortName = reader.GetString(7),
                    Testament = reader.GetString(8),
                    TotalChapters = reader.GetInt32(9)
                };
                books.Add(book);
            }
            return books.AsQueryable();
        }

        public List<int> GetChapterNumbers(long bookID)
        {
            SqliteCommand cmd = connection.CreateCommand();
            string sql = String.Format("select TotalChapters from VIEW_ALLBOOKS where BookID=@bookID");
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            SqliteDataReader reader = cmd.ExecuteReader();
            List<int> chapters = new List<int>();
            int totalChapters = 0;
            while (reader.Read())
            {
                totalChapters = reader.GetInt32(0);
            }
            for (int z = 1; z <= totalChapters; z++)
                chapters.Add(z);
            return chapters;
        }

        public string GetChapter(long bookID, int chapterIndex, bool separateLines)
        {
            StringBuilder sb = new StringBuilder();
            SqliteCommand cmd = connection.CreateCommand();
            string sql = String.Format("select VerseIndex, VerseText from VIEW_ALLVERSES where BookID=@bookID and ChapterIndex=@chapterIndex order by VerseIndex");
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@chapterIndex", chapterIndex);
            SqliteDataReader reader = cmd.ExecuteReader();
            List<AllVerses> books = new List<AllVerses>();
            while (reader.Read())
            {
                int vnum = reader.GetInt32(0);
                string vtxt = reader.GetString(1);
                if (separateLines)
                    sb.AppendLine(String.Format("{0}. {1}", vnum, vtxt));
                else
                    sb.Append(String.Format("{0}. {1} ", vnum, vtxt));
            }
            return sb.ToString();
        }

        async Task<bool> FileExistsAndDelete(StorageFolder folder, string file)
        {
            try
            {
                StorageFile storageFile = await folder.GetFileAsync(file);
                await storageFile.DeleteAsync();
                return true;
            }
            catch(FileNotFoundException)
            {
                return false;
            }
        }

        public async Task SetupConnection()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/biblesql.db"));
            string filePath = file.Path;

            StorageFolder newPath = Windows.Storage.ApplicationData.Current.LocalFolder;

            await FileExistsAndDelete(newPath, "biblesql.db");
            await file.CopyAsync(newPath);

            string sqlFile = Path.Combine(newPath.Path, "biblesql.db");

            //string filePath = "biblesql.db";
            string dsn = String.Format(@"Data Source={0}", sqlFile);
            connection = new SqliteConnection(dsn);
            connection.Open();
        }

        public SQLiteData()
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    connection.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SQLiteData() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
