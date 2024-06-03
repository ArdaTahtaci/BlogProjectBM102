using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlogProject.Models;
using BlogProject.Services;
using MongoDB.Driver;
using DotNetEnv;

namespace BlogProject
{
    public class Program
    {
        public static void Main(string[] args){

            DotNetEnv.Env.Load();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services){       
            DotNetEnv.Env.Load();

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var mongoConnectionString = configuration["MONGODB_CONNECTION_URI"];
            // var authMechanism = configuration["MONGODB_AUTH_MECHANISM"];

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(mongoConnectionString);
            });

            services.AddSingleton<IDatabaseSettings>(new DatabaseSettings(mongoConnectionString,"BlogProject"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddControllers();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
