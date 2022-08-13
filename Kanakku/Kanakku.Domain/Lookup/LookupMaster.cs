namespace Kanakku.Domain.Lookup;

public class LookupMaster : DomainBase
{
    public int Id { get; set; }
    public string InternalName { get; set; }
    public bool IsActive { get; set; }

    public List<LookupDetail> LookupDetails { get; set; }
}
