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
    
    public partial class General_Examination
    {
        public long Patient_Id { get; set; }
        public Nullable<int> Length { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Blood_Type { get; set; }
        public string Blood_Pressure { get; set; }
        public Nullable<int> Diabetes { get; set; }
        public string Habits { get; set; }
        public Nullable<int> Temperature { get; set; }
        public string Allergies { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
