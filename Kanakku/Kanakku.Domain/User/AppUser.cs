using Kanakku.Domain.Attachment;
using Microsoft.AspNetCore.Identity;

namespace Kanakku.Domain.User;

public class AppUser : IdentityUser
{
    public int? ImageId { get; set; }
    public bool IsActive { get; set; }
    public BinaryResource Image { get; set; }
}
