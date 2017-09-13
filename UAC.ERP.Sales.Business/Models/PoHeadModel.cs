using System;
using System.Collections.ObjectModel;
using UAC.ERP.Sales.Models;

namespace UAC.ERP.Sales.Business.Models
{
    public class PoHeadModel
    {
        public int POHeadID { get; set; }
        public string OrderNo { get; set; }
        public Nullable<System.DateTime> CustomerOrderDate { get; set; }
        public string Currency { get; set; }
        public Nullable<int> PaymentTerms { get; set; }
        public Nullable<int> DeliveryTerms { get; set; }
        public int DeliveryAddress { get; set; }
        public Nullable<int> InsideSalesRep { get; set; }
        public int CustomerId { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<int> ContactPerson { get; set; }
        public Nullable<int> Plant { get; set; }
        public virtual ObservableCollection<POLine> POLines { get; set; }
    }
}
