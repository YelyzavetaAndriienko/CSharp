using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for WalletDetails.xaml
    /// </summary>
    public partial class WalletDetailsView : UserControl
    {
        public WalletDetailsView()
        {

            InitializeComponent();
            ComboBox0.ItemsSource = LoadComboBoxData();
            //string str = "ua";
            //ComboBox0.SelectedIndex = ComboBox0.FindString(str);
            // ComboBox0.SelectedItem = "liza";
            //ComboBox0.SelectedItem = ;


            //DataContext = new WalletDetailsViewModel();
        }

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

        //private void ComboBox_Selected(object sender, SelectionChangedEventArgs e)
        //{
        //    throw new System.NotImplementedException();
        //}

        /*private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            MessageBox.Show(selectedItem.Content.ToString());
        }*/
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