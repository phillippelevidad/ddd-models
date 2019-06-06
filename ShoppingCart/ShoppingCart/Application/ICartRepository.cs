using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading.Tasks;

namespace ShoppingCart.Application
{
    public interface ICartRepository
    {
        Task<Maybe<Cart>> GetForCustomerAsync(EntityId customerId);

        Task UpdateAsync(Cart cart);
    }
}
