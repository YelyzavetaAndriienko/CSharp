using System;

namespace LI.CSharp.Lab.GUI.WPF.Navigation
{
    public interface INavigatable<TObject> where TObject: Enum
    {
        public TObject Type { get; }

        public void ClearSensitiveData();
    }
}