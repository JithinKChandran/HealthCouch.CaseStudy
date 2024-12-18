using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.Common.Validator
{
    public class AppointmentValidator
    {
        public List<string> Validate(Appointment appointment)
        {
            var errors = new List<string>();

            // Validate PatientName
            if (string.IsNullOrWhiteSpace(appointment.PatientName))
                errors.Add("Patient Name is required.");

            // Validate DoctorName
            if (string.IsNullOrWhiteSpace(appointment.DoctorName))
                errors.Add("Doctor Name is required.");

            // Validate StartTime and EndTime
            if (appointment.TimeSlot == default)
                errors.Add("Time is required.");

            // Additional validation rules can be added here

            return errors;
        }
    }
}
