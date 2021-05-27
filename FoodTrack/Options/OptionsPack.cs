using ControlzEx.Theming;
using FoodTrack.DeserializedUserNamespace;
using FoodTrack.Models;
using FoodTrack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodTrack.Options
{
    public class OptionsPack
    {
        public OptionsPack()
        {
            IsSplashScreenShown = true;
            IsStayAuthorized = true;

            CurrentAppAccent = "ForestGreen";
            CurrentAppTheme = "Light";

            OptionUserId = DeserializedUser.deserializedUser.Id;
        }

        public int OptionUserId { get; set; }

        public bool IsSplashScreenShown { get; set; }
        public bool IsStayAuthorized { get; set; }

        public string CurrentAppTheme { get; set; }
        public string CurrentAppAccent { get; set; }

        #region Открытые методы

        public  void setAppTheme()
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, this.CurrentAppTheme);
            Application.Current?.MainWindow?.Activate();
        }

        public void setAppAccent()
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, this.CurrentAppAccent);
            Application.Current?.MainWindow?.Activate();
        }


        #endregion
    }
}
