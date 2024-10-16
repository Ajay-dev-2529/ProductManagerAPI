using Asst.Products.API.Models;
using Asst.Products.API.Repositories.Interfaces;
using Asst.Products.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asst.Products.API.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _repository;

        public SellerService(ISellerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Seller>> GetSellersAsync()
        {
            return await _repository.GetSellersAsync();
        }

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _repository.GetSellerByIdAsync(id);
        }

        public async Task<Seller> AddSellerAsync(CreateSeller createSeller)
        {
            var seller = new Seller
            {
                Name = createSeller.Name,
                ContactInfo = createSeller.ContactInfo
            };
            return await _repository.AddSellerAsync(seller);
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            await _repository.UpdateSellerAsync(seller);
        }
    }
}
