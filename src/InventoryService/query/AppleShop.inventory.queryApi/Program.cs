using AppleShop.inventory.Persistence.DependencyInjection.Extensions;
using AppleShop.inventory.queryApi.Middleware;
using AppleShop.inventory.queryApi.MinimalApis;
using AppleShop.inventory.queryApplication.DependencyInjection.Extension;
using AppleShop.inventory.queryInfrastructure.DependencyInjection.Extension;

var builder = WebApplication.CreateBuilder(args);
HttpClientHandler handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};

HttpClient client = new HttpClient(handler);
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
app.InventoryAction();
app.Run();