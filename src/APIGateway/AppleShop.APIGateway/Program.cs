using AppleShop.Share.Constant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection(Const.AUTHEN_KEY).Value!);
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OptionalAuth", policy =>
    {
        policy.RequireAssertion(context =>
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext != null)
            {
                var excludedPaths = new[]
                {
                    "/products/get-all-products",
                    "/products/get-all-products-actived"
                };

                return excludedPaths.Any(path => httpContext.Request.Path.StartsWithSegments(path));
            }

            return false;
        });
    });
});
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection(Const.REVERSE_PROXY));
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();