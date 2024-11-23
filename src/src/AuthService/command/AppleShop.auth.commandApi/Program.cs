using AppleShop.auth.commandApi.Middleware;
using AppleShop.auth.commandApi.MinimalApis;
using AppleShop.auth.commandApplication.DependencyInjection.Extension;
using AppleShop.auth.commandInfrastructure.DependencyInjection.Extension;
using AppleShop.auth.Persistence.DependencyInjection.Extensions;

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
app.AuthAction();
app.UseStaticFiles();
app.Run();