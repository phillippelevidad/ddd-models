using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading.Tasks;

namespace ShoppingCart.Application
{
    public interface ICartRepository
    {
        Task<Result> AddAsync(Cart cart);

        Task<Maybe<Cart>> GetOpenCartForCustomerAsync(EntityId customerId);

        Task<Result> UpdateAsync(Cart cart);
    }
}
