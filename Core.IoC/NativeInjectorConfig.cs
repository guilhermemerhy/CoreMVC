using AutoMapper;
using Core.Data;
using Core.Data.Cache;
using Core.Data.Repository;
using Core.Data.UwO;
using Core.Domain.Cache;
using Core.Domain.Command.Handlers;
using Core.Domain.Repository;
using Core.Domain.UwO;
using Core.Infra.CrossCutting.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppDataContext, AppDataContext>();

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IDependentRepository, DependentRepository>();
            services.AddTransient<ICache, Cache>();

            // Domain - Commands
            services.AddTransient<EmployeeCommandHandler, EmployeeCommandHandler>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();

        }
    }
}
