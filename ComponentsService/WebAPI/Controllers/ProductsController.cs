using AutoMapper;
using ComputerStore.Services.CS.Api.Constants;
using ComputerStore.Services.CS.BusinessLogic.Exceptions;
using ComputerStore.Services.CS.BusinessLogic.Interfaces;
using ComputerStore.Services.CS.BusinessLogic.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route(ApiConstants.Products)]
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

    [HttpGet(ApiConstants.Id)]
    public async Task<ProductDto> GetProductAsync(Guid id)
    {
        var model = await _productsService.GetProductAsync(id);
        return _mapper.Map<ProductDto>(model);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> GetProductsAsync([FromQuery] ProductSearchDto searchDto)
    {
        var models = await _productsService.GetProductsAsync(searchDto.Title);
        return _mapper.Map<IEnumerable<ProductDto>>(models);
    }

    [HttpPost]
    public async Task<ProductDto> CreateProductAsync(ProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.CreateProductAsync(_mapper.Map<ProductModel>(dto));
        return _mapper.Map<ProductDto>(model);
    }

    [HttpPut(ApiConstants.Id)]
    public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.UpdateProductAsync(id, _mapper.Map<ProductModel>(dto));
        return _mapper.Map<ProductDto>(model);
    }

    [HttpDelete(ApiConstants.Id)]
    public async Task DeleteProductAsync(Guid id)
    {
        await _productsService.DeleteProductAsync(id);
    }
}
