using System;
using System.Collections.ObjectModel;
using System.Windows;
using HealthCouch.CaseStudy.Common.Commands;
using HealthCouch.CaseStudy.DataLayer.Entities;
using HealthCouch.CaseStudy.DataLayer.Repositories;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class DoctorViewModel : BaseViewModel
    {
        private readonly DoctorRepository _doctorRepository;

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return _doctors; }
            set
            {
                _doctors = value;
                OnPropertyChanged();
            }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        public string SearchDoctorName { get; set; }
        public string SearchSpeciality { get; set; }

        private string _doctorName;
        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                OnPropertyChanged();
            }
        }

        private string _speciality;
        public string Speciality
        {
            get { return _speciality; }
            set
            {
                _speciality = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand AddDoctorCommand { get; private set; }

        public DoctorViewModel()
        {
            _doctorRepository = new DoctorRepository();
            LoadDoctors();

            SearchCommand = new RelayCommand(OnSearchExecute);
            AddDoctorCommand = new RelayCommand(OnAddDoctorExecute);

            DoctorName = string.Empty;
            Speciality = string.Empty;
        }

        private void LoadDoctors()
        {
            try
            {
                Doctors = new ObservableCollection<Doctor>(_doctorRepository.GetDoctors());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSearchExecute(object parameter)
        {
            try
            {
                var filteredDoctors = _doctorRepository.SearchDoctors(SearchDoctorName, SearchSpeciality);
                Doctors = new ObservableCollection<Doctor>(filteredDoctors);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching doctors: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnAddDoctorExecute(object parameter)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(DoctorName) || string.IsNullOrEmpty(Speciality))
                {
                    MessageBox.Show("Please enter Doctor Name and Speciality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create a new Doctor object
                var newDoctor = new Doctor
                {
                    DoctorName = DoctorName,
                    Speciality = Speciality
                };

                // Add the new doctor to the database
                _doctorRepository.AddDoctor(newDoctor);

                // Refresh the Doctors collection
                LoadDoctors();

                // Clear the input fields
                DoctorName = string.Empty;
                Speciality = string.Empty;

                MessageBox.Show("Doctor added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding doctor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
