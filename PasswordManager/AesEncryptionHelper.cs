using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordManager.Helper
{
    class AesEncryptionHelper
    {
        public static byte[] DeriveKeyFromPassword(string masterPassword)
        {
            byte[] salt = Encoding.UTF8.GetBytes("AES_FIXED_SALT");
            byte[] key = KeyDerivation.Pbkdf2(
                password: masterPassword!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32
                );

            return key;
        }

        public static string EncryptPassword(string plainText, byte[] aesKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;
                aesAlg.GenerateIV();
                byte[] iv = aesAlg.IV;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptPassword(string encryptedText, byte[] aesKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesKey;

                byte[] fullCipher = Convert.FromBase64String(encryptedText);

                if (fullCipher.Length < aesAlg.BlockSize / 8)
                {
                    throw new ArgumentException("Ciphertext is too short or invalid.");
                }

                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

                aesAlg.IV = iv;

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
