using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCouch.CaseStudy.DataLayer.Entities;
using HealthCouch.CaseStudy.Common.Commands;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class DoctorViewModel : BaseViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        public Doctor SelectedDoctor { get; set; }

        public string SearchDoctorId { get; set; }
        public string SearchDoctorName { get; set; }
        public string SearchSpeciality { get; set; }

    }
}
