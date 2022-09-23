namespace Kanakku.Shared;

public static class AppInMemoryStore
{
    public static bool IsLoggedIn { get; set; }
    public static Guid UserId { get; set; }
    public static string Username { get; set; }
}
