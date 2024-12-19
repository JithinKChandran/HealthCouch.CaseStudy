using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<Patient> AllPatients { get; set; }
        public Patient SelectedPatient { get; set; }

        // Patient Properties
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string Symptoms { get; set; }
        public string DoctorSpeciality { get; set; }
        public List<string> DoctorNames { get; set; } = new List<string>();
        public string DoctorName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string TimeSlot { get; set; }

        // Search Criteria
        public string SearchPatientId { get; set; }
        public string SearchDoctor { get; set; }
        public string SearchGender { get; set; }
        public string SearchBloodGroup { get; set; }

        // Commands
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

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
                var newPatient = new Patient
                {
                    Name = Name,
                    Address = Address,
                    Age = Age,
                    Gender = Gender,
                    ContactNumber = ContactNumber,
                    EmergencyContact = EmergencyContact,
                    Symptoms = Symptoms,
                    DoctorSpeciality = DoctorSpeciality,
                    DoctorName = DoctorName,
                    AppointmentDate = AppointmentDate,
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
            return SelectedPatient != null;
        }

        private void OnEditPatientExecute(object parameter)
        {
            try
            {
                if (SelectedPatient != null)
                {
                    SelectedPatient.Name = Name;
                    SelectedPatient.Address = Address;
                    SelectedPatient.Age = Age;
                    SelectedPatient.Gender = Gender;
                    SelectedPatient.ContactNumber = ContactNumber;
                    SelectedPatient.EmergencyContact = EmergencyContact;
                    SelectedPatient.Symptoms = Symptoms;
                    SelectedPatient.DoctorSpeciality = DoctorSpeciality;
                    SelectedPatient.DoctorName = DoctorName;
                    SelectedPatient.AppointmentDate = AppointmentDate;
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
            return SelectedPatient != null;
        }

        private void OnDeletePatientExecute(object parameter)
        {
            try
            {
                if (SelectedPatient != null)
                {
                    _patientRepository.Delete(SelectedPatient.PatientID);
                    LoadAllPatients();
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
            DoctorSpeciality = string.Empty;
            DoctorNames.Clear();
            DoctorName = string.Empty;
            AppointmentDate = null;
            TimeSlot = string.Empty;
        }
    }
}