using AppleShop.cart.commandApi.Middleware;
using AppleShop.cart.commandApi.MinimalApis;
using AppleShop.cart.commandApplication.DependencyInjection.Extension;
using AppleShop.cart.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.cart.Persistence.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.CartAction();
app.UseStaticFiles();
app.Run();