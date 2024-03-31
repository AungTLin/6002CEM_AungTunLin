using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fungry.Models;
using Fungry.SQL;
using SQLite;

namespace Fungry.Services
{
    public class DatabaseService : IAsyncDisposable
    {
        private const string DatabaseName = "Food.db3";
        private static readonly string _databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);

        private SQLiteAsyncConnection? _connection;
       

        private SQLiteAsyncConnection Database =>
            _connection ??= new SQLiteAsyncConnection(_databasePath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

        private async Task<TResult>ExecuteAsync<TResult>(Func<Task<TResult>> actionOnDb)
        {
            await Database.CreateTableAsync<CartItemEntity>();
            return await actionOnDb.Invoke();
        }

        public async Task<int> AddStorecart(CartItemEntity entity) =>
            await ExecuteAsync(async () => await Database.InsertAsync(entity));
        
        public async Task UpdateStorecart(CartItemEntity entity) =>
            await ExecuteAsync(async () => await Database.UpdateAsync(entity));

        public async Task DeleteStorecart(CartItemEntity entity) =>
            await ExecuteAsync(async () => await Database.DeleteAsync(entity));


        public async Task<CartItemEntity>GetStorecartAsync(int id) =>
            await ExecuteAsync(async () => await Database.GetAsync<CartItemEntity>(id));

        public async Task<List<CartItemEntity>> GetAllStorecartsAsync() =>
             await ExecuteAsync(async () => await Database.Table<CartItemEntity>().ToListAsync());

        public async Task<int> ClearCartAsync() =>
            await ExecuteAsync(async () => await Database.DeleteAllAsync<CartItemEntity>());


        public async ValueTask DisposeAsync() =>
           await _connection?.CloseAsync();

    }
}
