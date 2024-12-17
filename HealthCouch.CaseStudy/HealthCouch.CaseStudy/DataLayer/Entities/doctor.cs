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

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Doctor(int doctorId, string doctorName, string speciality)
        {
            DoctorId = doctorId;
            DoctorName = doctorName;
            Speciality = speciality;
        }

        public override string ToString()
        {
            return $"Doctor [DoctorId={DoctorId}, DoctorName={DoctorName}, Speciality={Speciality}]";
        }
    }
}
