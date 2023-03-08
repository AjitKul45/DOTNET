using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class Leave
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? LeaveType { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public bool? Status { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}
