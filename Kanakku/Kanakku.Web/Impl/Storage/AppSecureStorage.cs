using Blazored.LocalStorage;
using Kanakku.Application.Contracts.Storage;

namespace Kanakku.UI.Impl.Storage;

public class AppSecureStorage : IAppSecureStorage
{
    readonly ILocalStorageService _localStorage;
    public AppSecureStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public T Get<T>(string key)
    {
        var val = _localStorage.GetItemAsync<string>(key).Result;
        if (val == null)
        {
            return default(T);
        }

        return System.Text.Json.JsonSerializer.Deserialize<T>(val);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var val = await _localStorage.GetItemAsync<string>(key);
        if (val == null)
        {
            return default(T);
        }

        return System.Text.Json.JsonSerializer.Deserialize<T>(val);
    }

    public void Set<TValue>(string key, TValue value)
    {
        _localStorage.SetItemAsync<string>(key, System.Text.Json.JsonSerializer.Serialize(value));
    }

    public async Task SetAsync<TValue>(string key, TValue value)
    {
        await _localStorage.SetItemAsync<string>(key, System.Text.Json.JsonSerializer.Serialize(value));
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public void RemoveAll()
    {
        throw new NotImplementedException();
    }
}