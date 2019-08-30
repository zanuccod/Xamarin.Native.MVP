using System;
using System.IO;
using SQLite;

namespace Common.Models
{
    public abstract class SqLiteBase : IDisposable
    {
        private const string databaseName = "dbSqLiteNetPcl.db";

        protected SQLiteAsyncConnection db;

        protected SqLiteBase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath ?? GetDatabasePath());
        }

        public void Dispose()
        {
            db.CloseAsync();
            db = null;

            // Must be called as the disposal of the connection is not released until the GC runs.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private string GetDatabasePath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            return Path.Combine(documentsPath, databaseName);
        }
    }
}
