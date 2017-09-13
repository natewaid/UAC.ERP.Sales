using System;

namespace UAC.ERP.Sales.Business.Models
{
    public class PoLineImportModel
    {
        public string SelectedItem { get; set; }
        public string SelectedCustomerPart { get; set; }
        public string SelectedDescription { get; set; }
        public string SelectedAlloy { get; set; }
        public string SelectedTemper { get; set; }
        public string SelectedLength { get; set; }
        public string SelectedLenghtUOM { get; set; }
        public string SelectedQuantity { get; set; }
        public string SelectedQuantityUOM { get; set; }
        public string SelectedUnitPrice { get; set; }
        public string SelectedPrice { get; set; }
        public string SelectedDiscount { get; set; }
        public string SelectedDeliveryDate { get; set; }
        public string SelectedRemarks { get; set; }
        public string SelectedStensile { get; set; }
        public string SelectedWeight { get; set; }
        public string SelectedCMS { get; set; }
        public string SelectedFAIRequired { get; set; }
        public string SelectedPackingInfo { get; set; }
        public string SelectedOil { get; set; }
        public string SelectedUpperShipTolerance { get; set; }
        public string SelectedLowerShipTolerance { get; set; }
        public string SelectedToleranceUOM { get; set; }
        public string SelectedStatus { get; set; }
        public Nullable<int> DeliveryAddress { get; set; }
    }
}
