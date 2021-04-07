using System.Windows;
using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF.Checking
{
    /// <summary>
    /// Interaction logic for CategoryDetails.xaml
    /// </summary>
    public partial class CategoryDetailsView : UserControl
    {
        public CategoryDetailsView()
        {

            InitializeComponent();
            ComboBox0.ItemsSource = LoadComboBoxData();
        }

        private string[] LoadComboBoxData()
        {
            string[] strArray =
            {
                Colors.Blue.ToString(),
                Colors.Green.ToString(),
                Colors.Orange.ToString(),
                Colors.Pink.ToString(),
                Colors.Red.ToString(),
                Colors.Yellow.ToString()
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