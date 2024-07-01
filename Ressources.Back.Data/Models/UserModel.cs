using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using BCrypt.Net;

namespace Ressources.Back.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = "";
        public string Mdp { get; set; } = "";

        public int Activate { get; set; }
        public string Age { get; set; }
        public string SituationFamiliale { get; set; } = "";

        public string CSP { get; set; } = "";
        public string Loisir { get; set; } = "";
        public string Autre { get; set; } = "";
        public int IdTypeUser { get; set; } = 1;
        public int IdStatus { get; set; }

        private static readonly string EncryptionKey = "your-encryption-key"; // 32 characters for AES-256

        public void EncryptData()
        {
            Login = Encrypt(Login);
            Mdp = HashPassword(Mdp); // Use bcrypt for passwords
            Age = Encrypt(Age);
            SituationFamiliale = Encrypt(SituationFamiliale);
            CSP = Encrypt(CSP);
            Loisir = Encrypt(Loisir);
            Autre = Encrypt(Autre);
        }

        public void DecryptData()
        {
            Login = Decrypt(Login);
            // Mdp remains hashed, no need to decrypt
            Age = Decrypt(Age);
            SituationFamiliale = Decrypt(SituationFamiliale);
            CSP = Decrypt(CSP);
            Loisir = Decrypt(Loisir);
            Autre = Decrypt(Autre);
        }

        private static string Encrypt(string clearText)
        {
            if (string.IsNullOrEmpty(clearText)) return clearText;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
                aes.IV = new byte[16]; // Initialize IV with zeros
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(clearText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        private static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
                aes.IV = new byte[16]; // Initialize IV with zeros
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
    }
}
