using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using UAC.ERP.Common.Controls;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.PO.Views;
using System.Windows;

namespace UAC.ERP.Sales.PO.ViewModels
{
    [POCOViewModel]
    public class PoListViewModel : BaseViewModel
    {
        public virtual ObservableCollection<PoHeadModel> PurchaseOrderList { get; set; }

        public virtual PoHeadModel SelectedPurchaseOrder { get; set; }

        public override string Caption { get { return "Purchase Order List"; } }        

        public PoListViewModel()
        {
            Messenger.Default.Register<Messages>(this, OnMsgReceive);
            GetAppBarButtons();
            PurchaseOrderList = PoHelper.GetAllPos();
        }

        public override IViewModel Create()
        {
            return ViewModelSource.Create(() => new PoListViewModel());
        }

        public override void OnMsgReceive(Messages msg)
        {
            if (!MessageCommands.ContainsKey(msg.MessageTypes))
            {
                return;
            }

            MessageCommands[msg.MessageTypes]();
        }

        public override void GetAppBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>()
            {
                new AppBarControlModel
                {
                    Label = "New Purchase Order",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(AddNewPOHead),
                },
                new AppBarControlModel
                {
                    Label = "Close",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(CloseCurrentTab),
                }     ,
                new AppBarControlModel
                {
                    Label = "Purchase Order Detail",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(OpenPurchaseOrder),
                }
            };
        }

        private void AddNewPOHead()
        {
            ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.PoHeadModule.ToString("F"), viewModelFactory: () => new PoHeadViewModel().Create(), viewType: typeof(PoHeadView)));            
            Messenger.Default.Send(new Messages(MessageType.AddNewPoHead));
        }

        private void OpenPurchaseOrder()
        {
            if (SelectedPurchaseOrder != null)
            {
                ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.PoHeadModule.ToString("F"), viewModelFactory: () => new PoHeadViewModel().Create(), viewType: typeof(PoHeadView)));
                Messenger.Default.Send(new Messages(SelectedPurchaseOrder.POHeadID, MessageType.ViewPoHead));
                return;
            }

            DXMessageBox.Show(
                    Language.LanguageHelper.getInstance().getTranslation("select_a_po"),
                    "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.None);
        }

        private void CloseCurrentTab()
        {
            ModuleManager.DefaultManager.Remove(Regions.Main, ModuleType.PoListModule.ToString());
        }
    }
}