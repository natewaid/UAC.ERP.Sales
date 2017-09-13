using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using UAC.ERP.Common.Controls.ViewModels;
using UAC.ERP.Common.Controls;
using DevExpress.Mvvm.POCO;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Business.Operation;
using DevExpress.Mvvm.ModuleInjection;
using System.Windows;
using DevExpress.Xpf.WindowsUI;

namespace UAC.ERP.Customer.ViewModels
{
    [POCOViewModel]
    public class CustomerViewModel
    {

        #region Public Virtual Properties

        public string Caption { get { return "Customer List"; } }
        public virtual ObservableCollection<AppBarControlModel> AppBarButtons { get; set; }
        public virtual ObservableCollection<CustomerModels> CustomerList { get; set; }
        public virtual CustomerModels CustomerSelectedItem { get; set; }
        public virtual bool BigCustomerVIsibility { get; set; }
    #endregion

        #region Constructor

    public CustomerViewModel()
        {
            Messenger.Default.Register<Messages>(this, OnMsgReceive);
            MakeBigCustomerList();
        }
        public CustomerViewModel(bool IsSmall)
        {          
            MakeSmallCustomerList();
        }

        public static CustomerViewModel Create()
        {
            return ViewModelSource.Create(() => new CustomerViewModel());
        }
        #endregion

        #region Message
        //Here we define what each message should do
        private void OnMsgReceive(Messages msg)
        {
            switch (msg.MessageTypes)
            {
                case MessageType.BigCustomerList:
                    MakeBigCustomerList();
                    break;
                case MessageType.SmallCustomerList:
                    MakeSmallCustomerList();
                    break;               
                
                default:
                    break;

            }
        }

        #endregion

        #region AppBarButtons

        private void GetEditBarButtons()
        {
            AppBarButtons = new ObservableCollection<AppBarControlModel>();
            AppBarButtons.Add(
                        new AppBarControlModel
                        {
                            Label = "Add",
                            Enabled = true,
                            Alignment = "Center",
                            IsSeparator = false,
                            ExecutingCommand = new DelegateCommand(AddCustomer),
                            
                        });
            AppBarButtons.Add(
                new AppBarControlModel
                        {
                            Label = "Edit",
                            Enabled = true,
                            Alignment = "Center",
                            IsSeparator = false,
                            ExecutingCommand = new DelegateCommand(EditCustomer),
                            
                        });
            AppBarButtons.Add(
                       new AppBarControlModel
                       {
                           Label = "Close",
                           Enabled = true,
                           Alignment = "Center",                        
                           IsSeparator = false,
                           ExecutingCommand = new DelegateCommand(CloseCurrentTab),
                           
                       });
        }

        
        #endregion

        #region Function

        private void MakeBigCustomerList()
        {
            CustomerList = CustomerHelper.GetAllCustomers();
            GetEditBarButtons();
            BigCustomerVIsibility = true;
        }

        private void MakeSmallCustomerList()
        {
            CustomerList = CustomerHelper.GetSmallCustomers();
            BigCustomerVIsibility = false;
        }

        private void EditCustomer()
        {
            if (CustomerSelectedItem != null)
            {
                ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.AddEditCustomerModel.ToString("F"), viewModelFactory: () => AddEditCustomerViewModel.Create(), viewType: typeof(Customer.Views.AddEditCustomer)));
                Messenger.Default.Send(new Common.Controls.Messages(CustomerSelectedItem, Common.Controls.MessageType.EditCustomer));
            }
            else
            {
                WinUIMessageBox.Show("Please Select a record", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddCustomer()
        {
            ModuleManager.DefaultManager.RegisterOrInjectOrNavigate(Regions.Main, new Module(ModuleType.AddEditCustomerModel.ToString("F"), viewModelFactory: () => AddEditCustomerViewModel.Create(), viewType: typeof(Customer.Views.AddEditCustomer)));

            Messenger.Default.Send(new Common.Controls.Messages(Common.Controls.MessageType.AddCustomer));
        }
        private void CloseCurrentTab()
        {
           
           // throw new NotImplementedException();
        }
      
        #endregion
    }

}