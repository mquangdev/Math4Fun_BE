using Math4FunBackedn.MapperPro;

namespace Math4FunBackedn.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
           //services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
