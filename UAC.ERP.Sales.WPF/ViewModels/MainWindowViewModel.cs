using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Common.Controls.Views;
using UAC.ERP.Common.Controls;
using System.Configuration;
using System.Windows.Input;
using DevExpress.Mvvm.ModuleInjection;
using System.Linq;
using UAC.ERP.Language;

namespace UAC.ERP.Sales.WPF.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Messenger.Default.Register<Messages>(this, OnMsgReceive);
        }

        private void OnMsgReceive(Messages msg)
        {
            switch (msg.MessageTypes)
            {
                //case Common.Controls.MessageType.RemovePoHead:
                //    ViewInjectionManager.Default.Remove(Regions.Main, ModuleType.PoHeadModule);
                //    break;
                //case Common.Controls.MessageType.ClosePoDetail:
                //    ViewInjectionManager.Default.Remove(Regions.Main, ModuleType.PoDetailModule);
                //    break;

                default:

                    break;
            }


        }
        private ICommand addSomething;
        public ICommand AddSomething
        {
            get
            {
                if (addSomething == null)
                {

                    addSomething = new DelegateCommand(addSS);

                }
                return addSomething;
            }

        }

        private void addSS()
        {
            Messenger.Default.Send(new Messages(Common.Controls.MessageType.CreatePODetail));
        }


        private ICommand screenLoaded;
        public ICommand ScreenLoaded
        {
            get
            {
                if (screenLoaded == null)
                {

                    screenLoaded = new DelegateCommand(screenLoadedExecute);

                }
                return screenLoaded;
            }

        }

        private void screenLoadedExecute()
        {// On Screen Loaded overwrite the events of logging in and logging out to save user settings and reload the menu bar
            MyUser.AuthenticationChanged += MyUser_AuthenticationChanged;
            MyUser.AuthenticationChanging += MyUser_AuthenticationChanging;
            // delete this after login works
            //LoadMenu();
        }

        private void MyUser_AuthenticationChanging(object sender, EventArgs e)
        {
            string _applicationSettings = "";
            foreach (SettingsProperty item in Properties.Settings.Default.Properties)
            {
                if (item.Name != "UAC_Login")
                {
                    _applicationSettings = string.Join(_applicationSettings == "" ? "" : ",", _applicationSettings, string.Format("{0}:{1}", item.Name, Properties.Settings.Default[item.Name]));
                }
            }
            MyUser.Settings = _applicationSettings;

        }

        private void MyUser_AuthenticationChanged(object sender, EventArgs e)
        {
            if (MyUser.Settings != "")
            {
                string[] Stetting = MyUser.Settings.Split(',');
                foreach (string d in Stetting)
                {
                    string[] CurrentSetting = d.Split(':');
                    Properties.Settings.Default[CurrentSetting[0]] = CurrentSetting[1];

                }
            }
            LoadMenu();

        }
        public bool cont { get; set; }
        private void LoadMenu()
        {
            
            var colType = Enum.GetValues(typeof(ModuleType)).Cast<ModuleType>();
            var type = typeof(ModuleType);
            if (!cont)
            {
                ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.PoListModule,
              () => NavBarControlViewModel.Create("Sales", LanguageHelper.getInstance().getTranslation("lblPurchaseOrderList"), ModuleType.PoListModule), typeof(NavBarControl));
                ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.CustomerModel,
              () => NavBarControlViewModel.Create("Sales","Customer", ModuleType.CustomerModel), typeof(NavBarControl));

            }
            cont = true;
        }
    }
}