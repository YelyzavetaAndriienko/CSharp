using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.Services
{
    public class AllServices
    {
        public WalletService WalletService { get; }
        public CategoryService CategoryService { get; }
        
        public AllServices(User user)
        {
            WalletService = new WalletService(user);
            CategoryService = new CategoryService(user);
        }

        public void SaveChanges()
        {
            WalletService.SaveChanges();
            CategoryService.SaveChanges();
        }
    }
}