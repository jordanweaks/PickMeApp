//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PickME.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class comp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public comp()
        {
            this.scores = new HashSet<score>();
            this.comments = new HashSet<comment>();
        }
    
        public int compId { get; set; }
        public string userId { get; set; }
        public int firstPlayerId { get; set; }
        public int secondPlayerId { get; set; }
        public Nullable<System.DateTime> exp { get; set; }
    
        public virtual userProfile userProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<score> scores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }
    }
}
