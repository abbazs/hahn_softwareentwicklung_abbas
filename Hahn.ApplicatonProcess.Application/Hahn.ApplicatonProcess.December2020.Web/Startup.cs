using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog;

namespace domain
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
            string cons = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IApplicantRepository>(x => new EFInMemoryRepository());
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Applicant API", Version = "v1" });

                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith("Hahn")))
                {
                    string FileName = $"{a.GetName().Name}.xml";
                    string FilePath = Path.Combine(Path.GetDirectoryName(a.Location), FileName);
                    Console.WriteLine($"{FilePath}");
                    c.IncludeXmlComments(FilePath);
                }
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf(typeof(Applicant));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicant api v1");
                c.RoutePrefix = String.Empty;});
            }

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
