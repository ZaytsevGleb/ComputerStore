using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IProductsService
{
    Task<ProductModel> GetProductAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<ProductModel>> GetProductsAsync(string? title, CancellationToken ct);
    Task<ProductModel> CreateProductAsync(ProductModel productModel, CancellationToken ct);
    Task<ProductModel> UpdateProductAsync(Guid id, ProductModel productModel, CancellationToken ct);
    Task DeleteProductAsync(Guid id, CancellationToken ct);
}
