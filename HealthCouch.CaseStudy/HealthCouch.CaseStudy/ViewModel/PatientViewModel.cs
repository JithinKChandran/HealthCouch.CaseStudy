using System;
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

        public PatientViewModel()
        {
            _patientRepository = new PatientRepository();
            _doctorRepository = new DoctorRepository();

            AllPatients = new ObservableCollection<Patient>();
            DoctorNames = new ObservableCollection<string>();
            DoctorSpecialities = new ObservableCollection<string>();

            AddCommand = new RelayCommand(OnAddPatientExecute);
            EditCommand = new RelayCommand(OnEditPatientExecute, CanEditPatient);
            DeleteCommand = new RelayCommand(OnDeletePatientExecute, CanDeletePatient);
            SearchCommand = new RelayCommand(OnSearchExecute);

            LoadAllPatients();
            LoadDoctorSpecialities();
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
        private int patientID;
        private string name;
        private string address;
        private int? age;
        private string gender;
        private string contactNumber;
        private string emergencyContact;
        private string bloodGroup;
        private string symptoms;
        private string doctorSpeciality;
        private string doctorName;
        private DateTime? appointmentDate;
        private string timeSlot;

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();

                if (_selectedPatient != null)
                {
                    PatientID = _selectedPatient.PatientID;
                    Name = _selectedPatient.Name;
                    Address = _selectedPatient.Address;
                    Age = _selectedPatient.Age;
                    Gender = _selectedPatient.Gender;
                    ContactNumber = _selectedPatient.ContactNumber;
                    EmergencyContact = _selectedPatient.EmergencyContact;
                    Symptoms = _selectedPatient.Symptoms;
                    DoctorSpeciality = _selectedPatient.DoctorSpeciality;
                    LoadDoctorNamesBySpeciality();
                    DoctorName = _selectedPatient.DoctorName;
                    AppointmentDate = _selectedPatient.AppointmentDate;
                    TimeSlot = _selectedPatient.TimeSlot;
                    BloodGroup = _selectedPatient.BloodGroup;
                }
            }
        }

        public int PatientID
        { get => patientID;
            set
            {
                patientID = value;
                OnPropertyChanged();
            }

        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Address 
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        public int? Age 
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged();
            }
        }
        public string Gender 
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }
        public string ContactNumber 
        {
            get => contactNumber;
            set
            {
                contactNumber = value;
                OnPropertyChanged();
            }
        }
        public string EmergencyContact 
        {
            get => emergencyContact;
            set
            {
                emergencyContact = value;
                OnPropertyChanged();
            }
        }
        public string BloodGroup 
        {
            get => bloodGroup;
            set
            {
                bloodGroup = value;
                OnPropertyChanged();
            }
        }
        public string Symptoms 
        { 
            get => symptoms;
            set
            {
                symptoms = value;
                OnPropertyChanged();
            }
        }
        public string DoctorSpeciality
        {
            get => doctorSpeciality;
            set
            {
                doctorSpeciality = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> DoctorNames { get; set; } = new ObservableCollection<string>();
        public string DoctorName
        {
            get => doctorName;
            set
            {
                doctorName = value;
                OnPropertyChanged();
            }
        }
        public DateTime? AppointmentDate 
        {
            get => appointmentDate;
            set
            {
                appointmentDate = value;
                OnPropertyChanged();
            }
        }
        public string TimeSlot 
        { 
            get => timeSlot;
            set
            {
                timeSlot = value;
                OnPropertyChanged();
            }
        }
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

        public void LoadDoctorSpecialities()
        {
            try
            {
                DoctorSpecialities.Clear();
                foreach (var speciality in _doctorRepository.GetAllDoctorSpecialities())
                {
                    DoctorSpecialities.Add(speciality);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor specialities: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadDoctorNamesBySpeciality()
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
            Age = null;
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

        private bool ValidateFields()
        {
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(DoctorSpeciality) &&
                   !string.IsNullOrEmpty(DoctorName) &&
                   !string.IsNullOrEmpty(TimeSlot) &&
                   AppointmentDate.HasValue;
        }

        private void OnAddPatientExecute(object parameter)
        {
            try
            {
                if (!ValidateFields())
                {
                    MessageBox.Show("Please fill in required fields: Name, Doctor Speciality, Doctor Name, Time Slot, and Appointment Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Patient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (!CanEditPatient(parameter) || !ValidateFields())
                {
                    MessageBox.Show("Please fill in required fields: Name, Doctor Speciality, Doctor Name, Time Slot, and Appointment Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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
                if (!CanDeletePatient(parameter))
                {
                    MessageBox.Show("Please select a valid patient to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _patientRepository.Delete(SelectedPatient.PatientID);
                LoadAllPatients();
                SelectedPatient = null;
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
