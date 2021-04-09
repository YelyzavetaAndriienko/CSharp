using System;
using System.Collections.Generic;
using System.Linq;
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
            //return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Login);
        }

        public async Task<bool> RegisterUserAsync(RegistrationUser regUser)
        {
            Thread.Sleep(2000);
            var users = await _storage.GetAllAsync(DBUser.FOLDER);
            var dbUser = users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password) || String.IsNullOrWhiteSpace(regUser.LastName))
                throw new ArgumentException("Login, Password or Last Name is Empty");
           
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
