using System;

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

        public string SearchDoctorId { get; set; }
        public string SearchDoctorName { get; set; }
        public string SearchSpeciality { get; set; }

        public ICommand SearchCommand { get; private set; }

        public DoctorViewModel(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
            LoadDoctors();

            SearchCommand = new RelayCommand(OnSearchExecute);
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
                var filteredDoctors = _doctorRepository.SearchDoctors(SearchDoctorId, SearchDoctorName, SearchSpeciality);
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
                if (string.IsNullOrEmpty(SelectedDoctor?.DoctorName) || string.IsNullOrEmpty(SelectedDoctor?.Speciality))
                {
                    // Handle empty fields (e.g., display error message)
                    return;
                }

                var newDoctor = new Doctor
                {
                    DoctorName = SelectedDoctor.DoctorName,
                    Speciality = SelectedDoctor.Speciality
                };

                _doctorRepository.Add(newDoctor);
                Doctors.Add(newDoctor);
                SelectedDoctor = new Doctor(); // Clear the form fields
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                // For example:
                // MessageBox.Show("Error adding doctor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanEditDoctor(object parameter)
        {
            return SelectedDoctor != null && SelectedDoctor.DoctorId > 0;
        }

        private void OnEditDoctorExecute(object parameter)
        {
            try
            {
                if (SelectedDoctor != null && SelectedDoctor.DoctorId > 0)
                {
                    _doctorRepository.Update(SelectedDoctor);
                    // Refresh the Doctors collection to reflect changes
                    Doctors = new ObservableCollection<Doctor>(_doctorRepository.GetDoctors());
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                // For example:
                // MessageBox.Show("Error editing doctor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanDeleteDoctor(object parameter)
        {
            return SelectedDoctor != null && SelectedDoctor.DoctorId > 0;
        }

        private void OnDeleteDoctorExecute(object parameter)
        {
            try
            {
                if (SelectedDoctor != null && SelectedDoctor.DoctorId > 0)
                {
                    _doctorRepository.Remove(SelectedDoctor.DoctorId);
                    Doctors.Remove(SelectedDoctor);
                    SelectedDoctor = null; // Clear the selection
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display error message)
                // For example:
                // MessageBox.Show("Error deleting doctor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}