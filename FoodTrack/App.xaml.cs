using FakeAtlas.Context.UnitOfWork;
using FoodTrack.Models;
using FoodTrack.Views;
using FoodTrack.Views.Windows;
using FoodTrack.XMLSerializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            User deserializedeUser = XmlSerializeWrapper.Deserialize("../lastUser.xml");
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<User> resultUserFound = unit.UserRepository.Get(x => x.UserLogin == deserializedeUser.UserLogin);

                if (resultUserFound.First<User>().UserPassword.SequenceEqual<byte>(deserializedeUser.UserPassword))
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
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
