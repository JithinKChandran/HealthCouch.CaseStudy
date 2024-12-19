using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCouch.CaseStudy.Common.Commands;
using HealthCouch.CaseStudy.DataLayer.Entities;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class AppointmentViewModel : BaseViewModel
    {
        private int _id;
        private string _patientName;
        private DateTime _date;
        private string _timeSlot;
        private string _doctorName;
        private string _speciality;
        private string _symptoms;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
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

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string TimeSlot
        {
            get { return _timeSlot; }
            set
            {
                _timeSlot = value;
                OnPropertyChanged();
            }
        }


        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                OnPropertyChanged();
            }
        }

        public string Speciality
        {
            get { return _speciality; }
            set
            {
                _speciality = value;
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

        public ObservableCollection<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public AppointmentViewModel()
        {
            Appointments = new ObservableCollection<Appointment>();
            SaveCommand = new RelayCommand(SaveAppointment);
            SearchCommand = new RelayCommand(SearchAppointments);
        }

        private void SaveAppointment(object parameter)
        {
            var newAppointment = new Appointment(Id, PatientName, Date, TimeSlot, DoctorName, Speciality, Symptoms)
            {
                Id = this.Id,
                PatientName = this.PatientName,
                Date = this.Date,
                TimeSlot = this.TimeSlot,
                DoctorName = this.DoctorName,
                Speciality = this.Speciality,
                Symptoms = this.Symptoms
            };

            if (SelectedAppointment == null)
            {
                Appointments.Add(newAppointment);
            }
            else
            {
                int index = Appointments.IndexOf(SelectedAppointment);
                Appointments[index] = newAppointment;
            }

            ClearFields();
        }

        private void SearchAppointments(object parameter)
        {
            var query = Appointments.AsQueryable();
            if (!string.IsNullOrEmpty(PatientName))
            {
                query = query.Where(a => a.PatientName.Contains(PatientName));
            }
            if (!string.IsNullOrEmpty(DoctorName))
            {
                query = query.Where(a => a.DoctorName.Contains(DoctorName));
            }
            if (!string.IsNullOrEmpty(Speciality))
            {
                query = query.Where(a => a.Speciality.Contains(Speciality));
            }

            var results = query.ToList();
            Appointments.Clear();
            foreach (var appointment in results)
            {
                Appointments.Add(appointment);
            }
        }
        private void ClearFields()
        {
            Id = 0;
            PatientName = string.Empty;
            TimeSlot = string.Empty;
            DoctorName = string.Empty;
            Speciality = string.Empty;
            Symptoms = string.Empty;
            SelectedAppointment = null;
        }
    }
}
