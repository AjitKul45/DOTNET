using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class Consultant
    {
        public int CId { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? NoDays { get; set; }
        public int? FixedPay { get; set; }
        public int? DaysWorked { get; set; }
        public int? ExtraHoursWorked { get; set; }
        public int? PerHourPay { get; set; }
        public bool? Status { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}
