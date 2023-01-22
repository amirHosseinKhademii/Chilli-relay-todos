using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace hot_demo.extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddInMemorySubscriptions();
            services.AddRedisSubscriptions((sp) =>
                ConnectionMultiplexer.Connect("host:port"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")),
                };
            });

            services.Configure<MongoDBSetting>(
                config.GetSection("DataBase"))
                .AddHttpContextAccessor()
                .AddSingleton<IJwtAuthentication>(new JwtAuthentication("this is my custom Secret key for authentication"))
                .AddSingleton<Service>()
                .AddGraphQLServer()
                .AddGlobalObjectIdentification()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddMongoDbPagingProviders()
                .AddAuthorization()
               ;




            return services;
        }
    }
}