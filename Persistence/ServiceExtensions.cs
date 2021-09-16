using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using System;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return services
                .AddSqlClient(configuration)
                .AddRepositories();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUsersRepository, UsersRepository>()
                .AddSingleton<ITodosRepository, TodosRepository>();                
        }

        private static IServiceCollection AddSqlClient(this IServiceCollection services , IConfiguration configuration)
        {
            // var connectionString = configuration.GetSection("ConnectionStrings")["SqlConnectionString"];
            // var connectionStringv1 = configuration.GetSection("ConnectionStrings:SqlConnectionString").value;
            var connectionString = configuration.GetConnectionString("SqlConnectionString");


            //var fluentConnectionStringBuilder = new FluentConnectionStringBuilder();

            return services.AddTransient<ISqlClient>(_ => new SqlClient(connectionString));
        }
    }
}
