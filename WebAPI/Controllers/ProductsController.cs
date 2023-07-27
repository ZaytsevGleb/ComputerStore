using AutoMapper;
using BusinessLogic.Common.Exceptions;
using BusinessLogic.Common.Interfaces;
using BusinessLogic.Products.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using static Microsoft.AspNetCore.Http.StatusCodes;

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

    [HttpGet("{guid:id}", Name = "GetProduct")]
    [ProducesResponseType(Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<ProductDto>> GetProductAsync(Guid id)
    {
        var model = await _productsService.GetProductAsync(id);
        return Ok(_mapper.Map<ProductDto>(model));
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<ProductDto>))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync([FromQuery] ProductSearchDto searchDto)
    {
        var models = await _productsService.GetProductsAsync(searchDto.Title);
        return Ok(models.Select(_mapper.Map<ProductDto>));
    }

    [HttpPost(Name = "CreateProduct")]
    [ProducesResponseType(Status201Created, Type = typeof(ProductDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<ProductDto>> CreateProductAsync(ProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.CreateProductAsync(_mapper.Map<ProductModel>(dto));
        return Created($"api/products/{model.Id}", _mapper.Map<ProductDto>(model));
    }

    [HttpPut("{guid:id}", Name = "UpdateProduct")]
    [ProducesResponseType(Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<ProductDto>> UpdateProductAsync(Guid id, ProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);

        var model = await _productsService.UpdateProductAsync(id, _mapper.Map<ProductModel>(dto));
        return Ok(_mapper.Map<ProductDto>(model));
    }

    [HttpDelete("{guid:id}", Name = "DeleteProduct")]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> DeleteProductAsync(Guid id)
    {
        await _productsService.DeleteProductAsync(id);
        return NoContent();
    }
}
