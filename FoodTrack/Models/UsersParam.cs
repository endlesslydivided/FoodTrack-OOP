using System;
using System.Collections.Generic;

#nullable disable

namespace FoodTrack.Models
{
    public partial class UsersParam
    {
        public int Id { get; set; }
        public int IdParams { get; set; }
        public DateTime ParamsDate { get; set; }
        public decimal UserWeight { get; set; }
        public int UserHeight { get; set; }

        public virtual User IdParamsNavigation { get; set; }
    }
}
