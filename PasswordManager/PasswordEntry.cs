using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class PasswordEntry
    {
        public string Website { get; set; }
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string Category { get; set; }
    }
}
