using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Wallets;
using LI.CSharp.Lab.Services;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for WalletsView.xaml
    /// </summary>
    public partial class WalletsView : UserControl
    {
        public WalletsView()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((WalletsViewModel) DataContext).CreateWallet();
        }
        
    }
}