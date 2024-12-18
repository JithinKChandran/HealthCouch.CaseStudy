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

        // Add a new appointment to the repository
        public void Add(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            _appointments.Add(appointment);
        }

        // Get all appointments from the repository
        public List<Appointment> GetAll()
        {
            return new List<Appointment>(_appointments);
        }

        // Update an existing appointment's information
        public void Update(Appointment updatedAppointment)
        {
            if (updatedAppointment == null)
                throw new ArgumentNullException(nameof(updatedAppointment));

            var existingAppointment = _appointments.FirstOrDefault(a => a.Id == updatedAppointment.Id);
            if (existingAppointment != null)
            {
                //

                existingAppointment.PatientName = updatedAppointment.PatientName;
                existingAppointment.TimeSlot = updatedAppointment.TimeSlot;
                existingAppointment.DoctorName = updatedAppointment.DoctorName;
                existingAppointment.Speciality = updatedAppointment.Speciality;
                existingAppointment.Symptoms = updatedAppointment.Symptoms;
            }
            else
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
        }

        // Remove an appointment from the repository
        public void Remove(int appointmentId)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
            }
            else
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
        }

        // Find an appointment by ID
        public Appointment FindById(int appointmentId)
        {
            return _appointments.FirstOrDefault(a => a.Id == appointmentId);
        }

        // Find appointments by patient name
        public List<Appointment> FindByPatientName(string patientName)
        {
            return _appointments.Where(a => a.PatientName.Equals(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
