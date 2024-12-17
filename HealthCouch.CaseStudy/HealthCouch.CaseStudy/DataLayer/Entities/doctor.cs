using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public Doctor(int doctorId, string doctorName, string speciality)
        {
            DoctorId = doctorId;
            DoctorName = doctorName;
            Speciality = speciality;
        }
    }
}
