using Pavas.Patterns.Context.Contracts;
using Pavas.Patterns.Context.DependencyInjection;
using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.DependencyInjection;
using UnitOfWork.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScopedContext<TenantContext>();

builder.Services.AddUnitOfWork<UnitOfWorkContext, UnitOfWorkConfigurator>(ServiceLifetime.Scoped);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IServiceProvider provider) =>
{
    var factory = provider.GetRequiredService<IContextFactory<TenantContext>>();
    factory.Construct(new TenantContext { TenantId = "MyTenant" });

    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
    var repository = unitOfWork.GetRepository<MyEntity>();
    var result = repository.AddAsync(new MyEntity());
    unitOfWork.SaveChangesAsync();
    return result;
}).WithName("GetWeatherForecast").WithOpenApi();

app.Run();