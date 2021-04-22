using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    public class LogInViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new LogInPageViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public LogInViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }

    }
}
