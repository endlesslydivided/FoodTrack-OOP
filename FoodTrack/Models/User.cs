using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
            Id = default;
            IsAdmin = default;
            Salt = default;
            UserLogin = default;
        }

        public int Id { get; set; }

        [XmlIgnore]
        public bool? IsAdmin { get; set; }
        public string UserLogin { get; set; }

        [XmlIgnore]
        public byte[] UserPassword { get; set; }
        [XmlIgnore]
        public string Salt { get; set; }

        [XmlIgnore]
        public virtual ICollection<Product> Products { get; set; }
        [XmlIgnore]
        public virtual ICollection<Report> Reports { get; set; }
        [XmlIgnore]
        public virtual ICollection<UsersDatum> UsersData { get; set; }
        [XmlIgnore]
        public virtual ICollection<UsersParam> UsersParams { get; set; }
    }
}
