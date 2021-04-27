using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for CategoryDetails.xaml
    /// </summary>
    public partial class CategoryDetailsView : UserControl
    {
        public static string[] COLORS =
        {
            Colors.Blue.ToString(),
            Colors.Green.ToString(),
            Colors.Orange.ToString(),
            Colors.Pink.ToString(),
            Colors.Red.ToString(),
            Colors.Yellow.ToString()
        };
        
        public CategoryDetailsView()
        {

            InitializeComponent();
            ComboBox0.ItemsSource = COLORS;
        }
        
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this category?", "Delete", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ((CategoryDetailsViewModel)DataContext).DeleteCategory();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}