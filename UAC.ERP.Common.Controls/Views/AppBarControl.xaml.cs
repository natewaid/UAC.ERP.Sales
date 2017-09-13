using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UAC.ERP.Common.Controls.ViewModels;

namespace UAC.ERP.Common.Controls.Views
{
    /// <summary>
    /// Interaction logic for AppBarControl.xaml
    /// </summary>
    public partial class AppBarControl : UserControl
    {
        public AppBarControl()
        {
            InitializeComponent();
        }


    }
    public class ButonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate butonTemplate { get; set; }
        public DataTemplate separatorTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            AppBarControlModel m = (AppBarControlModel)item;

            if (m.IsSeparator)
            {
                return separatorTemplate;
            }


            else return butonTemplate;
        }
    }
}