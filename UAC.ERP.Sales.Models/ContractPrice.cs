//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UAC.ERP.Sales.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContractPrice
    {
        public int Id { get; set; }
        public int ContractDetailId { get; set; }
        public decimal QuantityMinLimit { get; set; }
        public decimal QuantityMaxLimit { get; set; }
        public int QuantityLimitUoM { get; set; }
        public int Price { get; set; }
        public int UoM { get; set; }
        public int ContractDetailId2 { get; set; }
    
        public virtual ContractDetail ContractDetail { get; set; }
    }
}
