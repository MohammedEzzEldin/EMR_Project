//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GP_EMR_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Operation
    {
        public long Patient_Id { get; set; }
        public string Op_Name { get; set; }
        public string Op_Type { get; set; }
        public System.DateTime Op_Date { get; set; }
        public Nullable<long> Doctor_Id { get; set; }
        public Nullable<long> Organization_Id { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Medical_Organization Medical_Organization { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
