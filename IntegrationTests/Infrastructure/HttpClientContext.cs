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

    public async Task<T> GetAsync<T>(string path, Guid id)
    {
        var response = await _httpClient.GetAsync($"{path}/{id}");
        if (!response.IsSuccessStatusCode)
            return default!;

        return await ConvertResponseToDto<T>(response);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(string path, string title)
    {
        var response = await _httpClient.GetAsync($"{path}?title={title}");
        if (!response.IsSuccessStatusCode)
            return default!;

        return await ConvertResponseToDto<IEnumerable<T>>(response);
    }

    public async Task<T> CreateAsync<T>(string path, T dto)
    {
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PostAsync(path, byteContent);
        if (!response.IsSuccessStatusCode)
            return default!;

        return await ConvertResponseToDto<T>(response);
    }

    public async Task<T> UpdateAsync<T>(string path, Guid id, T dto)
    {
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _httpClient.PutAsync($"{path}/{id}", byteContent);
        return await ConvertResponseToDto<T>(response);
    }

    public async Task DeleteAsync(string path, Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{path}/{id}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());
    }

    private async Task<T> ConvertResponseToDto<T>(HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();
        if (stringContent == null)
        {
            return default!;
        }

        var dto = JsonConvert.DeserializeObject<T>(stringContent);

        return dto ?? default!;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
