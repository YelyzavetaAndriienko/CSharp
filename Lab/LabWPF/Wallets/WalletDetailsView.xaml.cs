using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Wallets
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
    }
}