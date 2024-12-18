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
        private DateTime _dateOfBirth;
        private string _contactNumber;
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

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
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

        public ObservableCollection<Patient> Patients { get; set; }
        public Patient SelectedPatient { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>();
            SaveCommand = new RelayCommand(SavePatient);
            SearchCommand = new RelayCommand(SearchPatients);
            BackCommand = new RelayCommand(Back);
        }

        private void SavePatient(object parameter)
        {
            var newPatient = new Patient(PatientId,PatientName,DateOfBirth, ContactNumber, Gender, BloodGroup, Symptoms)
            {
                PatientId = (SelectedPatient != null) ? SelectedPatient.PatientId : Patients.Count + 1,
                PatientName = this.PatientName,
                DateOfBirth = this.DateOfBirth,
                ContactNumber = this.ContactNumber,
                Gender = this.Gender,
                BloodGroup = this.BloodGroup,
                Symptoms = this.Symptoms
            };

            if (SelectedPatient == null)
            {
                Patients.Add(newPatient);
            }
            else
            {
                int index = Patients.IndexOf(SelectedPatient);
                Patients[index] = newPatient;
            }

            ClearFields();
        }

        private void SearchPatients(object parameter)
        {
            var query = Patients.AsQueryable();

            if (!string.IsNullOrEmpty(PatientName))
            {
                    query = query.Where(p => p.PatientName.Contains(PatientName));
                }
            if (!string.IsNullOrEmpty(ContactNumber))
            {
                query = query.Where(p => p.ContactNumber.Contains(ContactNumber));
            }
            if (!string.IsNullOrEmpty(Gender))
            {
                query = query.Where(p => p.Gender.Contains(Gender));
            }
            if (!string.IsNullOrEmpty(BloodGroup))
            {
                query = query.Where(p => p.BloodGroup.Contains(BloodGroup));
            }
            if (!string.IsNullOrEmpty(Symptoms))
            {
                query = query.Where(p => p.Symptoms.Contains(Symptoms));
            }

            var results = query.ToList();
            Patients.Clear();
            foreach (var patient in results)
            {
                Patients.Add(patient);
            }
        }

        private void Back(object parameter)
        {
            // Implement back logic, typically navigation-related
        }

        private void ClearFields()
        {
            PatientName = string.Empty;
            DateOfBirth = DateTime.Now;
            ContactNumber = string.Empty;
            Gender = string.Empty;
            BloodGroup = string.Empty;
            Symptoms = string.Empty;
            SelectedPatient = null;
        }
    }
}
