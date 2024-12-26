using AppleShop.promotion.commandApi.Middleware;
using AppleShop.promotion.commandApi.MinimalApis;
using AppleShop.promotion.commandApplication.DependencyInjection.Extension;
using AppleShop.promotion.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.promotion.Persistence.DependencyInjection.Extensions;

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
app.PromotionAction();
app.Run();