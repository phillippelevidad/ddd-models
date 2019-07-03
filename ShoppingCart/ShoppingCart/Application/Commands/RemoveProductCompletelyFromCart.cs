using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Commands
{
    public class RemoveProductCompletelyFromCart : ICommand
    {
        public RemoveProductCompletelyFromCart(EntityId customerId, Product product)
        {
            CustomerId = customerId;
            Product = product;
        }

        public EntityId CustomerId { get; }
        public Product Product { get; }
    }

    public class RemoveProductCompletelyFromCartHandler : ICommandHandler<RemoveProductCompletelyFromCart>
    {
        private readonly ICartRepository repository;

        public RemoveProductCompletelyFromCartHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> HandleAsync(RemoveProductCompletelyFromCart command, CancellationToken cancellationToken)
        {
            var result = await new GetOrStartCart(repository).ForCustomerAsync(command.CustomerId)
                .OnSuccess(cart => cart.Remove(command.Product))
                .OnSuccess(cart => repository.UpdateAsync(cart));

            return result;
        }
    }
}
