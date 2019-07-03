using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task<Result> HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
