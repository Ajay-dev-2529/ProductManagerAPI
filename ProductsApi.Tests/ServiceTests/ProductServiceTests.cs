using Asst.Products.API.Models;
using Asst.Products.API.Repositories.Interfaces;
using Asst.Products.API.Services;
using Asst.Products.API.Services.Interfaces;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Tests.ServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ISellerRepository> _mockSellerRepository;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockSellerRepository = new Mock<ISellerRepository>();
            _productService = new ProductService(_mockProductRepository.Object, _mockSellerRepository.Object);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", StockAvailable = 10 },
                new Product { Id = 2, Name = "Product2", StockAvailable = 20 }
            };
            _mockProductRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", StockAvailable = 10 };
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            // Arrange
            var createProduct = new CreateProduct { Name = "Product1", StockAvailable = 10, SellerId = 1 };
            var product = new Product { Id = 1, Name = "Product1", StockAvailable = 10, SellerId = 1 };
            _mockProductRepository.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _productService.AddProductAsync(createProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", StockAvailable = 10 };
            _mockProductRepository.Setup(repo => repo.UpdateProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            await _productService.UpdateProductAsync(product);

            // Assert
            _mockProductRepository.Verify(repo => repo.UpdateProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            // Arrange
            var productId = 1;
            _mockProductRepository.Setup(repo => repo.DeleteProductAsync(productId)).Returns(Task.CompletedTask);

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _mockProductRepository.Verify(repo => repo.DeleteProductAsync(productId), Times.Once);
        }

        [Fact]
        public async Task DecrementStockAsync_ShouldDecrementStock()
        {
            // Arrange
            var productId = 1;
            var quantity = 5;
            _mockProductRepository.Setup(repo => repo.DecrementStockAsync(productId, quantity)).Returns(Task.CompletedTask);

            // Act
            await _productService.DecrementStockAsync(productId, quantity);

            // Assert
            _mockProductRepository.Verify(repo => repo.DecrementStockAsync(productId, quantity), Times.Once);
        }

        [Fact]
        public async Task AddToStockAsync_ShouldAddToStock()
        {
            // Arrange
            var productId = 1;
            var quantity = 5;
            _mockProductRepository.Setup(repo => repo.AddToStockAsync(productId, quantity)).Returns(Task.CompletedTask);

            // Act
            await _productService.AddToStockAsync(productId, quantity);

            // Assert
            _mockProductRepository.Verify(repo => repo.AddToStockAsync(productId, quantity), Times.Once);
        }
    }
}
