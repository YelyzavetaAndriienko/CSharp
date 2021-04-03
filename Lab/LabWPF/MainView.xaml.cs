using System.Windows.Controls;

namespace LI.CSharp.Lab.GUI.WPF
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}