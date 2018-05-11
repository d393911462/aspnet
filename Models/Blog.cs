using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_mysql.Models
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
    }

    public class Blog
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public JsonObject<List<string>> Tags { get; set; } // Json storage (MySQL 5.7 only)
    }

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options): base(options) 
        {

        }
		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }
    }

    public class SampleData
    {
		public async static Task InitDB(IServiceProvider service)
		{
			using (var serviceScope = service.CreateScope())
			{
			 	var context = serviceScope.ServiceProvider.GetService<MyContext>();
				
				if (context.Database != null&&context.Database.EnsureCreated() )
				{
					try{
						var str = context.Database.GetDbConnection().ConnectionString;

					}
					catch(Exception e)
					{
						var e1 = e;
					}
				 	// Init sample data
					var user = new User { Name = "Yuuko" };
			         context.Add(user);
				 	var blog1 = new Blog
				 	{
				 		Title = "Title #1",
				 		UserId = user.UserId,
				 		Tags = new List<string>() { "ASP.NET Core", "MySQL", "Pomelo" }
				 	};
				 	context.Add(blog1);
				 	var blog2 = new Blog
				 	{
				 		Title = "Title #2",
				 		UserId = user.UserId,
				 		Tags = new List<string>() { "ASP.NET Core", "MySQL" }
				 	};
				 	context.Add(blog2);
                    

				 	// Changing and save json object #1
				 	blog1.Tags.Object.Clear();

				 	// Changing and save json object #2
				 	blog1.Tags.Object.Add("Pomelo");
				}
				 await context.SaveChangesAsync();
			}
		}
    }
}