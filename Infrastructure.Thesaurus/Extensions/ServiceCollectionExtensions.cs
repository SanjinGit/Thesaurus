using Business.Thesaurus.Interface;
using Business.Thesaurus.Mapping.Configuration;
using Business.Thesaurus.Repository;
using Common.Thesaurus.Interfaces.Logger;
using Common.Thesaurus.Interfaces.Service;
using DataAccess.Thesaurus.Context;
using Logger.Thesaurus.Manger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Thesaurus.Service;

namespace Infrastructure.Thesaurus.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// IServiceCollection extension which is used to register all project DI interfaces and classes.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionSettings")["ConnectionString"];

            services.AddDataAccessServices(connectionString);
            services.AddServiceLayerClassesAsServices();
            services.AddBusinessLayerClassesAsServices();
            services.AddLoggerServices();
            services.AddAutoMapperAsServices();
        }

        /// <summary>
        /// Database related configuration
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="connectionString">Database connection string</param>
        private static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                // If the connection string is not provided, in-memory database will be used
                services.AddDbContext<ThesaurusDbContext>(opt => opt.UseInMemoryDatabase("ThesaurusDb"));
            }
            else
            {
                services.AddDbContext<ThesaurusDbContext>(options => options.UseSqlServer(connectionString));
            }
            
            services.AddScoped<IThesaurusDbContext, ThesaurusDbContext>();
        }

        /// <summary>
        /// Service.Thesaurus project related configuration
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        private static void AddServiceLayerClassesAsServices(this IServiceCollection services)
        {
            services.AddScoped<IWordService, WordService>();
        }

        /// <summary>
        /// Business.Thesaurus project related configuration
        /// </summary>
        /// <param name="services"></param>
        private static void AddBusinessLayerClassesAsServices(this IServiceCollection services)
        {
            services.AddScoped<IWordRepository, WordRepository>();
        }

        /// <summary>
        /// Logging configuration
        /// </summary>
        /// <param name="services"></param>
        private static void AddLoggerServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// AutoMapper configuration
        /// </summary>
        /// <param name="services"></param>
        private static void AddAutoMapperAsServices(this IServiceCollection services)
        {
            services.AddSingleton(MappingConfiguration.Get());
        }
    }
}
