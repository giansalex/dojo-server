using App.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace App.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.Configure<Settings>(options => {
            //    options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            //    options.Database = Configuration.GetSection("MongoConnection:Demo01").Value;
            //});
            services.AddScoped<IMongoClient>(s => new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value));
            services.AddScoped<IMongoDatabase>(s => s.GetService<IMongoClient>().GetDatabase(Configuration.GetSection("MongoConnection:Database").Value));
            services.AddScoped<IMongoCollection<User>>(s => s.GetService<IMongoDatabase>().GetCollection<User>(nameof(User)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var hosts = Configuration.GetSection("Cors:AllowOrigin").Value;
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(hosts)
                .AllowCredentials()
            );
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
