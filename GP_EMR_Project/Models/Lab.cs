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
    
    public partial class Lab
    {
        public long Patient_Id { get; set; }
        public long Org_Id { get; set; }
        public string Lab_Name { get; set; }
        public string Lab_Description_Result { get; set; }
        public string Lab_Type { get; set; }
        public System.DateTime Lab_Date { get; set; }
        public string Lab_File { get; set; }
        public long Lab_Id { get; set; }
    
        public virtual Medical_Organization Medical_Organization { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
