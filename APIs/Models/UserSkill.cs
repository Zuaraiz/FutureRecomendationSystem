//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSkill
    {
        public int id { get; set; }
        public int skill { get; set; }
        public int rating { get; set; }
        public int user { get; set; }
    
        public virtual Skill Skill1 { get; set; }
        public virtual User User1 { get; set; }
    }
}
