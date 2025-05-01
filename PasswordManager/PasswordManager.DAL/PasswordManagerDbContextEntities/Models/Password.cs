using SQLite;

namespace PasswordManager.DAL.PasswordManagerDbContextEntities.Models
{
    [Table("Passwords")]
    public class Password
    {
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }

        public string Pass { get; set; }

        public string SiteName { get; set; }
    }
}
