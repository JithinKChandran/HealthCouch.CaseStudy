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
        public void Add(Patient patient)
        {
            _patients.Add(patient);
        }
        public List<Patient> GetAll()
        {
            return new List<Patient>(_patients);
        }
    }
}
