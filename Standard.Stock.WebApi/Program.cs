using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Standard.Stock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string env = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine)["ASPNETCORE_ENVIRONMENT"].ToString();

            return WebHost.CreateDefaultBuilder(args)
                          .UseEnvironment(env)
                          .UseStartup<Startup>();
        }
    }
}
