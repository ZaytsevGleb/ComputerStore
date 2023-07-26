using BusinessLogic.Products.Models;

namespace BusinessLogic.Common.Interfaces;

public interface IProductsService
{
    Task<ProductModel> GetProductAsync(Guid id);
    Task<List<ProductModel>> GetProductsAsync();
    Task<ProductModel> CreateProductAsync(ProductModel product);
    Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product);
    Task DeleteProductAsync(Guid id);
}
