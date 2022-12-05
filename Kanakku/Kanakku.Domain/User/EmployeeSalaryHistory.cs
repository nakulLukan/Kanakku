namespace Kanakku.Domain.User
{
    public class EmployeeSalaryHistory : DomainBase
    {
        public int Id { get; set; }
        public Guid EmpId { get; set; }
        public DateTime Period { get; set; }
        public float Salary { get; set; }
        public int DaysPresent { get; set; }
        public float? Bonus { get; set; }

        public Employee Employee { get; set; }
    }
}
