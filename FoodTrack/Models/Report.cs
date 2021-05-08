using System;
using System.Collections.Generic;

#nullable disable

namespace FoodTrack.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public int IdReport { get; set; }
        public string ProductName { get; set; }
        public DateTime ReportDate { get; set; }
        public string EatPeriod { get; set; }
        public decimal DayGram { get; set; }
        public string MostCategory { get; set; }

        public virtual User IdReportNavigation { get; set; }
        public virtual FoodCategory MostCategoryNavigation { get; set; }
        public virtual Product ProductNameNavigation { get; set; }
    }
}
