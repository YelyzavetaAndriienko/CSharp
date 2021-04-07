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
    //public class MainViewModel : NavigationBase<MainNavigatableTypes>
    //{
    //    private WalletService _walletService;

    //    public MainViewModel()
    //    {
    //        Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
    //        Navigate(MainNavigatableTypes.Auth);
    //    }
    //    void MainWindow_Closing(object sender, CancelEventArgs e)
    //    {
    //        _walletService.SaveChanges();
    //    }

    //    protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type, Object obj)
    //    {
    //        if (type == MainNavigatableTypes.Auth)
    //        {
    //            var res = new AuthViewModel(() => Navigate(MainNavigatableTypes.Check));
    //            _walletService = new WalletService(res.User);
    //            return res;
    //        }
    //        else
    //        {
    //            return new CheckViewModel(_walletService);
    //        }
    //    }
    //}

    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        private CategoryService _categoryService;

        public MainViewModel()
        {
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
            Navigate(MainNavigatableTypes.Auth);
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _categoryService.SaveChanges();
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type, Object obj)
        {
            if (type == MainNavigatableTypes.Auth)
            {
                var res = new AuthViewModel(() => Navigate(MainNavigatableTypes.Check));
                _categoryService = new CategoryService(res.User);
                return res;
            }
            else
            {
                return new CheckViewModel(_categoryService);
            }
        }
    }
}