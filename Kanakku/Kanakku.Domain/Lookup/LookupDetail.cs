namespace Kanakku.Domain.Lookup;

public class LookupDetail : DomainBase
{
    public int Id { get; set; }
    public int LookupMasterId { get; set; }
    public string Value { get; set; }
    public bool IsActive { get; set; }
    public LookupMaster LookupMaster { get; set; }
}
