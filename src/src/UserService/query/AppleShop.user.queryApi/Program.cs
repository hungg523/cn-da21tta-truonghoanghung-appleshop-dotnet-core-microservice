using AppleShop.user.Persistence.DependencyInjection.Extensions;
using AppleShop.user.queryApi.Middleware;
using AppleShop.user.queryApi.MinimalApis;
using AppleShop.user.queryApplication.DependencyInjection.Extension;
using AppleShop.user.queryInfrastructure.DependencyInjection.Extension;
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
app.UserAction();
app.UseStaticFiles();
app.Run();