using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Bars;

namespace UAC.ERP.Language.ViewModels
{
    [POCOViewModel]
    public class LangBarViewModel : ViewModelBase
    {
        public LangBarViewModel()
        {
            LanguageHelper.getInstance().setSelectedLanguage("EN", true);
            LoadAppbar();
        }

        public ObservableCollection<BarCheckItem> AppLangList { set; get; }   
     
        public void LoadAppbar()
        {
            AppLangList = LanguageActions.LoadAndRefreshLanguageList();          
        }
    }
}
