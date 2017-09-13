using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using UAC.ERP.Common.Controls;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Models;
using UAC.ERP.Sales.PO.Views;

namespace UAC.ERP.Sales.PO.ViewModels
{
    [POCOViewModel]
    public class PoHeadViewModel : BaseViewModel
    {
        public virtual PoHeadModel PoHead { get; set; }

        public virtual POLine SelectedPoLine { get; set; }

        public virtual bool IsEditPoHeadEnabled { get; set; }                

        public override string Caption { get { return "Purchase Order"; } }        

        private int _objId;

        public PoHeadViewModel()
        {
            MessageCommands = new Dictionary<MessageType, Action>
            {
                { MessageType.ViewPoHead, ViewPoHead },
                { MessageType.CloseImportForm, ViewPoHead },
                { MessageType.AddNewPoHead, AddNewPoHead }
            };

            Messenger.Default.Register<Messages>(this, OnMsgReceive);
        }

        public override IViewModel Create()
        {
            return ViewModelSource.Create(() => new PoHeadViewModel());
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
            AppBarButtons = new ObservableCollection<AppBarControlModel>()
            {
                new AppBarControlModel
                {
                    Label = "Generate Excel Template",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(GenerateExcelTemplate),
                },
                new AppBarControlModel
                {
                    Label = "Import Po Lines",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(ImportPOLines),
                },
                new AppBarControlModel
                {
                    Label = "Close",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(ClosePoHead),
                },
                new AppBarControlModel
                {
                    Label = "Edit Purchase Order",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(EditPoHead),
                },
                new AppBarControlModel
                {
                    Label = "Add New Line",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(AddNewPoLine),
                },
                new AppBarControlModel
                {
                    Label = "Edit Line",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(EditSelectedPoLine),
                }
            };
        }

        private void ViewPoHead()
        {
            PoHead = null;
            PoHead = PoHelper.GetPo(_objId);
            GetAppBarButtons();
            IsEditPoHeadEnabled = false;
        }

        private void AddNewPoHead()
        {
            PoHead = new PoHeadModel();
            GetEditBarButtons();
            IsEditPoHeadEnabled = true;
        }

        private void GenerateExcelTemplate()
        {
            ObservableCollection<PoLineModel> test = new ObservableCollection<PoLineModel>();

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    GridControl gridControl = new GridControl();
                    TableView tt = new TableView();
                    gridControl.View = tt;
                    gridControl.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew;
                    gridControl.EnableSmartColumnsGeneration = true;
                    gridControl.ItemsSource = test;
                    gridControl.View.ExportToXls(fbd.SelectedPath + "\\PurchaseOrderTemplate.xls");
                }
            }
        }

        private void ImportPOLines()
        {            
            ImportForm ImpForm = new ImportForm();
            ImpForm.DataContext = ImportFormViewModel.Create();
            Messenger.Default.Send(new Messages(PoHead.POHeadID, MessageType.PrepareImportView));
            TemplateWindow ImportWindow = new TemplateWindow(ImpForm,"ImportForm");          
            ImportWindow.ShowDialog();
        }       

        private void EditPoHead()
        {
            GetEditBarButtons();
            IsEditPoHeadEnabled = true;
            //Messenger.Default.Send(new Messages(PoHead.POHeadID, Common.Controls.MessageType.EditPoHead));
        }

        private void EditSelectedPoLine()
        {
            if (SelectedPoLine == null)
            {
                DXMessageBox.Show(
                    Language.LanguageHelper.getInstance().getTranslation("select_a_line_to_edit"), 
                    "", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning, 
                    MessageBoxResult.None);

                return;
            }

            ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.PoDetailModule.ToString("F"), viewModelFactory: () => new PoDetailViewModel().Create(), viewType: typeof(PoDetailView)));
            Messenger.Default.Send(new Messages(SelectedPoLine.POLineId, MessageType.EditPoLine));
        }

        private void AddNewPoLine()
        {
            ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.PoDetailModule.ToString("F"), viewModelFactory: () => new PoDetailViewModel().Create(), viewType: typeof(PoDetailView)));
            Messenger.Default.Send(new Messages(PoHead.POHeadID, MessageType.NewPoLine));
        }

        private void GetEditBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>()
            {
                new AppBarControlModel
                {
                    Label = "Close",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(ClosePoHead),
                },
                new AppBarControlModel
                {
                    Label = "Save",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(SavePoHead),
                }
            };         
        }     

        private void SavePoHead()
        {
            var result = DXMessageBox.Show(
                    Language.LanguageHelper.getInstance().getTranslation("save_po_confirm"),
                    Language.LanguageHelper.getInstance().getTranslation("save_po_confirm_caption"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                PoHelper.SavePoHead(PoHead);
                GetAppBarButtons();
                IsEditPoHeadEnabled = false;
            }
        }            

        private void ClosePoHead()
        {
            Messenger.Default.Unregister(this);
            //Messenger.Default.Send(new Messages(MessageType.RemovePoHead));

            //if (PoHead.POHeadID == 0)
            //{
            //    Messenger.Default.Send(new Messages(MessageType.RemovePoHead));
            //    ViewInjectionManager.Default.Remove(Regions.Main, ModuleType.PoHeadModule);
            //    return;     
            //}

            //Messenger.Default.Send(new Messages(PoHead.POHeadID, MessageType.ViewPoHead));      
        }       
    }
}