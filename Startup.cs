using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ARBUserManagement.Repository;
using ARBUserManagement.Services;
using ARBUserManagement.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ARBUserManagement
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
            services.Configure<DBSettings>(Configuration.GetSection("DBSettings"));
            services.AddDbContext<ARB_DevelopContext>(options => options.UseSqlServer(Configuration.GetSection("DBSettings:ConnectionString").ToString()));


            services.AddMvc()
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddCors();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors(options => options.WithOrigins("http://172.17.0.2").AllowAnyMethod());
            app.UseCors(options => options.WithOrigins("http://172.17.0.3").AllowAnyMethod());
            app.UseCors(options => options.WithOrigins("http://172.17.0.4").AllowAnyMethod());
        }
    }
}
