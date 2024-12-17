using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

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

        public Patient(int patientId, string patientName, DateTime dateOfBirth, string contactNumber, string gender, string bloodGroup, string symptoms)
        {
            PatientId = patientId;
            PatientName = patientName;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
            Gender = gender;
            BloodGroup = bloodGroup;
            Symptoms = symptoms;
        }

        public override string ToString()
        {
            return $"Patient [PatientId={PatientId}, PatientName={PatientName}, DateOfBirth={DateOfBirth}, ContactNumber={ContactNumber}, Gender={Gender}, BloodGroup={BloodGroup}, Symptoms={Symptoms}]";
        }
    }
}
