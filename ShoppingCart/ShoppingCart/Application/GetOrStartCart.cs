using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading.Tasks;

namespace ShoppingCart.Application
{
    internal class GetOrStartCart
    {
        private readonly ICartRepository repository;

        internal GetOrStartCart(ICartRepository repository)
        {
            this.repository = repository;
        }

        internal async Task<Result<Cart>> ForCustomerAsync(EntityId customerId)
        {
            var cartOrNothing = await repository.GetOpenCartForCustomerAsync(customerId);

            if (cartOrNothing.HasValue)
                return Result.Ok(cartOrNothing.Value);

            return await StartNewCartAsync(customerId);
        }

        private async Task<Result<Cart>> StartNewCartAsync(EntityId customerId)
        {
            var newCart = new Cart(customerId);

            var addResult = await repository.AddAsync(newCart);
            if (addResult.IsFailure)
                return Result.Fail<Cart>(addResult.Error);

            return Result.Ok(newCart);
        }
    }
}
