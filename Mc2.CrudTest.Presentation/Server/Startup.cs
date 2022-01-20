using Mc2.CrudTest.Presentation.Server.Common.Mappers;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.RepoInterfaces;
using Mc2.CrudTest.Presentation.Server.Core.Services.CustomerServices;
using Mc2.CrudTest.Presentation.Server.Persistence.Contexts;
using Mc2.CrudTest.Presentation.Server.Persistence.Repos.Customer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace Mc2.CrudTest.Presentation.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();


            services.AddDbContext<ScopeDBContext>(c => c.UseInMemoryDatabase("CustomerDb"));


            services.AddCors(o => o.AddPolicy("customerManagement", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader().AllowAnyMethod();

            }));

            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepo, CustomerRepo>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("customerManagement");
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
