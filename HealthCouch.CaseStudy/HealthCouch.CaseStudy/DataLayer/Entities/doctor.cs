using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public Doctor(int id, string name, string speciality)
        {
            Id = id;
            Name = name;
            Speciality = speciality;
        }
    }
}
