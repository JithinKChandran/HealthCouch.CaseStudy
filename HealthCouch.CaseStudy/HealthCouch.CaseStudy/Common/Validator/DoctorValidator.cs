using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.Common.Validator
{
    public class DoctorValidator
    {
        public List<string> Validate(Doctor doctor)
        {
            var errors = new List<string>();

            // Validate DoctorName
            if (string.IsNullOrWhiteSpace(doctor.DoctorName))
                errors.Add("Doctor Name is required.");

            // Validate Speciality
            if (string.IsNullOrWhiteSpace(doctor.Speciality))
                errors.Add("Speciality is required.");

            // Additional validation rules can be added here

            return errors;
        }
    }
}
