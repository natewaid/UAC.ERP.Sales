using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.NavBar;
using UAC.ERP.Common.Controls.ViewModels;

namespace UAC.ERP.Common.Controls
{
    public class NavBarControlWrapper : IItemsControlWrapper<NavBarControl>
    {
        public NavBarControl Target { get; set; }

        private object _ItemsSource;

        public object ItemsSource
        {
            get
            {
                return _ItemsSource;
            }
            set
            {
                if (_ItemsSource != value)
                {
                    if (_ItemsSource != null)
                    {
                        ((INotifyCollectionChanged)_ItemsSource).CollectionChanged -= NavBarControlWrapper_CollectionChanged;
                    }

                    _ItemsSource = value;
                    ((INotifyCollectionChanged)_ItemsSource).CollectionChanged += NavBarControlWrapper_CollectionChanged;
                }
            }
        }

        private void NavBarControlWrapper_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!e.Action.Equals(NotifyCollectionChangedAction.Add))
            {
                return;
            }

            foreach (NavBarControlViewModel vm in e.NewItems)
            {
                var group = Target.Groups.FirstOrDefault(c => c.Name.Equals(vm.Group) || c.Header.Equals(vm.Group));
                if (group == null)
                {
                    group = new NavBarGroup
                    {
                        Name = vm.Group.Replace(" ", ""),
                        Header = vm.Group,
                        IsVisible = false
                    };

                    Target.Groups.Add(group);
                }

                var item = new NavBarItem
                {
                    Content = vm.Item,
                    Name = vm.Item.Replace(" ", ""),
                    Command = vm.ExecutingCommand,
                    IsVisible = false
                };

                item.IsEnabledChanged += Item_IsEnabledChanged;
                group.Items.Add(item);
            }
        }

        private void Item_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var l = (NavBarItem)sender;
            l.Group.IsVisible = l.Group.Items.Count() > 0;
            ((NavBarItem)sender).IsVisible = ((NavBarItem)sender).IsEnabled;
        }

        public DataTemplate ItemTemplate
        {
            get { return Target.ItemTemplate; }
            set { Target.ItemTemplate = value; }
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return Target.ItemTemplateSelector; }
            set { Target.ItemTemplateSelector = value; }
        }
    }
}
