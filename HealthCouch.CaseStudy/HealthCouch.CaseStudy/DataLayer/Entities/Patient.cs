using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string Symptoms { get; set; }
        public string DoctorSpeciality { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
    }
}
