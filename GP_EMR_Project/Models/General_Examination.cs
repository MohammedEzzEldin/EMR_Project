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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public General_Examination()
        {
            this.Allergies = new HashSet<Allergy>();
            this.Habits = new HashSet<Habit>();
        }
    
        public long Patient_Id { get; set; }
        public Nullable<int> Length { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Blood_Type { get; set; }
        public string Blood_Pressure { get; set; }
        public Nullable<int> Diabetes { get; set; }
        public Nullable<double> Temperature { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Allergy> Allergies { get; set; }
        public virtual Patient Patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habit> Habits { get; set; }
    }
}
