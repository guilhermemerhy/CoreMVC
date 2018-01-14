using Core.Data;
using Core.Data.Repository;
using Core.Data.UwO;
using Core.Domain.Command.Handlers;
using Core.Domain.Repository;
using Core.Domain.UwO;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppDataContext, AppDataContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IDependentRepository, DependentRepository>();

            services.AddTransient<EmployeeCommandHandler, EmployeeCommandHandler>();

        }
    }
}
