using Kanakku.Application.Contracts.Essential;
using Microsoft.Maui.ApplicationModel;

namespace Kanakku.UI.Impl.Essential;

public class PermissionService : IPermissionService
{
    public async Task<bool> GetStoragePermission()
    {
        PermissionStatus statuswrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
        if (statuswrite != PermissionStatus.Granted)
        {
            AppInfo.ShowSettingsUI();
        }
        return statuswrite == PermissionStatus.Granted;
    }
}
