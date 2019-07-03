using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
