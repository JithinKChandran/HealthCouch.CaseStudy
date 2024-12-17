using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class DoctorRepository
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();

        // Add a new doctor to the repository
        public void Add(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            _doctors.Add(doctor);
        }

        // Get all doctors from the repository
        public List<Doctor> GetDoctors()
        {
            return new List<Doctor>(_doctors);
        }

        // Update an existing doctor's information
        public void Update(Doctor updatedDoctor)
        {
            if (updatedDoctor == null)
                throw new ArgumentNullException(nameof(updatedDoctor));

            var existingDoctor = _doctors.FirstOrDefault(d => d.DoctorId == updatedDoctor.DoctorId);
            if (existingDoctor != null)
            {
                existingDoctor.DoctorName = updatedDoctor.DoctorName;
                existingDoctor.Speciality = updatedDoctor.Speciality;
            }
            else
            {
                throw new KeyNotFoundException("Doctor not found.");
            }
        }

        // Remove a doctor from the repository
        public void Remove(int doctorId)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
            else
            {
                throw new KeyNotFoundException("Doctor not found.");
            }
        }

        // Find a doctor by ID
        public Doctor FindById(int doctorId)
        {
            return _doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }

        // Find doctors by speciality
        public List<Doctor> FindBySpeciality(string speciality)
        {
            return _doctors.Where(d => d.Speciality.Equals(speciality, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
