using FoodTrack.Commands;
using FoodTrack.Context.UnitOfWork;
using FoodTrack.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FoodTrack.ViewModels
{
    class StatisticViewModel : BaseViewModel
    {
        private IEnumerable<UsersParam> statisticCollection;

        private List<UsersParam> weightsTable;

        private decimal weight;
        private int height;
        private string lastReportDate;
        private string mostCategory;

        public SeriesCollection seriesCollection;
        private string[] labels;

        public StatisticViewModel()
        {
            using(UnitOfWork unit = new UnitOfWork())
            {
                StatisticCollection = unit.UserParamRepository.Get(x => x.IdParams == deserializedUser.Id);
                IEnumerable<Report> report = unit.ReportRepository.Get(x => x.IdReport == deserializedUser.Id);

                if(report.Count() != 0)
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

                    WeightsTable = StatisticCollection.Select(x => x).ToList();
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

        public List<UsersParam> WeightsTable
        {
            get { return weightsTable; }
            set
            {
                weightsTable = value;
                OnPropertyChanged("WeightsTable");
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
        public IEnumerable<UsersParam> StatisticCollection
        {
            get { return statisticCollection; }
            set
            {
                statisticCollection = value;
                OnPropertyChanged("StatisticCollection");
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
