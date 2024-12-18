using System.Windows.Input;
using HealthCouch.CaseStudy.Common.Commands;

namespace HealthCouch.CaseStudy.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowManagePatientsCommand { get; set; }
        public ICommand ShowDoctorPageCommand { get; set; }

        public MainWindowViewModel()
        {
            // Default ViewModel
            CurrentViewModel = new PatientViewModel();

            ShowManagePatientsCommand = new RelayCommand(ShowManagePatients);
            ShowDoctorPageCommand = new RelayCommand(ShowDoctorPage);
        }

        private void ShowManagePatients(object parameter)
        {
            CurrentViewModel = new PatientViewModel();
        }

        private void ShowDoctorPage(object parameter)
        {
            CurrentViewModel = new DoctorViewModel();
        }
    }
}
