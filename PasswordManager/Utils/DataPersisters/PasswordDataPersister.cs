using PasswordManager.API.Models;
using PasswordManager.DAL.PasswordManagerDbContextEntities.DbContextEntities;
using PasswordManager.DAL.PasswordManagerDbContextEntities.Models;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.API.Utils.DataPersisters
{
    public class PasswordDataPersister
    {
        
        public static void AddPassword(PasswordModel passwordModel)
        {
            PasswordManagerDBEntities entities = new();

            Password newPassword = new() 
            { 
                Pass = passwordModel.Pass, 
                SiteName = passwordModel.SiteName
            };

            entities.Connection.Insert(newPassword);
        }

        public static Task<IEnumerable<PasswordModel>> GetPasswords()
        {
            PasswordManagerDBEntities entities = new();

            
                    IEnumerable<PasswordModel> siteModels = from t in entities.Connection.Table<Password>()
                                                             select new PasswordModel()
                                                             {
                                                                 Pass = t.Pass,
                                                                 SiteName = t.SiteName
                                                             };
                    return Task.FromResult(siteModels);
               
        }

        public static Task<IEnumerable<PasswordModel>> SearchPassword(string siteName)
        {
            PasswordManagerDBEntities entities = new();


            IEnumerable<PasswordModel> siteModels = from t in entities.Connection.Table<Password>()
                                                     where t.SiteName.Equals(siteName)
                                                     select new PasswordModel()
                                                     {
                                                         Pass = t.Pass,
                                                         SiteName = t.SiteName
                                                     };
            if(siteModels.Count().Equals(0))
            {
                Shell.Current.DisplayAlert("Nope", $"There is no such site '{siteName}'", "OK");
            }
            
            return Task.FromResult(siteModels);
        }

        public static async Task<string> GetRandomGeneratedPassword()
        {
            int length = new Random().Next(1, 100);
            const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>.?/|";
            return await GetRandomPasswordAsync(length, alphanumericCharacters);
        }

        private static async Task<string> GetRandomPasswordAsync(int length, IEnumerable<char> characterSet)
        {
            RandomNumberGenerator cryptoServiceProvider =RandomNumberGenerator.Create();
            if (length < 0)
            {
                throw new ArgumentException("length must not be negative", "length");
            }
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
            {
                throw new ArgumentException("length is too big", "length");
            }
            if (characterSet == null)
            {
                throw new ArgumentNullException("characterSet");
            }

            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
            {
                throw new ArgumentException("characterSet must not be empty", "characterSet");
            }

            var bytes = new byte[length * 8];
            cryptoServiceProvider.GetBytes(bytes);
            StringBuilder result = await GetResultPassword(length, characterArray, bytes);
            return result.ToString();
        }

        private static Task<StringBuilder> GetResultPassword(int length, char[] characterArray, byte[] bytes)
        {
            StringBuilder result = new();

            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result.Append(characterArray[value % (uint)characterArray.Length]);
            }
            return Task.FromResult(result);
        }
    }
}
