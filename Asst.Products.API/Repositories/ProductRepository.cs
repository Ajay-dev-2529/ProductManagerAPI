using Asst.Products.API.Data;
using Asst.Products.API.Models;
using Asst.Products.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Asst.Products.API.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.Seller).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Seller).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecrementStockAsync(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (quantity <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
                }

                if (product.StockAvailable < quantity)
                {
                    throw new InvalidOperationException("Insufficient stock available.");
                }

                product.StockAvailable -= quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddToStockAsync(int id, int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (quantity <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
                }

                product.StockAvailable += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}

