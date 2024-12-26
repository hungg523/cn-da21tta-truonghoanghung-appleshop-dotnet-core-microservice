using AppleShop.category.Persistence.DependencyInjection.Extensions;
using AppleShop.category.queryApi.Middleware;
using AppleShop.category.queryApi.MinimalApis;
using AppleShop.category.queryApplication.DependencyInjection.Extension;
using AppleShop.category.queryInfrastructure.DependencyInjection.Extension;
using AppleShop.Share.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureRequest(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
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