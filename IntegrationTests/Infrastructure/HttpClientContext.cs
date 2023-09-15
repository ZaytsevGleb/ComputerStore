namespace IntegrationTests.Infrastructure;

public sealed class HttpClientContext
{
    private readonly HttpClient _httpClient;

    public HttpClientContext(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string path, Guid id)
    {
        var response = await _httpClient.GetAsync($"{path}/{id}");

        return response.IsSuccessStatusCode ? await response.ConvertToDto<T>() : default;
    }

    public async Task<IEnumerable<T>?> GetAllAsync<T>(string path, string title)
    {
        var response = await _httpClient.GetAsync($"{path}?title={title}");

        return response.IsSuccessStatusCode ? await response.ConvertToDto<IEnumerable<T>>() : default;
    }

    public async Task<T?> CreateAsync<T>(string path, T dto)
    {
        var response = await _httpClient.PostAsync(path, dto.ConvertToByteArray());

        return response.IsSuccessStatusCode ? await response.ConvertToDto<T>() : default;
    }

    public async Task<T?> UpdateAsync<T>(string path, Guid id, T dto)
    {
        var response = await _httpClient.PutAsync($"{path}/{id}", dto.ConvertToByteArray());

        return response.IsSuccessStatusCode ? await response.ConvertToDto<T>() : default;
    }

    public async Task DeleteAsync(string path, Guid id)
    {
        await _httpClient.DeleteAsync($"{path}/{id}");
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
