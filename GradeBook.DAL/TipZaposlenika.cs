//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GradeBook.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipZaposlenika
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipZaposlenika()
        {
            this.Zaposlenik = new HashSet<Zaposlenik>();
        }
    
        public int ID { get; set; }
        public string nazivTipa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
