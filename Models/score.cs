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
    
    public partial class score
    {
        public int scoreId { get; set; }
        public int compId { get; set; }
        public string userId { get; set; }
        public bool isFirstPlayerBetter { get; set; }
    
        public virtual comp comp { get; set; }
        public virtual userProfile userProfile { get; set; }
    }
}