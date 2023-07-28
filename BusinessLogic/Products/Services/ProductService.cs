using AutoMapper;
using BusinessLogic.Common.Exceptions;
using BusinessLogic.Common.Interfaces;
using BusinessLogic.Products.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Products.Services;

internal sealed class ProductService : IProductsService
{
    private readonly IRepository<Product> _repository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IRepository<Product> repository,
        IDateTimeService dateTimeService,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _repository = repository;
        _dateTimeService = dateTimeService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductModel> GetProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);
        if (product == null)
        {
            _logger.LogError($"Product with id: {id} was not found");
            throw new NotFoundException(nameof(Product), id);
        }

        return _mapper.Map<ProductModel>(product);
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync(string? title)
    {
        var products = string.IsNullOrEmpty(title)
            ? await _repository.FindAsync()
            : await _repository.FindAsync(x => x.Title == title
                || x.Title.Contains(title));

        return products.Select(_mapper.Map<ProductModel>);
    }

    public async Task<ProductModel> CreateProductAsync(ProductModel productModel)
    {
        var products = await _repository.FindAsync(x => x.Title == productModel.Title);
        if (products.Any())
        {
            throw new BadRequestException("This product already exists");
        }

        var product = new Product
        {
            Title = productModel.Title,
            Price = productModel.Price,
            Description = productModel.Description,
            Type = productModel.Type,
            CreatedDate = _dateTimeService.UtcNow
        };

        await _repository.CreateAsync(product);
        return _mapper.Map<ProductModel>(product);
    }

    public async Task<ProductModel> UpdateProductAsync(Guid id, ProductModel productModel)
    {
        var product = await _repository.GetAsync(id);
        if (product == null)
        {
            _logger.LogError($"Product with id: {id} was not found");
            throw new NotFoundException(nameof(Product), id);
        }

        product.Title = productModel.Title;
        product.Price = productModel.Price;
        product.Description = productModel.Description;
        product.Type = productModel.Type;
        product.ModifiedDate = _dateTimeService.UtcNow;

        var updatedProduct = await _repository.UpdateAsync(product);
        return _mapper.Map<ProductModel>(updatedProduct);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);

        if (product == null)
        {
            _logger.LogError($"Product with id: {id} was not found");
            throw new NotFoundException(nameof(Product), id);
        }

        await _repository.DeleteAsync(product);
    }
}
