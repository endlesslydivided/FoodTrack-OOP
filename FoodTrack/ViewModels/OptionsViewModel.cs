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
        #region Стили

        private void LightAccent1AppButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Accent1");
            Application.Current?.MainWindow?.Activate();
        }

        private void DarkAccent1AppButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Accent1");
            Application.Current?.MainWindow?.Activate();
        }

        private void LightAccent2AppButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Accent2");
            Application.Current?.MainWindow?.Activate();
        }

        private void DarkAccent2AppButtonClick(object sender, RoutedEventArgs e)

        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Accent2");
            Application.Current?.MainWindow?.Activate();
        }

        private void AccentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTheme = e.AddedItems.OfType<Theme>().FirstOrDefault();
            if (selectedTheme != null)
            {
                ThemeManager.Current.ChangeTheme(Application.Current, selectedTheme);
                Application.Current?.MainWindow?.Activate();
            }
        }

        private void ColorsSelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = e.AddedItems.OfType<KeyValuePair<string, Color>?>().FirstOrDefault();
            if (selectedColor != null)
            {
                var theme = ThemeManager.Current.DetectTheme(Application.Current);
                var inverseTheme = ThemeManager.Current.GetInverseTheme(theme);
                ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme(inverseTheme.BaseColorScheme, selectedColor.Value.Value));
                ThemeManager.Current.ChangeTheme(Application.Current, ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme(theme.BaseColorScheme, selectedColor.Value.Value)));
                Application.Current?.MainWindow?.Activate();
            }
        }
        #endregion
    }
}
