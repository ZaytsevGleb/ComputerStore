using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly IValidator<ProductDto> _validator;
    private readonly IMapper _mapper;

    public ProductsController(
        IProductsService productsService,
        IValidator<ProductDto> validator,
        IMapper mapper)
    {
        _productsService = productsService;
        _validator = validator;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}", Name = "GetProduct")]
    public async Task<ProductDto> GetProductAsync(Guid id, CancellationToken ct)
    {
        var model = await _productsService.GetProductAsync(id, ct);
        return _mapper.Map<ProductDto>(model);
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<ProductDto>> GetProductsAsync([FromQuery] ProductSearchDto searchDto, CancellationToken ct)
    {
        var models = await _productsService.GetProductsAsync(searchDto.Title, ct);
        return _mapper.Map<IEnumerable<ProductDto>>(models);
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<ProductDto> CreateProductAsync(ProductDto dto, CancellationToken ct)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.CreateProductAsync(_mapper.Map<ProductModel>(dto), ct);
        return _mapper.Map<ProductDto>(model);
    }

    [HttpPut("{id:guid}", Name = "UpdateProduct")]
    public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto, CancellationToken ct)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.UpdateProductAsync(id, _mapper.Map<ProductModel>(dto), ct);
        return _mapper.Map<ProductDto>(model);
    }

    [HttpDelete("{id:guid}", Name = "DeleteProduct")]
    public async Task DeleteProductAsync(Guid id, CancellationToken ct)
    {
        await _productsService.DeleteProductAsync(id, ct);
    }
}
