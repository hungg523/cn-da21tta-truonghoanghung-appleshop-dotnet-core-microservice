using AppleShop.Persistence.DependencyInjection.Extensions;
using AppleShop.queryApi.Middleware;
using AppleShop.queryApi.MinimalApis;
using AppleShop.queryApplication.DependencyInjection.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
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