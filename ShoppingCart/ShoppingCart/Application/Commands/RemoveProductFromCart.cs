using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Commands
{
    public class RemoveProductFromCart : ICommand
    {
        public RemoveProductFromCart(EntityId customerId, Product product, Quantity quantity)
        {
            CustomerId = customerId;
            Product = product;
            Quantity = quantity;
        }

        public EntityId CustomerId { get; }
        public Product Product { get; }
        public Quantity Quantity { get; }
    }

    public class RemoveProductFromCartHandler : ICommandHandler<RemoveProductFromCart>
    {
        private readonly ICartRepository repository;

        public RemoveProductFromCartHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> HandleAsync(RemoveProductFromCart command, CancellationToken cancellationToken)
        {
            var result = await new GetOrStartCart(repository).ForCustomerAsync(command.CustomerId)
                .OnSuccess(cart => cart.Remove(command.Product, command.Quantity))
                .OnSuccess(cart => repository.UpdateAsync(cart));

            return result;
        }
    }
}
