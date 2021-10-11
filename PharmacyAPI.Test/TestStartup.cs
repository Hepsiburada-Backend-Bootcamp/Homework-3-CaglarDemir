using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Application;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAPI.Test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationModuleTest(Configuration);
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Test";
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PharmacyDbContext>();

            AddTestData(context);

            base.Configure(app, env);
        }

        private void AddTestData(PharmacyDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                Company company = new();
                company.CompanyName = $"Company {i}";
                context.Companies.Add(company);
                context.SaveChanges();

                for (int j = 0; j < 20; j++)
                {
                    Medicine medicine = new();
                    medicine.CompanyId = company.CompanyId;
                    medicine.Details = $"Details {j}";
                    medicine.ExpirationDate = DateTime.Now.AddYears(j);
                    medicine.Name = $"Medicine {j}";
                    medicine.UnitPrice = (decimal)((j + 1) * 1.2);
                    medicine.UnitsInStock = (j + 1) * 10;

                    context.Medicines.Add(medicine);
                }

                context.SaveChanges();
            }


        }
    }
}
