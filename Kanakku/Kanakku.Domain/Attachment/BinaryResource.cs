namespace Kanakku.Domain.Attachment;

public class BinaryResource : DomainBase
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string FileFullName { get; set; }
    public byte[] Data { get; set; }
}
