using System;
using System.Collections.Generic;

#nullable disable

namespace FoodTrack.Models
{
    public partial class UsersDatum
    {
        public int Id { get; set; }
        public int IdData { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }

        public virtual User IdDataNavigation { get; set; }
    }
}
