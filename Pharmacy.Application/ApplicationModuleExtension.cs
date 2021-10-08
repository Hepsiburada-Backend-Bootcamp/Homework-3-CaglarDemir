using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Infrastructure;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.CompanyApp;
using Pharmacy.Application.MedicineApp;

namespace Pharmacy.Application
{
    public static class ApplicationModuleExtension
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureModuleDb(configuration);
            services.AddInfrastructureModule(configuration);
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMedicineService, MedicineService>();
            return services;
        }
        public static IServiceCollection AddApplicationModuleTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureModuleDbTest(configuration);
            services.AddInfrastructureModule(configuration);
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMedicineService, MedicineService>();
            return services;
        }
    }
}
