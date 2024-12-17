using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class AppointmentRepository
    {
        private readonly List<Appointment> _appointments = new List<Appointment>();
        public void Add(Appointment appointment)
        {
            _appointments.Add(appointment);
        }
        public List<Appointment> GetAll() 
        {
            return new List<Appointment>(_appointments);
        }
        public bool IsTimeSlotAvailable(DateTime startTime, DateTime endTime)
        {
            return !_appointments.Any(a => a.StartTime < endTime && a.EndTime > startTime);
        }
    }
}
