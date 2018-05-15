using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnet_mysql.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aspnetmysql.Controllers
{
    public class VersionController : Controller
    {
    //      static string connection = @"Server=cd-cdb-2b19h0iv.sql.tencentcdb.com;Port=63990;database=ef;uid=root;pwd=dqx930215115;";
    //     static DbContextOptions<MyContext> dbContextOption = new DbContextOptions<MyContext>();
    //     static DbContextOptionsBuilder<MyContext> dbContextOptionBuilder = new DbContextOptionsBuilder<MyContext>(dbContextOption);
       MyContext _Context;
	    // = new MyContext(dbContextOptionBuilder.UseSqlServer(connection).Options);
		public VersionController(MyContext context)
		{
			_Context = context;
		}
        public string Index()
		{
			return "0.0.8";
		}
        public string getUserName()
		{
			return _Context.Users.Find(1).Name;
			
		}
	}
 
}
