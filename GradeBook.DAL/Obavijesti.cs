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
    
    public partial class Obavijesti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Obavijesti()
        {
            this.Razred = new HashSet<Razred>();
        }
    
        public int ID { get; set; }
        public string tekst { get; set; }
        public System.DateTime vrijemeObjavljivanja { get; set; }
        public int IDobjavljivaca { get; set; }
    
        public virtual Zaposlenik Zaposlenik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Razred> Razred { get; set; }
    }
}
