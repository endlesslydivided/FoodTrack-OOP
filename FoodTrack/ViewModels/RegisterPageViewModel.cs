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
using System.Text.RegularExpressions;

namespace FoodTrack.ViewModels
{
    public class RegisterPageViewModel :BaseViewModel
    {
        private string userLogin;
        private string message;
        private string userPassword;
        private decimal userWeight;
        private int userHeight;

        private UsersDatum usersDatum;


        #region Properties

        public decimal UserWeight
        {
            get { return userWeight; }
            set
            {
                userWeight = value;
                OnPropertyChanged("UserWeight");
            }
        }

        public int UserHeight
        {
            get { return userHeight; }
            set
            {
                userHeight = value;
                OnPropertyChanged("UserHeight");
            }
        }

        public string UserLogin
        {
            get { return userLogin; }
            set
            {
                userLogin = value;
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
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public DateTime DateToChoose
        {
            get { return usersDatum.Birthday; }
            set
            {
                usersDatum.Birthday = value;
                OnPropertyChanged("DateToChoose");
            }
        }
        #endregion 

        #region Commands

        #region Перейти на окно входа

        public ICommand UpdateViewCommand { get; set; }

        public RegisterPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            Message = "Придумайте логин(от 5 до 20 символов) и пароль(от 8 до 20 символов). Логин и пароль могут содержать латинские символы и цифры";
            usersDatum = new UsersDatum();
            usersDatum.Birthday = new DateTime();
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
                    registerCommand = new DelegateCommand(Register,CanRegister);
                }
                return registerCommand;
            }
        }

        private void Register()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<User> result = unit.UserRepository.Get(x => x.UserLogin == UserLogin);
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
                    unit.UserRepository.Create(RegUser);
                    unit.Save();

                   

                    Message = "Регистрация прошла успешно! Введите логин и пароль";
                    UpdateViewCommand = new UpdateViewCommand(new LogInViewModel());
                }
            }
        }

        private bool CanRegister()
        {
           if(UserLogin == "" || UserPassword == "" || UserWeight == 0 || UserHeight == 0)
           {
                return false;
           }
           else if (!Regex.IsMatch(UserLogin, "^([a-z]|[A-Z]|[0-9]){5,20}$") || !Regex.IsMatch(UserPassword, "^([a-z]|[A-Z]|[0-9]){8,20}$"))
           {
                return false;
           }
           else
           {
                return true;
           }
        }

        #endregion 


        #endregion

    }
}
