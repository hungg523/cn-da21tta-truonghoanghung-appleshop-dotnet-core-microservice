using AppleShop.product.Persistence.DependencyInjection.Extensions;
using AppleShop.product.queryApi.Middleware;
using AppleShop.product.queryApi.MinimalApis;
using AppleShop.product.queryApplication.DependencyInjection.Extension;
using AppleShop.product.queryInfrastructure.DependencyInjection.Extension;
using AppleShop.Share.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
HttpClientHandler handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};

HttpClient client = new HttpClient(handler);
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
app.ProductAction();
app.Run();