using ArmyStore.Assembler;
using ArmyStore.Configurations;
using ArmyStore.Connections;
using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Interfaces;
using ArmyStore.Mappers;
using ArmyStore.Repositories;

namespace ArmyStore.Extentions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureConfiguration(this IServiceCollection services, ConfigurationManager config)
        {
            services.Configure<DataBaseConfigurations>(config.GetSection("ConnectionStrings"));

            services.AddScoped<IDapperContext, DapperContext>();

            // Assemblers
            services.AddScoped<IProductAssembler, ProductAssembler>();

            // Repositories
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<ProductMetadata>, ProductMetadataRepository>();

            // DataModel Mappers
            services.AddScoped<IMapper<Product, ProductModel>, ProductDataModelMapper>();
            services.AddScoped<IMapper<ProductMetadata, ProductMetadataModel>, ProductMetadataModelMapper>();
            return services;
        }
    }
}