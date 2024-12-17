using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public Patient(int patientId, string patientName, DateTime dateOfBirth, string contactNumber)
        {
            PatientId = patientId;
            PatientName = patientName;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
        }
    }
}
