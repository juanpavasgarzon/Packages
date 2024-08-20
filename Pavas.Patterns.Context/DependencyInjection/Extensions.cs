using Microsoft.Extensions.DependencyInjection;
using Pavas.Patterns.Context.Contracts;

namespace Pavas.Patterns.Context.DependencyInjection;

public static class Extensions
{
    public static void AddContext<TContext>(this IServiceCollection serviceCollection) where TContext : class
    {
        serviceCollection.AddTransient<IContextFactory<TContext>, ContextFactory<TContext>>();
        serviceCollection.AddScoped<IContextProvider<TContext>, ContextProvider<TContext>>();
    }
}