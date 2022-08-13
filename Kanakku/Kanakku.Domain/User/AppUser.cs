using Kanakku.Domain.Attachment;

namespace Kanakku.Domain.User;

public class AppUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? ImageId { get; set; }

    public BinaryResource Image { get; set; }
}
