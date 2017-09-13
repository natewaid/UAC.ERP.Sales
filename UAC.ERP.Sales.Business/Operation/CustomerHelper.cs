using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAC.ERP.Sales.Business.Models;
using UAC.ERP.Sales.Models;
using UAC.ERP.Sales.Repositories;

namespace UAC.ERP.Sales.Business.Operation
{
    public class CustomerHelper
    {
        public static ObservableCollection<CustomerModels> GetAllCustomers()
        {
            ObservableCollection<CustomerModels> CustomerList = new ObservableCollection<CustomerModels>();
            List<Customer> CustomerListEntity = CustomerData.ReturnAllCustomers();

            foreach (var Item in CustomerListEntity)

            {

                CustomerList.Add(new CustomerModels
                {
                    Id = Item.Id,
                    CustName = Item.CustName,
                    CustCode = Item.CustCode,
                    Address = Item.Address,
                    Address2 = Item.Address2,
                    City = Item.City,
                    Country = Item.Country,
                    Phone = Item.Phone,
                    Region = Item.Region ?? 0,
                    State = Item.State,
                    Fax = Item.Fax,
                    VATCode = Item.VATCode,
                    MotherComopani = Item.MotherCompany ?? 0,
                    CreditLimit = Item.CreditLimit,
                    ZipCode = Item.ZipCode
                });


            }
            return CustomerList;
        }

        public static ObservableCollection<CustomerModels> GetSmallCustomers()
        {
            ObservableCollection<CustomerModels> CustomerList = new ObservableCollection<CustomerModels>();
            List<Customer> CustomerListEntity = CustomerData.ReturnAllCustomers();

            foreach (var Item in CustomerListEntity)

            {

                CustomerList.Add(new CustomerModels
                {
                    Id = Item.Id,
                    CustName = Item.CustName,
                    CustCode = Item.CustCode,
                    Address = Item.Address,                   
                    City = Item.City,
                    Country = Item.Country,
                    Phone = Item.Phone,
                    MotherComopani = Item.MotherCompany ?? 0,
                    
                });


            }
            return CustomerList;
        }
    }
}
