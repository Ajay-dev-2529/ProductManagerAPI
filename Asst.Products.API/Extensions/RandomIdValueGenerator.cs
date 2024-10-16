using Asst.Products.API.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Asst.Products.API.Extensions
{
    public class RandomIdValueGenerator : ValueGenerator<int>
    {
        private static readonly Random _random = new Random();

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
        {
            // Generate a random 5-digit number for ProductId
            if (entry.Entity is Product)
            {
                return _random.Next(100000, 999999);
            }

            // Generate a random 7-digit number for SellerId
            if (entry.Entity is Seller)
            {
                return _random.Next(1000000, 9999999);
            }

            throw new InvalidOperationException("Unsupported entity type for random ID generation.");
        }
    }
}
