using Asst.Products.API.Models;

namespace Asst.Products.API.Services.Interfaces
{
    public interface ISellerService
    {
        Task<IEnumerable<Seller>> GetSellersAsync();
        Task<Seller> GetSellerByIdAsync(int id);
        Task<Seller> AddSellerAsync(CreateSeller createSeller);
        Task UpdateSellerAsync(Seller seller);

    }
}
