using Kanakku.Domain.Attachment;
using Kanakku.Domain.Inventory;
using Kanakku.Domain.Lookup;

namespace Kanakku.Domain.User;

public class Employee : DomainBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfJoining { get; set; }
    public string Email { get; set; }
    public string PhoneNumber1 { get; set; }
    public string PhoneNumber2 { get; set; }
    public string AddressLineOne { get; set; }
    public int? DistrictId { get; set; }
    public int? StateId { get; set; }
    public string Pincode { get; set; }
    public string EpfRegNo { get; set; }
    public string EsiRegNo { get; set; }
    public int? DpImageId { get; set; }
    public int? IdProofImageId { get; set; }

    public LookupDetail State { get; set; }
    public LookupDetail District { get; set; }
    public BinaryResource DisplayPicture { get; set; }
    public BinaryResource IdProof { get; set; }
    public List<WorkHistory> WorkHistories { get; set; }
}
