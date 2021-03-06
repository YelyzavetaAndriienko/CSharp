using System;
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
            ComboBoxCurrency.ItemsSource = WalletDetailsView.CURRENCIES;
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

        private void ComboBoxCategory_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((TransactionDetailsViewModel) DataContext != null)
            {
                bool categoryIsDefault = ((TransactionDetailsViewModel) DataContext).Category.Equals("DEFAULT");
                ComboBoxCategory.ItemsSource =
                    ((TransactionDetailsViewModel) DataContext).Tvm.Wallet.GetAvailableCategories(categoryIsDefault);
            }
        }
    }
}