using Context;
using Context.ContextCases;
using Context.Endpoints;
using Pavas.Patterns.Context.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var singletonContext = SingletonContext.Initialize();

builder.Services.AddSingletonContext(singletonContext);
builder.Services.AddScopedContext<ScopedContext>();
builder.Services.AddTransientContext<TransientContext>();
builder.Services.AddScoped<ScopedContextMiddleware>();
builder.Services.AddScoped<TransientContextMiddleware>();

var app = builder.Build();

app.MapSingletonEndpoints();
app.MapScopedEndpoints();
app.MapTransientEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ScopedContextMiddleware>();
app.UseMiddleware<TransientContextMiddleware>();
app.UseHttpsRedirection();
app.Run();