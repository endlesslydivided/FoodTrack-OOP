using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class ParamsViewModel : BaseViewModel
    {
        private IEnumerable categoryCollection;
        private decimal weight;
        private int height;
        private int calories;
        private decimal proteins;
        private decimal fats;
        private decimal carbohydrates;
        private string productName;
        private string selectedCategory;

        public ParamsViewModel()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                CategoryCollection = unit.FoodCategoryRepository.Get();
            }
        }

        #region Properties

        public IEnumerable CategoryCollection
        {
            get { return categoryCollection; }
            set
            {
                categoryCollection = value;
                OnPropertyChanged("CategoryCollection");
            }
        }

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        public decimal Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }
        public decimal Proteins
        {
            get { return proteins; }
            set
            {
                proteins = value;
                OnPropertyChanged("Proteins");
            }
        }
        public decimal Carbohydrates
        {
            get { return carbohydrates; }
            set
            {
                carbohydrates = value;
                OnPropertyChanged("Carbohydrates");
            }
        }
        public decimal Fats
        {
            get { return fats; }
            set
            {
                fats = value;
                OnPropertyChanged("Fats");
            }
        }
        public int  Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                OnPropertyChanged("Calories");
            }
        }

        #endregion

        #region Commands

        #region Добавить отчёт по пользователю

        private DelegateCommand addParamsReportCommand;

        public ICommand AddParamsReportCommand
        {
            get
            {
                if (addParamsReportCommand == null)
                {
                    addParamsReportCommand = new DelegateCommand(addParamsReport, canAddParamsReport);
                }
                return addParamsReportCommand;
            }
        }

        private void addParamsReport()
        {
            if (!Regex.IsMatch(Weight.ToString(), "^[0-9]{1,3}([.][0-9]{1,2})?$"))
            {

            }
            else if (!Regex.IsMatch(Height.ToString(), "^[1-9]{1}[0-9]{0,2}$"))
            {

            }
            else
            {
                using (UnitOfWork unit = new UnitOfWork())
                {
                    UsersParam usersParam = new UsersParam();
                    usersParam.ParamsDate = DateTime.Now;
                    usersParam.UserHeight = Height;
                    usersParam.UserWeight = Weight;
                    usersParam.IdParams = deserializedUser.Id;
                    unit.UserParamRepository.Create(usersParam);
                    unit.Save();
                    Height = default;
                    Weight = default;
                }
            }
        }

        private bool canAddParamsReport()
        {
            if (Weight != 0 && Height != 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Добавить продукт в коллекцию

        private DelegateCommand addProductToCollectionCommand;

        public ICommand AddProductToCollectionCommand
        {
            get 
            {
                if (addParamsReportCommand == null)
                {
                    addParamsReportCommand = new DelegateCommand(addProductToCollection, canAddProductToCollection);
                }
                return addParamsReportCommand;
            }
        }

        private void addProductToCollection()
        {    
            /*▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬*/

            if (!Regex.IsMatch(Weight.ToString(), "^[0-9]{1,3}([.][0-9]{1,2})?$"))
            {

            }
            else if (!Regex.IsMatch(Height.ToString(), "^[1-9]{1}[0-9]{0,2}$"))
            {

            }
            else
            {
                using (UnitOfWork unit = new UnitOfWork())
                {
                
                }
            }
        }

        private bool canAddProductToCollection()
        {
            /*▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬*/

            return false;
        }

        #endregion


        #endregion

    }
}
