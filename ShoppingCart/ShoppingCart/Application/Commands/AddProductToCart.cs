using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Commands
{
    public class AddProductToCart : ICommand
    {
        public AddProductToCart(EntityId customerId, Product product, Quantity quantity)
        {
            CustomerId = customerId;
            Product = product;
            Quantity = quantity;
        }

        public EntityId CustomerId { get; }
        public Product Product { get; }
        public Quantity Quantity { get; }
    }

    public class AddProductToCartHandler : ICommandHandler<AddProductToCart>
    {
        private readonly ICartRepository repository;

        public AddProductToCartHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> HandleAsync(AddProductToCart command, CancellationToken cancellationToken)
        {
            var result = await new GetOrStartCart(repository).ForCustomerAsync(command.CustomerId)
                .OnSuccess(cart => cart.CanAdd(command.Product, command.Quantity)
                    .OnSuccess(() => cart.Add(command.Product, command.Quantity))
                    .OnSuccess(() => repository.UpdateAsync(cart)));

            return result;
        }
    }
}
