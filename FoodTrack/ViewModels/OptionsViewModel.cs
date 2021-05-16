using ControlzEx.Theming;
using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FoodTrack.ViewModels
{
    class OptionsViewModel : BaseViewModel
    {

        #region Properties


        public string ExampleP
        {
            get { return ExampleP; }
            set
            {
                ExampleP = value;
                OnPropertyChanged("ExampleP");
            }
        }

        #endregion

        #region Commands

        #region Изменение темы

        private DelegateCommand<object> changeAppThemeCommand;

        public ICommand ChangeAppThemeCommand
        {
            get
            {
                if (changeAppThemeCommand == null)
                {
                    changeAppThemeCommand = new DelegateCommand<object>(changeAppTheme);
                }
                return changeAppThemeCommand;
            }
        }

        private void changeAppTheme(object sender)
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, sender.ToString());
            Application.Current?.MainWindow?.Activate();
        }
        #endregion
        #region Изменение акцента

        private DelegateCommand<object> changeAppAccentСommand;

        public ICommand ChangeAppAccentСommand
        {
            get
            {
                if (changeAppAccentСommand == null)
                {
                    changeAppAccentСommand = new DelegateCommand<object>(changeAppAccent);
                }
                return changeAppAccentСommand;
            }
        }

        private void changeAppAccent(object sender)
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, sender.ToString());
            Application.Current?.MainWindow?.Activate();
        }
        #endregion

        #endregion
    }
}
