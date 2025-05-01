using PasswordManager.DAL.PasswordManagerDbContextEntities.Models;
using SQLite;

namespace PasswordManager.DAL.PasswordManagerDbContextEntities.DbContextEntities
{
    public class PasswordManagerDBEntities
    {
        private const string dbPath = @"PasswordGenerator_Test.db";
        public SQLiteConnection Connection;

        public PasswordManagerDBEntities()
        {
            Connection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, dbPath), true);
            Connection.CreateTable<Password>();
        }
    }
}
