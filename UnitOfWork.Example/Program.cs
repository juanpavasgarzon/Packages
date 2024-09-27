using Pavas.Patterns.UnitOfWork.Contracts;
using Pavas.Patterns.UnitOfWork.DependencyInjection;
using Pavas.Runtime.TenantContext;
using Pavas.Runtime.TenantContext.DependencyInjection;
using UnitOfWork.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.AddTenantContext([
    new Tenant
    {
        Id = "Default",
        Name = "Default",
        Connection = "Host=localhost;Database=UnitOfWork;Username=root;Password=root",
        IsDefault = true,
    },
    new Tenant
    {
        Id = "Tenant1",
        Name = "Tenant1",
        Connection = "Host=localhost;Database=UnitOfWork;Username=root;Password=root",
    },
    new Tenant
    {
        Id = "Tenant2",
        Name = "Tenant2",
        Connection = "Host=localhost;Database=UnitOfWork;Username=root;Password=root",
    }
]);

builder.Services.AddUnitOfWork<UnitOfWorkContext, UnitOfWorkConfigurator>(ServiceLifetime.Scoped);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/{id:int}", async (IUnitOfWork unitOfWork, int id) =>
{
    var repository = await unitOfWork.GetRepositoryAsync<MyEntity>();
    var entity = await repository.GetByIdAsync(id);
    if (entity is null)
        goto Label;

    await repository.RemoveAsync(entity!);
    await unitOfWork.SaveChangesAsync();

    Label:
    var results = await repository.GetAllAsync();
    return results;
});

app.UseTenantContextMiddleware();
app.Run();