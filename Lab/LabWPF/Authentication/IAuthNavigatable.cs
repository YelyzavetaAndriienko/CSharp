namespace LI.CSharp.Lab.GUI.WPF.Authentication
{
    public enum AuthNavigatableTypes
    {
        SignIn,
        SignUp
    }


    public interface IAuthNavigatable
    {
        public AuthNavigatableTypes Type { get; }

        public void ClearSensitiveData();
    }
}
