using HealthCouch.CaseStudy.ViewModel;
using System.Windows.Controls;

namespace HealthCouch.CaseStudy.Pages
{
    public partial class ManagePatientsPage : Page
    {
        public ManagePatientsPage()
        {
            InitializeComponent();
        }

        private void OnSpecialitySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PatientViewModel viewModel)
            {
                viewModel.LoadDoctorNamesBySpeciality();
            }
        }
    }
}
