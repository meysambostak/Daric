
using Daric.Core.Infrastructure.Commands;

namespace Daric.Core.Contracts.ApplicationServices.Commands;
 
public interface ICommandDispatcher
{
 
  
    Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : class, ICommand;
 
    Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
}
