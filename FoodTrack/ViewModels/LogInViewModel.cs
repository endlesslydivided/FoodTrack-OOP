using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    public class LogInViewModel : BaseViewModel
    {
        private Page CurrentPage;
        public LogInViewModel()
        {
             
        }

        #region Commands

        #region Перейти на окно регистрации

        private DelegateCommand goToRegistrationCommand;

        public ICommand GoToRegistrationCommand
        {
            get
            {
                if (goToRegistrationCommand == null)
                {
                    goToRegistrationCommand = new DelegateCommand(GoToRegistration);
                }
                return goToRegistrationCommand;
            }
        }

        private void GoToRegistration()
        {
            CurrentPage.Content = new Uri("Views/Pages/RegisterPage.xaml", UriKind.RelativeOrAbsolute);
        }

        #endregion
        #endregion
    }
}
