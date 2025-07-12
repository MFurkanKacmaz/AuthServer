using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using SharedLibrary.Services;

namespace SharedLibrary.Extensions
{
    public static class TokenAuth
    {
        public static void AddTokenAuth(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));
            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions!.SecurityKey),
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = tokenOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();
        }
    }

}
