namespace Kanakku.Application.Models.User
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string AddressLineOne { get; set; }
    }
}
