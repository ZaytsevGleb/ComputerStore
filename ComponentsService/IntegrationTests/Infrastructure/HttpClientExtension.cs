using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ComputerStore.Services.CS.IntegrationTests.Infrastructure;

public static class HttpClientExtension
{
    public static ByteArrayContent ConvertToByteArray<T>(this T dto)
    {
        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return byteContent;
    }

    public static async Task<T?> ConvertToDto<T>(this HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();
        if (stringContent is null) return default;

        var dto = JsonConvert.DeserializeObject<T>(stringContent);

        return dto ?? default;
    }
}