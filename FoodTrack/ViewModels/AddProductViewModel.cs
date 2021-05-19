using FoodTrack.Context.UnitOfWork;
using FoodTrack.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using FoodTrack.Models;

namespace FoodTrack.ViewModels
{
    public class AddProductViewModel : BaseViewModel
    {
        private const decimal V = 0.01M;
        private string searchText;
        private IEnumerable collectionOfProducts;
        private Product selectedProduct;
        private DateTime dateToChoose;

        private string stateOfSearch;
        private Report report;
        private bool foodCategoryCheck;

        private string selectedCategory;

        public string[] foodCategories;

        public AddProductViewModel()
        {
            report = new Report();
            report.IdReport = deserializedUser.Id;
            SearchText = "";
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable products = unit.ProductRepository.Get();
                CollectionOfProducts = products;
                FoodCategories = unit.FoodCategoryRepository.Get().Select(x=>x.CategoryName).ToArray();
            }
            DateToChoose = DateTime.Now;
            GramValue = default;
            SelectedProduct = default;
            SelectedPeriod = "Завтрак";
            StateOfSearch = "All";
            FoodCategoryCheck = false;
        }

        #region Properties

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public string[] FoodCategories
        {
            get { return foodCategories; }
            set
            {
                foodCategories = value;
                OnPropertyChanged("FoodCategories");
            }
        }


        public bool FoodCategoryCheck
        {
            get { return foodCategoryCheck; }
            set
            {
                foodCategoryCheck = value;
                OnPropertyChanged("FoodCategoryCheck");
            }
        }

        public string StateOfSearch
        {
            get { return stateOfSearch; }
            set
            {
                stateOfSearch = value;
                OnPropertyChanged("GramValue");
            }
        }

        public decimal GramValue
        {
            get { return report.DayGram; }
            set
            {
                report.DayGram = value;
                OnPropertyChanged("GramValue");
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }


        public IEnumerable CollectionOfProducts
        {
            get { return collectionOfProducts; }
            set
            {
                collectionOfProducts = value;
                OnPropertyChanged("CollectionOfProducts");
            }
        }
        
        public DateTime DateToChoose
        {
            get { return dateToChoose; }
            set
            {
                dateToChoose = value;
                OnPropertyChanged("DateToChoose");
            }
        }

        public string SelectedPeriod
        {
            get { return report.EatPeriod; }
            set
            {
                report.EatPeriod = value;
                OnPropertyChanged("SelectedPeriod");
            }
        }

        #endregion

        #region Commands

        #region Показать продукты пользователя

        private DelegateCommand showUserProductsCommand;

        public ICommand ShowUserProductsCommand
        {
            get
            {
                if (showUserProductsCommand == null)
                {
                    showUserProductsCommand = new DelegateCommand(showUserProducts);
                }
                return showUserProductsCommand;
            }
        }

        private void showUserProducts()
        {
            StateOfSearch = "User";
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (FoodCategoryCheck)
                {
                    if (SearchText != "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id && Regex.IsMatch(x.ProductName, "^" + SearchText ) && x.FoodCategory == SelectedCategory);
                        CollectionOfProducts = products;

                    }
                    else if (SearchText == "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id);
                        CollectionOfProducts = products;
                    }
                }
                else
                {
                    if (SearchText != "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id && Regex.IsMatch(x.ProductName, "^" + SearchText ));
                        CollectionOfProducts = products;

                    }
                    else if (SearchText == "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id);
                        CollectionOfProducts = products;
                    }
                }
            }
        }

        #endregion
        #region Показать все продукты

        private DelegateCommand showAllProductsCommand;

        public ICommand ShowAllProductsCommand
        {
            get
            {
                if (showAllProductsCommand == null)
                {
                    showAllProductsCommand = new DelegateCommand(showAllProducts);
                }
                return showAllProductsCommand;
            }
        }

