//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop_Demo.DAL.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Good
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Good()
        {
            this.Photo = new HashSet<Photo>();
            this.SalePos = new HashSet<SalePos>();
        }
    
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePos> SalePos { get; set; }
    }
}