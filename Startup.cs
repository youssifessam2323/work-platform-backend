using System;
<<<<<<< HEAD
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
=======
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using work_platform_backend.Authorization;
<<<<<<< HEAD
using work_platform_backend.Hubs;
using work_platform_backend.Models;
using work_platform_backend.Repos;
using work_platform_backend.Services;
=======
using work_platform_backend.Models;
using work_platform_backend.Repos;
using work_platform_backend.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca

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
            services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

<<<<<<< HEAD
=======

             services.AddSwaggerGen(c =>
             {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); 
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
          
             });

>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<AuthService>();
            services.AddScoped<ICheckpointRepository, CheckpointRepo>();
            services.AddScoped<CheckPointService>();
            services.AddScoped<ITeamRepository, TeamRepo>();
            services.AddScoped<TeamService>();
            services.AddScoped<IProjectRepository, ProjectRepo>();
            services.AddScoped<ProjectService>();
            services.AddScoped<IAttachmentRepository, AttachmentRepo>();
            services.AddScoped<ICommentRepository,CommentRepository>();
            services.AddScoped<AttachmentService>();
            services.AddScoped<IRoomRepository, RoomRepo>();
            services.AddScoped<RoomService>();
            services.AddScoped<ISettingRepository, SettingRepo>();
            services.AddScoped<SettingService>();
            services.AddScoped<IRTaskRepository, RTaskRepo>();
            services.AddScoped<TaskService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UserService>();
            services.AddScoped<ProjectManagerService>();
            services.AddScoped<CommentService>();
            services.AddScoped<SessionService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<ISessionRepository,SessionRepository>();
<<<<<<< HEAD
=======
            services.AddScoped<ITeamMembersRepository,TeamMembersRepository>();
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            


            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatMessageTypeRepository, ChatMessageTypeRepository>();
            services.AddScoped<ITeamChatRepository, TeamChatRepository>();
            services.AddScoped<ChatMessageService>();
            services.AddScoped<ChatMessageService>();
            services.AddScoped<ChatMessageTypeService>();
            services.AddScoped<TeamChatService>();

            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            },ServiceLifetime.Transient);         

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailService,EmailService>();

             services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
<<<<<<< HEAD
=======
                options.SignIn.RequireConfirmedEmail = true;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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

<<<<<<< HEAD

            services.AddSignalR();
=======
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllHeaders",builder => 
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        
                services.AddSignalR();               
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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

<<<<<<< HEAD
            app.UseCors();
            
=======
                  // Enable middleware to serve generated Swagger as a JSON endpoint.
          
    // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });      

>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            app.UseAuthentication();

            app.UseAuthorization();

<<<<<<< HEAD
=======

>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            app.UseEndpoints(endpoints =>
            {
            //    endpoints.MapRazorPages();
               endpoints.MapControllers();
<<<<<<< HEAD
               endpoints.MapHub<ChatHub>("/chathub");
=======
            //    endpoints.MapHub<ChatHub>("/chatHub");
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            });
        }
    }
}
