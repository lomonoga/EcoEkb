using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace EcoEkb.Backend.Api;

public static class ConfigureServices
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddCors();
    }
    
    // Adding jwt Tokens to Swagger Requests 
    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EcoEkb"
                });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // pick comments from classes, including controller summary comments
            swagger.IncludeXmlComments(xmlPath); 

            swagger.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer ‘token‘"
                });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}