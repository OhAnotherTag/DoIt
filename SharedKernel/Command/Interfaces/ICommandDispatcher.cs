using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Command.Interfaces
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command, CancellationToken token = default)
            where TCommand : class, ICommand;
    }
}