using Authentication_Key_Policy_Based.Security;
using Authentication_Key_Policy_Based.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Authentication_Key_Policy_Based
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

            builder.Services.AddHttpContextAccessor();

            // Registration code to add out custom Authorization key policy.
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer();

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("ApiKeyPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes([JwtBearerDefaults.AuthenticationScheme]);
                    policy.Requirements.Add(new ApiKeyRequirement());
                });

            builder.Services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();
            // END of Registration code to add out custom Authorization key policy.



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
