using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;

namespace UAC.ERP.Common.Controls.ViewModels
{
    [POCOViewModel]
    public class NavBarControlViewModel : ViewModelBase
    {
        public string Group { get; set; }

        public string Item { get; set; }

        private DelegateCommand<string> _executingCommand;

        public DelegateCommand<string> ExecutingCommand
        {
            get
            {
                return _executingCommand;
            }

            set
            {
                _executingCommand = value;
                RaisePropertyChanged("ExecutingCommand");
            }
        }

        protected NavBarControlViewModel(ModuleType mod) { }

        public static NavBarControlViewModel Create(string group, string item, ModuleType mod)
        {
            var viewModel = new NavBarControlViewModel(mod);
            viewModel.Group = group;
            viewModel.Item = item;
            viewModel.ExecutingCommand = new DelegateCommand<string>((s) => MenuCommand(mod.ToString("F")), (s) => MyUser.HasPermision(mod.ToString("F") + "_CanRead"), null);
            return viewModel;
        }

        private static void MenuCommand(string s)
        {
            ModuleManager.DefaultManager.InjectOrNavigate(Regions.Main, s);
        }
    }
}

 