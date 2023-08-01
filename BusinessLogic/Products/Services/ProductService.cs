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
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IRepository<Product> repository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductModel> GetProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);

        return _mapper.Map<ProductModel>(product);
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync(string? title)
    {
        var products = string.IsNullOrEmpty(title)
            ? await _repository.GetByExpression()
            : await _repository.GetByExpression(x => x.Title == title
                || x.Title.Contains(title));

        return _mapper.Map<IEnumerable<ProductModel>>(products);
    }

    public async Task<ProductModel> CreateProductAsync(ProductModel productModel)
    {
        var products = await _repository.GetByExpression(x => x.Title == productModel.Title);
        if (products.Any())
        {
            throw new BadRequestException("This product already exists");
        }

        var createdProduct = await _repository.CreateAsync(_mapper.Map<Product>(productModel));
        return _mapper.Map<ProductModel>(createdProduct);
    }

    public async Task<ProductModel> UpdateProductAsync(Guid id, ProductModel productModel)
    {
        var product = await _repository.GetAsync(id);
        if (product == null)
        {
            _logger.LogError("Product with id: {id} was not found", id);
            throw new NotFoundException(nameof(Product), id);
        }

        var updatedProduct = await _repository.UpdateAsync(_mapper.Map<Product>(productModel));
        return _mapper.Map<ProductModel>(updatedProduct);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);

        if (product == null)
        {
            _logger.LogError("Product with id: {id} was not found", id);
            throw new NotFoundException(nameof(Product), id);
        }

        await _repository.DeleteAsync(product);
    }
}
