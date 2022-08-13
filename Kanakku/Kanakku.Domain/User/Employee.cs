using Kanakku.Domain.Attachment;
using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;

namespace Kanakku.Domain.User;

public class Employee : DomainBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int DistrictId { get; set; }
    public int StateId { get; set; }
    public string Pincode { get; set; }
    public string AddressLineOne { get; set; }
    public int? ImageId { get; set; }

    public LookupDetail State { get; set; }
    public LookupDetail District { get; set; }
    public BinaryResource Image { get; set; }
    public List<WorkHistory> WorkHistories { get; set; }
}
