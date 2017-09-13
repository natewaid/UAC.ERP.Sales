using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Spire.Xls;
using UAC.ERP.Common.Controls;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Business.Operation;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.PO.ViewModels
{
    [POCOViewModel]
    public class ImportFormViewModel
    {
        public struct ColumnMapping
        {
            public string DatabaseColumn { get; set; }

            public string ExcelColumn { get; set; }
        }

        public virtual PoHeadModel PurchaseOrder { get; set; }

        public virtual ObservableCollection<PoLineModel> PoLineImport { get; set; } = new ObservableCollection<PoLineModel>();

        public virtual PoLineImportModel ImportElements { get; set; }

        public virtual List<string> ExcelColumns { get; set; } = new List<string>();

        public virtual List<string> DatabaseColumns { get; set; } = new List<string>();

        public virtual List<ColumnMapping> MappedColumns { get; set; } = new List<ColumnMapping>();

        public virtual string SelectedDatabaseColumn { get; set; }

        public virtual string SelectedExcelColumn { get; set; }

        public virtual ICurrentWindowService WindowService { get { return null; } }

        public virtual ObservableCollection<AppBarControlModel> AppBarButtons { get; set; }


        public string Caption { get { return "Import PO Lines"; } }        

        public int PoId
        {
            get { return _poId; }
            set { _poId = value; }
        }

        private IDictionary<MessageType, Action> MessageCommands { get; set; } = new Dictionary<MessageType, Action>();        

        private IPOHelper PoHelper { get; set; } = new POHelper();

        private IList<DataRow> ImportList;

        private int _poId;        

        public ImportFormViewModel()
        {
            MessageCommands = new Dictionary<MessageType, Action>
            {
                { MessageType.PrepareImportView, PrepareImportView }
            };

            Messenger.Default.Register<Messages>(this, OnMsgReceive);
            ImportElements = new PoLineImportModel();
            PrepareImportXLS();
        }

        public static ImportFormViewModel Create()
        {            
            return ViewModelSource.Create(() => new ImportFormViewModel());            
        }

        public void OnMsgReceive(Messages msg)
        {
            if (!MessageCommands.ContainsKey(msg.MessageTypes))
            {
                return;
            }

            _poId = msg.ObjectIDInt;
            MessageCommands[msg.MessageTypes]();
        }

        private void GetAppBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>
            {
                new AppBarControlModel
                {
                    Label = "Map Selected Columns",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(AddColumnMapping)
                },
                new AppBarControlModel
                {
                    Label = "Start Import",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(StartImport),
                },
                new AppBarControlModel
                {
                    Label = "Cancel",
                    Enabled = true,
                    Alignment = "Center",
                    IsSeparator = false,
                    ExecutingCommand = new DelegateCommand(CloseWindow),
                }                
            };
        }

        private void AddColumnMapping()
        {
            MappedColumns.Add(new ColumnMapping
            {
                DatabaseColumn = SelectedDatabaseColumn,
                ExcelColumn = SelectedExcelColumn
            });
        }

        private void PrepareImportXLS()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.ShowDialog();           

            var workbook = new Workbook();
            workbook.LoadFromFile(dialog.FileName);

            var sheet = workbook.Worksheets[0];
            var table = sheet.ExportDataTable();

            ImportList = table.AsEnumerable().ToList();
            var item = ImportList.FirstOrDefault();            
                
            foreach (DataColumn j in item.Table.Columns)
            {
                MapColumn(j.Caption);
            }
            
            DatabaseColumns = new POLine().GetType().GetProperties()
                .Where(p => !p.GetAccessors().Any(a => a.IsVirtual))
                .Select(p => p.Name)
                .Except(MappedColumns.Select(m => m.DatabaseColumn)).ToList();
        }

        private void MapColumn(string column)
        {
            var props = new POLine().GetType().GetProperties()
                .Where(prop => !prop.GetAccessors().Any(a => a.IsVirtual))
                .Select(prop => prop.Name);

            var col = column.ToLower().Replace(" ", "");

            foreach (var p in props)
            {
                if (col.Equals(p.ToLower().Replace(" ", "")))
                {
                    MappedColumns.Add(new ColumnMapping { DatabaseColumn = p, ExcelColumn = column });
                    break;
                }
            }

            if (!MappedColumns.Any(m => m.ExcelColumn.Equals(column)))
            {
                ExcelColumns.Add(column);
            }

            ExcelColumns.Sort();
        }

        private void PrepareImportView()
        {
            PurchaseOrder = PoHelper.GetPo(_poId);
            GetAppBarButtons();
        }

        private void StartImport()
        {
            var lineNo = PurchaseOrder.POLines.Max(p => p.LineNo) + 1;
            foreach (var item in ImportList)
            {
                var poLine = CopyDataToPoLine(item);
                poLine.LineNo = lineNo;
                poLine.POHeadPOHeadID = PurchaseOrder.POHeadID;
                PoLineImport.Add(poLine);
                PoHelper.SavePoLine(poLine);
                lineNo++;
            }

            CloseWindow();
        }

        private PoLineModel CopyDataToPoLine(DataRow item)
        {
            var poLine = new POLine();

            foreach (var c in MappedColumns)
            {
                var prop = poLine.GetType().GetProperty(c.DatabaseColumn);
                var value = item[c.ExcelColumn];

                try
                {
                    prop.SetValue(poLine, Convert.ChangeType(value, prop.DetermineType()));
                }
                catch
                {
                    continue;
                }
            }

            return poLine.CopyTo(new PoLineModel());
        }

        private void CloseWindow()
        {
            Messenger.Default.Send(new Messages(PoId, MessageType.CloseImportForm));
            WindowService.Close();
        }
    }
}