using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IProductsService
{
    Task<ProductModel> GetProductAsync(Guid id);
    Task<IEnumerable<ProductModel>> GetProductsAsync(string? title);
    Task<ProductModel> CreateProductAsync(ProductModel productModel);
    Task<ProductModel> UpdateProductAsync(Guid id, ProductModel productModel);
    Task DeleteProductAsync(Guid id);
}
