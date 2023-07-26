using AutoMapper;
using BusinessLogic.Common.Exceptions;
using BusinessLogic.Common.Interfaces;
using BusinessLogic.Common.Services;
using BusinessLogic.Products.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace BusinessLogic.Products.Services;

internal sealed class ProductService : IProductsService
{
    private readonly IRepository<Product> _repository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;

    public ProductService(
        IRepository<Product> repository,
        IDateTimeService dateTimeService,
        IMapper mapper)
    {
        _repository = repository;
        _dateTimeService = dateTimeService;
        _mapper = mapper;
    }

    public async Task<ProductModel> GetProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);
        if (product == null)
        {
            throw new NotFoundException("");
        }

        return _mapper.Map<ProductModel>(product);
    }

    public Task<List<ProductModel>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductModel> CreateProductAsync(ProductModel product)
    {
        var existProducts = await _repository.FindAsync(x => x.Title == product.Title);
        if (existProducts != null)
        {
            throw new BadRequestException("");
        }

        var createdProduct = await _repository.CreateAsync(_mapper.Map<Product>(product));
        return _mapper.Map<ProductModel>(createdProduct);
    }

    public async Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product)
    {
        var existProduct = await _repository.GetAsync(id);
        if (existProduct == null)
        {
            throw new NotFoundException("");
        }

        existProduct.Title = product.Title;
        existProduct.Amount = product.Amount;
        existProduct.Price = product.Price;
        existProduct.Description = product.Description;
        existProduct.Type = product.Type;
        existProduct.ModifiedDate = _dateTimeService.UtcNow;

        var updatedProduct = await _repository.UpdateAsync(existProduct);
        return _mapper.Map<ProductModel>(updatedProduct);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);

        if (product == null)
            throw new BadRequestException("");

        await _repository.DeleteAsync(id);
    }
}
