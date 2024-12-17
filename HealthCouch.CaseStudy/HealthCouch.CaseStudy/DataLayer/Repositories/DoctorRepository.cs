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
        public void Add(Doctor doctor)
        {
            _doctors.Add(doctor);
        }
        public List<Doctor> GetDoctors()
        {
            return new List<Doctor>(_doctors);
        }
    }
}
