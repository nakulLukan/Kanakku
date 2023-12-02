using Kanakku.Application.Contracts.Storage;

namespace Kanakku.App.Impl.Storage;

public class AppSecureStorage : IAppSecureStorage
{
    public T Get<T>(string key)
    {
        var val = SecureStorage.Default.GetAsync(key).Result;
        if (val == null)
        {
            return default(T);
        }

        return System.Text.Json.JsonSerializer.Deserialize<T>(val);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var val = await SecureStorage.Default.GetAsync(key);
        if (val == null)
        {
            return default(T);
        }

        return System.Text.Json.JsonSerializer.Deserialize<T>(val);
    }

    public void Set<TValue>(string key, TValue value)
    {
        SecureStorage.Default.SetAsync(key, System.Text.Json.JsonSerializer.Serialize(value)).Wait();
    }

    public async Task SetAsync<TValue>(string key, TValue value)
    {
        await SecureStorage.Default.SetAsync(key, System.Text.Json.JsonSerializer.Serialize(value));
    }

    public void Remove(string key)
    {
        SecureStorage.Default.Remove(key);
    }

    public void RemoveAll()
    {
        SecureStorage.Default.RemoveAll();
    }
}