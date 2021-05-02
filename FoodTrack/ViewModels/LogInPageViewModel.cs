using FoodTrack.Context.UnitOfWork;
using FoodTrack.Commands;
using FoodTrack.Models;
using FoodTrack.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FoodTrack.Hash;
using FoodTrack.XMLSerializer;

namespace FoodTrack.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        public User User = new User();
        public string userPassword;
        public string message;

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

        #region Перейти на окно регистрации

        public ICommand UpdateViewCommand { get; set; }

        public LogInPageViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
            Message = "Введите свой логин и пароль";
        }

        #endregion

        #region Выполнить вход

        private DelegateCommand logInCommand;

        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                {
                    logInCommand = new DelegateCommand(LogIn);
                }
                return logInCommand;
            }
        }

        private void LogIn()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                IEnumerable<User> result = uow.UserRepository.Get(x => x.UserLogin == User.UserLogin);

                if(result.Count() == 0)
                {
                    Message = "Логин или пароль неверный!";
                    return;
                }
                else if(PasswordHash.IsPasswordValid(UserPassword, int.Parse(result.First<User>().Salt), result.First<User>().UserPassword))
                {
                    XmlSerializeWrapper.Serialize(result.First<User>(),"../lastUser.xml");                 
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LogInWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    Message = "Логин или пароль неверный!";
                    return;
                }             
            }
        }

        #endregion 


        #endregion

    }
}
