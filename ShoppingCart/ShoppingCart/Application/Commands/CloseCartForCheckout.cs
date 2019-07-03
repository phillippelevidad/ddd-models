using Core;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Commands
{
    public class CloseCartForCheckout : ICommand
    {
        public CloseCartForCheckout(EntityId customerId)
        {
            CustomerId = customerId;
        }

        public EntityId CustomerId { get; }
    }

    public class CloseCartForCheckoutHandler : ICommandHandler<CloseCartForCheckout>
    {
        private readonly ICartRepository repository;

        public CloseCartForCheckoutHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> HandleAsync(CloseCartForCheckout command, CancellationToken cancellationToken)
        {
            var result = await new GetOrStartCart(repository).ForCustomerAsync(command.CustomerId)
                .OnSuccess(cart => cart.CanCloseForCheckout()
                    .OnSuccess(() => cart.CloseForCheckout()));

            return result;
        }
    }
}
