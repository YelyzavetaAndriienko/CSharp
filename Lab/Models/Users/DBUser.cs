using System;
using LI.CSharp.Lab.DataStorage;

namespace LI.CSharp.Lab.Models.Users
{
    public class DBUser : IStorable
    {
        public static string FOLDER = "AllUsers";
        public Guid Guid { get; }

        public string OwnerGuid { get; }
        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }
        public string Login { get; }
        public string Password { get; }

        public DBUser(string firstName, string lastName, string email, string login, string password, Guid guid = default)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
            if (guid == default)
                Guid = Guid.NewGuid();
            else Guid = guid;
            OwnerGuid = FOLDER;
        }

    }
}