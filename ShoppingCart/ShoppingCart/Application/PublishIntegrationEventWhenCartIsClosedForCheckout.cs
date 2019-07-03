using Core;
using CSharpFunctionalExtensions;
using ShoppingCart.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCart.Application
{
    public class PublishIntegrationEventWhenCartIsClosedForCheckout : IDomainEventHandler<CartClosedForCheckout>
    {
        public Task<Result> HandleAsync(CartClosedForCheckout domainEvent, CancellationToken cancellationToken)
        {
            // TODO: publish an integration event to start the checkout process
            throw new NotImplementedException();
        }
    }
}
