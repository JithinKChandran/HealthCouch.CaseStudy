using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCouch.CaseStudy.DataLayer.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public Patient(int id, string name, DateTime dateOfBirth, string contactNumber)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
        }
    }
}
