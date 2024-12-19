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
        private int _age;
        private string _gender;

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

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
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

        public ObservableCollection<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public AppointmentViewModel()
        {
            Appointments = new ObservableCollection<Appointment>();
        }
    }
}
