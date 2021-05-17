using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FoodTrack.ViewModels
{
    class StatisticViewModel : BaseViewModel
    {

        private IEnumerable<UsersParam> statisticCollection;

        private decimal weight;
        private int height;
        private string lastReportDate;
        private string mostCategory;
        private UsersParam lastSelected;

        public SeriesCollection seriesCollection;
        private string[] labels;

        public StatisticViewModel()
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                LastSelected = default;

                StatisticCollection = unit.UserParamRepository.Get(x => x.IdParams == deserializedUser.Id);
                IEnumerable<Report> report = unit.ReportRepository.Get(x => x.IdReport == deserializedUser.Id);
                List<Report> mostReportCategory = (List<Report>)unit.ReportRepository.Get(x => x.IdReport == deserializedUser.Id && DateTime.Today.Date.Date.Equals(x.ReportDate.Date));

                if (mostReportCategory.Capacity != 0)
                {
                    MostCategory = mostReportCategory.GroupBy(i => i.MostCategory).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
                }
                else
                {
                    MostCategory = "---";
                }

                if (report.Count() != 0)
                {
                    LastReportDate = report.Last().ReportDate.ToString();
                }
                else
                {
                    LastReportDate = "---";
                }

                if (StatisticCollection.Count() != 0)
                {
                    Height = StatisticCollection.Last().UserHeight;
                    Weight = StatisticCollection.Last().UserWeight;


                    ChartValues<decimal> weights = new ChartValues<decimal>(StatisticCollection.Select(x => x.UserWeight));
                    List<string> labels = new List<string>();

                    foreach(DateTime x in StatisticCollection.Select(x => x.ParamsDate))
                    {
                        labels.Add(x.ToString("d"));
                    }

                    Labels = labels.ToArray();
                    YFormatter = value => value.ToString("0.00");

                    SeriesCollection = new SeriesCollection();

                    SeriesCollection.Add(new LineSeries
                    {
                        Title = "Вес",
                        Values = weights,
                        LineSmoothness = 0,
                        PointGeometrySize = 15,                     
                        PointForeground = Brushes.Coral,
                        Stroke = Brushes.Coral,
                        Fill = Brushes.Transparent
                    });

                }
                else
                {
                    Height = 0;
                    Weight = 0;
                }                
            }
        }
        
        #region Properties

        public Func<double, string> YFormatter { get; set; }

        public UsersParam LastSelected
        {
            get { return lastSelected; }
            set
            {
                lastSelected = value;
                OnPropertyChanged("LastSelected");
            }
        }

        public string[] Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                OnPropertyChanged("Labels");
            }
        }

        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public string MostCategory
        {
            get { return mostCategory; }
            set
            {
                mostCategory = value;
                OnPropertyChanged("MostCategory");
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
        public string LastReportDate
        {
            get { return lastReportDate; }
            set
            {
                lastReportDate = value;
                OnPropertyChanged("LastReportDate");
            }
        }

        public IEnumerable<UsersParam> StatisticCollection
        {
            get { return statisticCollection; }
            set
            {
                statisticCollection = value;
                OnPropertyChanged("StatisticCollection");
            }
        }
        #endregion

        #region Commands

        #region Удалить запись в таблице вес

        private DelegateCommand deleteWeightRowCommand;

        public ICommand DeleteWeightRowCommand
        {
            get
            {
                if (deleteWeightRowCommand == null)
                {
                    deleteWeightRowCommand = new DelegateCommand(deleteWeightRow, canDeleteWeightRow);
                }
                return deleteWeightRowCommand;
            }
        }


        private void deleteWeightRow()
        {
            if (LastSelected != null)
            {
                using (UnitOfWork unit = new UnitOfWork())
                {
                    UsersParam toDelete = unit.UserParamRepository.Get(x => x.ParamsDate == lastSelected.ParamsDate && x.IdParams == deserializedUser.Id).First();
                    unit.UserParamRepository.Remove(toDelete);
                    unit.Save();

                    StatisticCollection = unit.UserParamRepository.Get(x => x.IdParams == deserializedUser.Id);
                }

                SeriesCollection.Clear();

                ChartValues<decimal> weights = new ChartValues<decimal>(StatisticCollection.Select(x => x.UserWeight));
                List<string> labels = new List<string>();

                foreach (DateTime x in StatisticCollection.Select(x => x.ParamsDate))
                {
                    labels.Add(x.ToString("d"));
                }

                Labels = labels.ToArray();
                SeriesCollection.Add(new LineSeries
                {
                    Title = "Вес",
                    Values = weights,
                    LineSmoothness = 0,
                    PointGeometrySize = 15,
                    PointForeground = Brushes.Coral,
                    Stroke = Brushes.Coral,
                    Fill = Brushes.Transparent
                });


            }
        }
        private bool canDeleteWeightRow()
        {
            if(StatisticCollection.Count() >1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

            #endregion


        #endregion

        }
}
