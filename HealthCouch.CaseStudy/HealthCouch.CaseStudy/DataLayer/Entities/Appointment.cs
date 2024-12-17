using System;
using System.ComponentModel.DataAnnotations;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [StringLength(100)]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; }

        [StringLength(500)]
        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; }

        public Appointment(int id, string patientName, DateTime startTime, DateTime endTime, string doctorName, string speciality, string symptoms)
        {
            Id = id;
            PatientName = patientName;
            StartTime = startTime;
            EndTime = endTime;
            DoctorName = doctorName;
            Speciality = speciality;
            Symptoms = symptoms;
        }

        public override string ToString()
        {
            return $"Appointment [Id={Id}, PatientName={PatientName}, DoctorName={DoctorName}, Speciality={Speciality}, StartTime={StartTime}, EndTime={EndTime}, Symptoms={Symptoms}]";
        }
    }
}
