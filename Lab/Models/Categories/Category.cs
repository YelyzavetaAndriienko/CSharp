using System;
using System.IO;
using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.Models.Categories
{
    // Class Category keeps the name, description, color and icon.
    public class Category : Entity
    {
        private User _owner;
        private string _name;
        private string _description;
        private Colors? _color;
        private FileInfo _icon;
        private Guid _id;

        public User Owner
        {
            get { return _owner; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    //if (Owner.Categories.Any(category => category.Name == value))
                    //{
                    //    throw new ArgumentException("Category with this name already exists!");
                    //}
                    _name = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Name!");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Colors? Color
        {
            get
            {
                if (_color == null)
                {
                    _color = Colors.Red;
                }
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public FileInfo Icon
        {
            get { return _icon; }
            set
            {
                if (value != null)
                {
                    _icon = value;
                }
                else
                {
                    Console.WriteLine("Invalid value of Icon!");
                }
            }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Category() { }

        public Category(User user)
        {
            _id = Guid.NewGuid();
            _owner = user;
        }

        public Category(User user, string name, Colors? color, FileInfo icon, Guid id)
        {
            _owner = user;
            _name = name;
            _color = color;
            _icon = icon;
            _id = id;
            if (!Validate())
            {
                throw new ArgumentException("Invalid argument in constructor of Category!");
            }
        }

        public override bool Validate()
        {
            var result = true;

            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (Icon == null)
                result = false;
            if (Owner == null)
                result = false;

            return result;
        }

        public override string ToString()
        {
            if (Description != null)
            {
                return $"{Name}, {Description}, {Color}, {Icon.Name}";
            }
            else
            {
                return $"{Name}, {Color}, {Icon.Name}";
            }
        }
    }
}