using Asst.Products.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asst.Products.API.Repositories.Interfaces
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetSellersAsync();
        Task<Seller> GetSellerByIdAsync(int id);
        Task<Seller> AddSellerAsync(Seller seller);
        Task UpdateSellerAsync(Seller seller);
    }
}
