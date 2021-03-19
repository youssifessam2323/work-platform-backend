using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using work_platform_backend.Authorization;
using work_platform_backend.Models;
using work_platform_backend.Repos;
using work_platform_backend.Services;

namespace work_platform_backend
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
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<AuthService>();
            services.AddScoped<ICheckpointRepository, CheckpointRepo>();
            services.AddScoped<CheckPointService>();
            services.AddScoped<ITeamRepository, TeamRepo>();
            services.AddScoped<TeamService>();
            services.AddScoped<IProjectRepository, ProjectRepo>();
            services.AddScoped<ProjectService>();
            services.AddScoped<IAttachmentRepository, AttachmentRepo>();
            services.AddScoped<AttachmentService>();
            services.AddScoped<IRoomRepository, RoomRepo>();
            services.AddScoped<RoomService>();
            services.AddScoped<ISettingRepository, SettingRepo>();
            services.AddScoped<SettingService>();
            services.AddScoped<IRTaskRepository, RTaskRepo>();
            services.AddScoped<TaskService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UserService>();
            services.AddScoped<CommentService>();



            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });         

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailService,EmailService>();

             services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
            ClockSkew = TimeSpan.Zero
            };
            });



            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.LEADER,Policies.LeaderPolicy());
                config.AddPolicy(Policies.MEMBER,Policies.MemberPolicy());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
          

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
