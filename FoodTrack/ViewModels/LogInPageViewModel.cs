using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        #region Commands

        #region Перейти на окно регистрации

        public ICommand UpdateViewCommand { get; set; }

        public LogInPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }

        private DelegateCommand goToRegistrationCommand;

        //public ICommand GoToRegistrationCommand
        //{
        //    get
        //    {
        //        if (goToRegistrationCommand == null)
        //        {
        //            goToRegistrationCommand = new DelegateCommand(GoToRegistration);
        //        }
        //        return goToRegistrationCommand;
        //    }
        //}

        //private void GoToRegistration()
        //{
        //    this.LogInRegister.Content = new Uri("Views/Pages/RegisterPage.xaml", UriKind.RelativeOrAbsolute);
        //}


        #endregion
        #endregion

    }
}
