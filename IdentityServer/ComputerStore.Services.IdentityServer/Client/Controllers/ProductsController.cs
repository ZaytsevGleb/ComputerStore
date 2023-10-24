using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[ApiController]
[Route("api/client")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;

    public ProductsController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /*[HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var client = _clientFactory.CreateClient();
        var token = await client.GetAsync("https://localhost:7001");
    }*/
}
