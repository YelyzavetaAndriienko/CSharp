using LI.CSharp.Lab.Models.Users;

namespace LI.CSharp.Lab.Services
{
    public class AllServices
    {
        public WalletService WalletService { get; }
        public CategoryService CategoryService { get; }
        public TransactionService TransactionService { get; }

        public AllServices(User user)
        {
            WalletService = new WalletService(user, this);
            CategoryService = new CategoryService(user);
            //TransactionService = new TransactionService(CurrentWallet);
            TransactionService = new TransactionService();
        }

        public void SaveChanges()
        {
            WalletService.SaveChanges();
            CategoryService.SaveChanges();
            TransactionService.SaveChanges();
        }
    }
}