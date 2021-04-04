namespace LI.CSharp.Lab.GUI.WPF
{
    public enum MainNavigatableTypes
    {
        Auth,
        Check
    }

    public interface IMainNavigatable
    {
        public MainNavigatableTypes Type { get; }

        public void ClearSensitiveData();
    }
}