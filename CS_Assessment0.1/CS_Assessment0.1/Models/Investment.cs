using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class Investment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PfAmount { get; set; }
        public int? InsuranceAmount { get; set; }
        public DateTime? Date { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}
