using System;
using System.Collections.Generic;

#nullable disable

namespace FoodTrack.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public int IdReport { get; set; }
        public DateTime ReportDate { get; set; }
        public string EatPeriod { get; set; }
        public decimal DayCalories { get; set; }
        public decimal DayProteins { get; set; }
        public decimal DayFats { get; set; }
        public decimal DayCarbohydrates { get; set; }
        public string MostCategory { get; set; }

        public virtual User IdReportNavigation { get; set; }
        public virtual FoodCategory MostCategoryNavigation { get; set; }
    }
}
