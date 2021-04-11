using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for WalletDetails.xaml
    /// </summary>
    public partial class WalletDetailsView : UserControl
    {
        public static string[] CURRENCIES =
        {
            Currencies.UAH.ToString(),
            Currencies.EUR.ToString(),
            Currencies.USD.ToString(),
            Currencies.GBP.ToString(),
            Currencies.PLN.ToString(),
            Currencies.RUB.ToString()
        };
        public WalletDetailsView()
        {
            InitializeComponent();
            ComboBox0.ItemsSource = CURRENCIES;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this wallet?", "Delete", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ((WalletDetailsViewModel)DataContext).DeleteWallet();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = (CheckBox)sender;
            ((WalletDetailsViewModel)DataContext).Wallet.ChangeAvailabilityOfCategory(
                chBox.Content.ToString(), true,
                ((WalletDetailsViewModel)DataContext).Wallet.Owner.Id);
            MessageBox.Show("Category " + chBox.Content.ToString() + " is now available in this wallet");
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chBox = (CheckBox)sender;
            ((WalletDetailsViewModel)DataContext).Wallet.ChangeAvailabilityOfCategory(
                chBox.Content.ToString(), false,
                ((WalletDetailsViewModel)DataContext).Wallet.Owner.Id);
            /*MessageBox.Show("Are you sure you want to make category " 
                            + chBox.Content.ToString() + " unavailable in this wallet? " +
                            "All transactions of this category will change their category " +
                            "to default one");*/
            MessageBox.Show("Category " + chBox.Content.ToString() + " is now unavailable in this wallet");
        }

        private void Expander_OnExpanded(object sender, RoutedEventArgs e)
        {
            foreach (var category in ((WalletDetailsViewModel)DataContext).Wallet.Owner.Categories)
            {
                bool isChecked = ((WalletDetailsViewModel)DataContext).Wallet.IsAvailable(category);
                CheckBox checkBox = new CheckBox { Content = category.Name, MinHeight = 20, IsChecked = isChecked };
                checkBox.Checked += checkBox_Checked;
                checkBox.Unchecked += checkBox_Unchecked;
                stackPanel.Children.Add(checkBox);
            }
        }

        /*private void Transactions_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pressed");
            ((WalletDetailsViewModel)DataContext)._gotoTransactions.Invoke();
        }*/
    }
}