using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using UAC.ERP.Sales.Business.Models;
using DevExpress.Mvvm.POCO;
using UAC.ERP.Sales.Business.Operation;
using System.Linq;
using DevExpress.Xpf.Editors;

namespace UAC.ERP.Customer.ViewModels
{
    [POCOViewModel]
    public class CustomerSearchViewModel
    {
        #region Public Virtual Propreties

        public virtual ObservableCollection<CustomerModels> SmallCustomerList { get; set; }      
        public virtual ObservableCollection<CustomerModels> SmallComboboxCustomerList { get; set; }      
        public virtual CustomerModels SmallCustomerSelectedItem { get; set; }
        public virtual string SmallComboboxCustomerSelectedItem { get; set; }
        public virtual string SearchText { get; set; }
    public virtual bool ShowSmallTabelFlyout { get; set; }

    #endregion

        #region Constructor

    public CustomerSearchViewModel()
        {           
            ShowSmallTabelFlyout = false;
        }

        public static CustomerSearchViewModel Create()
        {
            return ViewModelSource.Create(() => new CustomerSearchViewModel());

        }

        #endregion

        #region Function     

        public void ShowFlyout() {
                                   
            ShowSmallTabelFlyout = true;
        }

        public void LoadList() {

            SmallCustomerList = CustomerHelper.GetSmallCustomers();
            SmallComboboxCustomerList = new ObservableCollection<CustomerModels>( CustomerHelper.GetSmallCustomers());
        }

        public void ComboboxText(ProcessNewValueEventArgs Params)
        {
            SearchText = Params.DisplayText;
        }


        #endregion
    }
    
}