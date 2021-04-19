using System;
using System.Collections.Generic;

#nullable disable

namespace FoodTrack.Models
{
    public partial class User
    {
        public User()
        {
            Products = new HashSet<Product>();
            Reports = new HashSet<Report>();
            UsersData = new HashSet<UsersDatum>();
            UsersParams = new HashSet<UsersParam>();
        }

        public int Id { get; set; }
        public bool? IsAdmin { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<UsersDatum> UsersData { get; set; }
        public virtual ICollection<UsersParam> UsersParams { get; set; }
    }
}
