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
    
    public partial class Skola
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Skola()
        {
            this.Razred = new HashSet<Razred>();
            this.Zaposlenik = new HashSet<Zaposlenik>();
        }
    
        public int ID { get; set; }
        public string nazivSkole { get; set; }
        public string adresa { get; set; }
        public string email { get; set; }
        public string mBrSkole { get; set; }
        public string oibSkole { get; set; }
        public string telefon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Razred> Razred { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
