using System;

namespace UAC.ERP.Sales.Business.Models
{
    public class PoLineModel
    {
        public int POLineId { get; set; }
        public int POHeadPOHeadID { get; set; }
        public Nullable<int> LineNo { get; set; }
        public string Item { get; set; }
        public string CustomerPart { get; set; }
        public string Description { get; set; }
        public string Alloy { get; set; }
        public string Temper { get; set; }
        public Nullable<decimal> Length { get; set; }
        public string LenghtUOM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string QuantityUOM { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> Stensile { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string CMS { get; set; }
        public Nullable<bool> FAIRequired { get; set; }
        public string PackingInfo { get; set; }
        public string Oil { get; set; }
        public Nullable<int> UpperShipTolerance { get; set; }
        public Nullable<int> LowerShipTolerance { get; set; }
        public string ToleranceUOM { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> DeliveryAddress { get; set; }
    }
}
