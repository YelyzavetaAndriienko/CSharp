using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for CategoryDetails.xaml
    /// </summary>
    public partial class TransactionDetailsView : UserControl
    {
        public TransactionDetailsView()
        {

            InitializeComponent();
            ComboBox0.ItemsSource = LoadComboBoxData();
        }
        public static string[] CURRENCIES =
        {
            Currencies.UAH.ToString(),
            Currencies.EUR.ToString(),
            Currencies.USD.ToString(),
            Currencies.GBP.ToString(),
            Currencies.PLN.ToString(),
            Currencies.RUB.ToString()
        };
        private string[] LoadComboBoxData()
        {
            string[] strArray =
            {
            Currencies.UAH.ToString(),
            Currencies.EUR.ToString(),
            Currencies.USD.ToString(),
            Currencies.GBP.ToString(),
            Currencies.PLN.ToString(),
            Currencies.RUB.ToString()
            };
            return strArray;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this transaction?", "Delete", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ((TransactionDetailsViewModel)DataContext).DeleteTransaction();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

    }
}
