using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Cqrs.Contracts;
using Pavas.Patterns.Cqrs.Exceptions;

namespace Pavas.Patterns.Cqrs;

public sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<TValue> DispatchAsync<TQuery, TValue>(TQuery query)
    {
        var queryHandler = serviceProvider.GetService<IQueryHandlerAsync<TQuery, TValue>>();
        if (queryHandler is null)
        {
            var serviceName = typeof(IQueryHandlerAsync<TQuery, TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = await queryHandler.HandleAsync(query);
        return result;
    }

    public async Task<TValue> DispatchAsync<TValue>()
    {
        var queryHandler = serviceProvider.GetService<IQueryHandlerAsync<TValue>>();
        if (queryHandler is null)
        {
            var serviceName = typeof(IQueryHandlerAsync<TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = await queryHandler.HandleAsync();
        return result;
    }

    public TValue Dispatch<TQuery, TValue>(TQuery query)
    {
        var queryHandler = serviceProvider.GetService<IQueryHandler<TQuery, TValue>>();
        if (queryHandler is null)
        {
            var serviceName = typeof(IQueryHandler<TQuery, TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = queryHandler.Handle(query);
        return result;
    }

    public TValue Dispatch<TValue>()
    {
        var queryHandler = serviceProvider.GetService<IQueryHandler<TValue>>();
        if (queryHandler is null)
        {
            var serviceName = typeof(IQueryHandler<TValue>).FullName;
            throw new NotFoundException($"Service {serviceName} Not Found");
        }

        var result = queryHandler.Handle();
        return result;
    }
}