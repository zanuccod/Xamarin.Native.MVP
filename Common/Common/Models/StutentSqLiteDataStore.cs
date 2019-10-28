using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Models
{
    public class StudentSqLiteDataStore : IDataStore<Student>
    {
		private readonly string dbPath;

        public StudentSqLiteDataStore()
        {
            using (var conn = new SqLiteBase())
            {
                // create table if not exist
                conn.db.CreateTableAsync<Student>();
            }
        }

        public StudentSqLiteDataStore(string dbPath)
		{
			this.dbPath = dbPath;

			using (var conn = new SqLiteBase(dbPath))
			{
				// create table if not exist
				conn.db.CreateTableAsync<Student>();
			}
		}

        public async Task AddItemAsync(Student item)
        {
			using (var conn = new SqLiteBase(dbPath))
			{
				await conn.db.InsertAsync(item).ConfigureAwait(false);
			}
        }

        public async Task UpdateItemAsync(Student item)
		{
			using (var conn = new SqLiteBase(dbPath))
			{
				await conn.db.UpdateAsync(item).ConfigureAwait(false);
			}
		}

        public async Task DeleteItemAsync(Student item)
		{
			using (var conn = new SqLiteBase(dbPath))
			{
				await conn.db.DeleteAsync(item).ConfigureAwait(false);
			}
		}

        public async Task<Student> GetItemAsync(long serialNumber)
        {
			using (var conn = new SqLiteBase(dbPath))
			{
				return await conn.db.Table<Student>().FirstOrDefaultAsync(x => x.SerialNumber == serialNumber).ConfigureAwait(false);
			}
		}

        public async Task<List<Student>> GetItemsAsync()
        {
			using (var conn = new SqLiteBase(dbPath))
			{
				return await conn.db.Table<Student>().OrderByDescending(x => x.SerialNumber).ToListAsync().ConfigureAwait(false);
			}
		}

        public async Task DeleteAllAsync()
		{
			using (var conn = new SqLiteBase(dbPath))
			{
				await conn.db.DeleteAllAsync<Student>().ConfigureAwait(false);
			}
		}
    }
}
