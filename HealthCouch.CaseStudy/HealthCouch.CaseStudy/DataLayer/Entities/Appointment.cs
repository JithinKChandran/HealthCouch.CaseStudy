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
        [Display(Name = "Appointment")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "TimeSlot")]
        public string TimeSlot { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public Appointment()
        {
            
        }
        public Appointment(int id, string patientName, DateTime date, string timeSlot, string doctorName, int age, string gender)
        {
            Id = id;
            PatientName = patientName;
            Date = date;
            TimeSlot = timeSlot;
            DoctorName = doctorName;
            Age = age;
            Gender = gender;
        }

    }
}