        private void showAllProducts()
        {
            StateOfSearch = "All";
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (FoodCategoryCheck)
                {
                    if (SearchText != "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => Regex.IsMatch(x.ProductName, "^" + SearchText ) && x.FoodCategory == SelectedCategory);
                        CollectionOfProducts = products;

                    }
                    else if (SearchText == "")
                    {
                        IEnumerable products = unit.ProductRepository.Get();
                        CollectionOfProducts = products;
                    }
                }
                else
                {
                    if (SearchText != "")
                    {
                        IEnumerable products = unit.ProductRepository.Get(x => Regex.IsMatch(x.ProductName, "^" + SearchText ));
                        CollectionOfProducts = products;

                    }
                    else if (SearchText == "")
                    {
                        IEnumerable products = unit.ProductRepository.Get();
                        CollectionOfProducts = products;
                    }
                }
            }
        }

        #endregion

        #region Добавить продукт

        private DelegateCommand addProductCommand;

        public ICommand AddProductCommand
        {
            get
            {
                if (addProductCommand == null)
                {
                    addProductCommand = new DelegateCommand(addProduct,canAddProduct);
                }
                return addProductCommand;
            }
        }

        private void addProduct()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                report.DayGram = GramValue;

                report.DayCarbohydrates = SelectedProduct.CarbohydratesGram * GramValue * V % 100000;
                report.DayCalories = SelectedProduct.CaloriesGram * GramValue * V % 100000;
                report.DayFats= SelectedProduct.FatsGram * GramValue * V % 100000;
                report.DayProteins = SelectedProduct.ProteinsGram * GramValue * V % 100000;

                report.ProductName = SelectedProduct.ProductName;
                report.ReportDate = DateToChoose;
                report.MostCategory = SelectedProduct.FoodCategory;
                
                unit.ReportRepository.Create(report);
                unit.Save();

                report = new Report();
                report.IdReport = deserializedUser.Id;
                GramValue = default;
                SelectedProduct = default;
            }
        }

        private bool canAddProduct()
        {
            if (SelectedProduct == null || GramValue == 0 || DateToChoose.Date.CompareTo(DateTime.Now.Date) > 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Увеличить дату на день

        private DelegateCommand addDayCommand;

        public ICommand AddDayCommand
        {
            get
            {
                if (addDayCommand == null)
                {
                    addDayCommand = new DelegateCommand(addDay);
                }
                return addDayCommand;
            }
        }

        private void addDay()
        {
            DateToChoose = DateToChoose.AddDays(1);
        }

        #endregion
        #region Уменьшить дату на день

        private DelegateCommand removeDayCommand;

        public ICommand RemoveDayCommand
        {
            get
            {
                if (removeDayCommand == null)
                {
                    removeDayCommand = new DelegateCommand(removeDay);
                }
                return removeDayCommand;
            }
        }

        private void removeDay()
        {
            DateToChoose = DateToChoose.AddDays(-1);
        }

        #endregion

        #region Поиск

        private DelegateCommand searchCommand;

        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new DelegateCommand(search);
                }
                return searchCommand;
            }
        }

        private void search()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (SearchText != null)
                {
                    if (StateOfSearch == "All")
                    {
                        if (FoodCategoryCheck)
                        {
                            if (SearchText != "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => Regex.IsMatch(x.ProductName, "^" + SearchText ) && x.FoodCategory == SelectedCategory);
                                CollectionOfProducts = products;

                            }
                            else if (SearchText == "")
                            {
                                IEnumerable products = unit.ProductRepository.Get();
                                CollectionOfProducts = products;
                            }
                        }
                        else
                        {
                            if (SearchText != "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => Regex.IsMatch(x.ProductName, "^" + SearchText ));
                                CollectionOfProducts = products;

                            }
                            else if (SearchText == "")
                            {
                                IEnumerable products = unit.ProductRepository.Get();
                                CollectionOfProducts = products;
                            }
                        }
                    }
                    else if(StateOfSearch == "User")
                    {
                        if (FoodCategoryCheck)
                        {
                            if (SearchText != "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id && Regex.IsMatch(x.ProductName, "^" + SearchText ) && x.FoodCategory == SelectedCategory);
                                CollectionOfProducts = products;

                            }
                            else if (SearchText == "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id);
                                CollectionOfProducts = products;
                            }
                        }
                        else
                        {
                            if (SearchText != "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id && Regex.IsMatch(x.ProductName, "^" + SearchText ));
                                CollectionOfProducts = products;

                            }
                            else if (SearchText == "")
                            {
                                IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id);
                                CollectionOfProducts = products;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
