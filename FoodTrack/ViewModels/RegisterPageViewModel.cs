using FoodTrack.Context.UnitOfWork;
using FoodTrack.Commands;
using FoodTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FoodTrack.Hash;
using System.Windows;

namespace FoodTrack.ViewModels
{
    public class RegisterPageViewModel :BaseViewModel
    {
        private User User = new User();
        private string message = default;
        private string userPassword = default;
       


        #region Properties

        public string UserLogin
        {
            get { return User.UserLogin; }
            set
            {
                User.UserLogin = value;
                OnPropertyChanged("UserLogin");
            }
        }

        
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }
        public bool UserIsAdmin
        {
            get { return (bool)User.IsAdmin; }
            set
            {
                User.IsAdmin = value;
                OnPropertyChanged("UserIsAdmin");
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        #endregion 

        #region Commands

        #region Перейти на окно входа

        public ICommand UpdateViewCommand { get; set; }

        public RegisterPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            Message = "Придумайте логин и пароль. Логин и пароль могут содержать латинские символы и цифры";
        }

        #endregion

        #region Выполнить регистрацию

        private DelegateCommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new DelegateCommand(Register);
                }
                return registerCommand;
            }
        }

        private void Register()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                IEnumerable<User> result = uow.UserRepository.Get(x => x.UserLogin == User.UserLogin);
                if (result.Count() != 0)
                {
                    Message = "Пользователь с таким логином уже существует!";
                    return;
                }
                else
                {
                    User RegUser = new User();
                    RegUser.Salt = PasswordHash.GenerateSaltForPassword().ToString();
                    RegUser.UserPassword = PasswordHash.ComputePasswordHash(UserPassword, int.Parse(RegUser.Salt));
                    RegUser.UserLogin = UserLogin;
                    uow.UserRepository.Create(RegUser);
                    uow.Save();
                    Message = "Регистрация прошла успешно! Введите логин и пароль";                  
                }
            }
        }

        #endregion 


        #endregion

    }
}
