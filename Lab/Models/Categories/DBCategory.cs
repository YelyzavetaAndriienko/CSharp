using System;
using System.IO;
using LI.CSharp.Lab.DataStorage;

namespace LI.CSharp.Lab.Models.Categories
{
    public class DBCategory: IStorable
    {
        public Guid Guid { get; }
        public string OwnerGuid { get; }
        public string Name { get; }
        public string Description { get; }
        public Colors? Color { get; }
        public FileInfo Icon { get; }
        
        public DBCategory(string name, string ownerGuid, string description,
            Colors? color, FileInfo icon, Guid guid)
        {
            //Guid = Guid.NewGuid();
            Name = name;
            OwnerGuid = ownerGuid;
            Description = description;
            Color = color;
            Icon = icon;
            Guid = guid;
        }
    }
}