using Asst.Products.API.Models;

namespace Asst.Products.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<IEnumerable<Product>>> GetProductsAsync();
        Task<ApiResponse<Product>> GetProductByIdAsync(int id);
        Task<ApiResponse<Product>> AddProductAsync(CreateProduct product);
        Task<ApiResponse<Product>> UpdateProductAsync(Product product);
        Task<ApiResponse<bool>> DeleteProductAsync(int id);
        Task<ApiResponse<bool>> DecrementStockAsync(int id, int quantity);
        Task<ApiResponse<bool>> AddToStockAsync(int id, int quantity);
    }
}
