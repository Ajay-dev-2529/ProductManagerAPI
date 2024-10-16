using Asst.Products.API.Data;
using Asst.Products.API.Models;
using Asst.Products.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asst.Products.API.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ProductDbContext _context;

        public SellerRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seller>> GetSellersAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers.FindAsync(id);
        }

        public async Task<Seller> AddSellerAsync(Seller seller)
        {
            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            _context.Entry(seller).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
