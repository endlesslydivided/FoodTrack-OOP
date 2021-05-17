using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using FoodTrack.Options;
using FoodTrack.ViewModels;
using FoodTrack.Views;
using FoodTrack.Views.Windows;
using FoodTrack.XMLSerializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FoodTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            User deserializedeUser = XmlSerializeWrapper<User>.Deserialize("../lastUser.xml", FileMode.Open);
            List<OptionsPack> optionsPacks = XmlSerializeWrapper<List<OptionsPack>>.Deserialize("../appSettings.xml", FileMode.Open);
            OptionsPack currentUserSettings = optionsPacks.Find(x => x.OptionUserId == deserializedeUser.Id);
            if (currentUserSettings?.IsSplashScreenShown ?? true)
            {
                SplashScreen splash = new SplashScreen("../Resources/foodTrackSplash.png");
                splash.Show(autoClose: true, topMost: true);
                splash.Close(TimeSpan.FromSeconds(1));
            }
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<User> resultUserFound = unit.UserRepository.Get(x => x.UserLogin == deserializedeUser.UserLogin);

                if (resultUserFound.Count() != 0 && (currentUserSettings?.IsStayAuthorized ?? false))
                {
                    if (resultUserFound.First<User>().UserPassword.SequenceEqual<byte>(deserializedeUser.UserPassword))
                    {
                        MainWindow mainWindow = new MainWindow();

                        mainWindow.Show();

                        currentUserSettings.setAppTheme();
                        currentUserSettings.setAppAccent();

                        OptionsViewModel.OptionsPack = currentUserSettings;
                    }
                    else
                    {
                        LogInWindow logInWindow = new LogInWindow();
                        logInWindow.Show();
                    }
                }               
                else
                {
                    LogInWindow logInWindow = new LogInWindow();
                    logInWindow.Show();                  
                }
            }
           
        }
    }
}
