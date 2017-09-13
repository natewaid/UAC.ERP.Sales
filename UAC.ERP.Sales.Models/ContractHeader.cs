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
    
    public partial class ContractHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContractHeader()
        {
            this.ContractDetails = new HashSet<ContractDetail>();
            this.ContractHeader1 = new HashSet<ContractHeader>();
        }
    
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContractNo { get; set; }
        public int ContractDate { get; set; }
        public int ValidFrom { get; set; }
        public int ValidTill { get; set; }
        public int LenghtUoM { get; set; }
        public int WeightUoM { get; set; }
        public int Currency { get; set; }
        public int PlantId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int ContractHeaderId { get; set; }
        public int CustomerId2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractDetail> ContractDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractHeader> ContractHeader1 { get; set; }
        public virtual ContractHeader ContractHeader2 { get; set; }
    }
}
