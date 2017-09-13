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
using UAC.ERP.Language.ViewModels;

namespace UAC.ERP.Language.Views
{
    /// <summary>
    /// Interaction logic for LangBarView.xaml
    /// </summary>
    public partial class LangBarView : UserControl
    {
        public LangBarView()
        {
            InitializeComponent();
            this.DataContext = new LangBarViewModel();
        }
    }
}
