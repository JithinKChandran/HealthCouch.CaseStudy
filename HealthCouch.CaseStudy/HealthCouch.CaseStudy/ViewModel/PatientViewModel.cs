using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HealthCouch.CaseStudy.Common.Commands;
using HealthCouch.CaseStudy.DataLayer.Entities;
using HealthCouch.CaseStudy.DataLayer.Repositories;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class PatientViewModel : BaseViewModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;

        private ObservableCollection<Patient> _allPatients;
        public ObservableCollection<Patient> AllPatients { get; set; } = new ObservableCollection<Patient>();

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();

                if (SelectedPatient != null)
                {
                    // Update ViewModel properties when a patient is selected
                    PatientID = SelectedPatient.PatientID;
                    Name = SelectedPatient.Name;
                    Address = SelectedPatient.Address;
                    Age = SelectedPatient.Age;
                    Gender = SelectedPatient.Gender;
                    ContactNumber = SelectedPatient.ContactNumber;
                    EmergencyContact = SelectedPatient.EmergencyContact;
                    Symptoms = SelectedPatient.Symptoms;
                    DoctorSpeciality = SelectedPatient.DoctorSpeciality;
                    LoadDoctorNamesBySpeciality(); // Load doctor names based on selected speciality
                    DoctorName = SelectedPatient.DoctorName;
                    AppointmentDate = SelectedPatient.AppointmentDate;
                    TimeSlot = SelectedPatient.TimeSlot;
                }
            }
        }

        // Patient Properties
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string BloodGroup { get; set; }
        public string Symptoms { get; set; }
        public string DoctorSpeciality { get; set; }
        public List<string> DoctorNames { get; set; } = new List<string>();
        public string DoctorName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string SearchPatientId { get; set; }
        public string SearchDoctor { get; set; }
        public string SearchGender { get; set; }
        public string SearchBloodGroup { get; set; }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public List<string> DoctorSpecialities { get; private set; }

        public PatientViewModel()
        {
            
        }
        public PatientViewModel(PatientRepository patientRepository, DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            LoadAllPatients();
            LoadDoctorSpecialities();

            AddCommand = new RelayCommand(OnAddPatientExecute);
            EditCommand = new RelayCommand(OnEditPatientExecute, CanEditPatient);
            DeleteCommand = new RelayCommand(OnDeletePatientExecute, CanDeletePatient);
            SearchCommand = new RelayCommand(OnSearchExecute);
        }

        private void LoadAllPatients()
        {
            try
            {
                foreach (var item in _patientRepository.GetAllPatients())
                {
                    AllPatients.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDoctorSpecialities()
        {
            try
            {
                // Corrected property access
                DoctorSpecialities = _doctorRepository.GetAllDoctorSpecialities();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor specialities: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDoctorNamesBySpeciality()
        {
            try
            {
                DoctorNames = string.IsNullOrEmpty(DoctorSpeciality)
                    ? new List<string>()
                    : _doctorRepository.GetDoctorNamesBySpeciality(DoctorSpeciality);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor names: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnAddPatientExecute(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(DoctorSpeciality) || string.IsNullOrEmpty(DoctorName))
                {
                    MessageBox.Show("Please fill in required fields: Name, Doctor Speciality, and Doctor Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newPatient = new Patient
                {
                    Name = Name,
                    Address = Address,
                    Age = Age,
                    Gender = Gender,
                    ContactNumber = ContactNumber,
                    EmergencyContact = EmergencyContact,
                    Symptoms = Symptoms,
                    BloodGroup = BloodGroup,
                    DoctorSpeciality = DoctorSpeciality,
                    DoctorName = DoctorName,
                    AppointmentDate = (DateTime)AppointmentDate,
                    TimeSlot = TimeSlot
                };

                _patientRepository.Add(newPatient);
                LoadAllPatients();
                ClearFormFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding patient: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanEditPatient(object parameter)
        {
            return SelectedPatient != null && SelectedPatient.PatientID > 0;
        }

        private void OnEditPatientExecute(object parameter)
        {
            try
            {
                if (SelectedPatient != null && SelectedPatient.PatientID > 0)
                {
                    SelectedPatient.Name = Name;
                    SelectedPatient.Address = Address;
                    SelectedPatient.Age = Age;
                    SelectedPatient.Gender = Gender;
                    SelectedPatient.ContactNumber = ContactNumber;
                    SelectedPatient.EmergencyContact = EmergencyContact;
                    SelectedPatient.Symptoms = Symptoms;
                    SelectedPatient.BloodGroup = BloodGroup;
                    SelectedPatient.DoctorSpeciality = DoctorSpeciality;
                    SelectedPatient.DoctorName = DoctorName;
                    SelectedPatient.AppointmentDate = (DateTime)AppointmentDate;
                    SelectedPatient.TimeSlot = TimeSlot;

                    _patientRepository.Update(SelectedPatient);
                    LoadAllPatients();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing patient: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanDeletePatient(object parameter)
        {
            return SelectedPatient != null && SelectedPatient.PatientID > 0;
        }

        private void OnDeletePatientExecute(object parameter)
        {
            try
            {
                if (SelectedPatient != null && SelectedPatient.PatientID > 0)
                {
                    _patientRepository.Delete(SelectedPatient.PatientID);
                    LoadAllPatients();
                    SelectedPatient = null; // Clear the selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting patient: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSearchExecute(object parameter)
        {
            try
            {
                var filteredPatients = _patientRepository.GetAllPatients();

                if (!string.IsNullOrEmpty(SearchPatientId))
                {
                    filteredPatients = filteredPatients.Where(p => p.PatientID.ToString().Contains(SearchPatientId)).ToList();
                }

                if (!string.IsNullOrEmpty(SearchDoctor))
                {
                    filteredPatients = filteredPatients.Where(p => p.DoctorName.Contains(SearchDoctor)).ToList();
                }

                if (!string.IsNullOrEmpty(SearchGender))
                {
                    filteredPatients = filteredPatients.Where(p => p.Gender == SearchGender).ToList();
                }

                if (!string.IsNullOrEmpty(SearchBloodGroup))
                {
                    filteredPatients = filteredPatients.Where(p => p.BloodGroup == SearchBloodGroup).ToList();
                }

                AllPatients = new ObservableCollection<Patient>(filteredPatients);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching patients: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFormFields()
        {
            Name = string.Empty;
            Address = string.Empty;
            Age = 0;
            Gender = string.Empty;
            ContactNumber = string.Empty;
            EmergencyContact = string.Empty;
            Symptoms = string.Empty;
            BloodGroup = string.Empty;
            DoctorSpeciality = string.Empty;
            DoctorNames.Clear();
            DoctorName = string.Empty;
            AppointmentDate = null;
            TimeSlot = string.Empty;
        }
    }
}