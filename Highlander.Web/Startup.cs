using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Helpers;
using System;
using Microsoft.Win32;

namespace Highlander.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Credential.AmazonS3AccessKeyID = Configuration.GetConnectionString("AmazonS3AccessKeyID");
            Credential.AmazonS3Secret = Configuration.GetConnectionString("AmazonS3Secret");
            Credential.AmazonBucket = Configuration.GetConnectionString("AmazonBucket");
            Credential.EmailServer = Configuration.GetConnectionString("EmailServer");
            Credential.EmailUsername = Configuration.GetConnectionString("EmailUsername");
            Credential.EmailPassword = Configuration.GetConnectionString("EmailPassword");
            Credential.Hostname = Configuration.GetConnectionString("Hostname");
        } 

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication();

            services.AddControllersWithViews();
                
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            UpdateDatabase(app);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MySQL AB") == null)
                    {
                        throw new Exception("MySQL is not installed");
                    }

                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        throw new Exception("MySQL login details are incorrect, please check appsettings.json");
                    }
                }
            }
        }
    }
}
