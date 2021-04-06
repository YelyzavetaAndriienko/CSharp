using System;

namespace LI.CSharp.Lab.DataStorage
{
    public interface IStorable
    {
        public Guid Guid { get; }
        public string OwnerGuid { get; }
    }
}