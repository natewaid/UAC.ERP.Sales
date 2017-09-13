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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;


namespace UAC.ERP.Common.Controls
{
    /// <summary>
    /// Interaction logic for TemplateWindow.xaml
    /// </summary>
    public partial class TemplateWindow : DXWindow
    {
        public TemplateWindow(UserControl specificControl, String title)
        {
            InitializeComponent();
            Grid.SetRow(specificControl, 1);
            grdTemplate.Children.Add(specificControl);
            this.Title = title;
        }
    }
}
