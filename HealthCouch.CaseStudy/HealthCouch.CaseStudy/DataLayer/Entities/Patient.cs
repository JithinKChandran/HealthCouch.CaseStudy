using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Patient
    {
        [Key]
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [StringLength(6)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(3)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [StringLength(500)]
        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; }

        public Patient(int patientId, string patientName, int age, string contactNumber, string gender, string bloodGroup, string symptoms)
        {
            PatientId = patientId;
            PatientName = patientName;
            Age = age;
            ContactNumber = contactNumber;
            Gender = gender;
            BloodGroup = bloodGroup;
            Symptoms = symptoms;
        }
        public Patient()
        {
            
        }
        public override string ToString()
        {
            return $"Patient [PatientId={PatientId}, PatientName={PatientName}, DateOfBirth={Age}, ContactNumber={ContactNumber}, Gender={Gender}, BloodGroup={BloodGroup}, Symptoms={Symptoms}]";
        }
    }
}
