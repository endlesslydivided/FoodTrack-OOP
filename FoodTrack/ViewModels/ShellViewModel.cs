using FoodTrack.Views.Pages;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrack.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> Menu => AppMenu;

        public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

        public ShellViewModel()
        {
            // Строим меню
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.TrophySolid },
                Label = "Результаты дня",
                NavigationType = typeof(TodayResultsPage),
                NavigationDestination = new Uri("Views/Pages/TodayResultsPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.AppleAltSolid },
                Label = "Питание",
                NavigationType = typeof(TodayDietPage),
                NavigationDestination = new Uri("Views/Pages/TodayDietPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ChartBarRegular },
                Label = "Статистика",
                NavigationType = typeof(StatisticPage),
                NavigationDestination = new Uri("Views/Pages/StatisticPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Human },
                Label = "Параметры тела",
                NavigationType = typeof(ParamsPage),
                NavigationDestination = new Uri("Views/Pages/ParamsPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid },
                Label = "О приложении",
                NavigationType = typeof(AboutAppPage),
                NavigationDestination = new Uri("Views/Pages/AboutAppPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ToolsSolid },
                Label = "Настройки",
                NavigationType = typeof(OptionsPage),
                NavigationDestination = new Uri("../Views/Pages/OptionsPage.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}
