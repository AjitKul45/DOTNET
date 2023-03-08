using System;
using System.Collections.Generic;

namespace CS_Assessment0._1.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            Consultants = new HashSet<Consultant>();
            Investments = new HashSet<Investment>();
            Leaves = new HashSet<Leave>();
            Salaries = new HashSet<Salary>();
            SalarySlips = new HashSet<SalarySlip>();
        }

        public int UserId { get; set; }
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Type { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int? AccNo { get; set; }

        public virtual AspNetUser? IdNavigation { get; set; }
        public virtual ICollection<Consultant> Consultants { get; set; }
        public virtual ICollection<Investment> Investments { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }
    }
}
