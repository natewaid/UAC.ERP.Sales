using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.NavBar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UAC.ERP.Common.Controls;
using UAC.ERP.Customer.ViewModels;
using UAC.ERP.Sales.PO.ViewModels;
using UAC.ERP.Sales.PO.Views;

namespace UAC.ERP.Sales.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
           ApplicationThemeHelper.ApplicationThemeName = "Office2010Blue";

            InitModules();

        }
        private void InitModules()


        {
            StrategyManager.Default.RegisterStrategy<NavBarControl, ItemsControlStrategy<NavBarControl, NavBarControlWrapper>>();
            //     ModuleManager.DefaultManager.Register(Regions.Main, new Module(ModuleType.PoDetailModule.ToString("F"), viewModelFactory: () => new PoDetailViewModel(), viewType: typeof(PoDetailView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Module(ModuleType.PoListModule.ToString("F"), viewModelFactory: () => new PoListViewModel(), viewType: typeof(PoListView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Module(ModuleType.CustomerModel.ToString("F"), viewModelFactory: () => new CustomerViewModel(), viewType: typeof(Customer.Views.Customer)));
            //  ModuleManager.DefaultManager.Register(Regions.Main, new Module(ModuleType.PoHeadModule.ToString("F"), viewModelFactory: () =>new PoHeadViewModel(), viewType: typeof(PoHeadView)));

        }


    }



}
