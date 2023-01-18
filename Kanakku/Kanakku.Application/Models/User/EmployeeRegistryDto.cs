namespace Kanakku.Application.Models.User
{
    public class EmployeeRegistryDto
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public string EmpName { get; set; }
        public int EmpCode { get; set; }
        public DateTime SalaryMonth { get; set; }
        public int DaysPresent { get; set; }
        public float Salary { get; set; }
        public float? Bonus { get; set; }
    }
}
