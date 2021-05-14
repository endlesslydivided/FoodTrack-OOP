using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class AdminViewModel : BaseViewModel
    {
        private string[] categoryCollection;

        private UsersParam lastSelectedUserParam;
        private UsersDatum lastSelectedUsersDatum;
        private User lastSelectedUser;
        private FoodCategory lastSelectedFoodCategory;
        private Product lastSelectedProduct;
        private Report lastSelectedReport;

        private IEnumerable<UsersParam> usersParamTableToShow;
        private IEnumerable<UsersDatum> usersDatumTableToShow;
        private IEnumerable<User> usersTableToShow;
        private IEnumerable<FoodCategory> categoriesTableToShow;
        private IEnumerable<Product> productsTableToShow;
        private IEnumerable<Report> reportsTableToShow;



        public AdminViewModel()
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                UsersParamTableToShow = unit.UserParamRepository.Get();
                UsersDatumTableToShow = unit.UserDatumRepository.Get();
                UsersTableToShow = unit.UserRepository.Get();
                CategoriesTableToShow = unit.FoodCategoryRepository.GetWithInclude(f => f.Products, g => g.Reports);
                ProductsTableToShow = unit.ProductRepository.Get();
                ReportsTableToShow =unit.ReportRepository.Get();
                CategoryCollection = unit.FoodCategoryRepository.Get().Select(x => x.CategoryName).ToArray();
                LastSelectedUserParam = new();
                LastSelectedUsersDatum = new();
                LastSelectedUser = new();
                LastSelectedFoodCategory = new();
                LastSelectedProduct = new();
                LastSelectedReport = new();
            }
        }
        #region Properties

        public IEnumerable<UsersParam> UsersParamTableToShow
        {
            get { return usersParamTableToShow; }
            set
            {
                usersParamTableToShow = value;
                OnPropertyChanged("UsersParamTableToShow");
            }
        }
        public IEnumerable<UsersDatum> UsersDatumTableToShow
        {
            get { return usersDatumTableToShow; }
            set
            {
                usersDatumTableToShow = value;
                OnPropertyChanged("UsersDatumTableToShow");
            }
        }
        public IEnumerable<User> UsersTableToShow
        {
            get { return usersTableToShow; }
            set
            {
                usersTableToShow = value;
                OnPropertyChanged("UsersTableToShow");
            }
        }
        public IEnumerable<FoodCategory> CategoriesTableToShow
        {
            get { return categoriesTableToShow; }
            set
            {
                categoriesTableToShow = value;
                OnPropertyChanged("CategoriesTableToShow");
            }
        }
        public IEnumerable<Product> ProductsTableToShow
        {
            get { return productsTableToShow; }
            set
            {
                productsTableToShow = value;
                OnPropertyChanged("ProductsTableToShow");
            }
        }
        public IEnumerable<Report> ReportsTableToShow
        {
            get { return reportsTableToShow; }
            set
            {
                reportsTableToShow = value;
                OnPropertyChanged("ReportsTableToShow");
            }
        }


        public string[] CategoryCollection
        {
            get { return categoryCollection; }
            set
            {
                categoryCollection = value;
                OnPropertyChanged("CategoryCollection");
            }
        }
        public UsersParam LastSelectedUserParam
        {
            get { return lastSelectedUserParam; }
            set
            {
                lastSelectedUserParam = value;
                OnPropertyChanged("LastSelectedUserParam");
            }
        }
        public UsersDatum LastSelectedUsersDatum
        {
            get { return lastSelectedUsersDatum; }
            set
            {
                lastSelectedUsersDatum = value;
                OnPropertyChanged("LastSelectedUsersDatum");
            }
        }
        public User LastSelectedUser
        {
            get { return lastSelectedUser; }
            set
            {
                lastSelectedUser = value;
                OnPropertyChanged("LastSelectedUser");
            }
        }
        public FoodCategory LastSelectedFoodCategory
        {
            get { return lastSelectedFoodCategory; }
            set
            {
                lastSelectedFoodCategory = value;
                OnPropertyChanged("LastSelectedFoodCategory");
            }
        }
        public Product LastSelectedProduct
        {
            get { return lastSelectedProduct; }
            set
            {
                lastSelectedProduct = value;
                OnPropertyChanged("LastSelectedProduct");
            }
        }
        public Report LastSelectedReport
        {
            get { return lastSelectedReport; }
            set
            {
                lastSelectedReport = value;
                OnPropertyChanged("LastSelectedReport");
            }
        }


        #endregion

        #region Commands

        #region FoodCategory Commands
        #region Сохранить категорию продуктов

        private DelegateCommand saveFoodCategoryCommand;

        public ICommand SaveFoodCategoryCommand
        {
            get
            {
                if (saveFoodCategoryCommand == null)
                {
                    saveFoodCategoryCommand = new DelegateCommand(saveFoodCategory,canSaveFoodCategory);
                }
                return saveFoodCategoryCommand;
            }
        }

        private void saveFoodCategory()
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                unit.FoodCategoryRepository.Update(LastSelectedFoodCategory);
                unit.Save();
                CategoriesTableToShow = unit.FoodCategoryRepository.Get();
            }
            LastSelectedFoodCategory = new();
        }

        private bool canSaveFoodCategory()
        {
            if(LastSelectedFoodCategory.Id == 0 || LastSelectedFoodCategory.CategoryName == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region Удалить категорию продуктов

        private DelegateCommand deleteFoodCategoryCommand;

        public ICommand DeleteFoodCategoryCommand
        {
            get
            {
                if (deleteFoodCategoryCommand == null)
                {
                    deleteFoodCategoryCommand = new DelegateCommand(deleteFoodCategory, canDeleteFoodCategory);
                }
                return deleteFoodCategoryCommand;
            }
        }

        private void deleteFoodCategory()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.FoodCategoryRepository.Remove(LastSelectedFoodCategory);
                unit.Save();
                CategoriesTableToShow = unit.FoodCategoryRepository.Get();
            }
            LastSelectedFoodCategory = new();

        }

        private bool canDeleteFoodCategory()
        {
            if (LastSelectedFoodCategory.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "категория продуктов"

        private DelegateCommand cleanFoodCategoryCommand;

        public ICommand CleanFoodCategoryCommand
        {
            get
            {
                if (cleanFoodCategoryCommand == null)
                {
                    cleanFoodCategoryCommand = new DelegateCommand(cleanFoodCategory);
                }
                return cleanFoodCategoryCommand;
            }
        }

        private void cleanFoodCategory()
        {
            LastSelectedFoodCategory = new();
        }


        #endregion
        #endregion

        #region Report Commands
        #region Сохранить продуктовый отчёт

        private DelegateCommand saveReportCommand;

        public ICommand SaveReportCommand
        {
            get
            {
                if (saveReportCommand == null)
                {
                    saveReportCommand = new DelegateCommand(saveReport, canSaveReport);
                }
                return saveReportCommand;
            }
        }

        private void saveReport()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                Product product = unit.ProductRepository.Get(x => x.ProductName == LastSelectedReport.ProductName).FirstOrDefault();
                
                LastSelectedReport.DayCalories = product.CaloriesGram * 0.01M * LastSelectedReport.DayGram ;
                LastSelectedReport.DayProteins= product.ProteinsGram * 0.01M * LastSelectedReport.DayGram;
                LastSelectedReport.DayCarbohydrates = product.CarbohydratesGram * 0.01M * LastSelectedReport.DayGram;
                LastSelectedReport.DayFats = product.FatsGram * 0.01M * LastSelectedReport.DayGram;

                unit.ReportRepository.Update(LastSelectedReport);
                unit.Save();
                ReportsTableToShow = unit.ReportRepository.Get();
            }
            LastSelectedReport = new();

        }

        private bool canSaveReport()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<Product> products = unit.ProductRepository.Get();
                Product product = new();
                bool productExist = false;
                foreach (Product x in products)
                {
                    if (x.ProductName == LastSelectedReport.ProductName)
                    {
                        productExist = true;
                        break;
                    }
                }

                if (LastSelectedReport.Id == 0 || LastSelectedReport.DayGram == 0 || !productExist || LastSelectedReport.ReportDate.Date.CompareTo(DateTime.Now.Date) == 1 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
        #endregion
        #region Удалить продуктовый отчёт

        private DelegateCommand deleteReportCommand;

        public ICommand DeleteReportCommand
        {
            get
            {
                if (deleteReportCommand == null)
                {
                    deleteReportCommand = new DelegateCommand(deleteReport, canDeleteReport);
                }
                return deleteReportCommand;
            }
        }

        private void deleteReport()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.ReportRepository.Remove(LastSelectedReport);
                unit.Save();
                ReportsTableToShow = unit.ReportRepository.Get();
            }
            LastSelectedReport = new Report();
        }

        private bool canDeleteReport()
        {
            if (LastSelectedReport.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "продуктовый отчёт"

        private DelegateCommand cleanReportCommand;

        public ICommand CleanReportCommand
        {
            get
            {
                if (cleanReportCommand == null)
                {
                    cleanReportCommand = new DelegateCommand(cleanReport);
                }
                return cleanReportCommand;
            }
        }

        private void cleanReport()
        {
            LastSelectedReport = new();
        }


        #endregion
        #endregion

        #region Product Commands
        #region Сохранить продукт

        private DelegateCommand saveProductCommand;

        public ICommand SaveProductCommand
        {
            get
            {
                if (saveProductCommand == null)
                {
                    saveProductCommand = new DelegateCommand(saveProduct, canSaveProduct);
                }
                return saveProductCommand;
            }
        }

        private void saveProduct()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.ProductRepository.Update(LastSelectedProduct);
                unit.Save();
                ProductsTableToShow = unit.ProductRepository.Get();
            }
            LastSelectedProduct = new();

        }

        private bool canSaveProduct()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<Product> products = unit.ProductRepository.Get();
                Product product = new();
                int productExist = 0;
                foreach (Product x in products)
                {
                    if (x.ProductName == LastSelectedProduct.ProductName)
                    {
                        productExist++;
                    }
                }

                if (LastSelectedProduct.Id == 0 ||
                    LastSelectedProduct.ProteinsGram <= 0 || 
                    LastSelectedProduct.CarbohydratesGram <= 0 || 
                    LastSelectedProduct.FatsGram <= 0 || 
                    LastSelectedProduct.CaloriesGram <= 0 ||
                    LastSelectedProduct.FatsGram <= 0 ||
                    productExist >1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        #endregion
        #region Удалить продукт

        private DelegateCommand deleteProductCommand;

        public ICommand DeleteProductCommand
        {
            get
            {
                if (deleteProductCommand == null)
                {
                    deleteProductCommand = new DelegateCommand(deleteProduct, canDeleteProduct);
                }
                return deleteProductCommand;
            }
        }

        private void deleteProduct()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.ProductRepository.Remove(LastSelectedProduct);
                unit.Save();
                ProductsTableToShow = unit.ProductRepository.Get();
            }
            LastSelectedProduct = new Product();
        }

        private bool canDeleteProduct()
        {
            if (LastSelectedProduct.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "продукт"

        private DelegateCommand cleanProductCommand;

        public ICommand CleanProductCommand
        {
            get
            {
                if (cleanProductCommand == null)
                {
                    cleanProductCommand = new DelegateCommand(cleanProduct);
                }
                return cleanProductCommand;
            }
        }

        private void cleanProduct()
        {
            LastSelectedProduct = new();
        }


        #endregion
        #endregion

        #region User Commands
        #region Сохранить пользователя

        private DelegateCommand saveUserCommand;

        public ICommand SaveUserCommand
        {
            get
            {
                if (saveUserCommand == null)
                {
                    saveUserCommand = new DelegateCommand(saveUser, canSaveUser);
                }
                return saveUserCommand;
            }
        }

        private void saveUser()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserRepository.Update(LastSelectedUser);
                unit.Save();
                UsersTableToShow = unit.UserRepository.Get();
            }
            LastSelectedUser = new();

        }

        private bool canSaveUser()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                IEnumerable<User> Users = unit.UserRepository.Get();
                User User = new();
                int UserExist = 0;
                foreach (User x in Users)
                {
                    if (x.UserLogin == LastSelectedUser.UserLogin)
                    {
                        UserExist++;
                    }
                }

                if (LastSelectedUser.Id == 0 ||
                    UserExist > 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        #endregion
        #region Удалить пользователя

        private DelegateCommand deleteUserCommand;

        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                {
                    deleteUserCommand = new DelegateCommand(deleteUser, canDeleteUser);
                }
                return deleteUserCommand;
            }
        }

        private void deleteUser()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserRepository.Remove(LastSelectedUser);
                unit.Save();
                UsersTableToShow = unit.UserRepository.Get();
            }
            LastSelectedUser = new User();
        }

        private bool canDeleteUser()
        {
            if (LastSelectedUser.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "пользователь"

        private DelegateCommand cleanUserCommand;

        public ICommand CleanUserCommand
        {
            get
            {
                if (cleanUserCommand == null)
                {
                    cleanUserCommand = new DelegateCommand(cleanUser);
                }
                return cleanUserCommand;
            }
        }

        private void cleanUser()
        {
            LastSelectedUser = new();
        }


        #endregion
        #endregion

        #region UserDatum Commands
        #region Сохранить информацию пользователя

        private DelegateCommand saveUsersDatumCommand;

        public ICommand SaveUsersDatumCommand
        {
            get
            {
                if (saveUsersDatumCommand == null)
                {
                    saveUsersDatumCommand = new DelegateCommand(saveUsersDatum, canSaveUsersDatum);
                }
                return saveUsersDatumCommand;
            }
        }

        private void saveUsersDatum()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserDatumRepository.Update(LastSelectedUsersDatum);
                unit.Save();
                UsersDatumTableToShow = unit.UserDatumRepository.Get();
            }
            LastSelectedUsersDatum = new();

        }

        private bool canSaveUsersDatum()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (LastSelectedUsersDatum.Id == 0 || LastSelectedUsersDatum.Birthday.Date.CompareTo(DateTime.Now.Date) == 1 || !Regex.IsMatch(LastSelectedUsersDatum.FullName, "^([А-Я]{1}[а-я]{1,99}){3}$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        #endregion
        #region Удалить информацию пользователя

        private DelegateCommand deleteUsersDatumCommand;

        public ICommand DeleteUsersDatumCommand
        {
            get
            {
                if (deleteUsersDatumCommand == null)
                {
                    deleteUsersDatumCommand = new DelegateCommand(deleteUsersDatum, canDeleteUsersDatum);
                }
                return deleteUsersDatumCommand;
            }
        }

        private void deleteUsersDatum()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserDatumRepository.Remove(LastSelectedUsersDatum);
                unit.Save();
                UsersDatumTableToShow = unit.UserDatumRepository.Get();
            }
            LastSelectedUsersDatum = new UsersDatum();
        }

        private bool canDeleteUsersDatum()
        {
            if (LastSelectedUsersDatum.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "информация пользователя"

        private DelegateCommand cleanUsersDatumCommand;

        public ICommand CleanUsersDatumCommand
        {
            get
            {
                if (cleanUsersDatumCommand == null)
                {
                    cleanUsersDatumCommand = new DelegateCommand(cleanUsersDatum);
                }
                return cleanUsersDatumCommand;
            }
        }

        private void cleanUsersDatum()
        {
            LastSelectedUsersDatum = new();
        }


        #endregion
        #endregion

        #region UserParam Commands
        #region Сохранить параметры пользователя

        private DelegateCommand saveUserParamCommand;

        public ICommand SaveUserParamCommand
        {
            get
            {
                if (saveUserParamCommand == null)
                {
                    saveUserParamCommand = new DelegateCommand(saveUserParam, canSaveUserParam);
                }
                return saveUserParamCommand;
            }
        }

        private void saveUserParam()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserParamRepository.Update(LastSelectedUserParam);
                unit.Save();
                UsersParamTableToShow = unit.UserParamRepository.Get();
            }
            LastSelectedUserParam = new();

        }

        private bool canSaveUserParam()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                if (LastSelectedUserParam.Id == 0 ||
                    LastSelectedUserParam.ParamsDate.Date.CompareTo(DateTime.Now.Date) == 1 ||
                    LastSelectedUserParam.UserWeight <= 1||
                    LastSelectedUserParam.UserHeight <= 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        #endregion
        #region Удалить параметры пользователя

        private DelegateCommand deleteUserParamCommand;

        public ICommand DeleteUserParamCommand
        {
            get
            {
                if (deleteUserParamCommand == null)
                {
                    deleteUserParamCommand = new DelegateCommand(deleteUserParam, canDeleteUserParam);
                }
                return deleteUserParamCommand;
            }
        }

        private void deleteUserParam()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.UserParamRepository.Remove(LastSelectedUserParam);
                unit.Save();
                UsersParamTableToShow = unit.UserParamRepository.Get();
            }
            LastSelectedUserParam = new UsersParam();
        }

        private bool canDeleteUserParam()
        {
            if (LastSelectedUserParam.Id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
        #region Очистить поля "параметры пользователя"

        private DelegateCommand cleanUserParamCommand;

        public ICommand CleanUserParamCommand
        {
            get
            {
                if (cleanUserParamCommand == null)
                {
                    cleanUserParamCommand = new DelegateCommand(cleanUserParam);
                }
                return cleanUserParamCommand;
            }
        }

        private void cleanUserParam()
        {
            LastSelectedUserParam = new();
        }


        #endregion
        #endregion


        #endregion

    }
}
