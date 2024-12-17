using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.DataLayer.Repositories
{
    public class PatientRepository
    {
        private readonly List<Patient> _patients = new List<Patient>();

        // Add a new patient to the repository
        public void Add(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));
            _patients.Add(patient);
        }

        // Get all patients from the repository
        public List<Patient> GetAll()
        {
            return new List<Patient>(_patients);
        }

        // Update an existing patient's information
        public void Update(Patient updatedPatient)
        {
            if (updatedPatient == null)
                throw new ArgumentNullException(nameof(updatedPatient));

            var existingPatient = _patients.FirstOrDefault(p => p.PatientId == updatedPatient.PatientId);
            if (existingPatient != null)
            {
                existingPatient.PatientName = updatedPatient.PatientName;
                existingPatient.DateOfBirth = updatedPatient.DateOfBirth;
                existingPatient.ContactNumber = updatedPatient.ContactNumber;
                existingPatient.Gender = updatedPatient.Gender;
                existingPatient.BloodGroup = updatedPatient.BloodGroup;
                existingPatient.Symptoms = updatedPatient.Symptoms;
            }
            else
            {
                throw new KeyNotFoundException("Patient not found.");
            }
        }

        // Remove a patient from the repository
        public void Remove(int patientId)
        {
            var patient = _patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
            else
            {
                throw new KeyNotFoundException("Patient not found.");
            }
        }

        // Find a patient by ID
        public Patient FindById(int patientId)
        {
            return _patients.FirstOrDefault(p => p.PatientId == patientId);
        }

        // Find patients by name
        public List<Patient> FindByName(string patientName)
        {
            return _patients.Where(p => p.PatientName.Equals(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
