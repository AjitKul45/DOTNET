namespace CS_Assessment0._1.Models
{
    public class PaySlip
    {
        public UserInfo? user { get; set; }
        public Leave? leave { get; set; }
        public Salary? salary { get; set; }
        public Investment? investment { get; set; }
        public SalarySlip? slip { get; set; }
    }
}
