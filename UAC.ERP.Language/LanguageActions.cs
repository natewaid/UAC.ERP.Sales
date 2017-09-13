using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;

namespace UAC.ERP.Language
{
    class LanguageActions : ViewModelBase
    {
        private static ICommand _changeLangCMD;

        private static ObservableCollection<BarCheckItem> _appLangList { set; get; }

        public static ICommand ChangeLangCMD
        {
            get
            {
                if (_changeLangCMD == null)
                {
                    _changeLangCMD = new DelegateCommand(onChangeLang);
                }
                return _changeLangCMD;
            }
        }

        private static void onChangeLang()
        {
            string chosenLang = (from lang in _appLangList
                                 where lang.IsChecked == true
                                 select (string)lang.Content).FirstOrDefault();

            LanguageHelper.getInstance().setSelectedLanguage(chosenLang, true);

            Messenger.Default.Send(new Message(MessageType.CultureChanged));
        }

        public static ObservableCollection<BarCheckItem> LoadAndRefreshLanguageList()
        {
            ObservableCollection<BarCheckItem> AppLanguageList = new ObservableCollection<BarCheckItem>();

            _appLangList = new ObservableCollection<BarCheckItem>();

            List<AppLang> list = LanguageHelper.getInstance().getLangList();

            foreach (AppLang lang in list)
            {
                _appLangList.Add(new BarCheckItem
                {
                    Content = lang.Name,
                    IsChecked = lang.Name == LanguageHelper.getInstance().getCurentLang(),
                    Command = ChangeLangCMD,
                    GroupIndex = 69
                });
            }

            AppLanguageList = _appLangList;
            return AppLanguageList;
        }
    }
}   