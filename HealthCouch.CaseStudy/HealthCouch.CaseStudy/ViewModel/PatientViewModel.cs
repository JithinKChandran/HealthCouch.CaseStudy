using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCouch.CaseStudy.Common.Commands;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class PatientViewModel : BaseViewModel
    {
        private int _patientId;
        private string _patientName;
        private int _age;
        private string _contactNumber;
        private string _emergencyContactNumber;
        private string _gender;
        private string _bloodGroup;
        private string _symptoms;

        public int PatientId
        {
            get { return _patientId; }
            set
            {
                _patientId = value;
                OnPropertyChanged();
            }
        }
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string ContactNumber
        {
            get { return _contactNumber; }
            set
            {
                _contactNumber = value;
                OnPropertyChanged();
            }
        }

        public string EmergencyContactNumber
        {
            get { return _emergencyContactNumber; }
            set
            {
                _emergencyContactNumber = value;
                OnPropertyChanged();
            }
        }
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string BloodGroup
        {
            get { return _bloodGroup; }
            set
            {
                _bloodGroup = value;
                OnPropertyChanged();
            }
        }

        public string Symptoms
        {
            get { return _symptoms; }
            set
            {
                _symptoms = value;
                OnPropertyChanged();
            }
        }

    }
}
