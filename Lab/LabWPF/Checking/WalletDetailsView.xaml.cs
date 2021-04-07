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
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this wallet?",  "Delete", MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    ((WalletDetailsViewModel) DataContext).DeleteWallet();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}