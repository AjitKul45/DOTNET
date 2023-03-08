using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class SalarySlip
    {
        public int SlipId { get; set; }
        public int? UserId { get; set; }
        public int? TotalEarnings { get; set; }
        public int? Contribution { get; set; }
        public int? Deduction { get; set; }
        public int? NetAmount { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}
