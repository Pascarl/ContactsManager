using ContactsManager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Data
{
    public class ContactsDbService
    {
        public const string DB_NAME = "ContactsManager.db3";
        public readonly SQLiteAsyncConnection _connection;

        public ContactsDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<ContactsModel>();
        }

        public async Task<List<ContactsModel>> GetAll()
        {
            return await _connection.Table<ContactsModel>().ToListAsync();
        }

        public async Task<List<ContactsModel>> GetById(string name)
        {
            return await _connection.Table<ContactsModel>().Where(X => X.Name == name).ToListAsync();
        }

        public async Task create(ContactsModel model)
        {
            await _connection.InsertAsync(model);
        }

        public async Task update(ContactsModel model)
        {
            await _connection.UpdateAsync(model);
        }

        public async Task delete(ContactsModel model)
        {
            await _connection.DeleteAsync(model);
        }
    }

}
