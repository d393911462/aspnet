using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using aspnet_mysql.Models;
using log4net.Repository;
using log4net;
using System.IO;

namespace aspnet_mysql
{
    public class Startup
    {
		public static ILoggerRepository repository { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
			repository = LogManager.CreateRepository("NETCoreRepository");
			log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
    {
			var connstr = Configuration.GetConnectionString("Mysql");
			services.AddDbContext<MyContext>(options => options.UseMySql(Configuration.GetConnectionString("Mysql")));
      
			services.AddMvc();
     
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

			app.UseStaticFiles();
            app.UseMvc(routes =>
            {
				
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
			await SampleData.InitDB(app.ApplicationServices);

        }
    }
}
