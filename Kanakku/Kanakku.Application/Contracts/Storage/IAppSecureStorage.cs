namespace Kanakku.Application.Contracts.Storage;

public interface IAppSecureStorage
{
    TValue Get<TValue>(string key);
    Task<TValue> GetAsync<TValue>(string key);
    void Set<TValue>(string key, TValue value);
    Task SetAsync<TValue>(string key, TValue value);
    void Remove(string key);
    void RemoveAll();
}
