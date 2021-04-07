using System.Windows;
using System.Windows.Controls;
using LI.CSharp.Lab.Models.Categories;
using LI.CSharp.Lab.Services;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((CategoriesViewModel)DataContext).CreateCategory();
        }
    }
}