using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Repositories
{
    public class CustomerData
    {
        public static List<Customer> ReturnAllCustomers()
        {
            using (Production2016Entities db = new Production2016Entities())
            {
                return (from Cust in db.Customers select Cust).ToList();
            }
        }    
    }
}
