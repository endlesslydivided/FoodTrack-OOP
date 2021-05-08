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
        private string searchText;
        private IEnumerable collectionOfProducts;
        private Product selectedProduct;
        private DateTime dateToChoose;

        private Report report;


        public AddProductViewModel()
        {
            report = new Report();
            report.IdReport = deserializedUser.Id;
            SearchText = default; 
            CollectionOfProducts = default;
            GramValue = default;
            SelectedProduct = default;
        }

        #region Properties

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
            using (UnitOfWork unit = new UnitOfWork())
            { 
                if (SearchText != "")
                {
                    IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id && Regex.IsMatch(x.ProductName,"%" + SearchText +"$"));
                    CollectionOfProducts = products;

                }
                else if(SearchText == "")
                {
                    IEnumerable products = unit.ProductRepository.Get(x => x.IdAdded == deserializedUser.Id);
                    CollectionOfProducts = products;
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
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (SearchText != "")
                {
                    IEnumerable products = unit.ProductRepository.Get(x => Regex.IsMatch(x.ProductName, "%" + SearchText + "$"));
                    CollectionOfProducts = products;

                }
                else if (SearchText == "")
                {
                    IEnumerable products = unit.ProductRepository.Get();
                    CollectionOfProducts = products;
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
                report.ProductName = SelectedProduct.ProductName;
                report.ReportDate = DateToChoose;
                report.MostCategory = SelectedProduct.FoodCategory;
                
                unit.ReportRepository.Create(report);
                unit.Save();
            }
        }

        private bool canAddProduct()
        {
            if (SelectedProduct == null || GramValue == 0)
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

        #endregion
    }
}
