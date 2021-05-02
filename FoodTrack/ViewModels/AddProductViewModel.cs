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
        private decimal gramValue;
        private Product selectedProduct;


        public AddProductViewModel()
        {
            SearchText = default; 
            CollectionOfProducts = default;
            GramValue = default;
            SelectedProduct = default;
        }

        #region Properties

        public decimal GramValue
        {
            get { return gramValue; }
            set
            {
                gramValue = value;
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
                TextChangedCommand.ChangeCanExecute();
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
        #endregion
    }
}
