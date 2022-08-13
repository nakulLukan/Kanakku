namespace Kanakku.Domain.Attachment;

public class BinaryResource : DomainBase
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
}
