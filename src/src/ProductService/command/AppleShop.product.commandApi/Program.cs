using AppleShop.product.commandApi.Middleware;
using AppleShop.product.commandApi.MinimalApis;
using AppleShop.product.commandApplication.DependencyInjection.Extension;
using AppleShop.product.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.product.Persistence.DependencyInjection.Extensions;

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
app.ProductAction();
app.ProductImageAction();
app.ColorAction();
app.UseStaticFiles();
app.Run();