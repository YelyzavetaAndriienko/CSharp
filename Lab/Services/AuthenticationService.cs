using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.Services
{
    public class AuthenticationService
    {
        private static List<DBUser> Users = new List<DBUser>() { new DBUser("1", "1", "1", "1", "1") };

        public User Authenticate(AuthenticationUser authUser)
        {
            Thread.Sleep(2000);
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            var dbUser = Users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);
            if (dbUser == null)
                throw new Exception("Wrong Login or Password");
            //return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Login);
        }

        public bool RegisterUser(RegistrationUser regUser)
        {
            Thread.Sleep(2000);
            var dbUser = Users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password) || String.IsNullOrWhiteSpace(regUser.LastName))
                throw new ArgumentException("Login, Password or Last Name is Empty");
            dbUser = new DBUser(regUser.LastName + "First", regUser.LastName, regUser.Login + "@gmail.com",
                regUser.Login, regUser.Password);
            Users.Add(dbUser);
            return true;
        }
    }
}
