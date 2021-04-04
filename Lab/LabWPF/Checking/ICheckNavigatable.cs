namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    public enum CheckNavigatableTypes
    {
        ShowWallets,
        ShowCategories
    }


    public interface ICheckNavigatable
    {
        public CheckNavigatableTypes Type { get; }

        public void ClearSensitiveData();
    }
}
