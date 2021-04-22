using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    public class RegisterPageViewModel :BaseViewModel
    {

        public ICommand UpdateViewCommand { get; set; }

        public RegisterPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
