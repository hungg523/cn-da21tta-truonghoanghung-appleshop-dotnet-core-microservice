using AppleShop.productCategory.commandApi.Middleware;
using AppleShop.productCategory.commandApi.MinimalApis;
using AppleShop.productCategory.commandApplication.DependencyInjection.Extension;
using AppleShop.productCategory.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.productCategory.Persistence.DependencyInjection.Extensions;

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
app.CategoryAction();
app.ProductAction();
app.ProductImageAction();
app.UseStaticFiles();
app.Run();