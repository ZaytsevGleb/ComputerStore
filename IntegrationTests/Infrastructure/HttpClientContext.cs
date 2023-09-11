using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

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

        return response.IsSuccessStatusCode ? await ConvertResponseToDto<T>(response) : default;
    }

    public async Task<IEnumerable<T>?> GetAllAsync<T>(string path, string title)
    {
        var response = await _httpClient.GetAsync($"{path}?title={title}");

        return response.IsSuccessStatusCode ? await ConvertResponseToDto<IEnumerable<T>>(response) : default;
    }

    public async Task<T?> CreateAsync<T>(string path, T dto)
    {
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync(path, byteContent);

        return response.IsSuccessStatusCode ? await ConvertResponseToDto<T>(response) : default;
    }

    public async Task<T?> UpdateAsync<T>(string path, Guid id, T dto)
    {
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PutAsync($"{path}/{id}", byteContent);

        return response.IsSuccessStatusCode ? await ConvertResponseToDto<T>(response) : default;
    }

    public async Task DeleteAsync(string path, Guid id)
    {
        await _httpClient.DeleteAsync($"{path}/{id}");
    }

    private async Task<T?> ConvertResponseToDto<T>(HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();
        if (stringContent is null)
        {
            return default;
        }

        var dto = JsonConvert.DeserializeObject<T>(stringContent);

        return dto ?? default;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
