using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HealthCouch.CaseStudy.Common.Commands;
using HealthCouch.CaseStudy.DataLayer.Entities;
using HealthCouch.CaseStudy.DataLayer.Repositories;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class PatientViewModel : BaseViewModel
    {
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;

        // Parameterless constructor for XAML binding
        public PatientViewModel()
        {
            // Initialize empty collections to avoid null reference issues
            AllPatients = new ObservableCollection<Patient>();
            DoctorNames = new ObservableCollection<string>();
            DoctorSpecialities = new ObservableCollection<string>();

            // Initialize commands
            AddCommand = new RelayCommand(OnAddPatientExecute);
            EditCommand = new RelayCommand(OnEditPatientExecute, CanEditPatient);
            DeleteCommand = new RelayCommand(OnDeletePatientExecute, CanDeletePatient);
            SearchCommand = new RelayCommand(OnSearchExecute);
        }

        public PatientViewModel(PatientRepository patientRepository, DoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            LoadAllPatients();
            LoadDoctorSpecialities();

            AddCommand = new RelayCommand(OnAddPatientExecute);
            EditCommand = new RelayCommand(OnEditPatientExecute, CanEditPatient);
            DeleteCommand = new RelayCommand(OnDeletePatientExecute, CanDeletePatient);
            SearchCommand = new RelayCommand(OnSearchExecute);
        }

        private ObservableCollection<Patient> _allPatients;
        public ObservableCollection<Patient> AllPatients
        {
            get { return _allPatients; }
            set
            {
                _allPatients = value;
                OnPropertyChanged();
            }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();

                if (_selectedPatient != null)
                {
                    // Update ViewModel properties when a patient is selected
                    PatientID = _selectedPatient.PatientID;
                    Name = _selectedPatient.Name;
                    Address = _selectedPatient.Address;
                    Age = _selectedPatient.Age;
                    Gender = _selectedPatient.Gender;
                    ContactNumber = _selectedPatient.ContactNumber;
                    EmergencyContact = _selectedPatient.EmergencyContact;
                    Symptoms = _selectedPatient.Symptoms;
                    DoctorSpeciality = _selectedPatient.DoctorSpeciality;
                    LoadDoctorNamesBySpeciality(); // Load doctor names based on selected speciality
                    DoctorName = _selectedPatient.DoctorName;
                    AppointmentDate = _selectedPatient.AppointmentDate;
                    TimeSlot = _selectedPatient.TimeSlot;
                    BloodGroup = _selectedPatient.BloodGroup;
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
        public ObservableCollection<string> DoctorNames { get; set; } = new ObservableCollection<string>();
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
        public ObservableCollection<string> DoctorSpecialities { get; private set; } = new ObservableCollection<string>();

        private void LoadAllPatients()
        {
            try
            {
                AllPatients = new ObservableCollection<Patient>(_patientRepository.GetAllPatients());
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
                DoctorSpecialities = new ObservableCollection<string>(_doctorRepository.GetAllDoctorSpecialities());
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
                if (!string.IsNullOrEmpty(DoctorSpeciality))
                {
                    DoctorNames.Clear();
                    foreach (var doctorName in _doctorRepository.GetDoctorNamesBySpeciality(DoctorSpeciality))
                    {
                        DoctorNames.Add(doctorName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor names: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            BloodGroup = string.Empty;
            Symptoms = string.Empty;
            DoctorSpeciality = string.Empty;
            DoctorName = string.Empty;
            AppointmentDate = null;
            TimeSlot = string.Empty;
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
                    BloodGroup = BloodGroup,
                    Symptoms = Symptoms,
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
    }
}
