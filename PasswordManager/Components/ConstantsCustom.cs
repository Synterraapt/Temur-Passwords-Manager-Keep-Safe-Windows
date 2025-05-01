using System;
using System.IO;

namespace PasswordManager.Components
{
    public static class ConstantsCustom
    {
        public static readonly string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PMiz");
        public static readonly string PasswordFilePath = Path.Combine(FolderPath, "passwords.txt");
        public static readonly string MasterFilePath = Path.Combine(FolderPath, "master.txt");
    }
}
