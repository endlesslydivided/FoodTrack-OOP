using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class ChangeProductViewModel : BaseViewModel
    {
        private Report report;

        public ChangeProductViewModel(Report selectedReport)
        {
            report = selectedReport;
        }

        #region Properties

        public string ProductName
        {
            get { return report.ProductName; }
            set
            {
                report.ProductName = value;
                OnPropertyChanged("ProductName");
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

        #endregion

        #region Commands

        #region Внести изменения

        private DelegateCommand changeProductCommand;

        public ICommand ChangeProductCommand
        {
            get
            {
                if (changeProductCommand == null)
                {
                    changeProductCommand = new DelegateCommand(changeProduct,canChangeProduct);
                }
                return changeProductCommand;
            }
        }

        private void changeProduct()
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.ReportRepository.Update(report);
                unit.Save();
            }
        }

        private bool canChangeProduct()
        {
            if(GramValue == 0)
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
