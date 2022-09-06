namespace Kanakku.Application.Models.Attachment
{
    public class BinaryResourceDto
    {
        public int Id { get; set; }
        public string LocalPath { get; set; }
        public string Base64String { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FileFullName { get; set; }
    }
}
