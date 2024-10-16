using Asst.Products.API.Models;
using Asst.Products.API.Repositories.Interfaces;
using Asst.Products.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asst.Products.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ISellerRepository _sellerRepository;

        public ProductService(IProductRepository repository, ISellerRepository sellerRepository)
        {
            _repository = repository;
            _sellerRepository = sellerRepository;
        }

        public async Task<ApiResponse<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await _repository.GetProductsAsync();
            return new ApiResponse<IEnumerable<Product>>(true, "Products retrieved successfully", products);
        }

        public async Task<ApiResponse<Product>> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<Product>(false, "Product not found", null);
            }
            return new ApiResponse<Product>(true, "Product retrieved successfully", product);
        }

        public async Task<ApiResponse<Product>> AddProductAsync(CreateProduct createProduct)
        {
            var seller = await _sellerRepository.GetSellerByIdAsync(createProduct.SellerId);
            if (seller == null)
            {
                return new ApiResponse<Product>(false, "Seller not found", null);
            }

            var product = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                StockAvailable = createProduct.StockAvailable,
                SellerId = createProduct.SellerId,
            };

            var createdProduct = await _repository.AddProductAsync(product);
            return new ApiResponse<Product>(true, "Product created successfully", createdProduct);
        }

        public async Task<ApiResponse<Product>> UpdateProductAsync(Product product)
        {
            var existingProduct = await _repository.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
            {
                return new ApiResponse<Product>(false, "Product not found", null);
            }

            await _repository.UpdateProductAsync(product);
            return new ApiResponse<Product>(true, "Product updated successfully", product);
        }

        public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
        {
            var existingProduct = await _repository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return new ApiResponse<bool>(false, "Product not found", false);
            }

            await _repository.DeleteProductAsync(id);
            return new ApiResponse<bool>(true, "Product deleted successfully", true);
        }

        public async Task<ApiResponse<bool>> DecrementStockAsync(int id, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<bool>(false, "Product not found", false);
            }

            if (product.StockAvailable < quantity)
            {
                return new ApiResponse<bool>(false, "Insufficient stock available", false);
            }

            await _repository.DecrementStockAsync(id, quantity);
            return new ApiResponse<bool>(true, "Stock decremented successfully", true);
        }

        public async Task<ApiResponse<bool>> AddToStockAsync(int id, int quantity)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return new ApiResponse<bool>(false, "Product not found", false);
            }

            await _repository.AddToStockAsync(id, quantity);
            return new ApiResponse<bool>(true, "Stock added successfully", true);
        }
    }
}
