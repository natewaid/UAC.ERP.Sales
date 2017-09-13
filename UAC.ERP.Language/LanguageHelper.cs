using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace UAC.ERP.Language
{
    public  class LanguageHelper
    {
        public static readonly List<string> supportedLanguages = new List<string>() { "RO", "EN" };
   
        private string currentLang = String.Empty;

        private static LanguageHelper instance = null;

        public static LanguageHelper getInstance()
        {
            if (instance == null)
            {
                instance = new LanguageHelper();
            }

            return instance;
        }

        private LanguageHelper() { }

        public void setSelectedLanguage(string newLang, bool persist)
        {
            try
            {
                if (supportedLanguages.Contains(newLang))
                {

                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/UAC.ERP.Language;component/Language/Lang." + newLang + ".xaml", UriKind.Relative) });
                    currentLang = newLang;
                    if (persist)
                    {
                        try
                        {
                            UserApplicationSettings.Instance.SelectedLanguage = newLang;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public string getCurentLang()
        {
            if (currentLang == null)
            {
                currentLang = "";
            }

            if (currentLang.Length > 0)
            {
                return currentLang;
            }

            try
            {
                currentLang = UserApplicationSettings.Instance.SelectedLanguage;
                return currentLang;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            currentLang = supportedLanguages[0];
            return supportedLanguages[0];
        }

     
        public List<AppLang> getLangList()
        {
            if (currentLang == null)
            {
                currentLang = "";
            }

            if (!(currentLang.Length > 0))
            {
                getCurentLang();
            }

            List<AppLang> list = new List<AppLang>();
            foreach (string lang in supportedLanguages)
            {

                list.Add(new AppLang { Name = lang, IsDefault = (currentLang == lang) });
            }

            return list;
        }

        public string getTranslation(string resName)
        {
            try
            {
                return (string)Application.Current.Resources[resName];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return string.Empty;
        }
    }
}
