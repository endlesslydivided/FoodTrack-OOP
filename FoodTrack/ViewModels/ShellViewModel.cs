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
                NavigationType = typeof(TodayResultsView),
                NavigationDestination = new Uri("Views/Pages/TodayResultsView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.AppleAltSolid },
                Label = "Питание",
                NavigationType = typeof(TodayDietView),              
                NavigationDestination = new Uri("Views/Pages/TodayDietView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.PlusSolid },
                Label = "Добавить продукт",
                NavigationType = typeof(AddProductView),
                NavigationDestination = new Uri("../Views/Pages/AddProductView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ChartBarRegular },
                Label = "Статистика",
                NavigationType = typeof(StatisticView),
                NavigationDestination = new Uri("Views/Pages/StatisticView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Human },
                Label = "Параметры тела",
                NavigationType = typeof(ParamsView),
                NavigationDestination = new Uri("Views/Pages/ParamsView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircleSolid },
                Label = "О приложении",
                NavigationType = typeof(AboutAppView),
                NavigationDestination = new Uri("Views/Pages/AboutAppView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ToolsSolid },
                Label = "Настройки",
                NavigationType = typeof(OptionsView),
                NavigationDestination = new Uri("../Views/Pages/OptionsView.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconModern() { Kind = PackIconModernKind.ControlGuide },
                Label = "Администрирование",
                NavigationType = typeof(AdminView),
                NavigationDestination = new Uri("../Views/Pages/AdminView.xaml", UriKind.RelativeOrAbsolute)
            });

        }
    }
}
