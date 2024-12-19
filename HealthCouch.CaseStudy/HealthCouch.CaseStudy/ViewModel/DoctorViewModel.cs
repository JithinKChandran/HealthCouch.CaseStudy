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

        //private Doctor _selectedDoctor;
        //public Doctor SelectedDoctor
        //{
        //    get { return _selectedDoctor; }
        //    set
        //    {
        //        _selectedDoctor = value;
        //        OnPropertyChanged();
        //    }
        //}

        public string SearchDoctorId { get; set; }
        public string SearchDoctorName { get; set; }
        public string SearchSpeciality { get; set; }

        public RelayCommand SearchCommand { get; private set; }

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
    }
}