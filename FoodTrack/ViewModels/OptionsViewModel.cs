using ControlzEx.Theming;
using FoodTrack.Commands;
using FoodTrack.Options;
using FoodTrack.XMLSerializer;
using System;
using System.Collections.Generic;
using System.IO;
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


        public OptionsViewModel()
        {
            if (OptionsPack == null)
            {
                OptionsPack = new();
            }
        }

        #region Properties

        public static OptionsPack OptionsPack { get; set; }

        public bool IsShowSplash
        {
            get { return OptionsPack.IsSplashScreenShown; }
            set
            {
                OptionsPack.IsSplashScreenShown = value;
                OnPropertyChanged("IsShowSplash");
            }
        }
        public bool IsStayAuthorized
        {
            get { return OptionsPack.IsStayAuthorized; }
            set
            {
                OptionsPack.IsStayAuthorized = value;
                OnPropertyChanged("IsStayAuthorized");
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
            OptionsPack.CurrentAppTheme = sender.ToString();
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
            OptionsPack.CurrentAppAccent = sender.ToString();
        }
        #endregion
        #region Сохранение настроек

        private DelegateCommand saveApplicationSettingsСommand;

        public ICommand SaveApplicationSettingsСommand
        {
            get
            {
                if (saveApplicationSettingsСommand == null)
                {
                    saveApplicationSettingsСommand = new DelegateCommand(saveApplicationSettings);
                }
                return saveApplicationSettingsСommand;
            }
        }

        private void saveApplicationSettings()
        {
            List<OptionsPack> optionsPacks = XmlSerializeWrapper<List<OptionsPack>>.Deserialize( "../appSettings.xml", FileMode.OpenOrCreate);
            optionsPacks.Remove(optionsPacks.Find(x => x.OptionUserId == deserializedUser.Id));
            optionsPacks.Add(OptionsPack);
            XmlSerializeWrapper<List<OptionsPack>>.Serialize(optionsPacks, "../appSettings.xml");
        }
        #endregion
        #region Установка настроек по умолчанию

        private DelegateCommand setDefaultSettingsСommand;

        public ICommand SetDefaultSettingsСommand
        {
            get
            {
                if (setDefaultSettingsСommand == null)
                {
                    setDefaultSettingsСommand = new DelegateCommand(setDefaultSettings);
                }
                return setDefaultSettingsСommand;
            }
        }

        private void setDefaultSettings()
        {
            OptionsPack.IsSplashScreenShown = true;
            OptionsPack.IsStayAuthorized = true;
            OptionsPack.CurrentAppTheme = "Light";
            OptionsPack.CurrentAppAccent = "ForestGreen";

            List<OptionsPack> optionsPacks = XmlSerializeWrapper<List<OptionsPack>>.Deserialize("../appSettings.xml", FileMode.OpenOrCreate);
            optionsPacks.Remove(optionsPacks.Find(x => x.OptionUserId == deserializedUser.Id));
            optionsPacks.Add(OptionsPack);
            XmlSerializeWrapper<List<OptionsPack>>.Serialize(optionsPacks, "../appSettings.xml");

            OptionsPack?.setAppAccent();

            OptionsPack?.setAppTheme();


        }
        #endregion
        #endregion
    }
    
}

