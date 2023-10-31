using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using PresentationLayer.helper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositaries;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Repositories;

namespace Soft_Gallery_Project
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers(); // Changed from AddControllersWithViews()

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepositary, CourseRepositary>();
            services.AddScoped<IEnrollmentRepositary, EnrollmentRepositary>();


            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseServices, CourseService>();




            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()  // Allow requests from any origin
                            .AllowAnyMethod()  // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                            .AllowAnyHeader(); // Allow any HTTP headers
                    });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Changed from MapControllerRoute()
            });

            app.UseHttpsRedirection();
        }
    }
}
