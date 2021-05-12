using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class AdminViewModel : BaseViewModel
    {
        public ObservableCollection<UsersParam> UsersParamTableToShow { get; set; }
        public ObservableCollection<UsersDatum> UsersDatumTableToShow { get; set; }
        public ObservableCollection<User> UsersTableToShow { get; set; }
        public ObservableCollection<FoodCategory> CategoriesTableToShow { get; set; }
        public ObservableCollection<Product> ProductsTableToShow { get; set; }
        public ObservableCollection<Report> ReportsTableToShow { get; set; }

        public AdminViewModel()
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                UsersParamTableToShow = new ObservableCollection<UsersParam>(unit.UserParamRepository.Get());
                UsersDatumTableToShow = new ObservableCollection<UsersDatum>(unit.UserDatumRepository.Get());
                UsersTableToShow = new ObservableCollection<User>(unit.UserRepository.Get());
                CategoriesTableToShow = new ObservableCollection<FoodCategory>(unit.FoodCategoryRepository.Get());
                ProductsTableToShow = new ObservableCollection<Product>(unit.ProductRepository.Get());
                ReportsTableToShow = new ObservableCollection<Report>(unit.ReportRepository.Get());
            }
        }
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

        #region COMMAND1

        private DelegateCommand exampleCommand;

        public ICommand ExampleCommand
        {
            get
            {
                if (exampleCommand == null)
                {
                    exampleCommand = new DelegateCommand(Example);
                }
                return exampleCommand;
            }
        }

        private void Example()
        {

        }

        #endregion


        #endregion

    }
}
