using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAC.ERP.Sales.Business.Models
{
   public class CustomerModels
    {

        public virtual int Id { get; set; }
        public virtual string CustCode { get; set; }
        public virtual string CustName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string VATCode { get; set; }
        public virtual decimal CreditLimit { get; set; }
        public virtual int Region { get; set; }
        public virtual int MotherComopani { get; set; }
}
   public class CustomerAddressModel
    {
    }
   public class CustomerContactModel
    {
    }
    public class SimpleCustModel {

        public virtual int Id { get; set; }
        public virtual string CustName { get; set; }
    }
    public class SimpleCountryModel {

        public virtual int Id { get; set; }
        public virtual string Country { get; set; }
    }

    
}
