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
        private List<string> categoryCollection;
        private decimal weight;
        private int height;

        private Product product;

        public ParamsViewModel()
        {
            product = new Product();
            ProductName = default;
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable categories = unit.FoodCategoryRepository.Get();
                CategoryCollection = new List<string>();
                foreach (FoodCategory x in categories)
                {
                    CategoryCollection.Add(x.CategoryName);
                }
            }
            SelectedCategory = categoryCollection.First();
        }

        #region Properties

        public List<string> CategoryCollection
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
            get { return product.FoodCategory; }
            set
            {
                product.FoodCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        public string ProductName
        {
            get { return product.ProductName; }
            set
            {
                product.ProductName = value;
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
            get { return product.ProteinsGram; }
            set
            {
                product.ProteinsGram = value;
                OnPropertyChanged("Proteins");
            }
        }
        public decimal Carbohydrates
        {
            get { return product.CarbohydratesGram; }
            set
            {
                product.CarbohydratesGram = value;
                OnPropertyChanged("Carbohydrates");
            }
        }
        public decimal Fats
        {
            get { return product.FatsGram; }
            set
            {
                product.FatsGram = value;
                OnPropertyChanged("Fats");
            }
        }
        public decimal  Calories
        {
            get { return product.CaloriesGram; }
            set
            {
                product.CaloriesGram = value;
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
                if (addProductToCollectionCommand == null)
                {
                    addProductToCollectionCommand = new DelegateCommand(addProductToCollection, canAddProductToCollection);
                }
                return addProductToCollectionCommand;
            }
        }

        private void addProductToCollection()
        {    
            using (UnitOfWork unit = new UnitOfWork())
            {
                product.IdAdded = deserializedUser.Id;
                product.FoodCategory = SelectedCategory;

                unit.ProductRepository.Create(product);
                unit.Save();
            }
        }

        private bool canAddProductToCollection()
        {
            if (ProductName == "" || Calories == 0 || Proteins == 0 || Fats == 0 || Carbohydrates == 0 || SelectedCategory == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion


        #endregion

    }
}
