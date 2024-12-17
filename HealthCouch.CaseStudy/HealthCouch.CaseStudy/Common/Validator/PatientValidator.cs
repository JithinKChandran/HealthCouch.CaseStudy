using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.Common.Validator
{
    public class PatientValidator
    {
        public List<string> Validate(Patient patient)
        {
            var errors = new List<string>();

            // Validate PatientName
            if (string.IsNullOrWhiteSpace(patient.PatientName))
                errors.Add("Patient Name is required.");

            // Validate DateOfBirth
            if (patient.DateOfBirth == default)
                errors.Add("Date of Birth is required.");

            // Validate ContactNumber
            if (string.IsNullOrWhiteSpace(patient.ContactNumber))
                errors.Add("Contact Number is required.");
            else if (patient.ContactNumber.Length > 15)
                errors.Add("Contact Number cannot exceed 15 characters.");

            // Validate Gender
            if (string.IsNullOrWhiteSpace(patient.Gender))
                errors.Add("Gender is required.");

            // Validate BloodGroup
            if (string.IsNullOrWhiteSpace(patient.BloodGroup))
                errors.Add("Blood Group is required.");

            // Additional validation rules can be added here

            return errors;
        }
    }
}
