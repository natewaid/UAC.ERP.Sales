using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using UAC.ERP.Common.Controls;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Sales.Business.Models;

namespace UAC.ERP.Sales.PO.ViewModels
{
    [POCOViewModel]
    public class PoDetailViewModel : BaseViewModel
    {
        public virtual PoLineModel PoDetailSelected { get; set; }

        public virtual PoHeadModel PurchaseOrder { get; set; }

        public override string Caption { get { return "Purchase Order Detail"; } }

        private int _objId;

        public PoDetailViewModel()
        {
            MessageCommands = new Dictionary<MessageType, Action>
            {
                { MessageType.NewPoLine, NewPoLine },
                { MessageType.EditPoLine, EditPoLine },
                //{ MessageType.ClosePoDetail, ClosePoDetail }
            };

            Messenger.Default.Register<Messages>(this, OnMsgReceive);
        }

        public override IViewModel Create()
        {
            return ViewModelSource.Create(() => new PoDetailViewModel());
        }

        public override void OnMsgReceive(Messages msg)
        {
            if (!MessageCommands.ContainsKey(msg.MessageTypes))
            {
                return;
            }

            _objId = msg.ObjectIDInt;
            MessageCommands[msg.MessageTypes]();
        }

        public override void GetAppBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>
            {
                new AppBarControlModel
                {
                    Label = "Save",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(SaveNewLine)
                },
                new AppBarControlModel
                {
                    Label = "Cancel",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(ClosePoDetail)
                }
            };
        }

        private void NewPoLine()
        {
            PurchaseOrder = PoHelper.GetPo(_objId);
            PoDetailSelected = new PoLineModel();            
            PoDetailSelected.POHeadPOHeadID = PurchaseOrder.POHeadID;
            GetAppBarButtons();
        }

        private void EditPoLine()
        {
            PoDetailSelected = new PoLineModel();
            PoDetailSelected = PoHelper.GetPoLine(_objId);
            PurchaseOrder = PoHelper.GetPo(PoDetailSelected.POHeadPOHeadID);
            GetAppBarButtons();
        }

        private void ClosePoDetail()
        {
            Messenger.Default.Unregister(this);
            //Messenger.Default.Send(new Messages(Common.Controls.MessageType.ClosePoDetail));
        }        

        private void SaveNewLine()
        {
            var result = DXMessageBox.Show(
                    Language.LanguageHelper.getInstance().getTranslation("save_line_confirm"),
                    Language.LanguageHelper.getInstance().getTranslation("save_line_confirm_caption"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                PoDetailSelected.POHeadPOHeadID = PurchaseOrder.POHeadID;
                PoHelper.SavePoLine(PoDetailSelected);
            }
        }
    }
}