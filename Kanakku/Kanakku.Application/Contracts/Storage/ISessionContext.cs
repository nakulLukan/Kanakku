namespace Kanakku.Application.Contracts.Storage
{
    public interface ISessionContext
    {
        public Task<string> GetUserId();
    }
}
