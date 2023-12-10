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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IAdminRepositary, AdminRepositary>();


            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseServices, CourseService>();
            services.AddScoped<IAdminServices, AdminServices>();

            services.AddCors(options => options.AddDefaultPolicy(

                builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44309",
                    ValidAudience = "https://localhost:44309",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

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
