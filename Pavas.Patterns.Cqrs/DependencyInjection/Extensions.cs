using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;

namespace Pavas.Patterns.Cqrs.DependencyInjection;

public static class Extensions
{
    public static void AddCommands(this ServiceCollection serviceCollection)
    {
        var commands = new List<Assignable>();
        commands.AddRange(HandlerExtractor.Get(typeof(ICommandHandler<>)));
        commands.AddRange(HandlerExtractor.Get(typeof(ICommandHandlerAsync<>)));
        commands.AddRange(HandlerExtractor.Get(typeof(ICommandHandler<,>)));
        commands.AddRange(HandlerExtractor.Get(typeof(ICommandHandlerAsync<,>)));

        serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
        commands.ForEach(command => serviceCollection.AddScoped(command.Interface!, command.Type));
    }

    public static void AddQueries(this ServiceCollection serviceCollection)
    {
        var queries = new List<Assignable>();
        queries.AddRange(HandlerExtractor.Get(typeof(IQueryHandler<>)));
        queries.AddRange(HandlerExtractor.Get(typeof(IQueryHandlerAsync<>)));
        queries.AddRange(HandlerExtractor.Get(typeof(IQueryHandler<,>)));
        queries.AddRange(HandlerExtractor.Get(typeof(IQueryHandlerAsync<,>)));

        serviceCollection.AddScoped<IQueryDispatcher, QueryDispatcher>();
        queries.ForEach(query => serviceCollection.AddScoped(query.Interface!, query.Type));
    }
}