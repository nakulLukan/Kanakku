namespace Kanakku.Application.Contracts.Essential
{
    public interface IPermissionService
    {
        public Task<bool> GetStoragePermission();
    }
}
