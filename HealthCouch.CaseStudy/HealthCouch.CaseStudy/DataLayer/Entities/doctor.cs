using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string DoctorName { get; set; }

        [Required]
        [StringLength(100)]
        public string Speciality { get; set; }

        public Doctor()
        {
            
        }
        public Doctor(int doctorId, string doctorName, string speciality)
        {
            DoctorId = doctorId;
            DoctorName = doctorName;
            Speciality = speciality;
        }
    }
}
