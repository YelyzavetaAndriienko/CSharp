using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using LI.CSharp.Lab.GUI.WPF.Authentication;
using LI.CSharp.Lab.GUI.WPF.Checking;
using LI.CSharp.Lab.GUI.WPF.Navigation;
using LI.CSharp.Lab.Services;
using Prism.Mvvm;

namespace LI.CSharp.Lab.GUI.WPF
{
    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        private AllServices _allServices;

        public MainViewModel()
        {
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
            Navigate(MainNavigatableTypes.Auth);
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _allServices.SaveChanges();
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type, AllServices allServices)
        {
            if (type == MainNavigatableTypes.Auth)
            {
                var res = new AuthViewModel(() => Navigate(MainNavigatableTypes.Check));
                _allServices = new AllServices(res.User);
                return res;
            }
            else 
            {
                return new CheckViewModel(_allServices);
            }
        }
    }
}