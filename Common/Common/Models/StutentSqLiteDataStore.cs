using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Models
{
    public class StudentSqLiteDataStore : SqLiteBase, IDataStore<Student>
    {
        public StudentSqLiteDataStore(string dbPath = null)
            : base(dbPath)
        {
            // create table if not exist
            db.CreateTableAsync<Student>();
        }

        public async Task AddItemAsync(Student item)
        {
            await db.InsertAsync(item).ConfigureAwait(false);
        }

        public async Task UpdateItemAsync(Student item)
        {
            await db.UpdateAsync(item).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(Student item)
        {
            await db.DeleteAsync(item).ConfigureAwait(false);
        }

        public async Task<Student> GetItemAsync(long serialNumber)
        {
            return await db.Table<Student>().FirstOrDefaultAsync(x => x.SerialNumber == serialNumber).ConfigureAwait(false);
        }

        public async Task<List<Student>> GetItemsAsync()
        {
            return await db.Table<Student>().OrderByDescending(x => x.SerialNumber).ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteAllAsync()
        {
            await db.DeleteAllAsync<Student>().ConfigureAwait(false);
        }
    }
}
