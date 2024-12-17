using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Appointment(int id, string patientName, DateTime startTime, DateTime endTime)
        {
            Id = id;
            PatientName = patientName;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
