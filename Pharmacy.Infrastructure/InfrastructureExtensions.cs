using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Repositories;
using Pharmacy.Infrastructure.Context;
using Pharmacy.Infrastructure.Repositories.DapperRepositories;
using Pharmacy.Infrastructure.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<PharmacyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                ,b =>b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))); // migration sırasında problem çıkmaması için derste eklenmişti.

            services.AddScoped<IMedicineRepository, EFMedicineRepository>();
            services.AddScoped<ICompanyRepository, EFCompanyRepository>();
            //services.AddScoped<ICompanyRepository, DapperCompanyRepository>();

            return services;
        }
    }
}
