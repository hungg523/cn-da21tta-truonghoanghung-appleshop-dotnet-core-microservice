using AppleShop.category.commandApi.Middleware;
using AppleShop.category.commandApi.MinimalApis;
using AppleShop.category.commandApplication.DependencyInjection.Extension;
using AppleShop.category.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.category.Persistence.DependencyInjection.Extensions;

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
app.UseStaticFiles();
app.Run();