using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Transactions;
using LI.CSharp.Lab.Services;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for TransactionsView.xaml
    /// </summary>
    public partial class TransactionsView : UserControl
    {
        public TransactionsView()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            ((TransactionsViewModel)DataContext).ShowTransactions();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((TransactionsViewModel)DataContext).CreateTransaction();
        }
    }
}