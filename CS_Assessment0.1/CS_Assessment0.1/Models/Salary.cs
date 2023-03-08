using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class Salary
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BasicPay { get; set; }
        public int? Hra { get; set; }
        public int? Ta { get; set; }
        public int? Da { get; set; }
        public int? Tds { get; set; }
        public int? Overtime { get; set; }
        public int? WeekendWorked { get; set; }
        public bool? Status { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}
