using FakeAtlas.Context.UnitOfWork;
using FoodTrack.Commands;
using FoodTrack.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class TodayDietViewModel : BaseViewModel
    {
        private IEnumerable tableToShow;
        private DateTime dateToChoose;

        public TodayDietViewModel()
        {
            DateToChoose = DateTime.Today;
        }

        #region Properties


        public IEnumerable TableToShow
        {
            get { return tableToShow; }
            set
            {
                tableToShow = value;
                OnPropertyChanged("TableToShow");
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

        #region Изменить таблицу "завтрак"

        private DelegateCommand tableChangeToBreakfastCommand;

        public ICommand TableChangeToBreakfastCommand
        {
            get
            {
                if (tableChangeToBreakfastCommand == null)
                {
                    tableChangeToBreakfastCommand = new DelegateCommand(tableChangeToBreakfast);
                }
                return tableChangeToBreakfastCommand;
            }
        }

        private void tableChangeToBreakfast()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                User foundUser = unit.UserRepository.Get(x => x.UserLogin == deserializedUser.UserLogin).First<User>();
                IEnumerable<Report> reports = unit.ReportRepository.Get(x => x.ReportDate == DateToChoose && x.IdReport == foundUser.Id && x.EatPeriod == "Breakfast");
                TableToShow = reports;
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
