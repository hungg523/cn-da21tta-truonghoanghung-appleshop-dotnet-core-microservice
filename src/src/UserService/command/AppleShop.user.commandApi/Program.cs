using AppleShop.user.commandApi.Middleware;
using AppleShop.user.commandApi.MinimalApis;
using AppleShop.user.commandApplication.DependencyInjection.Extension;
using AppleShop.user.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.user.Persistence.DependencyInjection.Extensions;

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
app.UserAction();
app.UseStaticFiles();
app.Run();