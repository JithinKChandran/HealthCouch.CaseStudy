using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCouch.CaseStudy.DataLayer.Entities;
using HealthCouch.CaseStudy.Common.Command;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class DoctorViewModel : BaseViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        public Doctor SelectedDoctor { get; set; }

        public string SearchDoctorId { get; set; }
        public string SearchDoctorName { get; set; }
        public string SearchSpeciality { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public DoctorViewModel()
        {
            Doctors = new ObservableCollection<Doctor>();
            SearchCommand = new RelayCommand(SearchDoctors);
            BackCommand = new RelayCommand(Back);
        }

        private void SearchDoctors(object parameter)
        {
            var query = Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(SearchDoctorId))
            {
                query = query.Where(d => d.DoctorId.ToString().Contains(SearchDoctorId));
            }
            if (!string.IsNullOrEmpty(SearchDoctorName))
            {
                query = query.Where(d => d.DoctorName.Contains(SearchDoctorName));
            }
            if (!string.IsNullOrEmpty(SearchSpeciality))
            {
                query = query.Where(d => d.DoctorName.Contains(SearchSpeciality));
            }

            var results = query.ToList();
            Doctors.Clear();
            foreach (var doctor in results)
            {
                Doctors.Add(doctor);
            }
        }

        private void Back(object parameter)
        {
            // Implement back logic, typically navigation-related
        }
    }
}
