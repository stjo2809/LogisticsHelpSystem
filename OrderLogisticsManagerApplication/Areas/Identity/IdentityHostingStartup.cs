using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using OrderLogisticsManagerApplication.Data;

[assembly: HostingStartup(typeof(OrderLogisticsManagerApplication.Areas.Identity.IdentityHostingStartup))]
namespace OrderLogisticsManagerApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApplicationIdentityContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationIdentityContext>();
            });
        }
    }
}