using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ratemyprofessors.Models;

namespace ratemyprofessors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = CreateWebHostBuilder(args).Build();
            using (var scope = p.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetService<DataBaseContext>();
                    context.Database.Migrate();

                }
                catch
                {

                }
            }
            p.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
