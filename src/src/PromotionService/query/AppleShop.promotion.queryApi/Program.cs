using AppleShop.promotion.Persistence.DependencyInjection.Extensions;
using AppleShop.promotion.queryApi.Middleware;
using AppleShop.promotion.queryApi.MinimalApis;
using AppleShop.promotion.queryApplication.DependencyInjection.Extension;
using AppleShop.promotion.queryInfrastructure.DependencyInjection.Extension;
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
app.PromotionAction();
app.UseStaticFiles();
app.Run();