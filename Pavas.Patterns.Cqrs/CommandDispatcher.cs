using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;
using Pavas.Patterns.Cqrs.Exceptions;

namespace Pavas.Patterns.Cqrs;

public sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<TValue> DispatchAsync<TCommand, TValue>(TCommand command)
    {
        var commandHandler = serviceProvider.GetService<ICommandHandlerAsync<TCommand, TValue>>();
        if (commandHandler is null)
        {
            var serviceName = typeof(ICommandHandlerAsync<TCommand, TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = await commandHandler.HandleAsync(command);
        return result;
    }

    public async Task DispatchAsync<TCommand>(TCommand command)
    {
        var commandHandler = serviceProvider.GetService<ICommandHandlerAsync<TCommand>>();
        if (commandHandler is null)
        {
            var serviceName = typeof(ICommandHandlerAsync<TCommand>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        await commandHandler.HandleAsync(command);
    }

    public TValue Dispatch<TCommand, TValue>(TCommand command)
    {
        var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand, TValue>>();
        if (commandHandler is null)
        {
            var serviceName = typeof(ICommandHandler<TCommand, TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = commandHandler.Handle(command);
        return result;
    }

    public void Dispatch<TCommand>(TCommand command)
    {
        var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand>>();
        if (commandHandler is null)
        {
            var serviceName = typeof(ICommandHandler<TCommand>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        commandHandler.Handle(command);
    }
}