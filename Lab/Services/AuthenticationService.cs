using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LI.CSharp.Lab.DataStorage;
using LI.CSharp.Lab.Models.Users;
using System.Windows;

namespace LI.CSharp.Lab.Services
{
    public class AuthenticationService
    {
        private FileDataStorage<DBUser> _storage = new FileDataStorage<DBUser>();
        const int encryptingIndex = 1;
        const string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-<>?:[]{}`|/.,";

        public async Task<User> AuthenticateAsync(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            var users = await _storage.GetAllAsync(DBUser.FOLDER);
            var dbUser = users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == Encrypt(authUser.Password, encryptingIndex));
            if (dbUser == null)
                throw new Exception("Wrong Login or Password");
            if ((dbUser.Login.Length < 2))
                throw new ArgumentException("Login must consist at least of 2 symbols");
            if ((dbUser.Password.Length < 3))
                throw new ArgumentException("Password must consist at least of 3 symbols");

            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Login);
        }

        public async Task<bool> RegisterUserAsync(RegistrationUser regUser)
        {
            Thread.Sleep(1000);
            var users = await _storage.GetAllAsync(DBUser.FOLDER);
            var dbUser = users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Name) || String.IsNullOrWhiteSpace(regUser.LastName) || String.IsNullOrWhiteSpace(regUser.Email) ||
                String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password))
                throw new ArgumentException("At least one field is empty");
            if ((regUser.Name.Length < 2) ||
            (regUser.LastName.Length < 2) || (regUser.Login.Length < 2) || (regUser.Name.Length > 50) ||
            (regUser.LastName.Length > 50) || (regUser.Login.Length > 50))
                throw new ArgumentException("First name, last name and login must consist at least of 2 symbols. Max 50");
            if (!Regex.IsMatch(regUser.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                throw new ArgumentException("Incorrect email");
            if ((regUser.Password.Length < 3))
               throw new ArgumentException("Password must consist at least of 3 symbols");

            string encryptedPassword = Encrypt(regUser.Password, encryptingIndex);
            dbUser = new DBUser(regUser.Name, regUser.LastName, regUser.Login + "@gmail.com", regUser.Login, encryptedPassword);
            await _storage.AddOrUpdateAsync(dbUser);
            return true;
        }

        private string CodeEncode(string text, int k)
        {
            var fullAlfabet = alfabet + alfabet.ToLower();
            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }
            return retVal;
        }

        public string Encrypt(string plainMessage, int key)
            => CodeEncode(plainMessage, key);

        public string Decrypt(string encryptedMessage, int key)
            => CodeEncode(encryptedMessage, -key);
    }
}
