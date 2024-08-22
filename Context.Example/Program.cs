using Context.Example.ContextCases;
using Context.Example.ContextCases.Contracts;
using Context.Example.ContextCases.Tools;
using Context.Example.Endpoints;
using Context.Example.Middlewares;
using Pavas.Patterns.Context.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IContextInitializer, ContextInitializer>();
builder.Services.AddScopedContext<ScopedContext>();
builder.Services.AddTransientContext<TransientContext>();
builder.Services.AddScoped<ScopedContextMiddleware>();
builder.Services.AddScoped<TransientContextMiddleware>();
builder.Services.AddSingletonContext(new SingletonContext());
builder.Services.AddSingletonContext(provider =>
{
    var initializer = provider.GetRequiredService<IContextInitializer>();
    return initializer.Initialize<SingletonInitializedContext>();
});

var app = builder.Build();

app.MapScopedEndpoints();
app.MapTransientEndpoints();
app.MapSingletonEndpoints();
app.MapSingletonInitializedEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ScopedContextMiddleware>();
app.UseMiddleware<TransientContextMiddleware>();
app.UseHttpsRedirection();
app.Run();