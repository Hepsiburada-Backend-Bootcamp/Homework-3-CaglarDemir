using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pharmacy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAPI.Test
{
    public class PharmacyApiFactory : WebApplicationFactory<TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var context = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<PharmacyDbContext>));
                services.Remove(context);
            });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TestStartup>();
                });
        }
    }
}
